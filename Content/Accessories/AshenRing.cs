using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Accessories
{
	public class AshenRing : ModItem
	{
		public const float Radius = 128f;
		public const int Cooldown = 30;		// INCREASE BACK TO 300 AFTER DEMO IS FILMED RAHHHH!!!!!!!
		public const int Damage = 500;
		public static readonly Color Colour = new(255, 191, 73);

		public override void SetDefaults()
		{
			Item.accessory = true;
			Item.rare = ModContent.RarityType<Turquoise>();
			Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            RPPlayer modPlayer = player.RPify();
			return !(modPlayer.ilmeranRing || modPlayer.ashenFlower);
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RPPlayer modPlayer = player.RPify();
			modPlayer.ashenFlower = true;
			modPlayer.ashenFlowerReduced = hideVisual;
			modPlayer.ashenRing = true;
			modPlayer.ilmeranRing = true;
			modPlayer.ilmeranRingReduced = hideVisual;
		}

        public override void AddRecipes()
        {
			CreateRecipe().
				AddIngredient<AshenFlower>().
				AddIngredient<IlmeranRing>().
				AddIngredient<DivineGeode>(5).
				AddIngredient(ItemID.LifeCrystal).
				AddTile(TileID.TinkerersWorkbench).
				Register();
        }
	}
}
