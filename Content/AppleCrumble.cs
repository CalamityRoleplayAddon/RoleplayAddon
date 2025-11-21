using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod;

namespace RoleplayAddon.Content
{
    public class AppleCrumble : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;

            // Our sprite will have 3 sprites in it, so we need to register it as an animation
            // The first arguement dedicates how long an animation frame lasts is set to `int.MaxValue`
            // This is so we can keep it on the first frame essentially indefinitely
            // The second arguement determines how many frames we have
            // All food items have 3 frames, for the inventory, use and placed sprites
            // Food items have placed sprites due to Vanilla's "Plate" item
            // Future me will thank current me for this explanation. I just know it.
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            // Coloured particles that form whenever the food item is used, uses RGB values
            // Foods usually have 3 colours, but more or less can be used
            // Either FoodParticleColors or DrinkParticleColors can be used
            // FoodParticleColors fly outwards while DrinkParticleColors fall down and are slightly transparent
            ItemID.Sets.FoodParticleColors[Item.type] = [
                new Color(249, 230, 136),
                new Color(152, 93, 95),
                new Color(174, 192, 192)
            ];

            ItemID.Sets.IsFood[Type] = true; // Allows to be placed on Plate tiles
         }

        public override void SetDefaults()
        {
            // DefaultToFood lets us set food related defaults, like buff type and duration
            // The first two arguements are for the sprite's new height and width, in case the holdout sprite is a different size to the item sprite
            // The third and fourth arguements are for the tier of Well Fed and how long it lasts
            // The fifth arguement is a bool and controls if the item is eaten or drunk. Set it to `true` for a gulp sound and animation. This arguement is set to `false` if left empty
            // The sixth arguement can be added to change animation time
            // 162000  = 45 minutes
            Item.DefaultToFood(22, 22, BuffID.WellFed3, 162000);
            Item.value = Item.buyPrice(0, 3);
            Item.rare = ItemRarityID.Purple;
        }
    }

    public class AppleCrumbleDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // 4 different Phantom Spirit enemies exist
            if (npc.type == ModContent.NPCType<PhantomSpirit>() || npc.type == ModContent.NPCType<PhantomSpiritS>() || npc.type == ModContent.NPCType<PhantomSpiritL>())
            {
                // NPC Loot is done in fractions (Putting a 1 makes it 1/1, AKA 100%, while 20 makes it 1/20, making it 5%)
                npcLoot.Add(ModContent.ItemType<AppleCrumble>(), 20);
            }

            // Higher chance for PhantomSpiritM because it is the "Angry" Phantom Spirit, connecting Apple Crumble to Sarenio more
            if (npc.type == ModContent.NPCType<PhantomSpiritM>())
            { 
                npcLoot.Add(ModContent.ItemType<AppleCrumble>(), 10); // 10%
            }
        }
    }
}