using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles.Ranged
{
    public class IlmeranRingproj : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.ArmorPenetration = 20;
            Projectile.CritChance = 100;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[Projectile.owner].RPify().ashenRing)
            {
                target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
            }
            else
            {
                target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 180);
            }
        }

        public override void OnKill(int timeLeft)
        {
            // Was considering putting this in OnHitNPC but that would cancel the visual effect for the Ashen Ring if the NPC dies from the original projectile
            bool ashen = Projectile.ai[0] == 1;
            Color colour = ashen ? Color.Gold : Color.Lerp(Color.DeepSkyBlue, Color.LightBlue, Main.rand.NextFloat());

            if (ashen)
            {
                CustomPulse pulse = new(
                    Projectile.Center, 
                    Vector2.Zero, 
                    colour, 
                    "CalamityMod/Particles/BloomRing", 
                    Vector2.One, 
                    Main.rand.NextFloat(), 
                    0.1f, 
                    1f, 
                    24, 
                    true);
                GeneralParticleHandler.SpawnParticle(pulse);
                for (int i = 0; i < 5; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(6 + i/2, 6 + i/2);
                    SparkParticle spark = new(Projectile.Center, velocity, false, 24, 0.5f, colour);
                    GeneralParticleHandler.SpawnParticle(spark);
                }
            }
            else
            {
                CustomPulse pulse = new(
                    Projectile.Center, 
                    Vector2.Zero, 
                    colour, 
                    "CalamityMod/Particles/BloomRing", 
                    Vector2.One, 
                    Main.rand.NextFloat(), 
                    0.1f, 
                    0.75f, 
                    24, 
                    true);
                GeneralParticleHandler.SpawnParticle(pulse);
                for (int i = 0; i < 5; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(6 + i/2, 6 + i/2);
                    WaterFlavoredParticle droplet = new(Projectile.Center, velocity, true, 36, 0.5f, colour);
                    GeneralParticleHandler.SpawnParticle(droplet);
                }
            }
        }
    }
}