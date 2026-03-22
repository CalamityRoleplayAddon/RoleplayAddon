using System.Linq;
using CalamityMod;
using CalamityMod.Items.Potions;
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
		/// <returns>True if the projectile is homing, false otherwise</returns>
		public static bool IsHoming(this Projectile projectile)
		{
			if (ProjectileID.Sets.CultistIsResistantTo[projectile.type])
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets an array of vanilla potions to be selected from.
		/// Excludes Lifeforce, Rage, Wrath and the like.
		/// Excludes modded and/or hardmode potions.
		/// </summary>
		/// <returns><c>IItemDropRule[]</c> array of potions</returns>
		public static IItemDropRule[] GetCommonPotionsForGrabBag()
		{
			IItemDropRule[] rule =
            [
                ItemDropRule.NotScalingWithLuck(ItemID.RegenerationPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.SwiftnessPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.IronskinPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.ObsidianSkinPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.SpelunkerPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.MiningPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.HunterPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.HeartreachPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.ManaRegenerationPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.MagicPowerPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.AmmoReservationPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.BuilderPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.TrapsightPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.FlipperPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.GillsPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.InvisibilityPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.NightOwlPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.ShinePotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.ThornsPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.WarmthPotion, 1, 2, 5),
			];

			return rule;
		}

		/// <summary>
		/// Gets an array of 'rare' (read: powerful or difficult to acquire pre-boss) vanilla potions to be selected from.
		/// Includes Lifeforce, Rage, Wrath, Summoning, Gravitation, Greater Luck, Inferno.
		/// Excludes modded and/or harmode potions.
		/// </summary>
		/// <returns>IItemDropRule[]</c> array of potions</returns>
		public static IItemDropRule[] GetRarePotionsForGrabBag()
		{
			IItemDropRule[] rule =
            [
                ItemDropRule.NotScalingWithLuck(ItemID.GravitationPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.LuckPotionGreater, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.InfernoPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.LifeforcePotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.RagePotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.SummoningPotion, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ItemID.WrathPotion, 1, 2, 5)
			];

			return rule;
		}

		/// <summary>
		/// Gets an array of hardmode Calamity potions to be selected from.
		/// Potions are made available as the required progression is made.
		/// </summary>
		/// <returns>IItemDropRule[]</c> array of potions</returns>
		public static IItemDropRule[] GetHMCalamityPotionsForGrabBag()
		{
			IItemDropRule[] rule =
            [
                ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PhotosynthesisPotion>(), 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SoaringPotion>(), 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FlaskOfCrumbling>(), 1, 2, 5),
			];

			if (DownedBossSystem.downedCalamitasClone)
			{
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FlaskOfBrimstone>(), 1, 2, 5));
			}
			
			if (DownedBossSystem.downedAstrumAureus)
			{
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<GravityNormalizerPotion>(), 1, 2, 5));
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AureusCell>(), 1, 2, 5));
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AstralInjection>(), 1, 2, 5));
			}

			if (DownedBossSystem.downedProvidence)
			{
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FlaskOfHolyFlames>(), 1, 2, 5));
			}

			if (DownedBossSystem.downedCeaselessVoid)
			{
				rule.Append(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CeaselessHungerPotion>(), 1, 2, 5));
			}

			return rule;
		}
	}
}