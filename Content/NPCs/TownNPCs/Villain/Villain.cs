using System.Collections.Generic;
using CalamityMod;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using RoleplayAddon.Core.ModPlayers;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace RoleplayAddon.Content.NPCs.TownNPCs.Villain
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
            /*
            TO BE ADDED:
                Thunderstorm
            */
            WeightedRandom<string> dialogue = new();
            Player player = Main.LocalPlayer;

            dialogue.Add(GetText("Chat.Normal1"));
            dialogue.Add(GetText("Chat.Normal2"));
            dialogue.Add(GetText("Chat.Normal3"));
            dialogue.Add(GetText("Chat.Normal4"));
            dialogue.Add(GetText("Chat.Normal5"));

            if (NPC.homeless)
            {
                int choice = Main.rand.Next(2) + 1;
                dialogue.Add(GetText($"Chat.Homeless{choice}"));
            }

            if (player.ZoneSkyHeight)
            {
                if (Main.rand.NextBool())
                {
                    dialogue.Add(GetText("Chat.Space1"));
                }
                else
                {
                    dialogue.Add(GetText("Chat.Space2"));
                }
            }

            int guide = NPC.FindFirstNPC(NPCID.Guide);
            if (guide != -1 && Main.hardMode)
            {
                dialogue.Add(GetText("Chat.Guide", Main.npc[guide].GivenName));
            }

            int amidias = NPC.FindFirstNPC(ModContent.NPCType<SeaKing>());
            if (amidias != -1 && Main.IsItRaining)
            {
                dialogue.Add(GetText("Chat.SeaKing"));
            }

            if (!Main.IsItDay() && Main.GetMoonPhase() != MoonPhase.Empty && !NPC.downedMoonlord)
            {
                dialogue.Add(GetText("Chat.MoonLordHint"));
            }

            if (Main.bloodMoon)
            {
                if (Main.rand.NextBool())
                {
                    dialogue.Add(GetText("Chat.BloodMoon1", player.name));
                }
                else
                {
                    dialogue.Add(GetText("Chat.BloodMoon2"));
                }
            }

            if (player.ZoneGraveyard)
            {
                Main.NewText("graveyard active btw. delete this when done testing!!");
                if (Main.rand.NextBool())
                {
                    dialogue.Add(GetText("Chat.Graveyard1"));
                }
                else
                {
                    dialogue.Add(GetText("Chat.Graveyard2"));
                }
            }

            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = GetText("QuestButton");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Main.NewText("second button");
            Player player = Main.LocalPlayer;
            RPPlayer modPlayer = player.RPify();

            if (!modPlayer.QuestActive)
            {
                QuestDifficulties difficulty = QuestDifficulties.Easy;
                int roll = Main.rand.Next(10);
                if (roll > 8) { difficulty = QuestDifficulties.Heroic; }
                else if(roll > 5) { difficulty = QuestDifficulties.Moderate; }
                VillainQuests.GiveQuest(player, difficulty);
            }
            else
            {
                KeyValuePair<int, QuestDifficulties> result = VillainQuests.TryCompleteQuest(player);
                Main.NewText($"Progress left: {result.Key}");
                if (result.Key == 0)
                {
                    if (result.Value == QuestDifficulties.Easy)
                    {
                        Main.npcChatText = GetText($"Chat.EasyHandIn");
                    }
                    else if (result.Value == QuestDifficulties.Moderate)
                    {
                        Main.npcChatText = GetText($"Chat.ModerateHandIn");
                    }
                    else
                    {
                        Main.npcChatText = GetText($"Chat.HeroicHandIn");
                    }
                }
                else
                {
                    Main.npcChatText = GetText($"Chat.HandInFailure", result.Key);
                    Main.NewText(Main.LocalPlayer.RPify().QuestKey);
                }
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
}

