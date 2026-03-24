using CalamityMod;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.GrabBags
{
    public class VillainHeroicBag : ModItem
    {
        public override string Texture => "Terraria/Images/Item_3331";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<VillainModerateBag>();
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 24;
            Item.maxStack = 9999;
            Item.value = 0;
            Item.rare = ItemRarityID.Master;
        }

        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            IItemDropRule[] potions = RPUtils.GetRarePotions();
            itemLoot.Add(new FewFromRulesRule(5, 1, potions));

            VillainModerateBag.ConditonalDrops(itemLoot, 1);

            // ECUMENA ITEM PLACEHOLDER
            itemLoot.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.FamiliarShirt, 8));

            // HOSPITAL BILLS PLACEHOLDER
            itemLoot.Add(ItemDropRule.Common(ItemID.PaperAirplaneA, 4));

            // BLADE SOUL PLACEHOLDER
            itemLoot.Add(ItemDropRule.ByCondition(DropHelper.PostML(false), ItemID.SoulBottleFlight, 20));

            // OKRAM'S RAZOR PLACEHOLDER
            itemLoot.Add(ItemDropRule.ByCondition(DropHelper.PostML(false), ItemID.Razorpine, 20));
        }
    }
}