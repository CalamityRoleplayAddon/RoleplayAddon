using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RoleplayAddon.Core.ModPlayers
{
    public partial class RPPlayer : ModPlayer
    {
        public override void Initialize()
        {
            QuestActive = false;
            QuestProgression = 0;
            QuestKey = "";
            QuestRewardBladeSoulReceived = false;
            QuestRewardRazorReceived = false;
        }


        // DELETE LATER. FOR DEBUG 
        public override void OnEnterWorld()
        {
            Main.NewText($"{QuestActive}");
            Main.NewText($"{QuestProgression}");
            Main.NewText($"{QuestKey}");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["QuestActive"] = QuestActive;
            tag["QuestProgression"] = QuestProgression;
            tag["QuestKey"] = QuestKey;
            tag["QuestRewardBladeSoulReceived"] = QuestRewardBladeSoulReceived;
            tag["QuestRewardRazorReceived"] = QuestRewardRazorReceived;
        }

        public override void LoadData(TagCompound tag)
        {
            QuestActive = tag.GetBool("QuestActive");
            QuestProgression = tag.GetInt("QuestProgression");
            QuestKey = tag.GetString("QuestKey");
            QuestRewardBladeSoulReceived = tag.GetBool("QuestRewardBladeSoulReceived");
            QuestRewardRazorReceived = tag.GetBool("QuestRewardRazorReceived");
        }
    }
}