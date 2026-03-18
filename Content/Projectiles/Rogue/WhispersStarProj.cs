using Microsoft.Xna.Framework;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using CalamityMod.Particles;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace RoleplayAddon.Content.Projectiles.Rogue
{
    public class WhispersStarProj : ModProjectile
    {
        // ai[0] == direction
        // ai[2] == ai state
        public override string Texture => "CalamityMod/Projectiles/Magic/Starblast";

        private const double OrbitAngleIncrement = Math.PI / 180;
        private const float HomingSpeed = 25f;
		private const float EndDistance = 96f;
		private const float Offset = 32f;
        private const int SpawnDelay = 5;

        private bool outward = true;
        private double orbitAngle = 0;
        private int spawnDelay = 0;
        private int time = 0;
        private float orbitDistance = 0;
        private float timer = 0;
        private float wanderSpeed = 0;
        private NPC target = null;
        private Player player = null;
        private RPPlayer modPlayer = null;   
        private Vector2 direction = Vector2.Zero;
        private Vector2 wanderPlayerPos = Vector2.Zero;
       

        // I saw this way of implementing AI in Short Circuit's code and I really like it
        private enum State
        {
            Spawning,
            Emerging,
            Wandering,
            Chasing
        }

        private State AIState
        {
            get => (State)(int)Projectile.ai[2];
            set => Projectile.ai[2] = (int)value;
        }

        private void ChangeState(State newState)
        {
            if (AIState == newState)
            {
                return;
            }
            AIState = newState;
        }

        public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Type] = true;
		}

        public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 24;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.DamageType = RoleplayAddon.Rogue;
			Projectile.timeLeft = 480;
		}

        public override void AI()
        {
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
            }

            // Initialisation
            if (direction == Vector2.Zero)
            {
                player = Main.player[Projectile.owner];
                modPlayer = player.RPify();
                spawnDelay = (int)Projectile.ai[0] * SpawnDelay;
                Projectile.timeLeft += spawnDelay;

                direction = Projectile.ai[0] switch
                {
                    0 => new Vector2(-1f, -1f),
                    1 => new Vector2(0f, -1f),
                    2 => new Vector2(1f, -1f),
                    3 => new Vector2(1f, 0f),
                    4 => new Vector2(1f, 1f),
                    5 => new Vector2(0, 1f),
                    6 => new Vector2(-1f, 1f),
                    _ => new Vector2(-1f, 0f),
                };
                direction = Vector2.Normalize(direction);
            }

            // The only control over the star's AI outside of this switch is in Emerging()
            // Which is just, "if it's done emerging and there's a target, start chasing it"
            // 
            switch (AIState)
            {
                case State.Emerging:
                    if (time >= 440)
                    {
                        // goose note,,,,,, play a minor-sounding sound when it switches to wandering
                        // like a sad note.
                        // because it failed to find a target and now must wander without true purpose :c
                        // (but actually just as an indicator of why the star is firing)
                        // switching to chasing should play a major-sounding sound instead. altho. idk how
                        // to make sure it doesnt happen way too often....
                        wanderPlayerPos = player.Center;
                        ChangeState(State.Wandering);
                    }
                    Emerge();
                    break;
                case State.Wandering:
                    if (modPlayer.WhispersTargetDict.Count != 0)
                    {
                        ChangeState(State.Chasing);
                    }
                    Wander();
                    break;
                case State.Chasing:
                    GlobalProcesses();
                    if (modPlayer.WhispersTargetDict.Count == 0)
                    {
                        ChangeState(State.Wandering);
                        break;
                    }
                    KeyValuePair<NPC, int> item = modPlayer.WhispersTargetDict.ElementAt(0);
                    NPC target = item.Key;
                    Projectile.velocity = Projectile.SuperhomeTowardsTarget(target, HomingSpeed, 10);
                    break;
                default:
                    if (spawnDelay <= 0)
                    {
                        ChangeState(State.Emerging);
                    }
                    Projectile.Center = player.Center;
                    orbitAngle += OrbitAngleIncrement;
                    spawnDelay--;
                    break;
            }
        }

        /// <summary>
        /// Processes done... not all globally, actually, but at least in every state OTHER than Spawning
        /// </summary>
        private void GlobalProcesses()
        {
            time++;
            Projectile.rotation += 0.1f;
            if (Projectile.timeLeft % 2 == 0)
            {
                SparkleParticle sparkle = new(
					Projectile.Center + Main.rand.NextVector2Circular(8, 8), 
					AIState == State.Chasing ? -Projectile.velocity / 3 : -Projectile.velocity, 
					Color.Lerp(Color.Violet, Color.DeepSkyBlue, Main.rand.NextFloat()), 
					Color.White, 
					0.1f, 
					24, 
					0.5f
					);
			    GeneralParticleHandler.SpawnParticle(sparkle);
            }
        }

        private void Emerge()
        {
            GlobalProcesses();
            if (timer >= 1)
            {
                outward = false;
            }
            else if (timer <= 0)
            {
                outward = true;
            }

            if (outward)
            {
                timer += 0.01f;
            }
            else
            {
                timer -= 0.01f;
            }

            if (time < 100)
            {
                float scale = RPUtils.EaseOutQuart(timer);
                orbitDistance = scale * (Offset + EndDistance);
            }
            else
            {
                float scale = RPUtils.EaseInOutQuad(timer);
                // Star will oscillate between 96 and 128 pixels out from the player while orbiting
                // If anyone wants to decrease the oscillation range, move a portion of Offset to EndDistance
                // Do the opposite to increase the oscillation range
                orbitDistance = (scale * Offset) + EndDistance;
            }

            // Orbit around the player according to the distance determined above
            orbitAngle += OrbitAngleIncrement;
            double baseAngle = direction.ToRotation();
            Vector2 positionInOrbit = RPUtils.MoveAlongCircle(baseAngle + orbitAngle, orbitDistance, player, Projectile);
            Projectile.Center = positionInOrbit + new Vector2(8, 8);

            if (time > 100)
            {
                if (modPlayer.WhispersTargetDict.Count > 0)
                {
                    KeyValuePair<NPC, int> item = modPlayer.WhispersTargetDict.ElementAt(0);
                    target = item.Key;
                    Projectile.timeLeft += 100;

                    // Visual effects for starting the chase
                    // Kinda wanna give them something extra every one in a while DURING the chase
                    // but I can't get it to look right due to how the stars tend to clump
                    float pulseRotation = Projectile.Center.AngleTo(target.Center);
                    Vector2 vel = -1.5f * pulseRotation.ToRotationVector2();
                    for (int i = 1; i < 3; i++)
                    {
                        Color colour = Color.Lerp(Color.AliceBlue, Color.BlueViolet, Main.rand.NextFloat(0.4f, 0.6f));
                        CustomPulse boostPulse = new(
                            Projectile.Center, 
                            vel * i, 
                            colour, 
                            "CalamityMod/Particles/BloomRingThinLarge", 
                            new Vector2(0.3f, 1f), 
                            pulseRotation, 
                            0.05f * i, 
                            0.025f * i, 
                            24);
                        GeneralParticleHandler.SpawnParticle(boostPulse);
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        Color colour = Color.Lerp(Color.AliceBlue, Color.BlueViolet, Main.rand.NextFloat(0.4f, 0.6f));
                        SparkParticle spark = new(Projectile.Center, Main.rand.NextVector2Circular(8, 8), false, 24, 0.5f, colour);
                        GeneralParticleHandler.SpawnParticle(spark);
                    }

                    ChangeState(State.Chasing);
                }
            }
        }

        private void Wander()
        {
            GlobalProcesses();
            // If we don't check for this, the stars will fly directly away from the top-left corner of the world if switching from Chasing
            if (wanderPlayerPos == Vector2.Zero)
            {
                CalamityUtils.HomeInOnNPC(Projectile, true, 30 * 16, HomingSpeed, 30);
                // And if we don't check for THIS there's a small chance of a star staying still due to finding and losing a target before it can gain velocity
                if (Projectile.position == Projectile.oldPosition)
                {
                    Vector2 direction = player.Center.AngleTo(Projectile.Center).ToRotationVector2();
                    Projectile.velocity = 1 * direction;
                }
                // Finally, this is just to make sure that, by arbitrarily giving a star speed in the last step, we won't accidentally make it overtake a star 
                // that started gaining velocity through Chasing before it did
                if (Projectile.velocity.Length() < 2f)
                {
                    Projectile.velocity = 2f * Projectile.velocity.SafeNormalize(Vector2.One);
                }
            }
            else
            {
            Vector2 direction = wanderPlayerPos.AngleTo(Projectile.Center).ToRotationVector2();
            wanderSpeed += HomingSpeed * 0.05f;
            Projectile.velocity = direction * wanderSpeed;
            }
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/Starblast", AssetRequestMode.ImmediateLoad).Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
			float rotation = Projectile.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float scale = Projectile.scale;

			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
			return false;
		}

        public override void OnKill(int timeLeft)
		{
			Particle pulse = new DirectionalPulseRing(Projectile.Center, Vector2.Zero, Color.AliceBlue, Vector2.One, 0, 0.1f, 0.50f, 30);
			Particle explosion = new CustomPulse(
                Projectile.Center, 
                Vector2.Zero, 
                Color.LightSkyBlue, 
                "CalamityMod/Particles/FlameExplosion", 
                Vector2.One, 
                Main.rand.NextFloat(),
				0.1f / 14f, 
                0.50f / 14f, // FlameExplosion is about 13.13 times larger than HollowCircleHardEdge, so this is needed to make the former just a little smaller
				30);
			GeneralParticleHandler.SpawnParticle(pulse);
			GeneralParticleHandler.SpawnParticle(explosion);

			Color colour = Color.Lerp(Color.AliceBlue, Color.BlueViolet, Main.rand.NextFloat(0.4f, 0.6f));
			for (int i = 0; i < 5; i++)
			{
				SparkParticle spark = new(Projectile.Center, Main.rand.NextVector2Circular(8, 8), false, 12, 0.5f, colour);
				GeneralParticleHandler.SpawnParticle(spark);
			}
		}
    }
}