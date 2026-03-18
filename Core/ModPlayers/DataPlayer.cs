using RoleplayAddon.Content.NPCs.TownNPCs;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RoleplayAddon.Core.ModPlayers
{
    public partial class RPPlayer : ModPlayer
    {
        public override void Initialize()
        {
            QuestActive = false;
            QuestDifficulty = 0;    // Maybe delete this one
            QuestProgression = 0;
            QuestKey = "";
            QuestRewardBladeSoulReceived = false;
            QuestRewardRazorReceived = false;
        } 

        public override void SaveData(TagCompound tag)
        {
            tag["QuestActive"] = QuestActive;
            tag["QuestDifficulty"] = (int)QuestDifficulty;
            tag["QuestProgression"] = QuestProgression;
            tag["QuestKey"] = QuestKey;
            tag["QuestRewardBladeSoulReceived"] = QuestRewardBladeSoulReceived;
            tag["QuestRewardRazorReceived"] = QuestRewardRazorReceived;
        }

        public override void LoadData(TagCompound tag)
        {
            QuestActive = tag.GetBool("QuestActive");
            QuestDifficulty = (QuestDifficulties)tag.GetInt("QuestDifficulty");
            QuestProgression = tag.GetInt("QuestProgression");
            QuestKey = tag.GetString("QuestKey");
            QuestRewardBladeSoulReceived = tag.GetBool("QuestRewardBladeSoulReceived");
            QuestRewardRazorReceived = tag.GetBool("QuestRewardRazorReceived");
        }
    }
}