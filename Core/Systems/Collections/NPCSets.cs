using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.SlimeGod;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Systems.Collections
{
    [ReinitializeDuringResizeArrays]
    public static class RPNPCSets
    {
        public static SetFactory Factory = new(NPCLoader.NPCCount, "RoleplayAddon/NPCSets");
        
        /// <summary>
        /// Set containing all enemies deemed to be undead. Excludes enemies already present in NPCID.Sets.Skeletons and the like.
        /// </summary>
        public static bool[] IsModdedUndead = Factory.CreateBoolSet(NPCID.Zombie, NPCID.Skeleton /*and so on*/);

        public static bool[] IsBloodMoonFishingEnemy = Factory.CreateBoolSet(NPCID.ZombieMerman, NPCID.EyeballFlyingFish, 
            NPCID.BloodEelHead, NPCID.GoblinShark, NPCID.BloodNautilus);

        public static bool[] IsMimic = Factory.CreateBoolSet(NPCID.Mimic, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, 
            NPCID.BigMimicHallow, NPCID.BigMimicJungle, NPCID.IceMimic, NPCID.PresentMimic);

        public static bool[] IsSlime = Factory.CreateBoolSet(NPCID.SlimeRibbonGreen, NPCID.SlimeRibbonRed, NPCID.SlimeRibbonWhite, 
            NPCID.SlimeRibbonYellow, NPCID.SlimeSpiked, NPCID.IceSlime, NPCID.SandSlime, NPCID.BlueSlime, 
            NPCID.KingSlime, NPCID.LavaSlime, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue, NPCID.QueenSlimeMinionPink, 
            NPCID.QueenSlimeMinionPurple, NPCID.GoldenSlime, NPCID.MotherSlime, NPCID.CorruptSlime, NPCID.Crimslime, 
            NPCID.DungeonSlime, NPCID.RainbowSlime, NPCID.UmbrellaSlime, NPCID.SpikedIceSlime, NPCID.SpikedJungleSlime, 
            NPCID.IlluminantSlime, ModContent.NPCType<SlimeGodCore>(), ModContent.NPCType<AeroSlime>(),
            ModContent.NPCType<CryoSlime>(),ModContent.NPCType<BloomSlime>(), ModContent.NPCType<PerennialSlime>(),
            ModContent.NPCType<GammaSlime>(),ModContent.NPCType<AstralSlime>(), ModContent.NPCType<CorruptSlimeSpawn>(),
            ModContent.NPCType<CorruptSlimeSpawn2>(), ModContent.NPCType<CrimsonSlimeSpawn>(), ModContent.NPCType<CrimsonSlimeSpawn2>(), 
            ModContent.NPCType<PestilentSlime>(), ModContent.NPCType<IrradiatedSlime>(), ModContent.NPCType<EbonianBlightSlime>(),
            ModContent.NPCType<CrimulanBlightSlime>(), ModContent.NPCType<EbonianPaladin>(), ModContent.NPCType<CrimulanPaladin>());

        // WHY ARE THERE SO MANY OF YOUUUUUUUU
        public static bool[] IsSalamander = Factory.CreateBoolSet(NPCID.Salamander, NPCID.Salamander2, NPCID.Salamander3,
            NPCID.Salamander4, NPCID.Salamander5, NPCID.Salamander6, NPCID.Salamander7, NPCID.Salamander8, NPCID.Salamander9);

        public static bool[] IsCorruptionEnemy = Factory.CreateBoolSet(NPCID.EaterofSouls, NPCID.DevourerHead, NPCID.CorruptGoldfish,
            NPCID.CorruptBunny, NPCID.EaterofWorldsHead, NPCID.Corruptor, NPCID.CorruptSlime, NPCID.Slimer, NPCID.SeekerHead,
            NPCID.DarkMummy, ModContent.NPCType<EbonianPaladin>(), ModContent.NPCType<EbonianBlightSlime>());

        public static bool[] IsCrimsonEnemy = Factory.CreateBoolSet(NPCID.BloodCrawler, NPCID.CrimsonGoldfish, NPCID.CrimsonBunny,
            NPCID.FaceMonster, NPCID.Crimera, NPCID.BrainofCthulhu, NPCID.Herpling, NPCID.Crimslime, NPCID.BloodJelly, NPCID.BloodFeeder,
            NPCID.BloodMummy, ModContent.NPCType<CrimulanPaladin>(), ModContent.NPCType<CrimulanBlightSlime>());

        public static bool[] IsHallowEnemy = Factory.CreateBoolSet(NPCID.Pixie, NPCID.Gastropod, NPCID.Unicorn, NPCID.RainbowSlime,
            NPCID.LightMummy, NPCID.QueenSlimeBoss, NPCID.EmpressButterfly);

        public static bool[] IsPlagueEnemy = Factory.CreateBoolSet(ModContent.NPCType<Melter>(), ModContent.NPCType<PestilentSlime>(),
            ModContent.NPCType<PlagueCharger>(), ModContent.NPCType<PlaguebringerMiniboss>(), ModContent.NPCType<Plagueshell>(),
            ModContent.NPCType<Viruling>(), ModContent.NPCType<PlaguebringerGoliath>());
        
        public static bool[] IsProfanedEnemy = Factory.CreateBoolSet(ModContent.NPCType<ImpiousImmolator>(), ModContent.NPCType<ScornEater>(),
            ModContent.NPCType<ProfanedEnergyBody>(), ModContent.NPCType<ProfanedGuardianHealer>(), ModContent.NPCType<ProfanedGuardianDefender>(),
            ModContent.NPCType<ProfanedGuardianCommander>(), ModContent.NPCType<Providence>());
    }
}