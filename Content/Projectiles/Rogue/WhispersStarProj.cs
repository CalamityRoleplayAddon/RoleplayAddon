using CalamityMod;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RoleplayAddon.Content.Projectiles.Rogue
{
	public class WhispersStarProj : ModProjectile
	{
		public override string Texture => "CalamityMod/Projectiles/Magic/Starblast";

		private int spawnTimer;
		private bool setSpawnDelay;
		private float timer = 0;
		private int time;

		private bool forward = true;
		private Vector2 direction = Vector2.Zero;
		private double rotation = 0;
		private float distance;
		//private float deathSpeed = 0;

		private NPC target = null;
		private bool shouldHome = false;
		private bool shouldDie = false;
		private Rectangle targetLastKnownRect;

		private const int SpawnDelay = 5;

		// Used to move out from the player 
		private const float EndDistance = 96f;
		private const float Offset = 32;
		//private const float SpeedIncrement = 0.1f;

		private const float HomingSpeed = 25f;

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
			Projectile.timeLeft = 500;
		}

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];
			RPPlayer modPlayer = owner.RPify();

			if (Projectile.ai[0] > 0 && !setSpawnDelay)
			{
				spawnTimer = (int)Projectile.ai[0] * SpawnDelay;
				Projectile.timeLeft += spawnTimer;
				setSpawnDelay = true;
			}

			if (spawnTimer > 0)
			{
				spawnTimer--;
				rotation += Math.PI / 180;
				Projectile.Center = owner.Center;
			}
			else
			{
				Lighting.AddLight(Projectile.Center, TorchID.Ice);

				if (Projectile.soundDelay == 0 && Projectile.ai[0] == 0)
				{
					SoundEngine.PlaySound(SoundID.Item4 with { PitchVariance = 0.3f, Volume = 0.5f }, Projectile.Center);
					Projectile.soundDelay = 60;
				}

				time++;

				// Initialisation stage
				if (direction == Vector2.Zero)
				{
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

				if (!shouldHome && !shouldDie)
				{
					if (timer >= 1)
					{
						forward = false;
					}
					else if (timer <= 0)
					{
						forward = true;
					}

					if (forward)
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
						distance = scale * (EndDistance + Offset);
					}
					else
					{
						float scale = RPUtils.EaseInOutQuad(timer);
						// Should oscillate between 96 and 128 pixels out from the player while orbiting
						distance = (scale * Offset) + EndDistance;
					}

					rotation += Math.PI / 180;
					double baseAngle = direction.ToRotation();
					Vector2 positionInOrbit = RPUtils.MoveAlongCircle(baseAngle + rotation, distance, owner, Projectile);
					Projectile.Center = positionInOrbit + new Vector2(8, 8);        // for some reason it won't be centred properly,,,,,, idk why, i wrote MoveAlongCircle like months go and left no comments !! cuz im just so smart and thoughtful

					Projectile.rotation += 0.001f * MathHelper.Clamp(time, 0, 100);
				}

				// If the star's target isn't the foremost NPC in the target list, set it to that NPC
				// If lists for some reason don't automatically push items to the earliest index possible whenever they update, this will not work : ) (i somehow have not learned if they do this yet)
				if (modPlayer.WhispersTargetList != null && target != modPlayer.WhispersTargetList[0])
				{
					target = modPlayer.WhispersTargetList[0];
					if (target == null)
					{
						targetLastKnownRect = target.Hitbox;
					}
				}

				if (time > 100 && timer < 470 && !shouldDie)
				{
					if (target != null)
					{
						// To give it a bit of visual and auditory oopmh
						if (!shouldHome)
						{
							DustBoost(5f);
							if (Projectile.ai[0] == 0)
							{
								SoundEngine.PlaySound(SoundID.Item4);
							}
						}

						// So stars that start homing at the last second actually reach their target
						Projectile.timeLeft += 100;

						shouldHome = true;
					}
				}
				else if (timer >= 470 && target == null && !shouldDie)
				{
					shouldDie = true;
				}

				if (shouldHome)
				{
					Homing();
				}

				VisualEffects();
			}
		}

		private void Homing()
		{
			targetLastKnownRect = target.Hitbox;
			Vector2 targetPos = target.Center;
			Vector2 homingDirection = Projectile.SafeDirectionTo(targetPos, Vector2.One);

			Projectile.velocity = HomingSpeed * homingDirection;
			Projectile.rotation += 0.001f * MathHelper.Clamp(time, 0, 100);

			if (Projectile.Hitbox.Intersects(targetLastKnownRect))
			{
				Projectile.Kill();
			}
		}

		/*	// not currently used
			private void Death()
			{
				Main.NewText("Dying so hard rn!!");
				deathSpeed += SpeedIncrement;
				Projectile.velocity += deathSpeed * direction;
				Projectile.rotation += 0.001f * MathHelper.Clamp(time, 0, 200);
			}*/

		private void VisualEffects()
		{
			Color colour;
			int type = Main.rand.Next(1, 4);
			colour = type switch
			{
				1 => Color.LightSkyBlue,
				2 => Color.Gold,
				_ => Color.Magenta,
			};
			Particle sparkle = new SparkleParticle(Projectile.Center, Main.rand.NextVector2Unit() * 1.5f, colour, Color.White, 0.1f, 12, 0.5f);
			GeneralParticleHandler.SpawnParticle(sparkle);
		}

		/// <summary>
		/// Produces a burst of dust specifically meant to give a change in direction, such as when the stars leave their orbits to start homing, some light visual oomph.
		/// </summary>
		/// <param name="divisor">Used to divide HomingSpeed. The lower this value, the faster the dust will be, and the greater the visual oomph. Probably. To an extent.</param>
		private void DustBoost(float divisor)
		{
			float angle = 0;
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool() ? 15 : 173, angle.ToRotationVector2() * HomingSpeed / divisor).noGravity = true;
				angle += (float)Math.PI / 10;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/Starblast", AssetRequestMode.ImmediateLoad).Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
			float rotation = Projectile.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float scale = Projectile.scale;

			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, rectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			// could make a visual utilities file with a like 'StarDeath()' function that takes colour and size arguments and can be reused for any generic star projectile
			Particle pulse = new DirectionalPulseRing(Projectile.Center, Vector2.Zero, Color.AliceBlue, Vector2.One, 0, 0.15f, 0.75f, 30);
			Particle explosion = new CustomPulse(Projectile.Center, Vector2.Zero, Color.LightSkyBlue, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(),
				0.15f / 14f, 0.75f / 14f, // FlameExplosion is about 13.13 times larger than HollowCircleHardEdge, so this is needed to make the former just a little smaller
				30);
			GeneralParticleHandler.SpawnParticle(pulse);
			GeneralParticleHandler.SpawnParticle(explosion);

			for (int i = 0; i < 20; i++)
			{
				float xSpeed = Main.rand.NextFloat(-3, 3);
				float ySpeed = Main.rand.NextFloat(-3, 3);
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, Main.rand.NextBool() ? 15 : 173, xSpeed, ySpeed);
			}
		}
	}
}