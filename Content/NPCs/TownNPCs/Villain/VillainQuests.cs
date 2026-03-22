using CalamityMod.Items;
using RoleplayAddon.Content.GrabBags;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.NPCs.TownNPCs.Villain
{
    /// <summary>
    /// The Villain's quests are organised into a difficulty hierarchy.
    /// Higher difficulty tiers are not locked behind higher progression; a quest's tier
    /// depends on the difficulty of completion at the intended stage in the game.
    /// </summary>
    public enum QuestDifficulties
    {
        Easy = 1,
        Moderate,
        Heroic
    }

    /// <summary>
    /// The Villain's quests are made available at different stages of progression,
    /// or temporarily under different world states, like events.
    /// Progression also influences rewards.
    /// A QuestArea is a stage of progression or world state.
    /// </summary>
    public enum QuestAreas
    {
        None,
        PreHardMode,
        Hardmode,
        BloodMoonActive,
        GolemDefeated,
        MoonLordDefeated
    }

    /// <summary>
    /// Class containing members for storing, giving out, and rewarding completion of the Villain NPC's quests.
    /// There should ALWAYS be at least one quest of each difficulty available. I.e., every difficulty should have one {None, None} quest.
    /// </summary>
    public static class VillainQuests
    {
        /// <summary>
        /// Checks for each stage of progression that acts as a quest lock, so we know
        /// which quests the player can and can't access currently.
        /// Up to two quest locks are currently supported per quest.
        /// </summary>
        /// <returns>List of unlocked stages</returns>
        public static List<QuestAreas> CurrentQuestAreas()
        {
            // Maybe add a full moon check for werewolf quest? except, that would have issues with getting the quest RIGHT before full moon ends lol
            List<QuestAreas> activeAreas = [];

            if (!Main.hardMode)
            {
                activeAreas.Add(QuestAreas.PreHardMode);
            }
            else
            {
                activeAreas.Add(QuestAreas.Hardmode);
            }
            if (Main.bloodMoon)
            {
                activeAreas.Add(QuestAreas.BloodMoonActive);
            }
            if (NPC.downedGolemBoss)
            {
                activeAreas.Add(QuestAreas.GolemDefeated);
            }
            if (NPC.downedMoonlord)
            {
                activeAreas.Add(QuestAreas.MoonLordDefeated);
            }

            activeAreas.Add(QuestAreas.None);
            return activeAreas;
        }

        /// <summary>
        /// Dictionary containing key details of each Easy quest
        /// </summary>
        /// <returns></returns>
        public readonly static Dictionary<KeyValuePair<string, int>, KeyValuePair<QuestAreas, QuestAreas>> EasyQuests = new()
        {
            // Change back to 35 for gravedigger, idk for werewolf
            {new("Gravedigger", 1), new(QuestAreas.PreHardMode, QuestAreas.None)},
            {new("Wattpad", 1), new(QuestAreas.Hardmode, QuestAreas.None)},
        };

        /// <summary>
        /// Dictionary containing key details of each Moderate quest
        /// </summary>
        /// <returns></returns>
        public readonly static Dictionary<KeyValuePair<string, int>, KeyValuePair<QuestAreas, QuestAreas>> ModerateQuests = new()
        {
            {new("Clickbait", 1), new(QuestAreas.PreHardMode, QuestAreas.None)},
            {new("BloodMoonFishing", 5), new(QuestAreas.BloodMoonActive, QuestAreas.None)},
            {new("Ragebait", 1), new(QuestAreas.Hardmode, QuestAreas.None)},
        };

        /// <summary>
        /// Dictionary containing key details of each Heroic quest
        /// </summary>
        /// <returns></returns>
        public readonly static Dictionary<KeyValuePair<string, int>, KeyValuePair<QuestAreas, QuestAreas>> HeroicQuests = new()
        {
            {new("ShadowWizard", 1), new(QuestAreas.PreHardMode, QuestAreas.None)},
            {new("TheMightyKraken", 1), new(QuestAreas.Hardmode, QuestAreas.BloodMoonActive)},
            {new("RageBait", 1), new(QuestAreas.Hardmode, QuestAreas.None)},
        };

        /// <summary>
        /// Hands out a quest to a player. Return value should be used to determine dialogue
        /// </summary>
        /// <returns><c>True</c>if the quest was successfully given, <c>False</c>if the player already had a quest</returns>
        public static bool GiveQuest(Player player, QuestDifficulties difficulty)
        {
            // for SOME reason. this creates an exception when main.hardMode. not even at the line in CurrentQuestAreas() when Main.hardMode is checked
            // just. MAKING THIS SPECIFIC LIST
            List<QuestAreas> activeAreas = CurrentQuestAreas();

            foreach (var a in activeAreas)
            {
                Main.NewText(a);
            }

            RPPlayer modPlayer = player.RPify();
            if (!modPlayer.QuestActive)
            {
                List<string> possibleQuests = [];
                switch (difficulty)
                {
                    case QuestDifficulties.Easy:
                        foreach (var item in EasyQuests)
                        {
                            KeyValuePair<QuestAreas, QuestAreas> questLocks = item.Value;
                            if (activeAreas.Contains(questLocks.Key) && activeAreas.Contains(questLocks.Value))
                            {
                                possibleQuests.Add(item.Key.Key);
                            }
                        }
                        break;
                    case QuestDifficulties.Moderate:
                        foreach (var item in ModerateQuests)
                            {
                                KeyValuePair<QuestAreas, QuestAreas> questLocks = item.Value;
                                if (activeAreas.Contains(questLocks.Key) && activeAreas.Contains(questLocks.Value))
                                {
                                    possibleQuests.Add(item.Key.Key);
                                }
                            }
                        break;
                    case QuestDifficulties.Heroic:
                        foreach (var item in HeroicQuests)
                            {
                                KeyValuePair<QuestAreas, QuestAreas> questLocks = item.Value;
                                if (activeAreas.Contains(questLocks.Key) && activeAreas.Contains(questLocks.Value))
                                {
                                    possibleQuests.Add(item.Key.Key);
                                }
                            }
                        break;
                }
                string questKey = possibleQuests[Main.rand.Next(possibleQuests.Count)];
                modPlayer.QuestActive = true;
                modPlayer.QuestProgression = 0;
                modPlayer.QuestKey = questKey;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to be called when the player tries to hand in a quest. If the player has killed all required enemies, a
        /// reward will be given using GiveReward(); else, the player will be told how many enemies they have left to kill.
        /// </summary>
        public static KeyValuePair<int, QuestDifficulties> TryCompleteQuest(Player player)
        {
            RPPlayer modPlayer = player.RPify();

            string questKey = modPlayer.QuestKey;
            int questProgress = modPlayer.QuestProgression;
            int maxQuestProgression = 0;        // needed for hand-in failure dialogue

            QuestDifficulties difficulty = QuestDifficulties.Easy;
            foreach (var item in ModerateQuests)
            {
                if (questKey == item.Key.Key)
                {
                    difficulty = QuestDifficulties.Moderate;
                }
            }
            foreach (var item in HeroicQuests)
            {
                if (questKey == item.Key.Key)
                {
                    difficulty = QuestDifficulties.Heroic;
                }
            }

            switch (difficulty)
            {
                case QuestDifficulties.Easy:
                    foreach (var item in EasyQuests)
                    {
                        if (questKey == item.Key.Key)
                        {
                            if (questProgress >= item.Key.Value)
                            {
                                return GiveReward(player, difficulty);
                            }
                            else
                            {
                                maxQuestProgression = item.Key.Value;
                            }
                        }
                    }
                    break;
                case QuestDifficulties.Moderate:
                    foreach (var item in ModerateQuests)
                        {
                            if (questKey == item.Key.Key)
                            {
                                if (questProgress >= item.Key.Value)
                                {
                                    return GiveReward(player, difficulty);
                                }
                                else
                                {
                                    maxQuestProgression = item.Key.Value;
                                }
                            }
                        }
                    break;
                case QuestDifficulties.Heroic:
                    foreach (var item in HeroicQuests)
                        {
                            if (questKey == item.Key.Key)
                            {
                                if (questProgress >= item.Key.Value)
                                {
                                    return GiveReward(player, difficulty);
                                }
                                else
                                {
                                    maxQuestProgression = item.Key.Value;
                                }
                            }
                        }
                    break;
            }
            KeyValuePair<int, QuestDifficulties> failure = new(maxQuestProgression - questProgress, difficulty);
            return failure;
        }

        /// <summary>
        /// Fetches a number of gold coins based on the difficulty of the quest and the world state
        /// Note: currently not giving a random value, for some reason
        /// </summary>
        public static int GetCoinReward(QuestDifficulties difficulty)
        {
            List<QuestAreas> activeAreas = CurrentQuestAreas();

            foreach (var a in activeAreas)
            {
                Main.NewText(a);
            }
            int hardmodeMulti = activeAreas.Contains(QuestAreas.Hardmode) ? 2 : 1;
            int moonlordMulti = activeAreas.Contains(QuestAreas.MoonLordDefeated) ? 2 : 1;

            return 5 * (int)difficulty * hardmodeMulti * moonlordMulti;
        }

        private static KeyValuePair<int, QuestDifficulties> GiveReward(Player player, QuestDifficulties difficulty)
        {
            Main.NewText("give reward buddy");

            if (difficulty == QuestDifficulties.Easy)
            {
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<VillainEasyBag>());
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, GetCoinReward(difficulty));
            }

            /* if (!player.RPify().QuestRewardBladeSoulReceived)
            {
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), bladesoul);
                player.RPify().QuestRewardBladeSoulReceived = true;
            }

            if (!player.RPify().QuestRewardRazorReceived)
            {
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), razor);
                player.RPify().QuestRewardRazorReceived = true;
            } */

            ResetPlayerData(player);
            KeyValuePair<int, QuestDifficulties> result = new(0, difficulty);
            return result;
        }

        private static void ResetPlayerData(Player player)
        {
            RPPlayer modPlayer = player.RPify();
            modPlayer.QuestActive = false;
            modPlayer.QuestProgression = 0;
            modPlayer.QuestKey = "";
        }
    }
}