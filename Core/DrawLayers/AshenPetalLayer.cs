/* using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RoleplayAddon.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RoleplayAddon.Core.DrawLayers
{
    public class AshenPetalLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.RPify().ashenFlower && drawInfo.drawPlayer.RPify().ashenFlowerGlowing && drawInfo.shadow == 0f;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            int hairWidth = player.hairFrame.Width;
            
            Texture2D tex =  ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", AssetRequestMode.ImmediateLoad).Value;
            Vector2 playerCenter = player.RotatedRelativePoint(player.Center);
            Vector2 offset = new(hairWidth/2, -player.height/2);
            Vector2 drawPos = playerCenter - Main.screenPosition + offset;
            Rectangle rect = new(0, 0, tex.Width, tex.Height);
            drawInfo.DrawDataCache.Add(new DrawData(tex, drawPos, rect, Color.Gold, 0f, rect.Size() / 2, 0.2f, SpriteEffects.None));
        }
    }
} */