using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	public partial class RPPlayer : ModPlayer
	{
		public bool ashenFlower;
		public bool ashenFlowerGlowing;
		public bool ashenFlowerReduced;
		public bool ashenRing;
		public bool ilmeranRing;
		public bool ilmeranRingReduced;
		public bool manaPotLock;

		public override void ResetEffects()
		{
			ashenFlower = false;
			ashenFlowerGlowing = false;
			ashenFlowerReduced = false;
			ashenRing = false;
			ilmeranRing = false;
			ilmeranRingReduced = false;
			manaPotLock = false;
		}
	}
}