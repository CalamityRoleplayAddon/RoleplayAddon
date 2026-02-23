using RoleplayAddon.Core.Globals;
using RoleplayAddon.Core.ModPlayers;
using Terraria;

namespace RoleplayAddon.Utilities
{
	public static partial class RPUtils
	{
		/// <summary>
		/// Returns the <c>RPPlayer</c> attached to the passed Player.
		/// </summary>
		public static RPPlayer RPify(this Player player) => player.GetModPlayer<RPPlayer>();

		/// <summary>
		/// Returns the <c>RPGlobalNPC</c> instance attached to the passed NPC.
		/// </summary>
		public static RPGlobalNPC RPify(this NPC npc) => npc.GetGlobalNPC<RPGlobalNPC>();

		/// <summary>
		/// Returns the <c>RPGlobalProjectile</c> instance attached to the passed Projectile.
		/// </summary>
		public static RPGlobalProjectile RPify(this Projectile proj) => proj.GetGlobalProjectile<RPGlobalProjectile>();
	}
}