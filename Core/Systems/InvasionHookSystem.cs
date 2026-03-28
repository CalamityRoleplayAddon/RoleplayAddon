using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Systems
{
    // For code that should run during an invasion-related hook
    public class InvasionHookSystem : ModSystem
    {
        public override void PostUpdateInvasions()
        {
            if (Main.invasionProgress == Main.invasionProgressMax)
            {
                foreach (Player player in Main.ActivePlayers)
                {
                    RPPlayer modPlayer = player.RPify();
                    if (modPlayer.QuestKey == "OneTerrarianArmy")
                    {
                        modPlayer.QuestProgression++;
                    }
                }
            }
        }
    }
}