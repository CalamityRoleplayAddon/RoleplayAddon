using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using RoleplayAddon.Content.Items.Other;

namespace RoleplayAddon.Content.Items.Weapons
{
    public class UmbralCutter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.ResearchUnlockCount = 1;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.Turquoise>();

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.knockBack = 4f;
            Item.damage = 110;
            Item.crit = 6;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.axe = 210;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig

        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<UmbralCutter>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddIngredient(ModContent.ItemType<Umbrafluid>(), 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}