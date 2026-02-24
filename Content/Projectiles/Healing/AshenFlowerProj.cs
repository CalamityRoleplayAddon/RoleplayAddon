using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RoleplayAddon.Content.Accessories;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles.Healing
{
	public class AshenFlowerProj : ModProjectile
	{
        private const float HomingSpeed = 5f;
        private const int Height = 14;
        private const int Width = 26;

        private bool hasHealedOwner = false;
        private Color baseColour = new(255, 191, 73);
        private float radius;
        private int framesSinceFlicker = 0;

		public override void SetDefaults()
		{
			Projectile.width = 26;
			Projectile.height = 14;

			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 300;
		}

        public override void OnSpawn(IEntitySource source)
        {
            // If the owner spawned this through Ashen Ring, not Ashen Flower, make it last a little longer
            // (This buffs several aspects of the projectile)
            if (Projectile.ai[0] == 1)
            {
                Projectile.timeLeft += 50;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // The glow should occasionally dim or brighten a little, like the flickering of a flame
            // In this case, that means timeLeft should decrease or increase occasionally, but not enough to be infinite
            framesSinceFlicker--;
            if (framesSinceFlicker <= 0)
            {
                Projectile.timeLeft += Main.rand.Next(-10, 30);
                framesSinceFlicker = Main.rand.Next(40, 80);
            }

            Projectile.velocity *= 0.95f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.scale = MathHelper.Lerp(0.1f, 1f, (float)Projectile.timeLeft/300);
            Projectile.width = (int)(Projectile.scale * Width);
            Projectile.height = (int)(Projectile.scale * Height);

            // I wanted to just draw this in PreDraw but. Not working! and i feel like this is more expensive...
            float glowScale = (float)Projectile.timeLeft / 150;
            BloomParticle glow = new(Projectile.Center, Vector2.Zero, baseColour * 0.8f, glowScale, glowScale * 0.8f, 3);
            GeneralParticleHandler.SpawnParticle(glow);

            // If the player is within the glow, the petal will drift towards them
            // As the glow weakens, so too does this effect... indirectly
            // To explain the numbers: 
            // taking only half of the texture so as to consider the radius and not the diameter (which is why the width is being halved), 
            // there are about 35 pixels of what I'm considering blank space that should not be included in the homing radius (which is why I'm then subtracting by 35),
            // and then multiplying by glowScale makes the homing radius decrease over time just as the glow's brightness does.
            radius = ((ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", AssetRequestMode.ImmediateLoad).Value.Width / 2) - 35) * glowScale;
            if (player.lifeMagnet)  // Heartreach. i think
            {
                radius *= 1.5f;
            }
            if (Projectile.Center.Distance(player.Center) < radius)
            {
                Vector2 direction = Projectile.Center.DirectionTo(player.Center);
                Projectile.velocity = HomingSpeed * direction;
            }
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                player.Heal((int)(player.statLifeMax2 * AshenFlower.HealFactor));
                hasHealedOwner = true;
                Projectile.Kill();
            }

            // This creates ember-like particles, to fit the flame theme
            if (Projectile.timeLeft % 10 == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(6, 6);
                    Color colour = Color.Lerp(Color.Gold, Color.Red, Main.rand.NextFloat());
                    GlowOrbParticle ember = new(Projectile.Center, velocity, i == 0, 24, 0.2f * (i + 1), colour);
                    GeneralParticleHandler.SpawnParticle(ember);
                }
            }
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Don't want the petal getting stuck in any blocks or deleted upon contact
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (hasHealedOwner)
            {
                float glowScale = (float)timeLeft / 150;
                DirectionalPulseRing pulse = new(Projectile.Center, Vector2.Zero, baseColour, Vector2.One, 0, 0, glowScale, 24);
                GeneralParticleHandler.SpawnParticle(pulse);
            }
            else
            {
                // This is meant to be like a flame flickering brightly the moment before it goes out
                // Not fully sold on it but I definitely want SOMETHING similar. I think.
                float glowScale = 1f;
                BloomParticle glow = new(Projectile.Center, Vector2.Zero, baseColour, 0f, glowScale, 15);
                GeneralParticleHandler.SpawnParticle(glow);
            }
        }

		public override bool PreDraw(ref Color lightColor)
		{
            Texture2D tex = ModContent.Request<Texture2D>("RoleplayAddon/Content/Projectiles/Healing/AshenFlowerProj", AssetRequestMode.ImmediateLoad).Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
            Color colour = Color.Lerp(Color.Black, Color.White, (float)Projectile.timeLeft / 300);
			float rotation = Projectile.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float scale = Projectile.scale;
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rectangle, colour, rotation, origin, scale, SpriteEffects.None);
			return false;
		}
	}
}
