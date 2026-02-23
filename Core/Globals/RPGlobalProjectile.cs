using Terraria.ModLoader;

namespace RoleplayAddon.Core.Globals
{
	public class RPGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public bool IlmeranRingTriggered = false;
		public bool AshenFlowerTriggered = false;
	}
}
