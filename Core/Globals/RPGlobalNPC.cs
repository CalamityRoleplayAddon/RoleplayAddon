using CalamityMod;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.TownNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoleplayAddon.Content.Accessories;
using RoleplayAddon.Content.Other;
using RoleplayAddon.Content.Projectiles.Rogue;
using RoleplayAddon.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.Globals
{
	public class RPGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;     // Otherwise can't do this for individual enemies, which is necessary

		// Whispers of Snowfall		
		public const int IceShatterDamage = 1000;
		public const float IceShatterRange = 256f;
		public const int IceStacksCap = 30;
		public int IceStacks = 0;



        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == ModContent.NPCType<SeaKing>())
			{
				shop.InsertBefore(ItemID.TruffleWorm, ModContent.ItemType<IlmeranRing>(), Condition.Hardmode);
			}
        }

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			RPGlobalNPC modNPC = npc.RPify();

			if (projectile.type == ModContent.ProjectileType<WhispersStarProj>() && modNPC.IceStacks > 0 || modNPC.IceStacks == 30)
			{
				SingleInstanceGlobalNPC.ShatterIceStacks(npc, projectile);
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
		public static float IceStackScaling(int stackCount) => (1 + (float)Math.Log(stackCount)) * 1.2f;

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