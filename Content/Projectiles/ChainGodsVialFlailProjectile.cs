using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace RoleplayAddon.Content.Projectiles
{
    public class ChainGodsVialFlailProjectile : ModProjectile
    {
        bool hasHitEnemy = false;
        NPC projTarget;
        int timer = 0;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
            Projectile.width = 22; 
            Projectile.height = 22; 
            Projectile.friendly = true; //Friendly projectiles damage enemies rather than you

            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true; 
            Projectile.localNPCHitCooldown = 6; 
            Projectile.scale = 0.8f;

            Projectile.aiStyle = ProjAIStyleID.Harpoon;
            AIType = ProjectileID.ChainKnife;
        }
        public override bool PreAI()
        {
            if(hasHitEnemy == true)
            {
                timer++;
                Projectile.Center = projTarget.Center;
                if(timer > 165)
                {
                    projTarget = null;
                    hasHitEnemy = false;
                    timer = 0;
                }
                return false;
            }
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hasHitEnemy = true;
            projTarget = target;
        }
    }
}