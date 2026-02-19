using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles.Rogue
{
	public class WhispersIceShatterProj : ModProjectile
	{
		public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 1;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.DamageType = RoleplayAddon.Rogue;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			ShatterEffect((int)Projectile.ai[0]);
			/* Particle shatter = new PlasmaExplosion(target.Center, Vector2.Zero, Color.DeepSkyBlue, Vector2.One, Main.rand.NextFloat(-5, 5), 0.01f, 0.05f, 24);
			GeneralParticleHandler.SpawnParticle(shatter); */
		}

		public  void ShatterEffect(int incidentNPC = 0)
		{
			Vector2 origin = Vector2.One;
			float angle = 0;

			// If incidentNPC is 0, then this NPC's shattering is the first in the chain (or the only one)
			if (incidentNPC == 0)
			{
				origin = Main.player[Projectile.owner].Center;
				angle = Projectile.AngleFrom(origin);
			}
			else
			{
				origin = Main.npc[incidentNPC].Center;
				angle = Projectile.AngleFrom(origin);
			}
			Vector2 pos = Projectile.Center + 8 * Projectile.velocity.SafeNormalize(Vector2.One);
			Vector2 velocity = Vector2.Zero;
			Color colour = Color.AliceBlue;
			Vector2 squish = Vector2.One;
			float rotation = angle + MathHelper.PiOver2 + Main.rand.NextFloat(-0.3f, 0.3f);
			float initialScale = 1.5f;
			float finalScale = 0.8f;
			int lifetime = Main.rand.Next(25, 30);

			CrackParticle crack = new(
				pos, 
				velocity, 
				colour, 
				squish, 
				rotation, 
				initialScale, 
				finalScale, 
				lifetime
				);
			GeneralParticleHandler.SpawnParticle(crack);

			for (int i = 0; i < 5; i++)
			{
				SparkParticle spark = new(Projectile.Center, Main.rand.NextVector2Circular(8, 8), true, 24, 1f, colour);
				GeneralParticleHandler.SpawnParticle(spark);
			}
		}
	}
}