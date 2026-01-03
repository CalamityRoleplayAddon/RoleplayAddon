using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Projectiles.Rogue;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RoleplayAddon.Content.Weapons.Rogue
{
	public class WhispersJavelin : RogueWeapon
	{
		public override void SetDefaults()
		{
			Item.width = 110;
			Item.height = 110;

			Item.damage = 100;
			Item.knockBack = 3f;
			Item.useTime = Item.useAnimation = 30;
			Item.shootSpeed = 10f;
			Item.autoReuse = true;

			Item.DamageType = RoleplayAddon.Rogue;
			Item.shoot = ModContent.ProjectileType<WhispersJavelinProj>();
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;

			Item.rare = ModContent.RarityType<Turquoise>();
			Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.Calamity().StealthStrikeAvailable())
			{
				int javelin = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
				if (javelin.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[javelin].Calamity().stealthStrike = true;
				}

				// Produces 8 stars set to travel radially
				// Direction of travel is determined in WhisperStarProj.cs via a switch expression that uses Projectile.ai[0]
				for (int i = 0; i < 8; i++)
				{
					int proj = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<WhispersStarProj>(), damage, knockback, player.whoAmI, i);
					if (proj.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj].Calamity().stealthStrike = true;
					}
				}
				return false;
			}

			return true;
		}
	}
}