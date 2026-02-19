using Terraria;
using Terraria.ID;

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
	}
}