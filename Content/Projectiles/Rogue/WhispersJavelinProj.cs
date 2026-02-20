using CalamityMod;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoleplayAddon.Core.Globals;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles.Rogue
{
	public class WhispersJavelinProj : ModProjectile
	{
		public override string Texture => "RoleplayAddon/Content/Weapons/Rogue/WhispersJavelin";

		private bool firstHit = true;
		private int time = 0;

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.penetrate = -1;
			Projectile.extraUpdates = 4;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
			Projectile.DamageType = RoleplayAddon.Rogue;
		}

		public override void OnSpawn(IEntitySource source)
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
		}

		public override void AI()
		{
			if (Vector2.Distance(Projectile.Center, Main.player[Projectile.owner].Center) < 1600f)
            {
				VisualEffects();
				time++;
			}
		}
		private void VisualEffects()
		{
		if (Projectile.Calamity().stealthStrike)
			{
				SparkParticle lightTrail = new(Projectile.Center, Projectile.velocity * 0.001f, false, 5, 1.5f, Color.AliceBlue);
				GeneralParticleHandler.SpawnParticle(lightTrail);
				if (time % 4 == 0)
				{
					SparkParticle sparkTrail = new(
						Projectile.Center + Main.rand.NextVector2Circular(8, 8), 
						Projectile.velocity * Main.rand.NextFloat(0.9f, 1.1f), 
						false, 
						24, 
						0.5f, 
						Color.Lerp(Color.Violet, Color.LightSkyBlue, Main.rand.NextFloat()));
			    	GeneralParticleHandler.SpawnParticle(sparkTrail);
				}
			}
			else
			{
				SparkParticle sparkTrail = new(
					Projectile.Center + Main.rand.NextVector2Circular(8, 8), 
					Projectile.velocity * Main.rand.NextFloat(0.9f, 1.1f), 
					false, 
					24, 
					0.5f, 
					Color.Lerp(Color.Violet, Color.LightSkyBlue, Main.rand.NextFloat()));
				GeneralParticleHandler.SpawnParticle(sparkTrail);
			}

			// Falling snowflakes seem thematically fitting
			// Would be cool to make them larger but less frequent, but that would make the texture's unsnowflakiness more apparent
			SparkleParticle snowflake = new (Projectile.Center, new (0, Main.rand.NextFloat(2, 4)), Color.AliceBlue, Color.LightBlue, 0.15f, 60);
			GeneralParticleHandler.SpawnParticle(snowflake);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
			float rotation = Projectile.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float scale = Projectile.scale;

			Main.EntitySpriteDraw(
				tex, 
				Projectile.Center - Main.screenPosition, 
				rectangle, 
				Color.White, 
				rotation, 
				origin, 
				scale, 
				SpriteEffects.None
				);
			return false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[Projectile.owner];
			RPPlayer modPlayer = player.RPify();
			ref Dictionary<NPC, int> targets = ref modPlayer.WhispersTargetDict;

			// Stored for stars to access for homing
			// The javelin's first hit resets all targeting
			if (firstHit)
			{
				targets.Clear();
				firstHit = false;
			}
			if (!targets.TryAdd(target, 0))
			{
				targets[target] = 0;
			}

			if (!SingleInstanceGlobalNPC.WhispersFrozenNPCs.Contains(target))
			{
				SingleInstanceGlobalNPC.WhispersFrozenNPCs.Add(target);
			}

			// Ice stacks application
			// On-hit visual effects are also placed here to avoid repeating the same control statement
			RPGlobalNPC modNPC = target.RPify();
			Color colour = Main.rand.NextBool() ? Color.BlueViolet : Color.AliceBlue;
			if (Projectile.Calamity().stealthStrike)
			{
				modNPC.IceStacks += 3;
				SparkleParticle sparkle = new(target.Center, Vector2.Zero, Color.AliceBlue, Color.BlueViolet, 2f, 20, 0.05f);
				//Particle snowflake = new SnowflakeSparkle(Projectile.Center, Vector2.Zero, colour, Color.AliceBlue, 1f, 20);
				GeneralParticleHandler.SpawnParticle(sparkle);
			}
			else
			{
				modNPC.IceStacks++;
				Particle snowflake = new SnowflakeSparkle(Projectile.Center, Vector2.Zero, colour, Color.AliceBlue, 1f, 20);
				GeneralParticleHandler.SpawnParticle(snowflake);
			}

			if (modNPC.IceStacks > RPGlobalNPC.IceStacksCap)
			{
				modNPC.IceStacks = RPGlobalNPC.IceStacksCap;
			}

			Main.NewText($"New ice stacks count: {modNPC.IceStacks}");
		}
	}
}