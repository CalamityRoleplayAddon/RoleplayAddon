using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
	// File for general ModPlayer code that doesn't fit in a dedicated file. Currently only accessory effects have a dedicated ModPlayer file.
	// All ModPlayer files are partials of the RPPlayer class
	public partial class RPPlayer : ModPlayer
	{
		public Dictionary<NPC, int> WhispersTargetDict = [];
		public bool reduceEffects = true;		// for showcasing different things more easily. should remove later
		private const int WhispersTargetLifespan = 240;

		public override void PreUpdate()
		{
			// Look through each marked NPC and increment the corresponding timer if the NPC is active
			// Inactive or timed out NPC-timer pairs will be removed in PostUpdate()
			foreach (KeyValuePair<NPC, int> item in WhispersTargetDict)
			{
				if (item.Key.active)
				{
					WhispersTargetDict[item.Key]++;
				}
			}
		}

		public override void PostUpdate()
		{

			if (WhispersTargetDict.Count > 0)
			{
				// Look at each NPC and remove it and its counter if:
				foreach (KeyValuePair<NPC, int> item in WhispersTargetDict)
				{
					// A) The NPC is inactive
					if (!item.Key.active)
					{
						WhispersTargetDict.Remove(item.Key);
					}
					// B) The NPC has reached the end of its lifespan as a target
					if (item.Value > WhispersTargetLifespan)
					{
						WhispersTargetDict.Remove(item.Key);
					}
				}
			}
		}
	}
}