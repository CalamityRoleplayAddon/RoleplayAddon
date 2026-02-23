using CalamityMod.Items;
using CalamityMod.Rarities;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Accessories
{
	public class AshenRing : ModItem
	{
		public const float Radius = 128f;
		public const int Cooldown = 30;		// INCREASE BACK TO 300 AFTER DEMO IS FILMED RAHHHH!!!!!!!
		public const int Damage = 500;

		public override void SetDefaults()
		{
			Item.accessory = true;
			Item.rare = ModContent.RarityType<Turquoise>();
			Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RPPlayer modPlayer = player.RPify();
			modPlayer.ashenFlower = true;
			modPlayer.ashenRing = true;
			modPlayer.ilmeranRing = true;
		}
	}
}
