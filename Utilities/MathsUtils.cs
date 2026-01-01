using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace RoleplayAddon.Utilities
{
	public static partial class RPUtils
	{
		#region Easing functions
		
		/// <summary>
		/// Clamped ^2 Ease-out function
		/// </summary>
		/// <param name="x">Progress of the transition, between 0 (not started) and 1 (completed)</param>
		/// <returns>Float to multiply the transitioning value by</returns>
		public static float EaseOutQuad(float x) => 1 - (float)Math.Pow(1 - Math.Clamp(x, 0, 1),2);

		/// <summary>
		/// ^2 Ease-in-out function
		/// </summary>
		/// <param name="x">Progress of the transition</param>
		/// <returns>Float to multiply the transitioning value by</returns>
		public static float EaseInOutQuad(float x) =>  x < 0.5 ? 2 * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2;

		/// <summary>
		/// Clamped ^4 Ease-out function
		/// </summary>
		/// <param name="x">Progress of the transition, between 0 (not started) and 1 (completed)</param>
		/// <returns>Float to multiply the transitioning value by</returns>
		public static float EaseOutQuart(float x) => 1 - (float)Math.Pow(1 - Math.Clamp(x, 0, 1), 4);

		/// <summary>
		/// ^4 Ease-in-out function
		/// </summary>
		/// <param name="x">Progress of the transition</param>
		/// <returns>Float to multiply the transitioning value by</returns>
		public static float EaseInOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - (float)Math.Pow(-2 * x + 2, 4) / 2;

		#endregion

		/// <summary>
		/// Moves an entity around another entity according to the passed angle (radians) and radius (world coordinates)
		/// </summary>
		/// <param name="angle">The angle in radians the orbiting entity will be moved according to. This should be incremented every tick.</param>
		/// <param name="radius">How far out the orbiting entity is in world coordinates</param>
		/// <param name="pivot">The entity being orbited</param>
		/// <param name="target">The entity in orbit</param>
		/// <returns>Vector2 containing the world coordinates of the projectile's new position in its orbit</returns>
		public static Vector2 MoveAlongCircle(double angle, double radius, Entity pivot, Entity target)
		{
			return new Vector2((float)(pivot.Center.X - Math.Cos(angle) * radius - target.width / 2), (float)(pivot.Center.Y - Math.Sin(angle) * radius - target.height / 2));
		}
	}
}