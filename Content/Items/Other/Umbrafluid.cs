using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace RoleplayAddon.Content.Items.Other
{
    public class Umbrafluid : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.ResearchUnlockCount = 5;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.Turquoise>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<Umbrafluid>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.TwistingNether>(), 2);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.PurifiedGel>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}