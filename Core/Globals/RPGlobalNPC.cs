using CalamityMod;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoleplayAddon.Content.Other;
using RoleplayAddon.Content.Projectiles.Rogue;
using RoleplayAddon.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Globals
{
	public class RPGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;     // Otherwise can't do this for individual enemies, which is necessary

		public int IceStacks = 0;

		public const int IceStacksCap = 30;
		private const int IceShatterDamage = 1000;
		private const float IceShatterRange = 128f;

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			RPGlobalNPC modNPC = npc.RPify();

			if (projectile.type == ModContent.ProjectileType<WhispersStarProj>() || modNPC.IceStacks == 30)
			{
				ShatterIceStacks(projectile, npc);
			}
		}

		private static void ShatterIceStacks(Projectile projectile, NPC npc)
		{
			// Find all frozen NPCs within the range of this one
			List<NPC> nearbyFrozen = [];
			foreach (NPC n in Main.npc)
			{
				if (n.Distance(npc.Center) < IceShatterRange && n.RPify().IceStacks > 0 && n.active)
				{
					nearbyFrozen.Add(n);
				}
			}
			if (npc.RPify().IceStacks > 0)
			{
				nearbyFrozen.Add(npc);
			}

			foreach (NPC n in nearbyFrozen)
			{
				// goose note: wanna make it happen in sequential frames rather than all at aonce,,, set things to happen on a timer? and give a bigger delay to each next npc in the list?

				int damage = (int)(IceShatterDamage * IceStackScaling(n.RPify().IceStacks));
				Projectile.NewProjectile(projectile.GetSource_FromThis(), n.Center, Vector2.Zero, ModContent.ProjectileType<WhispersIceShatterProj>(), damage, 0f, projectile.owner);

				SoundEngine.PlaySound(SoundID.Shatter);

				n.RPify().IceStacks = 0;
				ShatterIceStacks(projectile, n);
			}
		}

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D tex = Terraria.GameContent.TextureAssets.Item[ItemID.ThinIce].Value;
			Rectangle rectangle = new(0, 0, tex.Width, tex.Height);
			float rotation = npc.rotation;
			Vector2 origin = rectangle.Size() / 2f;
			float npcSmallerDimension = npc.width < npc.height ? npc.width : npc.height;
			float npcScale = npcSmallerDimension / tex.Width;
			float scale = 0.2f * npcScale * IceStackScaling(npc.RPify().IceStacks);

			Main.spriteBatch.Draw(tex, npc.Center - Main.screenPosition, rectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
		}

		/// <summary>
		/// Calculates the multiplier for Whispers of Snowfall's ice drawing scale and ice shatter damage
		/// </summary>
		/// <param name="stackCount">Number of ice stacks attached to the NPC</param>
		/// <returns>Multiplier to scale ice drawing and shatter damage</returns>
		private static float IceStackScaling(int stackCount) => 1 + (float)Math.Log(stackCount);	// maybe multiply the found exponent by like 1.5 to make multiple stacks a little more significanat? idk that's a balancing thing

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// 4 different Phantom Spirit enemies exist
			if (npc.type == ModContent.NPCType<PhantomSpirit>() || npc.type == ModContent.NPCType<PhantomSpiritS>() || npc.type == ModContent.NPCType<PhantomSpiritL>())
			{
				// NPC Loot is done in fractions (Putting a 1 makes it 1/1, AKA 100%, while 20 makes it 1/20, making it 5%)
				npcLoot.Add(ModContent.ItemType<AppleCrumble>(), 20);
			}

			// Higher chance for PhantomSpiritM because it is the "Angry" Phantom Spirit, connecting Apple Crumble to Sarenio more
			if (npc.type == ModContent.NPCType<PhantomSpiritM>())
			{
				npcLoot.Add(ModContent.ItemType<AppleCrumble>(), 10); // 10%
			}
		}
	}
}