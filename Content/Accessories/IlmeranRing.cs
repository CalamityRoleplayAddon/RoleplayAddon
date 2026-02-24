using CalamityMod.Items;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Accessories
{
	public class IlmeranRing : ModItem
	{
		public override string Texture => "Terraria/Images/Item_4243";
		
		public const float Radius = 96f;
		public const int Damage = 50;

		public override void SetDefaults()
		{
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.RPify().ilmeranRing = true;
		}
	}
}
