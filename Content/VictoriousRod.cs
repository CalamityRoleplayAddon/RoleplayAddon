using System.Threading.Channels;
using CalamityMod.Items.Materials;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using rail;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content
{
    public class VictoriousRod : ModItem
    {

        public override void SetDefaults()
        {
            // Copy Chum Caster's defaults
            Item.CloneDefaults(ItemID.BloodFishingRod);
           
            Item.fishingPole = 50; // 50 Fishing Power
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<VictoriousBobber>(); // Beware: Bobber projectiles get replaced whenever the player has a Fishing Bobber accessory
            Item.rare = ItemRarityID.Cyan;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spreadAmount = 75f; // Spreads the bobbers out

            // Loop 3 times to shoot 3 bobbers
            // With 3 bobbers and 50 fishing power, this is slightly worse than the Rift Reeler
            // This is intentional, as the main purpose of this rod is to make a rod better than Chum Caster for Blood Orb farming
            for (int index = 0; index < 3; ++index)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

                // Generate new bobbers
                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
            return false;
        }

        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // The origin point of the fishing line
            lineOriginOffset = new Vector2(43, -30);

            // The colour of the fishing line
            lineColor = new Color(0, 0, 255);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BloodFishingRod, 1)
                .AddIngredient<AstralBar>(10)
                .AddIngredient(ItemID.FrostCore, 1)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<VictoriousRodCatch>().victorRod = true;
        }
    }


    public class VictoriousRodCatch : ModPlayer
    {
        public bool victorRod;

        public override void ResetEffects()
        {
            victorRod = false;
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (victorRod == true)
            {
                if (Main.bloodMoon)
                {
                    // Debug code for Mark (Villain) spawning
                    // Change to ! and both NPCID to Villain whenever he is implemented
                    // Setting itemDrop to none means items cannot be caught
                    if (NPC.AnyNPCs(NPCID.GoblinSummoner))
                    {
                        npcSpawn = NPCID.GoblinScout;
                        itemDrop = ItemID.None;
                    }

                    // If Villain is in the world, spawn the custom weighted Blood Moon fish enemies
                    npcSpawn = GetRandomBloodMoonEnemy();
                    itemDrop = ItemID.None;
                }
            }
        }
        private int GetRandomBloodMoonEnemy()
        {
            // Thank you, Crispy!
            // Weight system for Blood Moon enemy catches
            var enemyWeights = new (int item, float weight)[]
            {
              // (NPCID.ENEMY, DECIMAL PERCENTAGE)
              // You want your decimals to add up to 1, it causes issues if it doesnt because you aren't getting the full 100%
              // Thank you, Crispy!
              // Dreadnautilus is usually rarer (10%), and still will be
               (NPCID.BloodEelHead, 0.375f),
               (NPCID.GoblinShark, 0.375f),
               (NPCID.BloodNautilus, 0.25f)
            };

            float totalWeight = 0f;
            foreach (var enemy in enemyWeights)
                totalWeight += enemy.weight;

            float random = Main.rand.NextFloat() * totalWeight;

            foreach (var enemy in enemyWeights)
            {
                if (random < enemy.weight)
                    return enemy.item;
                random -= enemy.weight;
            }

            return enemyWeights[0].item;
        }

    }
}