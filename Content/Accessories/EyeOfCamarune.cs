using CalamityMod.Items;
using RoleplayAddon.Core.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RoleplayAddon.Content.Accessories
{
    public class EyeOfCamarune : ModItem
    {

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Tupid high amounts of Magic damage
            player.GetDamage<MagicDamageClass>() += 0.65f;

            // Should lock Max Mana, regardless of buffs, to 50%
            if (player.statMana >= (int)(player.statManaMax2 * 0.5f))
            {
                player.statMana = (int)(player.statManaMax2 * 0.5f);
            }

            player.GetModPlayer<RPPlayer>().manaPotLock = true;

        }
    }
}