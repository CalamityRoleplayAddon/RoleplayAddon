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
            
            Texture2D tex1 =  ModContent.Request<Texture2D>("RoleplayAddon/Core/DrawLayers/AshenFlowerGlow", AssetRequestMode.ImmediateLoad).Value;
            Rectangle rect1 = new(0, 0, tex1.Width, tex1.Height);
            Vector2 playerCenter = player.RotatedRelativePoint(player.Center);
            Vector2 offset = new(10 * -player.direction, -player.height/2 + 6);
            Vector2 drawPos = playerCenter - Main.screenPosition + offset;
            DrawData d1 = new(tex1, drawPos, rect1, Color.Gold * 0.5f, player.headRotation, rect1.Size() / 2, 0.05f, SpriteEffects.None);
            drawInfo.DrawDataCache.Add(d1);
        }
    }
}  */