using Microsoft.Xna.Framework;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using RoleplayAddon.Core.ModPlayers;

namespace RoleplayAddon.Content.Other
{
    public class Ringeffects : ModItem
    {
        public override string Texture => "CalamityMod/Items/Weapons/Magic/Biofusillade";

        public override void SetDefaults()
        {
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useTime = Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            RPPlayer modPlayer = player.RPify();
            modPlayer.QuestActive = false;
            modPlayer.QuestProgression = 0;
            modPlayer.QuestKey = "";


            // hijacking for villain testing bc lazy
            /* player.RPify().altRingEffects++;
            if (player.RPify().altRingEffects == 2)
            {
                player.RPify().altRingEffects = 0;
            }
            Main.NewText($"Ring effect mode: {player.RPify().altRingEffects}", Color.Violet); */
            return false;
        }
    }
}