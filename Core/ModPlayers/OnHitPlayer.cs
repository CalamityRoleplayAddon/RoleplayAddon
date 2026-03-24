using System;
using CalamityMod;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Accessories;
using RoleplayAddon.Content.Projectiles.Healing;
using RoleplayAddon.Content.Projectiles.Ranged;
using RoleplayAddon.Core.Systems.Collections;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.ModPlayers
{
    public partial class RPPlayer : ModPlayer
    {
        // Need to be tracked for the Squashed! Villain quest
        public int RedBeetle = 0;
        public int CyanBeetle = 0;
        public int VioletBeetle = 0;
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
                if (!Player.HasCooldown(Content.Cooldowns.AshenRing.ID) && hit.Crit && (ilmeranRing || ashenFlower))
                {
                    // A few values are shared between Ilmeran Ring and Ashen Flower, so these are set here to avoid setting them twice for no reason
                    float effectReduction = ilmeranRingReduced ? 0.3f : 1f;
                    Color colour = ashenRing ? AshenRing.Colour * effectReduction 
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
                            DirectionalPulseRing pulse = new(contactPoint, Vector2.Zero, colour, Vector2.One, 0, 0f, 1f, 36);
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
                                    ashenRing ? 1: 0,
                                    target.whoAmI);
                                burst.scale = scale;
                            }
                        }
                    }
                
                    if (ashenFlower && Player.RPify().altRingEffects == 0)
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
                    Player.AddCooldown(Content.Cooldowns.AshenRing.ID, AshenRing.Cooldown);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life <= 0 && !target.friendly && target.type != NPCID.TargetDummy)
            {
                OnKillNPC(target, target.SpawnedFromStatue, target.CountsAsACritter);
            }
        }

        /// <summary>
        /// Method to be called when a player damaging an NPC sets its life to 0 or below.
        /// Created because there is no hook for having an NPC's death affect specifically its killer. I think...
        /// </summary>
        public void OnKillNPC(NPC npc, bool spawnedFromStatue, bool critter)
        {  
            #region Villain Quest Kills
            if (QuestActive && !spawnedFromStatue)
            {
                // Easy quests
                if (QuestKey == "Purification" && (RPNPCSets.IsCorruptionEnemy[npc.type] || RPNPCSets.IsCrimsonEnemy[npc.type]))
                {
                    QuestProgression++;
                }
                else if (QuestKey == "Gravedigger" && (NPCID.Sets.Zombies[npc.type] || NPCID.Sets.Skeletons[npc.type] || RPNPCSets.IsModdedUndead[npc.type]))
                {
                    QuestProgression++;
                }
                else if (QuestKey == "GelatinWorldTour" && RPNPCSets.IsSlime[npc.type])
                {
                    QuestProgression++;
                }
                else if (QuestKey == "CaveCrawlies" && (NPCID.GiantShelly == npc.type || NPCID.GiantShelly2 == npc.type || NPCID.Crawdad == npc.type 
                                                        || NPCID.Crawdad2 == npc.type || RPNPCSets.IsSalamander[npc.type]))
                {
                    QuestProgression++;
                }
                else if (QuestKey == "VengefulDivinity" && RPNPCSets.IsHallowEnemy[npc.type])
                {
                    QuestProgression++;
                }
                else if (QuestKey == "ItsYharHarsFault" && RPNPCSets.IsPlagueEnemy[npc.type])
                {
                    QuestProgression++;
                }
                else if (QuestKey == "AshesToAshes" && RPNPCSets.IsProfanedEnemy[npc.type])
                {
                    QuestProgression++;
                }
                // Moderate quests
                else if (QuestKey == "BloodMoonFishing" && RPNPCSets.IsBloodMoonFishingEnemy[npc.type])
                {
                    QuestProgression++;
                }
                else if(QuestKey == "Clickbait" && NPCID.Nymph == npc.type)
                {
                    QuestProgression++;
                }
                else if (QuestKey == "GoblinFriend" && NPCID.GoblinScout == npc.type)
                {
                    QuestProgression++;
                }
                else if (QuestKey == "Ragebait" && RPNPCSets.IsMimic[npc.type])
                {
                    QuestProgression++;
                }
                else if (QuestKey == "FalseIdol" && ModContent.NPCType<EarthElemental>() == npc.type)
                {
                    QuestProgression++;
                }
                // Heroic quests
                else if(QuestKey == "Squashed")
                {
                    if (NPCID.CochinealBeetle == npc.type) { RedBeetle++; }
                    if (NPCID.CyanBeetle == npc.type) { CyanBeetle++; }
                    if (NPCID.LacBeetle == npc.type) { VioletBeetle++; }
                    // The quest needs all three beetles to have been killed at least 3 times, so the lowest
                    // beetle count is the one we need to make sure is above 3
                    QuestProgression = Math.Min(RedBeetle, CyanBeetle);
                    QuestProgression = Math.Min(QuestProgression, VioletBeetle);
                }
                else if(QuestKey == "ShadowWizard" && NPCID.Tim == npc.type)
                {
                    QuestProgression++;
                }
                else if (QuestKey == "TheMightyKraken" && NPCID.BloodNautilus == npc.type)
                {
                    QuestProgression++;
                }
                else if (QuestKey == "MoneyGang" && NPCID.RuneWizard == npc.type)
                {
                    QuestProgression++;
                }
                else if (QuestKey == "RealDeal" && NPCID.SandElemental == npc.type)
                {
                    QuestProgression++;
                }
                // and so on!
            }
            #endregion
        }
    }
}