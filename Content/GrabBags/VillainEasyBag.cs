using CalamityMod.Items;
using RoleplayAddon.Content.NPCs.TownNPCs.Villain;
using RoleplayAddon.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.GrabBags
{
    public class VillainEasyBag : ModItem
    {
        public override string Texture => "Terraria/Images/Item_3331";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
            //set shimmer to lower bag type for other bags maybe?
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 24;
            Item.maxStack = 9999;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;     // set to orang and light red for others
        }

        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            IItemDropRule[] potions = RPUtils.GetRarePotionsForGrabBag();
            itemLoot.Add(new FewFromRulesRule(3, 1, potions));

            // POTTED ROSES PLACEHOLDER
            itemLoot.Add(ItemDropRule.Common(ItemID.ClayPot, 4));

            Main.NewText($"potions. {potions.Length}. {potions}");
        }
    }
}