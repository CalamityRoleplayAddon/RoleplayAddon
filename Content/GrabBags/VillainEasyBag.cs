using RoleplayAddon.Utilities;
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
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 24;
            Item.maxStack = 9999;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
        }

        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            IItemDropRule[] potions = RPUtils.GetCommonPotions();
            itemLoot.Add(new FewFromRulesRule(5, 1, potions));

            // POTTED ROSES PLACEHOLDER
            itemLoot.Add(ItemDropRule.Common(ItemID.ClayPot, 4));
        }
    }
}