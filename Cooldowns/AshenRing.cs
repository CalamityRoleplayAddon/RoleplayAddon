using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace RoleplayAddon.Cooldowns
{
    public class AshenRing : CooldownHandler
    {
        public static new string ID => "AshenRing";
        public override bool ShouldDisplay => true;
        public override LocalizedText DisplayName => Language.GetText("Cooldowns.AshenRingCooldown"); 
        public override string Texture => "RoleplayAddon/Content/Accessories/AshenRing";
        public override string OutlineTexture => $"RoleplayAddon/Cooldowns/{ID}Overlay";
        public override string OverlayTexture => $"RoleplayAddon/Cooldowns/{ID}Overlay";
        public override Color OutlineColor => Color.DarkGoldenrod;
        public override Color CooldownStartColor => Color.Gold;
        public override Color CooldownEndColor => Color.Gold;
    }
}