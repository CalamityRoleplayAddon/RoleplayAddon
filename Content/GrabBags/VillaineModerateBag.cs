using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Tools;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Typeless;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.GrabBags
{
    public class VillainModerateBag : ModItem
    {
        public override string Texture => "Terraria/Images/Item_3331";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<VillainEasyBag>();
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 24;
            Item.maxStack = 9999;
            Item.value = 0;
            Item.rare = ItemRarityID.Expert;
        }

        public override bool CanRightClick() => true;
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            IItemDropRule[] potions = RPUtils.GetUncommonPotions();
            itemLoot.Add(new FewFromRulesRule(5, 1, potions));

            // LONELY ROSE PLACEHOLDER
            // Guarantees that, if a Lonely Rose is given, two Potted Roses will be given alongside it
            if (Main.rand.NextBool(4))
            {
                itemLoot.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.JungleRose));
                itemLoot.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(false),ItemID.ClayPot));
                itemLoot.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(false),ItemID.ClayPot));
            }

            ConditonalDrops(itemLoot, 2);

            // ECUMENA ITEM PLACEHOLDER
            itemLoot.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.FamiliarShirt, 8));

            // CHILD CUSTODY PAPERS PLACEHOLDER
            itemLoot.Add(ItemDropRule.Common(ItemID.LicenseCat, 3));

            // HOSPITAL BILLS PLACEHOLDER
            itemLoot.Add(ItemDropRule.Common(ItemID.PaperAirplaneA, 4));
        }

        /// <summary>
        /// For adding boss drops to Moderate and Heroic bags. Heroic bags should be more likely to drop boss loot.
        /// </summary>
        public static void ConditonalDrops(ItemLoot loot, int denominator = 1)
        {
            IItemDropRule[] lootTable = [
                ItemDropRule.ByCondition(DropHelper.PostKS(false), ItemID.KingSlimeBossBag),
                ItemDropRule.ByCondition(DropHelper.PostEoC(false), ItemID.EyeOfCthulhuBossBag,20 ),
                ItemDropRule.ByCondition(Condition.DownedBrainOfCthulhu.ToDropCondition(ShowItemDropInUI.Never), ItemID.BrainOfCthulhuBossBag),
                ItemDropRule.ByCondition(Condition.DownedEaterOfWorlds.ToDropCondition(ShowItemDropInUI.Never), ItemID.EaterOfWorldsBossBag),
                ItemDropRule.ByCondition(DropHelper.PostQB(false), ItemID.QueenBeeBossBag),
                ItemDropRule.ByCondition(DropHelper.PostDeer(false), ItemID.DeerclopsBossBag),
                ItemDropRule.ByCondition(DropHelper.PostSkele(false), ItemID.SkeletronBossBag),
                ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.WallOfFleshBossBag),
                ItemDropRule.ByCondition(DropHelper.PostQS(false), ItemID.QueenSlimeBossBag),
                ItemDropRule.ByCondition(Condition.DownedSkeletronPrime.ToDropCondition(ShowItemDropInUI.Never), ItemID.SkeletronPrimeBossBag),
                ItemDropRule.ByCondition(Condition.DownedTwins.ToDropCondition(ShowItemDropInUI.Never), ItemID.TwinsBossBag),
                ItemDropRule.ByCondition(Condition.DownedDestroyer.ToDropCondition(ShowItemDropInUI.Never), ItemID.DestroyerBossBag),
                ItemDropRule.ByCondition(DropHelper.PostPlant(false), ItemID.PlanteraBossBag),
                ItemDropRule.ByCondition(DropHelper.PostGolem(false), ItemID.GolemBossBag),
                ItemDropRule.ByCondition(DropHelper.PostEoL(false), ItemID.FairyQueenBossBag),
                ItemDropRule.ByCondition(DropHelper.PostFish(false), ItemID.FishronBossBag),
                ItemDropRule.ByCondition(DropHelper.PostBetsy(false), ItemID.BossBagBetsy),
                ItemDropRule.ByCondition(DropHelper.PostML(false), ItemID.MoonLordBossBag),

                ItemDropRule.ByCondition(DropHelper.PostDS(false), ModContent.ItemType<DesertScourgeBag>()),
                ItemDropRule.ByCondition(DropHelper.PostCrab(false), ModContent.ItemType<CrabulonBag>()),
                ItemDropRule.ByCondition(DropHelper.PostPerfs(false), ModContent.ItemType<PerforatorBag>()),
                ItemDropRule.ByCondition(DropHelper.PostHM(false), ModContent.ItemType<HiveMindBag>()),
                ItemDropRule.ByCondition(DropHelper.PostSG(false), ModContent.ItemType<SlimeGodBag>()),
                ItemDropRule.ByCondition(DropHelper.PostCryo(false), ModContent.ItemType<CryogenBag>()),
                ItemDropRule.ByCondition(DropHelper.PostAS(false), ModContent.ItemType<AquaticScourgeBag>()),
                ItemDropRule.ByCondition(DropHelper.PostBrim(false), ModContent.ItemType<BrimstoneElementalBag>()),
                ItemDropRule.ByCondition(DropHelper.PostCal(false), ModContent.ItemType<CalamitasCloneBag>()),
                ItemDropRule.ByCondition(DropHelper.PostAureus(false), ModContent.ItemType<AstrumAureusBag>()),
                ItemDropRule.ByCondition(DropHelper.PostLevi(false), ModContent.ItemType<LeviathanBag>()),
                ItemDropRule.ByCondition(DropHelper.PostPBG(false), ModContent.ItemType<PlaguebringerGoliathBag>()),
                ItemDropRule.ByCondition(DropHelper.PostRav(false), ModContent.ItemType<RavagerBag>()),
                ItemDropRule.ByCondition(DropHelper.PostAD(false), ModContent.ItemType<AstrumDeusBag>()),
                ItemDropRule.ByCondition(DropHelper.PostBirb(false), ModContent.ItemType<DragonfollyBag>()),
                ItemDropRule.ByCondition(DropHelper.PostGuard(false), ModContent.ItemType<WarbanneroftheRighteous>()),
                ItemDropRule.ByCondition(DropHelper.PostGuard(false), ModContent.ItemType<RelicOfDeliverance>()),
                ItemDropRule.ByCondition(DropHelper.PostGuard(false), ModContent.ItemType<RelicOfConvergence>()),
                ItemDropRule.ByCondition(DropHelper.PostGuard(false), ModContent.ItemType<RelicOfResilience>()),
                ItemDropRule.ByCondition(DropHelper.PostProv(false), ModContent.ItemType<ProvidenceBag>()),
                ItemDropRule.ByCondition(DropHelper.PostCV(false), ModContent.ItemType<CeaselessVoidBag>()),
                ItemDropRule.ByCondition(DropHelper.PostSW(false), ModContent.ItemType<StormWeaverBag>()),
                ItemDropRule.ByCondition(DropHelper.PostSig(false), ModContent.ItemType<SignusBag>()),
                ItemDropRule.ByCondition(DropHelper.PostOD(false), ModContent.ItemType<OldDukeBag>()),
                ItemDropRule.ByCondition(DropHelper.PostDoG(false), ModContent.ItemType<DevourerofGodsBag>()),
                ItemDropRule.ByCondition(DropHelper.PostYharon(false), ModContent.ItemType<YharonBag>()),
                ItemDropRule.ByCondition(DropHelper.PostExos(false), ModContent.ItemType<DraedonBag>()),
                ItemDropRule.ByCondition(DropHelper.PostSCal(false), ModContent.ItemType<CalamitasCoffer>()),
                ItemDropRule.ByCondition(DropHelper.PostAEW(false), ModContent.ItemType<HalibutCannon>()),
            ];
            
            //loot.Add(new AlwaysAtleastOneSuccessDropRule(lootTable));
            //loot.Add(new OneFromRulesRule(denominator, lootTable));
            loot.Add(new FewFromRulesRule(3, denominator, lootTable));
        }
    }
}