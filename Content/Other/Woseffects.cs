using Microsoft.Xna.Framework;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Other
{
    public class Woseffects : ModItem
    {
        public override string Texture => "CalamityMod/Items/Weapons/Magic/WintersFury";

        public override void SetDefaults()
        {
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useTime = Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.RPify().reduceEffects = !player.RPify().reduceEffects;
            Main.NewText($"Reduced WoS effects: {player.RPify().reduceEffects}", Color.Violet);
            return false;
        }
    }
}