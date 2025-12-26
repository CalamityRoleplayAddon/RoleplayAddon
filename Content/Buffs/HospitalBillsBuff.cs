using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Projectiles;

namespace RoleplayAddon.Content.Buffs
{
    public class HospitalBillsBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // Get rid of the buff when leaving the world
            Main.buffNoTimeDisplay[Type] = true; // Don't display remaining time
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Refresh buff time indefinitely if the minions exist, get rid of the buff if they don't
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HospitalBillsMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}