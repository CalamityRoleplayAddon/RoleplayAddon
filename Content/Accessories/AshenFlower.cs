using CalamityMod.Items;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Accessories
{
	public class AshenFlower : ModItem
	{
		public override string Texture => "Terraria/Images/Item_3989";

		public const float HealFactor = 0.01f;

		public override void SetDefaults()
		{
			Item.accessory = true;
			Item.rare = ItemRarityID.Purple;
			Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<AshenRing>();
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.RPify().ashenFlower = true;
			player.RPify().ashenFlowerReduced = hideVisual;
		}
	}
}
