using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RoleplayAddon.Content;
using System.Collections.Generic;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items;


namespace RoleplayAddon.Content
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

            player.GetModPlayer<EyeOfCamarunePlayer>().manaPotLock = true;

        }

        public class EyeOfCamarunePlayer : ModPlayer
        {
            public bool manaPotLock;

            public override void ResetEffects()
            {
                manaPotLock = false;
            }
        }


        public class EyeOfCamaruneMana : GlobalItem
        {
            public override void UpdateAccessory(Item item, Player player, bool hideVisual)
            {
                if (player.GetModPlayer<EyeOfCamarunePlayer>().manaPotLock == true)
                {
                    player.manaFlower = false;
                }
            }

            static HashSet<int> manaPots = [ItemID.LesserManaPotion, ItemID.ManaPotion, ItemID.GreaterManaPotion, ItemID.SuperManaPotion, ModContent.ItemType<SupremeManaPotion>(), ModContent.ItemType<HadalStew>(), ModContent.ItemType<AureusCell>(), ModContent.ItemType<GrapeBeer>(), ModContent.ItemType<WhiteWine>(), ModContent.ItemType<Margarita>()];

            public override bool CanUseItem(Item item, Player player)
            {
                if (manaPots.Contains(item.type) && player.GetModPlayer<EyeOfCamarunePlayer>().manaPotLock == true)
                {
                    return false;
                }
                return true;
            }
        }
    }
}