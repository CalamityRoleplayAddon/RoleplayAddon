using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	public partial class RPPlayer : ModPlayer
	{
		public bool manaPotLock;

		public override void ResetEffects()
		{
			manaPotLock = false;
		}
	}
}