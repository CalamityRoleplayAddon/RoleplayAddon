using CalamityMod;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Accessories;
using RoleplayAddon.Content.Projectiles.Healing;
using RoleplayAddon.Content.Projectiles.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
    public partial class RPPlayer : ModPlayer
    {
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (Main.myPlayer != Player.whoAmI)
            {
                return;
            }

            // Ranged effects
			if (proj.DamageType == DamageClass.Ranged)
			{
				// Ashen Ring effects
                if (!Player.HasCooldown(Cooldowns.AshenRing.ID) && hit.Crit && (ilmeranRing || ashenFlower))
                {
                    // A few values are shared between Ilmeran Ring and Ashen Flower, so these are set here to avoid setting them twice for no reason
                    float effectReduction = ilmeranRingReduced ? 0.3f : 1f;
                    Color colour = ashenRing ? new Color(255, 191, 73) * effectReduction 
                        : Color.Lerp(Color.DeepSkyBlue, Color.LightBlue, Main.rand.NextFloat()) * effectReduction;
                    Vector2 contactPoint = proj.Center + (target.Center - proj.Center) / 2;
                    
                    // is the globalproj bool necessary now that there's a cooldown. probably not i think.........
                    if (ilmeranRing)
                    {
                        // If the Ashen Ring is triggering this effect, a few things should be different to match the different power
                        int damage = ashenRing ? AshenRing.Damage : IlmeranRing.Damage;
                        float radius = ashenRing ? AshenRing.Radius : IlmeranRing.Radius;
                        float scale = ashenRing ? 0.12f: 0.09f;
                        
                        if (ashenRing)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_BetsysWrathShot with { Volume = effectReduction }, contactPoint);
                            CustomPulse flash = new(
                                contactPoint, 
                                Vector2.Zero, 
                                colour, 
                                "CalamityMod/Particles/FlameExplosion", 
                                Vector2.One, 
                                Main.rand.NextFloat(), 
                                0f, 
                                scale, 
                                48);
                            GeneralParticleHandler.SpawnParticle(flash);
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.Item21 with { Volume = effectReduction }, contactPoint);
                            DirectionalPulseRing pulse = new(contactPoint, Vector2.Zero, colour, Vector2.One, 0, 0f, scale, 36);
                            GeneralParticleHandler.SpawnParticle(pulse);
                            for (int i = 0; i < 3; i++)
                            {
                                Vector2 velocity = Main.rand.NextVector2CircularEdge(12, 12);
                                WaterFlavoredParticle splash = new(
                                    contactPoint, 
                                    velocity, 
                                    true, 
                                    60, 
                                    2f, 
                                    colour);
                                GeneralParticleHandler.SpawnParticle(splash);
                            }
                        }

                        foreach (NPC n in Main.ActiveNPCs)
                        {
                            if (n.Distance(contactPoint) < radius)
                            {
                                Projectile burst = Projectile.NewProjectileDirect(
                                    Player.GetSource_FromThis(), 
                                    n.Center, 
                                    Vector2.Zero, 
                                    ModContent.ProjectileType<IlmeranRingproj>(), 
                                    damage, 
                                    3f, 
                                    Player.whoAmI, 
                                    ashenRing ? 1: 0);
                                burst.scale = scale;
                            }
                        }
                    }
                
                    if (ashenFlower)
                    {
                        int count = ashenRing ? Main.rand.Next(2, 5) : Main.rand.Next(1, 4);
                        // Ashen Ring makes the two effects interact a little, so the flame explosion (Ilmeran Ring) propels the petals (Ashen Flower) faster
                        // Also increases the petals' timeLeft, which is used in the calculations for their glow strength and homing radius
                        int velocityBounds = ashenRing ? 12 : 8;
                        for (int i = 0; i < count; i++)
                        {
                            Vector2 velocity = Main.rand.NextVector2CircularEdge(velocityBounds, velocityBounds);
                            Projectile.NewProjectile(
                                Player.GetSource_FromThis(), 
                                contactPoint, 
                                velocity, 
                                ModContent.ProjectileType<AshenFlowerProj>(), 
                                0, 
                                0, 
                                Player.whoAmI, 
                                ashenRing ? 1 : 0);
                        }
                    }
                    Player.AddCooldown(Cooldowns.AshenRing.ID, AshenRing.Cooldown);
                }
            }
        }
    }
}