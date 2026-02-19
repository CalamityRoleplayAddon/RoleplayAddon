using CalamityMod.Particles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Projectiles.Rogue;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Immutable;
using Terraria.Audio;

namespace RoleplayAddon.Core.Globals
{
    public class SingleInstanceGlobalNPC : GlobalNPC
    {
        public static List<NPC> WhispersFrozenNPCs = [];

        private static readonly SoundStyle ShatterSound = new("CalamityMod/Sounds/NPCHit/CryogenPhaseTransitionCrack");

        public override bool PreAI(NPC npc)
        {
            // Make sure every NPC in the list has at least one ice stack
            foreach (NPC n in WhispersFrozenNPCs.ToImmutableList<NPC>())
            {
                RPGlobalNPC modNPC = n.RPify();
                if (modNPC.IceStacks < 1)
                {
                    WhispersFrozenNPCs.Remove(n);
                }
            }
            return true;
        }

        public static void ShatterIceStacks(NPC start, Projectile proj)
        {
            List<NPC> shatterList = [];
            List<NPC> propagatorList = [];
            shatterList.Add(start);
            propagatorList.Add(start);

            // Repeatedly looks for all iced NPCs within range of a set of propagators
            // If the iced NPCs is not currently in shatterList (the list of NPCs to have their ice shattered after the WHILE loop) it is added to both lists
            // When all iced NPCs have been checked against a propagator, it is removed from the propagator list
            // When the final propagator is removed with no new propagators having been added that loop, the chain has reached its maximum possible length
            while (propagatorList.Count > 0)
            {
                foreach (NPC propagator in propagatorList.ToImmutableList<NPC>())
                {
                    foreach (NPC receiver in WhispersFrozenNPCs)
                    {
                        if (propagator.Distance(receiver.Center) < RPGlobalNPC.IceShatterRange && !shatterList.Contains(receiver))
                        {
                            shatterList.Add(receiver);
                            propagatorList.Add(receiver);
                        }
                    }
                    propagatorList.Remove(propagator);
                }
            }

            for (int i = 0; i < shatterList.Count; i++)
            {   
                // Previous NPC (or player) location is used to determine the CrackParticle's orientation
                NPC n1 = shatterList[i];
                int i2 = i+1 >= shatterList.Count ? i : i + 1;
                NPC n2 = shatterList[i2];
                int damage = (int)(RPGlobalNPC.IceShatterDamage * RPGlobalNPC.IceStackScaling(n1.RPify().IceStacks));
                Projectile.NewProjectile(proj.GetSource_FromThis(), n1.Center, Vector2.Zero, ModContent.ProjectileType<WhispersIceShatterProj>(), damage, 0f, proj.owner, 
                    i == 0 ? 0 : shatterList[i-1].whoAmI);

                n1.RPify().IceStacks = 0;

                // Drawing a branching line between all enemies in the chain
                Vector2 firstPoint = n1.Center;
                Vector2 secondPoint = n2.Center;
                BloomLineVFX connection = new(firstPoint, secondPoint - firstPoint, 0.6f, Color.LightSkyBlue, 12, true);
                GeneralParticleHandler.SpawnParticle(connection);

            }

            // Sound is only played once regardless of the shatter count for all our ears' sakes
            // Sound location is the average location of all shattered NPCs
            Vector2 location = Vector2.Zero;
            foreach (NPC n in shatterList)
            {
                location.X += n.Center.X;
                location.Y += n.Center.Y;
            }
            location /= shatterList.Count;
            SoundEngine.PlaySound(ShatterSound, location);

            foreach (NPC n in shatterList)
            {
                WhispersFrozenNPCs.Remove(n);
            }
        }
    }
}