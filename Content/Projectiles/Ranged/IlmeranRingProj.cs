using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ModLoader;
using RoleplayAddon.Content.Accessories;

namespace RoleplayAddon.Content.Projectiles.Ranged
{
    public class IlmeranRingproj : ModProjectile
    {
        // ai[0] is 1 if the owner has the Ashen Ring equipped, 0 otherwise
        // ai[1] is the incident NPC's whoAmI, used to stop certain effects from applying to the NPC hit by the critting projectile
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

        private Player Player => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.ArmorPenetration = 20;
            Projectile.CritChance = 100;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.RPify().ashenRing)
            {
                target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
            }
            else
            {
                target.AddBuff(ModContent.BuffType<Eutrophication>(), 180);
            }

            if (target.whoAmI != (int)Projectile.ai[1])
            {
                bool ashen = Projectile.ai[0] == 1;
                Color colour = ashen ? AshenRing.Colour : Color.Lerp(Color.DeepSkyBlue, Color.LightBlue, Main.rand.NextFloat());
                colour *= Player.RPify().ilmeranRingReduced ? 0.5f : 1f;
                float scale = ashen ? 0.1f : 0.075f;

                CustomPulse pulse = new(
                Projectile.Center, 
                Vector2.Zero, 
                colour, 
                "CalamityMod/Particles/BloomRingThinLarge", 
                Vector2.One, 
                Main.rand.NextFloat(), 
                0, 
                scale,
                24);
                GeneralParticleHandler.SpawnParticle(pulse);

                if (ashen)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2CircularEdge(6 + i/2, 6 + i/2);
                        SparkParticle spark = new(Projectile.Center, velocity, false, 24, 0.5f, colour);
                        GeneralParticleHandler.SpawnParticle(spark);
                    }
                }
                else
                {
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
}