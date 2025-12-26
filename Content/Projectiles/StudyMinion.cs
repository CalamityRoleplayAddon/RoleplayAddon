using System;
using Microsoft.Xna.Framework;
using RoleplayAddon.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Content.Projectiles {

    public class StudyMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 30;
            Projectile.aiStyle = 67;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 18;
            Projectile.decidesManualFallThrough = true;
        }


public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            bool flag = Projectile.type == ModContent.ProjectileType<StudyMinion>();
            bool flag2 = Projectile.type == 758;
            bool flag3 = Projectile.type == 833 || Projectile.type == 834 || Projectile.type == 835;
            bool flag4 = Projectile.type == 834 || Projectile.type == 835;
            bool flag5 = Projectile.type == 951;
            int num = 450;
            float num2 = 500f;
            float num3 = 300f;
            int num4 = 15;
            /*
            if (flag5)
            {
                if (player.dead)
                {
                    player.flinxMinion = false;
                }
                if (player.flinxMinion)
                {
                    Projectile.timeLeft = 2;
                }
                num = 800;
            }
            */
            if (flag)
            {
                if (player.dead || !player.active)
                {
                    player.ClearBuff(ModContent.BuffType<HospitalBillsBuff>());
                }

                if (player.HasBuff(ModContent.BuffType<HospitalBillsBuff>()))
                {
                    Projectile.timeLeft = 2;
                }
                num = 800;
            }
            /*
            if (flag3)
            {
                if (player.dead)
                {
                    player.stormTiger = false;
                }
                if (player.stormTiger)
                {
                    Projectile.timeLeft = 2;
                }
                num = 800;
                if (Projectile.ai[0] != 4f)
                {
                    if (Projectile.velocity != Vector2.Zero && Main.rand.Next(18) == 0)
                    {
                        Dust obj = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 269)];
                        obj.fadeIn = 0.5f;
                        obj.scale = 0.3f;
                        obj.noLight = true;
                        obj.velocity += Projectile.velocity * 0.005f;
                    }
                    if (Projectile.type == 833)
                    {
                        Lighting.AddLight(Projectile.Center, Vector3.One * 0.5f);
                    }
                    if (Projectile.type == 834)
                    {
                        Lighting.AddLight(Projectile.Center, Vector3.One * 0.8f);
                    }
                    if (Projectile.type == 835)
                    {
                        Lighting.AddLight(Projectile.Center, Color.Lerp(Main.OurFavoriteColor, Color.White, 0.8f).ToVector3() * 1f);
                    }
                }
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.localAI[0] <= 0f)
                    {
                        int num5 = Projectile.type switch
                        {
                            834 => 300, 
                            835 => 240, 
                            _ => 360, 
                        };
                        if (Projectile.damage != 0)
                        {
                            bool flag6 = Projectile.AI_067_TigerSpecialAttack();
                            Projectile.localAI[0] = (flag6 ? num5 : 10);
                        }
                    }
                    else
                    {
                        Projectile.localAI[0] -= 1f;
                    }
                }
            }
            */
            /*
            if (flag2)
            {
                if (player.dead)
                {
                    player.vampireFrog = false;
                }
                if (player.vampireFrog)
                {
                    Projectile.timeLeft = 2;
                }
                num = 800;
            }
            */
            /*
            if (Projectile.type == 500)
            {
                num2 = 200f;
                if (player.dead)
                {
                    player.crimsonHeart = false;
                }
                if (player.crimsonHeart)
                {
                    Projectile.timeLeft = 2;
                }
            }
            */
            /*
            if (Projectile.type == 653)
            {
                num2 = 300f;
                if (player.dead)
                {
                    player.companionCube = false;
                }
                if (player.companionCube)
                {
                    Projectile.timeLeft = 2;
                }
            }
            */
            /*
            if (Projectile.type == 1018)
            {
                num2 = 200f;
                if (player.dead)
                {
                    player.petFlagDirtiestBlock = false;
                }
                if (player.petFlagDirtiestBlock)
                {
                    Projectile.timeLeft = 2;
                }
            }
            */
            /*
            if (flag3 && Projectile.ai[0] == 4f)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.frame = 9;
                if (flag4)
                {
                    Projectile.frame = 11;
                }
                Projectile.ai[1] -= 1f;
                if (!(Projectile.ai[1] <= 0f))
                {
                    return;
                }
                Projectile.ai[0] = 0f;
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            */
            Vector2 vector = player.Center;
    /*
	if (flag5)
	{
		vector.X -= (45 + player.width / 2) * player.direction;
		vector.X -= Projectile.minionPos * 30 * player.direction;
	}
	*/
    if (flag)
            {
                vector.X -= (15 + player.width / 2) * player.direction;
                vector.X -= Projectile.minionPos * 20 * player.direction;
            }
            /*
            else if (flag3)
            {
                vector.X -= (15 + player.width / 2) * player.direction;
                vector.X -= Projectile.minionPos * 40 * player.direction;
            }
            else if (flag2)
            {
                vector.X -= (35 + player.width / 2) * player.direction;
                vector.X -= Projectile.minionPos * 40 * player.direction;
            }
            else if (Projectile.type == 500)
            {
                vector.X -= (15 + player.width / 2) * player.direction;
                vector.X -= 40 * player.direction;
            }
            else if (Projectile.type == 1018)
            {
                vector.X = player.Center.X;
            }
            else if (Projectile.type == 653)
            {
                vector.X = player.Center.X;
            }
            if (Projectile.type == 500)
            {
                Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
                int num6 = 6;
                if (Projectile.frame == 0 || Projectile.frame == 2)
                {
                    num6 = 12;
                }
                if (++Projectile.frameCounter >= num6)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 0;
                    }
                }
                Projectile.rotation += Projectile.velocity.X / 20f;
                Vector2 vector2 = (-Vector2.UnitY).RotatedBy(Projectile.rotation).RotatedBy((float)Projectile.direction * 0.2f);
                int num7 = Dust.NewDust(Projectile.Center + vector2 * 10f - new Vector2(4f), 0, 0, 5, vector2.X, vector2.Y, 0, Color.Transparent);
                Main.dust[num7].scale = 1f;
                Main.dust[num7].velocity = vector2.RotatedByRandom(0.7853981852531433) * 3.5f;
                Main.dust[num7].noGravity = true;
                Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Main.player[Projectile.owner].cLight, Main.player[Projectile.owner]);
            }
            if (Projectile.type == 1018)
            {
                Projectile.rotation += Projectile.velocity.X / 20f;
            }
            if (Projectile.type == 653)
            {
                Projectile.rotation += Projectile.velocity.X / 20f;
                bool flag7 = Projectile.owner >= 0 && Projectile.owner < 255;
                if (flag7)
                {
                    Projectile._CompanionCubeScreamCooldown[Projectile.owner] -= 1f;
                    if (Projectile._CompanionCubeScreamCooldown[Projectile.owner] < 0f)
                    {
                        Projectile._CompanionCubeScreamCooldown[Projectile.owner] = 0f;
                    }
                }
                Tile tileSafely = Framing.GetTileSafely(Projectile.Center);
                if (tileSafely.liquid > 0 && tileSafely.lava())
                {
                    Projectile.localAI[0] += 1f;
                }
                else
                {
                    Projectile.localAI[0] -= 1f;
                }
                Projectile.localAI[0] = MathHelper.Clamp(Projectile.localAI[0], 0f, 20f);
                if (Projectile.localAI[0] >= 20f)
                {
                    if (flag7 && Projectile._CompanionCubeScreamCooldown[Projectile.owner] == 0f)
                    {
                        Projectile._CompanionCubeScreamCooldown[Projectile.owner] = 3600f;
                        SoundEngine.PlaySound((Main.rand.Next(10) == 0) ? SoundID.NPCDeath61 : SoundID.NPCDeath59, Projectile.position);
                    }
                    Projectile.Kill();
                }
                if (flag7 && Projectile.owner == Main.myPlayer && Main.netMode != 2)
                {
                    Vector3 vector3 = Lighting.GetColor((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16).ToVector3();
                    Vector3 vector4 = Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16).ToVector3();
                    if (vector3.Length() < 0.15f && vector4.Length() < 0.15f)
                    {
                        Projectile.localAI[1] += 1f;
                    }
                    else if (Projectile.localAI[1] > 0f)
                    {
                        Projectile.localAI[1] -= 1f;
                    }
                    Projectile.localAI[1] = MathHelper.Clamp(Projectile.localAI[1], -3600f, 120f);
                    if (Projectile.localAI[1] > (float)Main.rand.Next(30, 120) && !player.immune && player.velocity == Vector2.Zero)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            SoundEngine.PlaySound(in SoundID.Item16, Projectile.Center);
                            Projectile.localAI[1] = -600f;
                        }
                        else
                        {
                            SoundEngine.PlaySound(in SoundID.Item1, Projectile.Center);
                            player.Hurt(PlayerDeathReason.ByOther(6), 3, 0);
                            player.immune = false;
                            player.immuneTime = 0;
                            Projectile.localAI[1] = -300 + Main.rand.Next(30) * -10;
                        }
                    }
                }
            } */
            bool flag8 = true;
            /*
            if (Projectile.type == 500 || Projectile.type == 653 || Projectile.type == 1018)
            {
                flag8 = false;
            }
            */
            Projectile.shouldFallThrough = player.position.Y + (float)player.height - 12f > Projectile.position.Y + (float)Projectile.height;
            Projectile.friendly = false;
            int num8 = 0;
            int num9 = 15;
            int attackTarget = -1;
            bool flag9 = true;
            bool flag10 = Projectile.ai[0] == 5f;
            /*
            if (flag5)
            {
                flag9 = false;
                Projectile.friendly = true;
            }
            if (flag2)
            {
                Projectile.friendly = true;
                num9 = 20;
                num8 = 60;
            }
            if (flag3)
            {
                flag9 = false;
                Projectile.friendly = true;
                Projectile.originalDamage = player.highestStormTigerGemOriginalDamage;
            }
            */
            bool flag11 = Projectile.ai[0] == 0f;
            /*
            if (flag3 && flag10)
            {
                flag11 = true;
            }
            */
            /* AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            if (flag11 && flag8)
            {
                Projectile.Minion_FindTargetInRange(num, ref attackTarget, skipIfCannotHitWithOwnBody: true, AI_067_CustomEliminationCheck_Pirates);
            }
            */
            float playerDistance;
            float myDistance;
            bool closerIsMe;
            /*
            if (flag3 && flag10)
            {
                if (attackTarget >= 0)
                {
                    float maxDistance = num;
                    NPC nPC = Main.npc[attackTarget];
                    vector = nPC.Center;
                    if (!Projectile.IsInRangeOfMeOrMyOwner(nPC, maxDistance, out playerDistance, out myDistance, out closerIsMe))
                    {
                        Projectile.ai[0] = 0f;
                        Projectile.ai[1] = 0f;
                        return;
                    }
                    Point point = nPC.Top.ToTileCoordinates();
                    int num10 = 0;
                    int num11 = point.Y;
                    while (num10 < num4)
                    {
                        Tile tile = Main.tile[point.X, num11];
                        if (tile == null || tile.HasTile)
                        {
                            break;
                        }
                        num10++;
                        num11++;
                    }
                    int num12 = num4 / 2;
                    if (num10 < num12)
                    {
                        Projectile.ai[0] = 0f;
                        Projectile.ai[1] = 0f;
                        return;
                    }
                    if (Projectile.Hitbox.Intersects(nPC.Hitbox) && Projectile.velocity.Y >= 0f)
                    {
                        Projectile.velocity.Y = -8f;
                        Projectile.velocity.X = Projectile.direction * 10;
                    }
                    float num13 = 20f;
                    float maxAmountAllowedToMove = 4f;
                    float num14 = 40f;
                    float num15 = 40f;
                    Vector2 top = nPC.Top;
                    float num16 = (float)Math.Cos(Main.timeForVisualEffects / (double)num14 * 6.2831854820251465);
                    if (num16 > 0f)
                    {
                        num16 *= -1f;
                    }
                    num16 *= num15;
                    top.Y += num16;
                    Vector2 vector5 = top - Projectile.Center;
                    if (vector5.Length() > num13)
                    {
                        vector5 = vector5.SafeNormalize(Vector2.Zero) * num13;
                    }
                    Projectile.velocity = Projectile.velocity.MoveTowards(vector5, maxAmountAllowedToMove);
                    Projectile.frame = 8;
                    if (flag4)
                    {
                        Projectile.frame = 10;
                    }
                    Projectile.rotation += 0.6f * (float)Projectile.spriteDirection;
                }
                else
                {
                    Projectile.ai[0] = 0f;
                    Projectile.ai[1] = 0f;
                }
                return;
            }
            */
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                float num17 = 0.2f;
                float num18 = 10f;
                int num19 = 200;
                if (num18 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num18 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                }
                Vector2 vector6 = player.Center - Projectile.Center;
                float num20 = vector6.Length();
                if (num20 > 2000f)
                {
                    Projectile.position = player.Center - new Vector2(Projectile.width, Projectile.height) / 2f;
                }
                if (num20 < (float)num19 && player.velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                    if (Projectile.velocity.Y < -6f)
                    {
                        Projectile.velocity.Y = -6f;
                    }
                }
                if (!(num20 < 60f))
                {
                    vector6.Normalize();
                    vector6 *= num18;
                    if (Projectile.velocity.X < vector6.X)
                    {
                        Projectile.velocity.X += num17;
                        if (Projectile.velocity.X < 0f)
                        {
                            Projectile.velocity.X += num17 * 1.5f;
                        }
                    }
                    if (Projectile.velocity.X > vector6.X)
                    {
                        Projectile.velocity.X -= num17;
                        if (Projectile.velocity.X > 0f)
                        {
                            Projectile.velocity.X -= num17 * 1.5f;
                        }
                    }
                    if (Projectile.velocity.Y < vector6.Y)
                    {
                        Projectile.velocity.Y += num17;
                        if (Projectile.velocity.Y < 0f)
                        {
                            Projectile.velocity.Y += num17 * 1.5f;
                        }
                    }
                    if (Projectile.velocity.Y > vector6.Y)
                    {
                        Projectile.velocity.Y -= num17;
                        if (Projectile.velocity.Y > 0f)
                        {
                            Projectile.velocity.Y -= num17 * 1.5f;
                        }
                    }
                }
                if (Projectile.velocity.X != 0f)
                {
                    Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
                }
                if (flag5)
                {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter > 3)
                    {
                        Projectile.frame++;
                        Projectile.frameCounter = 0;
                    }
                    if (Projectile.frame < 2 || Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 2;
                    }
                    Projectile.rotation = Projectile.rotation.AngleTowards(Projectile.rotation + 0.25f * (float)Projectile.spriteDirection, 0.25f);
                }
                if (flag)
                {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter > 3)
                    {
                        Projectile.frame++;
                        Projectile.frameCounter = 0;
                    }
                    if ((Projectile.frame < 10) | (Projectile.frame > 13))
                    {
                        Projectile.frame = 10;
                    }
                    Projectile.rotation = Projectile.velocity.X * 0.1f;
                }
                if (flag2)
                {
                    int num21 = 3;
                    if (++Projectile.frameCounter >= num21 * 4)
                    {
                        Projectile.frameCounter = 0;
                    }
                    Projectile.frame = 14 + Projectile.frameCounter / num21;
                    Projectile.rotation = Projectile.velocity.X * 0.15f;
                }
                if (flag3)
                {
                    Projectile.frame = 8;
                    if (flag4)
                    {
                        Projectile.frame = 10;
                    }
                    Projectile.rotation += 0.6f * (float)Projectile.spriteDirection;
                }
                if (Projectile.type == 1018 && Main.LocalPlayer.miscCounter % 3 == 0)
                {
                    int num22 = 2;
                    Dust obj2 = Main.dust[Dust.NewDust(Projectile.position + new Vector2(-num22, -num22), 16 + num22 * 2, 16 + num22 * 2, 0, 0f, 0f, 0, default(Color), 0.8f)];
                    obj2.velocity = -Projectile.velocity * 0.25f;
                    obj2.velocity = obj2.velocity.RotatedByRandom(0.2617993950843811);
                }
            }
            if (Projectile.ai[0] == 2f && Projectile.ai[1] < 0f)
            {
                Projectile.friendly = false;
                Projectile.ai[1] += 1f;
                if (num9 >= 0)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                    return;
                }
            }
            else if (Projectile.ai[0] == 2f)
            {
                Projectile.spriteDirection = Projectile.direction;
                Projectile.rotation = 0f;
                if (flag)
                {
                    Projectile.friendly = true;
                    Projectile.frame = 4 + (int)((float)num9 - Projectile.ai[1]) / (num9 / 3);
                    if (Projectile.velocity.Y != 0f)
                    {
                        Projectile.frame += 3;
                    }
                }
                if (flag2)
                {
                    float num23 = ((float)num9 - Projectile.ai[1]) / (float)num9;
                    if ((double)num23 > 0.25 && (double)num23 < 0.75)
                    {
                        Projectile.friendly = true;
                    }
                    int num24 = (int)(num23 * 5f);
                    if (num24 > 2)
                    {
                        num24 = 4 - num24;
                    }
                    if (Projectile.velocity.Y != 0f)
                    {
                        Projectile.frame = 21 + num24;
                    }
                    else
                    {
                        Projectile.frame = 18 + num24;
                    }
                    if (Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X *= 0.8f;
                    }
                }
                Projectile.velocity.Y += 0.4f;
                if (Projectile.velocity.Y > 10f)
                {
                    Projectile.velocity.Y = 10f;
                }
                Projectile.ai[1] -= 1f;
                if (Projectile.ai[1] <= 0f)
                {
                    if (num8 <= 0)
                    {
                        Projectile.ai[1] = 0f;
                        Projectile.ai[0] = 0f;
                        Projectile.netUpdate = true;
                        return;
                    }
                    Projectile.ai[1] = -num8;
                }
            }
            if (attackTarget >= 0)
            {
                float maxDistance2 = num;
                float num25 = 20f;
                if (flag2)
                {
                    num25 = 50f;
                }
                NPC nPC2 = Main.npc[attackTarget];
                Vector2 center = nPC2.Center;
                vector = center;
                if (Projectile.IsInRangeOfMeOrMyOwner(nPC2, maxDistance2, out myDistance, out playerDistance, out closerIsMe))
                {
                    Projectile.shouldFallThrough = nPC2.Center.Y > Projectile.Bottom.Y;
                    bool flag12 = Projectile.velocity.Y == 0f;
                    if (Projectile.wet && Projectile.velocity.Y > 0f && !Projectile.shouldFallThrough)
                    {
                        flag12 = true;
                    }
                    if (center.Y < Projectile.Center.Y - 30f && flag12)
                    {
                        float num26 = (center.Y - Projectile.Center.Y) * -1f;
                        float num27 = 0.4f;
                        float num28 = (float)Math.Sqrt(num26 * 2f * num27);
                        if (num28 > 26f)
                        {
                            num28 = 26f;
                        }
                        Projectile.velocity.Y = 0f - num28;
                    }
                    if (flag9 && Vector2.Distance(Projectile.Center, vector) < num25)
                    {
                        if (Projectile.velocity.Length() > 10f)
                        {
                            Projectile.velocity /= Projectile.velocity.Length() / 10f;
                        }
                        Projectile.ai[0] = 2f;
                        Projectile.ai[1] = num9;
                        Projectile.netUpdate = true;
                        Projectile.direction = ((center.X - Projectile.Center.X > 0f) ? 1 : (-1));
                    }
                    
                    if (flag3)
                    {
                        Point point2 = nPC2.Top.ToTileCoordinates();
                        int num29 = 0;
                        int num30 = point2.Y;
                        while (num29 < num4)
                        {
                            Tile tile2 = Main.tile[point2.X, num30];
                            if (tile2 == null || tile2.HasTile)
                            {
                                break;
                            }
                            num29++;
                            num30++;
                        }
                        if (num29 >= num4)
                        {
                            Projectile.ai[0] = 5f;
                            Projectile.ai[1] = 0f;
                            Projectile.netUpdate = true;
                            return;
                        }
                        if (Projectile.Hitbox.Intersects(nPC2.Hitbox) && Projectile.velocity.Y >= 0f)
                        {
                            Projectile.velocity.Y = -4f;
                            Projectile.velocity.X = Projectile.direction * 10;
                        }
                    }
                }
                if (flag2)
                {
                    int num31 = 1;
                    if (center.X - Projectile.Center.X < 0f)
                    {
                        num31 = -1;
                    }
                    vector.X += 20 * -num31;
                }
            }
            if (Projectile.ai[0] == 0f && attackTarget < 0)
            {
                if (Main.player[Projectile.owner].rocketDelay2 > 0)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.netUpdate = true;
                }
                Vector2 vector7 = player.Center - Projectile.Center;
                if (vector7.Length() > 2000f)
                {
                    Projectile.position = player.Center - new Vector2(Projectile.width, Projectile.height) / 2f;
                }
                else if (vector7.Length() > num2 || Math.Abs(vector7.Y) > num3)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.netUpdate = true;
                    if (Projectile.velocity.Y > 0f && vector7.Y < 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                    if (Projectile.velocity.Y < 0f && vector7.Y > 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                }
            }
            if (Projectile.ai[0] == 0f)
            {
                if (attackTarget < 0)
                {
                    if (Projectile.Distance(player.Center) > 60f && Projectile.Distance(vector) > 60f && Math.Sign(vector.X - player.Center.X) != Math.Sign(Projectile.Center.X - player.Center.X))
                    {
                        vector = player.Center;
                    }
                    Rectangle r = Utils.CenteredRectangle(vector, Projectile.Size);
                    for (int i = 0; i < 20; i++)
                    {
                        if (Collision.SolidCollision(r.TopLeft(), r.Width, r.Height))
                        {
                            break;
                        }
                        r.Y += 16;
                        vector.Y += 16f;
                    }
                    Vector2 vector8 = Collision.TileCollision(player.Center - Projectile.Size / 2f, vector - player.Center, Projectile.width, Projectile.height);
                    vector = player.Center - Projectile.Size / 2f + vector8;
                    if (Projectile.Distance(vector) < 32f)
                    {
                        float num32 = player.Center.Distance(vector);
                        if (player.Center.Distance(Projectile.Center) < num32)
                        {
                            vector = Projectile.Center;
                        }
                    }
                    Vector2 vector9 = player.Center - vector;
                    if (vector9.Length() > num2 || Math.Abs(vector9.Y) > num3)
                    {
                        Rectangle r2 = Utils.CenteredRectangle(player.Center, Projectile.Size);
                        Vector2 vector10 = vector - player.Center;
                        Vector2 vector11 = r2.TopLeft();
                        for (float num33 = 0f; num33 < 1f; num33 += 0.05f)
                        {
                            Vector2 vector12 = r2.TopLeft() + vector10 * num33;
                            if (Collision.SolidCollision(r2.TopLeft() + vector10 * num33, r.Width, r.Height))
                            {
                                break;
                            }
                            vector11 = vector12;
                        }
                        vector = vector11 + Projectile.Size / 2f;
                    }
                }
                Projectile.tileCollide = true;
                float num34 = 0.5f;
                float num35 = 4f;
                float num36 = 4f;
                float num37 = 0.1f;
                if (flag5 && attackTarget != -1)
                {
                    num34 = 0.65f;
                    num35 = 5.5f;
                    num36 = 5.5f;
                }
                if (flag && attackTarget != -1)
                {
                    num34 = 1f;
                    num35 = 8f;
                    num36 = 8f;
                }
                if (flag2 && attackTarget != -1)
                {
                    num34 = 0.7f;
                    num35 = 6f;
                    num36 = 6f;
                }
                if (flag3 && attackTarget != -1)
                {
                    num34 = 1f;
                    num35 = 8f;
                    num36 = 8f;
                }
                if (num36 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num36 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                    num34 = 0.7f;
                }
                if (Projectile.type == 653 || Projectile.type == 1018)
                {
                    float num38 = player.velocity.Length();
                    if (num38 < 0.1f)
                    {
                        num38 = 0f;
                    }
                    if (num38 != 0f && num38 < num36)
                    {
                        num36 = num38;
                    }
                }
                int num39 = 0;
                bool flag13 = false;
                float num40 = vector.X - Projectile.Center.X;
                Vector2 vector13 = vector - Projectile.Center;
                if (Projectile.type == 1018 && Math.Abs(num40) < 50f)
                {
                    Projectile.rotation = Projectile.rotation.AngleTowards(0f, 0.2f);
                    Projectile.velocity.X *= 0.9f;
                    if ((double)Math.Abs(Projectile.velocity.X) < 0.1)
                    {
                        Projectile.velocity.X = 0f;
                    }
                }
                else if (Projectile.type == 653 && Math.Abs(num40) < 150f)
                {
                    Projectile.rotation = Projectile.rotation.AngleTowards(0f, 0.2f);
                    Projectile.velocity.X *= 0.9f;
                    if ((double)Math.Abs(Projectile.velocity.X) < 0.1)
                    {
                        Projectile.velocity.X = 0f;
                    }
                }
                else if (Math.Abs(num40) > 5f)
                {
                    if (num40 < 0f)
                    {
                        num39 = -1;
                        if (Projectile.velocity.X > 0f - num35)
                        {
                            Projectile.velocity.X -= num34;
                        }
                        else
                        {
                            Projectile.velocity.X -= num37;
                        }
                    }
                    else
                    {
                        num39 = 1;
                        if (Projectile.velocity.X < num35)
                        {
                            Projectile.velocity.X += num34;
                        }
                        else
                        {
                            Projectile.velocity.X += num37;
                        }
                    }
                    bool flag14 = true;
                    if (flag)
                    {
                        flag14 = false;
                    }
                    if (Projectile.type == 653)
                    {
                        flag14 = false;
                    }
                    if (Projectile.type == 1018)
                    {
                        flag14 = false;
                    }
                    if (flag2 && attackTarget == -1)
                    {
                        flag14 = false;
                    }
                    if (flag3)
                    {
                        flag14 = vector13.Y < -80f;
                    }
                    if (flag5)
                    {
                        flag14 = attackTarget > -1 && Main.npc[attackTarget].Hitbox.Intersects(Projectile.Hitbox);
                    }
                    if (flag14)
                    {
                        flag13 = true;
                    }
                }
                else
                {
                    Projectile.velocity.X *= 0.9f;
                    if (Math.Abs(Projectile.velocity.X) < num34 * 2f)
                    {
                        Projectile.velocity.X = 0f;
                    }
                }
                bool flag15 = Math.Abs(vector13.X) >= 64f || (vector13.Y <= -48f && Math.Abs(vector13.X) >= 8f);
                if (num39 != 0 && flag15)
                {
                    int num41 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
                    int num42 = (int)Projectile.position.Y / 16;
                    num41 += num39;
                    num41 += (int)Projectile.velocity.X;
                    for (int j = num42; j < num42 + Projectile.height / 16 + 1; j++)
                    {
                        if (WorldGen.SolidTile(num41, j))
                        {
                            flag13 = true;
                        }
                    }
                }
                if (Projectile.type == 500 && Projectile.velocity.X != 0f)
                {
                    flag13 = true;
                }
                if (Projectile.type == 653 && Math.Abs(Projectile.velocity.X) > 3f)
                {
                    flag13 = true;
                }
                if (Projectile.type == 1018 && Math.Abs(Projectile.velocity.X) > 3f)
                {
                    flag13 = true;
                }
                Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
                float num43 = Utils.GetLerpValue(0f, 100f, vector13.Y, clamped: true) * Utils.GetLerpValue(-2f, -6f, Projectile.velocity.Y, clamped: true);
                if (Projectile.velocity.Y == 0f)
                {
                    if (flag13)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int num44 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
                            if (k == 0)
                            {
                                num44 = (int)Projectile.position.X / 16;
                            }
                            if (k == 2)
                            {
                                num44 = (int)(Projectile.position.X + (float)Projectile.width) / 16;
                            }
                            int num45 = (int)(Projectile.position.Y + (float)Projectile.height) / 16;
                            if (!WorldGen.SolidTile(num44, num45) && !Main.tile[num44, num45].IsHalfBlock && Main.tile[num44, num45].Slope <= 0 && (!TileID.Sets.Platforms[Main.tile[num44, num45].TileType] || !Main.tile[num44, num45].HasTile || Main.tile[num44, num45].IsActuated))
                            {
                                continue;
                            }
                            try
                            {
                                num44 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
                                num45 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
                                num44 += num39;
                                num44 += (int)Projectile.velocity.X;
                                if (!WorldGen.SolidTile(num44, num45 - 1) && !WorldGen.SolidTile(num44, num45 - 2))
                                {
                                    Projectile.velocity.Y = -5.1f;
                                }
                                else if (!WorldGen.SolidTile(num44, num45 - 2))
                                {
                                    Projectile.velocity.Y = -7.1f;
                                }
                                else if (WorldGen.SolidTile(num44, num45 - 5))
                                {
                                    Projectile.velocity.Y = -11.1f;
                                }
                                else if (WorldGen.SolidTile(num44, num45 - 4))
                                {
                                    Projectile.velocity.Y = -10.1f;
                                }
                                else
                                {
                                    Projectile.velocity.Y = -9.1f;
                                }
                            }
                            catch
                            {
                                Projectile.velocity.Y = -9.1f;
                            }
                        }
                        if (vector.Y - Projectile.Center.Y < -48f)
                        {
                            float num46 = vector.Y - Projectile.Center.Y;
                            num46 *= -1f;
                            if (num46 < 60f)
                            {
                                Projectile.velocity.Y = -6f;
                            }
                            else if (num46 < 80f)
                            {
                                Projectile.velocity.Y = -7f;
                            }
                            else if (num46 < 100f)
                            {
                                Projectile.velocity.Y = -8f;
                            }
                            else if (num46 < 120f)
                            {
                                Projectile.velocity.Y = -9f;
                            }
                            else if (num46 < 140f)
                            {
                                Projectile.velocity.Y = -10f;
                            }
                            else if (num46 < 160f)
                            {
                                Projectile.velocity.Y = -11f;
                            }
                            else if (num46 < 190f)
                            {
                                Projectile.velocity.Y = -12f;
                            }
                            else if (num46 < 210f)
                            {
                                Projectile.velocity.Y = -13f;
                            }
                            else if (num46 < 270f)
                            {
                                Projectile.velocity.Y = -14f;
                            }
                            else if (num46 < 310f)
                            {
                                Projectile.velocity.Y = -15f;
                            }
                            else
                            {
                                Projectile.velocity.Y = -16f;
                            }
                        }
                        if (Projectile.wet && num43 == 0f)
                        {
                            Projectile.velocity.Y *= 2f;
                        }
                    }
                    if (Projectile.type == 1018 && Projectile.localAI[2] == 0f)
                    {
                        Projectile.localAI[2] = 1f;
                        for (int l = 0; l < 6; l++)
                        {
                            Dust obj4 = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity, 16, 16, 0, 0f, 0f, 0, default(Color), 0.8f)];
                            obj4.velocity.X = Projectile.velocity.X * 0.25f;
                            obj4.velocity.Y = -2f + Math.Abs(Projectile.velocity.Y) * 0.25f;
                            obj4.velocity = obj4.velocity.RotatedByRandom(0.2617993950843811);
                        }
                    }
                }
                else if (Projectile.type == 1018)
                {
                    Projectile.localAI[2] = 0f;
                }
                if (Projectile.velocity.X > num36)
                {
                    Projectile.velocity.X = num36;
                }
                if (Projectile.velocity.X < 0f - num36)
                {
                    Projectile.velocity.X = 0f - num36;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.direction = -1;
                }
                if (Projectile.velocity.X > 0f)
                {
                    Projectile.direction = 1;
                }
                if (Projectile.velocity.X == 0f)
                {
                    Projectile.direction = ((player.Center.X > Projectile.Center.X) ? 1 : (-1));
                }
                if (Projectile.velocity.X > num34 && num39 == 1)
                {
                    Projectile.direction = 1;
                }
                if (Projectile.velocity.X < 0f - num34 && num39 == -1)
                {
                    Projectile.direction = -1;
                }
                Projectile.spriteDirection = Projectile.direction;
                if (flag5)
                {
                    if (Projectile.velocity.Y == 0f)
                    {
                        Projectile.rotation = Projectile.rotation.AngleTowards(0f, 0.3f);
                        if (Projectile.velocity.X == 0f)
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                        else if (Math.Abs(Projectile.velocity.X) >= 0.5f)
                        {
                            Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
                            Projectile.frameCounter++;
                            if (Projectile.frameCounter > 10)
                            {
                                Projectile.frame++;
                                Projectile.frameCounter = 0;
                            }
                            if (Projectile.frame < 2 || Projectile.frame >= Main.projFrames[Projectile.type])
                            {
                                Projectile.frame = 2;
                            }
                        }
                        else
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                    }
                    else if (Projectile.velocity.Y != 0f)
                    {
                        Projectile.rotation = Math.Min(4f, Projectile.velocity.Y) * -0.1f;
                        if (Projectile.spriteDirection == -1)
                        {
                            Projectile.rotation -= (float)Math.PI * 2f;
                        }
                        Projectile.frameCounter = 0;
                        Projectile.frame = 1;
                    }
                }
                if (flag)
                {
                    Projectile.rotation = 0f;
                    if (Projectile.velocity.Y == 0f)
                    {
                        if (Projectile.velocity.X == 0f)
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                        else if (Math.Abs(Projectile.velocity.X) >= 0.5f)
                        {
                            Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
                            Projectile.frameCounter++;
                            if (Projectile.frameCounter > 10)
                            {
                                Projectile.frame++;
                                Projectile.frameCounter = 0;
                            }
                            if (Projectile.frame >= 4)
                            {
                                Projectile.frame = 0;
                            }
                        }
                        else
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                    }
                    else if (Projectile.velocity.Y != 0f)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame = 14;
                    }
                }
                if (flag2)
                {
                    Projectile.rotation = 0f;
                    if (Projectile.velocity.Y == 0f)
                    {
                        if (Projectile.velocity.X == 0f)
                        {
                            int num47 = 4;
                            if (++Projectile.frameCounter >= 7 * num47 && Main.rand.Next(50) == 0)
                            {
                                Projectile.frameCounter = 0;
                            }
                            int num48 = Projectile.frameCounter / num47;
                            if (num48 >= 4)
                            {
                                num48 = 6 - num48;
                            }
                            if (num48 < 0)
                            {
                                num48 = 0;
                            }
                            Projectile.frame = 1 + num48;
                        }
                        else if (Math.Abs(Projectile.velocity.X) >= 0.5f)
                        {
                            Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
                            Projectile.frameCounter++;
                            int num49 = 15;
                            int num50 = 8;
                            if (Projectile.frameCounter >= num50 * num49)
                            {
                                Projectile.frameCounter = 0;
                            }
                            int num51 = Projectile.frameCounter / num49;
                            Projectile.frame = num51 + 5;
                        }
                        else
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                    }
                    else if (Projectile.velocity.Y != 0f)
                    {
                        if (Projectile.velocity.Y < 0f)
                        {
                            if (Projectile.frame > 9 || Projectile.frame < 5)
                            {
                                Projectile.frame = 5;
                                Projectile.frameCounter = 0;
                            }
                            if (++Projectile.frameCounter >= 1 && Projectile.frame < 9)
                            {
                                Projectile.frame++;
                                Projectile.frameCounter = 0;
                            }
                        }
                        else
                        {
                            if (Projectile.frame > 13 || Projectile.frame < 9)
                            {
                                Projectile.frame = 9;
                                Projectile.frameCounter = 0;
                            }
                            if (++Projectile.frameCounter >= 2 && Projectile.frame < 11)
                            {
                                Projectile.frame++;
                                Projectile.frameCounter = 0;
                            }
                        }
                    }
                }
                if (flag3)
                {
                    int num52 = 8;
                    if (flag4)
                    {
                        num52 = 10;
                    }
                    Projectile.rotation = 0f;
                    if (Projectile.velocity.Y == 0f)
                    {
                        if (Projectile.velocity.X == 0f)
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                        else if (Math.Abs(Projectile.velocity.X) >= 0.5f)
                        {
                            Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
                            Projectile.frameCounter++;
                            if (Projectile.frameCounter > 10)
                            {
                                Projectile.frame++;
                                Projectile.frameCounter = 0;
                            }
                            if (Projectile.frame >= num52 || Projectile.frame < 2)
                            {
                                Projectile.frame = 2;
                            }
                        }
                        else
                        {
                            Projectile.frame = 0;
                            Projectile.frameCounter = 0;
                        }
                    }
                    else if (Projectile.velocity.Y != 0f)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame = 1;
                        if (flag4)
                        {
                            Projectile.frame = 9;
                        }
                    }
                }
                Projectile.velocity.Y += 0.4f + num43 * 1f;
                if (Projectile.velocity.Y > 10f)
                {
                    Projectile.velocity.Y = 10f;
                }
            }
            if (!flag)
            {
                return;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.velocity.X == 0f)
            {
                Projectile.localAI[0] += 1f;
            }
            if (Projectile.localAI[0] >= (float)Main.rand.Next(900, 1200))
            {
                Projectile.localAI[0] = 0f;
                for (int m = 0; m < 6; m++)
                {
                    int num53 = Dust.NewDust(Projectile.Center + Vector2.UnitX * -Projectile.direction * 8f - Vector2.One * 5f + Vector2.UnitY * 8f, 3, 6, 216, -Projectile.direction, 1f);
                    Main.dust[num53].velocity /= 2f;
                    Main.dust[num53].scale = 0.8f;
                }


            }
        }
    }
}