using System.Collections.Generic;
using System.Linq;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace RoleplayAddon.Content.NPCs.TownNPCs
{
    public class Villain : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_22";

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.lifeMax = 1000000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] 
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Bestiary.Villain")
            }
            );
        }

        public override List<string> SetNPCNameList()
        {
            List<string> name = ["Mark (E)"];
            return name;
        }

        public override void AI()
        {
        }

        public override string GetChat()
        {
            //if (NPC.homeless)
            {
                int choice = Main.rand.Next(2) + 1;
                return Language.GetTextValue($"Chat.Homeless{choice}");
            }

            return base.GetChat();
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = GetText("ChatButton");
            button2 = GetText("QuestButton");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                Main.NewText("first button");
            }
            else
            {
                Main.NewText("second button");
                Player player = Main.LocalPlayer;
                RPPlayer modPlayer = player.RPify();
                bool result = VillainQuests.GiveQuest(player, QuestDifficulties.Easy);
                if (result)
                {
                    Main.NewText($"quest given: {modPlayer.QuestKey}, {modPlayer.QuestProgression}");
                }
                else
                {
                    Main.NewText($"quest already there: {modPlayer.QuestKey}, {modPlayer.QuestProgression}");
                }
                VillainQuests.TryCompleteQuest(player, QuestDifficulties.Easy);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            //idk
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.AmberBolt;
            attackDelay = 2;
        }

        private string GetText(string dialogueKey, params object[] args) => Language.GetTextValue($"Mods.RoleplayAddon.NPCs.Villain.{dialogueKey}", args);
    }

    /// <summary>
    /// The Villain's quests are organised into a difficulty hierarchy.
    /// Higher difficulty tiers are not locked behind higher progression; a quest's tier
    /// depends on the difficulty of completion at the intended stage in the game.
    /// </summary>
    public enum QuestDifficulties
    {
        Easy,
        Moderate,
        Heroic
    }

    public enum QuestAreas
    {
        None,
        BloodMoon,
        Hardmode,
        Golem,
        MoonLord
    }

    public static class VillainQuests
    {
        /// <summary>
        /// Checks for each stage of progression that acts as a quest lock, so we know
        /// which quests the player can and can't access currently.
        /// Up to two quest locks are currently supported per quest
        /// </summary>
        /// <returns>List of unlocked stages</returns>
        public static List<QuestAreas> CurrentQuestAreas()
        {
            List<QuestAreas> activeAreas = [];
            activeAreas.Add(QuestAreas.None);

            if (Main.bloodMoon)
            {
                activeAreas.Add(QuestAreas.BloodMoon);
            }
            if (Main.hardMode)
            {
                activeAreas.Add(QuestAreas.Hardmode);
            }
            

            // checks go here ...
            return activeAreas;
        }

        /// <summary>
        /// Dictionary containing all of the key details of each easy quest, bar the related NPC ID(s).
        /// ID(s) covered in QuestIDs.
        /// </summary>
        /// <returns></returns>
        public readonly static Dictionary<KeyValuePair<string, int>, KeyValuePair<QuestAreas, QuestAreas>> EasyQuests = new()
        {
            {new("Gravedigger", 35), new(QuestAreas.None, QuestAreas.None)},
            {new("BloodMoonFishing", 5), new(QuestAreas.BloodMoon, QuestAreas.None)}
            // blah blah blah
        };

        /// <summary>
        /// Hands out a quest to a player. Return value should be used to determine dialogue
        /// </summary>
        /// <returns><c>True</c>if the quest was successfully given, <c>False</c>if the player already had a quest</returns>
        public static bool GiveQuest(Player player, QuestDifficulties difficulty)
        {
            List<QuestAreas> activeAreas = CurrentQuestAreas();
            RPPlayer modPlayer = player.RPify();
            if (!modPlayer.QuestActive)
            {
                switch (difficulty)
                {
                    case QuestDifficulties.Easy:
                        while (!modPlayer.QuestActive)
                        {
                            int index = Main.rand.Next(EasyQuests.Count);
                            KeyValuePair<KeyValuePair<string, int>, KeyValuePair<QuestAreas, QuestAreas>> item = EasyQuests.ElementAt(index);
                            KeyValuePair<string, int> questDetails = item.Key;
                            KeyValuePair<QuestAreas, QuestAreas> questLocks = item.Value;

                            // Only give the quest if it is currently valid to do so
                            if (activeAreas.Contains(questLocks.Key) && activeAreas.Contains(questLocks.Value))
                            {
                                string questKey = questDetails.Key;
                                modPlayer.QuestActive = true;
                                modPlayer.QuestProgression = 0;
                                modPlayer.QuestKey = questKey;
                            }
                        }
                        break;
                    case QuestDifficulties.Moderate:
                        break;
                    case QuestDifficulties.Heroic:
                        break;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to be called when the player tries to hand in a quest. If the player has killed all required enemies, a
        /// reward will be given using GiveReward(); else, the player will be told how many enemies they have left to kill.
        /// </summary>
        public static void TryCompleteQuest(Player player, QuestDifficulties difficulty)
        {
            RPPlayer modPlayer = player.RPify();

            // Don't allow a player to hand in a quest when they don't have one
            // NOTE in final implementation, this button should not even be there without an active quest, making this redundant by then
            if (!modPlayer.QuestActive)
            {
                return;
            }

            string questKey = modPlayer.QuestKey;
            int questProgress = modPlayer.QuestProgression;
            switch (difficulty)
            {
                case QuestDifficulties.Easy:
                    foreach (var item in EasyQuests)
                    {
                        if (questKey == item.Key.Key && questProgress >= item.Key.Value)
                        {
                            GiveReward(player, difficulty);
                            return;
                        }
                    }
                    break;
                case QuestDifficulties.Moderate:
                    break;
                case QuestDifficulties.Heroic:
                    break;
            }
        }

        public static void GiveReward(Player player, QuestDifficulties difficulty)
        {
            // if giving blade soul or razor, set their bools to true !
            Main.NewText("give reward buddy");
        }
    }
}