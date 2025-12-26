using System;
using System.Data.Odbc;
using System.IO;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RoleplayAddon.Content.Projectiles
{
    public class HospitalBillsMinion : ModProjectile
    {
        int playerStill = 0;
        bool playerFlying = false;
        bool isAttacking = false;

        NPC CurrentTarget = null;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Type] = true; // Prioritises targets with right click targetting
            Main.projPet[Type] = true; // Says this is a pet or minion
            ProjectileID.Sets.MinionSacrificable[Type] = true; // Allows the minion to be summoned, and replaced if another summon item is used instead
            ProjectileID.Sets.CultistIsResistantTo[Type] = true; // Cultist resists **all** homing and minions.
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true; // Controls if it has contact damage active
            Projectile.minion = true; // Says its a minion. Important
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1; // Minion slot usage
            Projectile.penetrate = -1; // Infinite pierce, for obvious reasons
            Projectile.tileCollide = true;
            Projectile.aiStyle = AIType = -1;
            Projectile.netImportant = true;
            Projectile.width = 66;
            Projectile.height = 76;
        }

        public override bool MinionContactDamage() // Contact damage
        {
            return true;
        }

        public override bool? CanCutTiles() // Can break certain tiles like grass or Queen Bee larvae
        {
            return false;
        }

        // Determines if the minion can pass through platforms
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        // AI is split into multiple methods to make it easier to read
        public override void AI()
        {
            // 
            Player owner = Main.player[Projectile.owner];

            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<HospitalBillsBuff>());
            }

            if (owner.HasBuff(ModContent.BuffType<HospitalBillsBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            MoveTestBehaviour(owner, foundTarget, distanceFromTarget, targetCenter, out Vector2 IdlePosition, out float IdlePositionDistance, out Vector2 IdlePositionVector);
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            // This is around 40 tiles away from the minion
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            Projectile.friendly = foundTarget;
        }

        private void MoveTestBehaviour(Player owner, bool foundTarget, float distanceFromTarget, Vector2 targetCenter, out Vector2 IdlePosition, out float IdlePositionDistance, out Vector2 IdlePositionVector)
        {
            float speed = 2f;
            // Find owner
            IdlePosition = owner.Center;
            IdlePositionVector = IdlePosition - Projectile.Center;
            IdlePositionDistance = IdlePositionVector.Length();

            if (!foundTarget)
            {
                //
                // Move there
                // Minion's speed changes depending on how far it is from the owner
                speed = IdlePositionDistance * 0.03f;
                IdlePositionVector.Normalize();
                IdlePositionVector.X *= speed;
                Projectile.velocity.X = IdlePositionVector.X;
                

            }
            // Simple gravity
            float gravity = 0.1f;
            Projectile.velocity.Y += gravity * (1 + Math.Abs(Projectile.velocity.Y));
            /* Commented out for testing
            if (Projectile.velocity.Y <= 0)
            {
                Projectile.velocity.Y += gravity * 12;
            }
            if (Projectile.velocity.Y >= 0 && !(Projectile.velocity.Y >= 12))
            {
                Projectile.velocity.Y += gravity;
            }
            */
            // Jump
            // First, we check if the minion is lower than the player
            // Then, we check if the distance between the minion and the player is greater than 240 pixels (15 tiles)
            // Finally, we check if the minion's Y position was the same as the previous frame
            // If all return true, we give the minion a large vertical boost, making it jump
            if (Projectile.Center.Y > owner.Center.Y && (Projectile.Center.Y - owner.Center.Y) > 240f && Projectile.position.Y == Projectile.oldPosition.Y)
            {
                Projectile.velocity.Y = -20f;
            }
        }
    }
}