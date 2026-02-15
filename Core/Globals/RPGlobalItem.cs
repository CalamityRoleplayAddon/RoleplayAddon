using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Potions.Food;
using RoleplayAddon.Core.ModPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Globals
{
	public class RPGlobalItem : GlobalItem
	{
		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			if (player.GetModPlayer<RPPlayer>().manaPotLock == true)
			{
				player.manaFlower = false;
			}
		}

		static HashSet<int> manaPots = [ItemID.LesserManaPotion, ItemID.ManaPotion, ItemID.GreaterManaPotion, ItemID.SuperManaPotion, ModContent.ItemType<SupremeManaPotion>(), ModContent.ItemType<HadalStew>(), ModContent.ItemType<AureusCell>(), ModContent.ItemType<GrapeBeer>(), ModContent.ItemType<WhiteWine>(), ModContent.ItemType<Margarita>()];

		public override bool CanUseItem(Item item, Player player)
		{
			if (manaPots.Contains(item.type) && player.GetModPlayer<RPPlayer>().manaPotLock == true)
			{
				return false;
			}
			return true;
		}
	}
}