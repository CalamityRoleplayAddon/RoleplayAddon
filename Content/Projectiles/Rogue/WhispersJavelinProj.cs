using CalamityMod;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoleplayAddon.Core.Globals;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles.Rogue
{
	public class WhispersJavelinProj : ModProjectile
	{
		public override string Texture => "RoleplayAddon/Content/Weapons/Rogue/WhispersJavelin";

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.penetrate = -1;
			Projectile.extraUpdates = 4;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;  // would be REALLY cool if it turned water to thin ice as it flies through... but that sounds like a great way to destroy all the bodies of water near ur arena so if it's a water-themed build......
			Projectile.tileCollide = false;
			Projectile.DamageType = RoleplayAddon.Rogue;
		}

		public override void OnSpawn(IEntitySource source)
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
		}

		public override void AI()
		{
			VisualEffects();
		}
		private void VisualEffects()
		{
			// iffy on this tbh
			Color colour;
			int type = Main.rand.Next(1, 4);
			colour = type switch
			{
				1 => Color.BlueViolet,
				2 => Color.AliceBlue,
				_ => Color.DeepSkyBlue,
			};
			Particle sparkle = new SparkleParticle(Projectile.Center, Main.rand.NextVector2Unit(), colour, Color.White, 0.1f, 36, 0.5f);
			GeneralParticleHandler.SpawnParticle(sparkle);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
			float rotation = Projectile.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float scale = Projectile.scale;

			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, rectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
			return false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[Projectile.owner];
			RPPlayer modPlayer = player.RPify();
			ref Dictionary<NPC, int> targets = ref modPlayer.WhispersTargetDict;

			// Stored for stars to access for homing
			// Both lists are null by default, so they need to be made into empty lists if they are currently null before being appended to
			if (targets.ContainsKey(target))
			{
				targets[target] = 0;
			}
			else
			{
				targets.Add(target, 0);
			}

			// ice thingymajigy. maybe make it like. only work on first hit? just have a field increment on hit and if it aint zero, dont work buddy. easily adjustable for other values too!
			RPGlobalNPC modNPC = target.RPify();
			if (Projectile.Calamity().stealthStrike)
			{
				modNPC.IceStacks += 3;
			}
			else
			{
				modNPC.IceStacks++;
			}

			if (modNPC.IceStacks > RPGlobalNPC.IceStacksCap)
			{
				modNPC.IceStacks = RPGlobalNPC.IceStacksCap;
			}

			Main.NewText($"New ice stacks count: {modNPC.IceStacks}");

			// Visual effects
			Color colour = Main.rand.NextBool() ? Color.BlueViolet : Color.AliceBlue;
			Particle snowflake = new SnowflakeSparkle(Projectile.Center, Vector2.Zero, colour, Color.AliceBlue, 1f, 20);
			GeneralParticleHandler.SpawnParticle(snowflake);
		}
	}
}