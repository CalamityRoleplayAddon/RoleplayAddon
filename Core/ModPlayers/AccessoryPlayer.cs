using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	public partial class RPPlayer : ModPlayer
	{
		public bool ashenFlower;
		public bool ashenRing;
		public bool ilmeranRing;
		public bool manaPotLock;

		public override void ResetEffects()
		{
			ashenFlower = false;
			ashenRing = false;
			ilmeranRing = false;
			manaPotLock = false;
		}
	}
}