using CalamityMod;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Food;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Utilities
{
	public partial class RPUtils
	{
		/// <summary>
		/// Looks at the Lunatic Cultist's list of resistances, which has every vanilla homing projectile and usually modded ones, to determine if a passed-in projectile is homing.
		/// Should be used in conjunction with a damage class check to account for any minions added to the Lunatic Cultist's list of resistances, unless minions are to be included in what the method is being used for.
		/// </summary>
		/// <param name="projectile">The projectile to be checked</param>
		/// <returns><c>True</c> if the projectile is homing, <c>False</c> otherwise</returns>
		public static bool IsHoming(this Projectile projectile)
		{
			if (ProjectileID.Sets.CultistIsResistantTo[projectile.type])
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creates an array of fairly common or easy-to-acquire potions:
		/// Regeneration, Swiftness, Ironskin, Shine, Mining, Spelunker, Healing, Mana, Calcium.
		/// </summary>
		/// <returns>An<c>IItemDropRule</c>array of drop rules that can be selected from</returns>
		public static IItemDropRule[] GetCommonPotions()
		{
			IItemDropRule[] rule =
			[
				ItemDropRule.NotScalingWithLuck(ItemID.RegenerationPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.SwiftnessPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.IronskinPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.ShinePotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.MiningPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.SpelunkerPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CalciumPotion>(), 1, 2, 5),

				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.GreaterHealingPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.HealingPotion, 1, 2, 5),

				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.FlaskofIchor, 1, 3, 6),
				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.FlaskofCursedFlames, 1, 3, 6),
				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ItemID.FlaskofGold, 1, 3, 6),
				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ModContent.ItemType<FlaskOfCrumbling>(), 1, 3, 6),
				ItemDropRule.ByCondition(DropHelper.PostPlant(false), ItemID.FlaskofVenom, 1, 3, 6),
			];

			return rule;
		}

		/// <summary>
		/// Creates an array of uncommon potions:
		/// Gravitation, Featherfall, Summoning, Wrath, Rage, Endurance, Mana Regeneration, Magic Power, Battle, Calming, Bounding.
		/// </summary>
		/// <returns>An<c>IItemDropRule</c>array of drop rules that can be selected from</returns>
		public static IItemDropRule[] GetUncommonPotions()
		{
			IItemDropRule[] rule =
			[
				ItemDropRule.NotScalingWithLuck(ItemID.GravitationPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.FeatherfallPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.SummoningPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.WrathPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.RagePotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.EndurancePotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.ManaRegenerationPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.MagicPowerPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.BattlePotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ItemID.CalmingPotion, 1, 3, 6),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BoundingPotion>(), 1, 3, 6),

				ItemDropRule.ByCondition(DropHelper.PostProv(false), ModContent.ItemType<SupremeHealingPotion>(), 1, 2, 5),
				ItemDropRule.ByCondition(DropHelper.PostML(false), ItemID.SuperHealingPotion, 1, 2, 5),

				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ModContent.ItemType<PhotosynthesisPotion>(), 1, 3, 6),
				ItemDropRule.ByCondition(DropHelper.Hardmode(false), ModContent.ItemType<SoaringPotion>(), 1, 3, 6),
			];

			return rule;
		}

		/// <summary>
		/// Creates an array of rare potions:
		/// Lifeforce, Inferno, Return, Greater Luck, Wrath, Rage, Endurance, Zen, Zerg. Also includes Hadal Stew.
		/// </summary>
		/// <returns>An<c>IItemDropRule</c>array of drop rules that can be selected from</returns>
		public static IItemDropRule[] GetRarePotions()
		{
			IItemDropRule[] rule =
			[
				ItemDropRule.NotScalingWithLuck(ItemID.LifeforcePotion, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.InfernoPotion, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.PotionOfReturn, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.LuckPotionGreater, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.WrathPotion, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.RagePotion, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ItemID.EndurancePotion, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<HadalStew>(), 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZenPotion>(), 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ZergPotion>(), 1, 4, 7),

				ItemDropRule.ByCondition(DropHelper.PostDoG(false), ModContent.ItemType<OmegaHealingPotion>(), 1, 2, 5),
				ItemDropRule.ByCondition(DropHelper.PostProv(false), ModContent.ItemType<Bloodfin>(), 1, 2, 5),

				ItemDropRule.ByCondition(DropHelper.PostAureus(false), ModContent.ItemType<GravityNormalizerPotion>(), 1, 4, 7),
				ItemDropRule.ByCondition(DropHelper.PostCV(false), ModContent.ItemType<CeaselessHungerPotion>(), 1, 4, 7),
			];

			return rule;
		}
	}
}