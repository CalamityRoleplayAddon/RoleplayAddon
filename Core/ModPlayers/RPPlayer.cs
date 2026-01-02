using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	// File for general ModPlayer code that doesn't fit in a dedicated file. Currently only accessory effects have a dedicated ModPlayer file.
	// All ModPlayer files are partials of the RPPlayer class
	public partial class RPPlayer : ModPlayer
	{
		// broke the whole weapon doing this,,, try just keeping each list's base state as empty instead of null?
		public List<NPC> WhispersTargetList = null;
		public List<int> WhispersTargetAgeList = null;

		private const int whispersTargetLifespan = 180;

		public override void PreUpdate()
		{
			// Look through each NPC in the target list and increment the corresponding age counter in the age list if the NPC is active
			// Inactive NPCs and counters past the lifespan will be removed in PostUpdate()
			foreach (NPC n in WhispersTargetList)
			{
				if (n.active)
				{
					int index = WhispersTargetList.IndexOf(n);
					WhispersTargetAgeList[index]++;
				}
			}
		}

		public override void PostUpdate()
		{
			// No point in doing these checks if we already know the list is empty (and therefore null due to the below code)
			if (WhispersTargetList != null)
			{
				// Look at each NPC and remove it and its counter if:
				foreach (NPC n in WhispersTargetList)
				{
					int index = WhispersTargetList.IndexOf(n);
					// A) The NPC is inactive
					if (!n.active)
					{
						WhispersTargetList.Remove(n);
						WhispersTargetAgeList.RemoveAt(index);
					}
					// B) The NPC has reached the end of its lifespan as a target
					if (WhispersTargetAgeList[index] > whispersTargetLifespan)
					{
						WhispersTargetList.Remove(n);
						WhispersTargetAgeList.RemoveAt(index);
					}
				}
				// If there are no NPCs left in the target list (due to them all having become inactive or died of old age) set both lists to null
				// This prevents future checks in PostUpdate() until the list is no longer null
				if (WhispersTargetList.Count == 0)
				{
					WhispersTargetList = null;
					WhispersTargetAgeList = null;
				}
			}
		}
	}
}