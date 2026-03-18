using CalamityMod.NPCs.NormalNPCs;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Systems.Collections
{
    [ReinitializeDuringResizeArrays]
    public static class RPNPCSets
    {
        private static SetFactory factory = new(NPCLoader.NPCCount, "RoleplayAddon/NPCSets");
        
        /// <summary>
        /// Set containing all enemies deemed to be undead. Excludes enemies already present in NPCID.Sets.Skeletons and the like.
        /// </summary>
        public static bool[] IsModdedUndead = factory.CreateBoolSet(NPCID.Zombie, NPCID.Skeleton /*and so on*/);

        public static bool[] IsBloodMoonFishingEnemy = factory.CreateBoolSet(NPCID.ZombieMerman, NPCID.EyeballFlyingFish, 
            NPCID.BloodEelHead, NPCID.GoblinShark, NPCID.BloodNautilus);
    }

    public class RPNPCSetsSystem : ModSystem
    {
        public override void SetStaticDefaults()
        {
            // blah blah blah
        }
    }
}