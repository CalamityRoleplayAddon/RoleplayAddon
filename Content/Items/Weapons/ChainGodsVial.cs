using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using System.Numerics;
using Terraria.DataStructures;
using RoleplayAddon.Content.Items.Other;
using RoleplayAddon.Content.Projectiles;


namespace RoleplayAddon.Content.Items.Weapons
{
    public class ChainGodsVial : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.ResearchUnlockCount = 1;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.Turquoise>();

            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 14f; 


            Item.knockBack = 4f;
            Item.damage = 195;
            Item.crit = 8;
            Item.autoReuse = true;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<ChainGodsVialFlailProjectile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ChainGodsVial>(), 1);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.Chain, 10);
            recipe.AddIngredient(ItemID.LunarBar, 4);
            recipe.AddIngredient(ModContent.ItemType<Umbrafluid>(), 2);
            recipe.Register();
        }
        public override bool MeleePrefix() => true;
    
        public override bool AltFunctionUse(Player player) => true; 

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2)
            {
                //Placeholder
                //Katana Swing. 
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ChainGodsVialFlailProjectile>(), damage, knockback, Main.myPlayer);
                return false;
            }
            //Flail Throw.
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ChainGodsVialFlailProjectile>(), damage, knockback, Main.myPlayer);
            return false;
        }
    }
}