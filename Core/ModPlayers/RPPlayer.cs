using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	// File for general ModPlayer code that doesn't fit in a dedicated file. Currently only accessory effects have a dedicated ModPlayer file.
	// All ModPlayer files are partials of the RPPlayer class
	public partial class RPPlayer : ModPlayer
	{
		public NPC WhispersTarget = null;
		public int WhispersTargetAge = 0;

		public override void PreUpdate()
		{
			if (WhispersTarget != null)
			{
				WhispersTargetAge++;
			}
		}

		public override void PostUpdate()
		{
			if (WhispersTarget != null && !WhispersTarget.active)
			{
				WhispersTarget = null;
				WhispersTargetAge = 0;
			}
			if (WhispersTargetAge > 180)
			{
				WhispersTarget = null;
				WhispersTargetAge = 0;
			}
		}
	}
}