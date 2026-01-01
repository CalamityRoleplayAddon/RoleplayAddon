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
			Particle shatter = new PlasmaExplosion(target.Center, Vector2.Zero, Color.DeepSkyBlue, Vector2.One, Main.rand.NextFloat(-5, 5), 0.01f, 0.05f, 24);
			GeneralParticleHandler.SpawnParticle(shatter);
		}
	}
}