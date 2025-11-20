using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content
{
    public class VictoriousBobber : ModProjectile
    {
        public override void SetDefaults()
        {
            // Copy Chum Caster's defaults
            Projectile.CloneDefaults(ProjectileID.BobberBloody);
        }
    }
}