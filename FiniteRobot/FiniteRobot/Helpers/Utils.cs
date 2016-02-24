using System;
using Drot.Helpers;

using Robocode.Util;

namespace Drot
{
	public static class Utils
	{
		public static bool IsZero(this double val, double offByTolerance = 0.00001)
		{
			return Math.Abs(val) < offByTolerance;
		}

		public static double RadToDeg(double rad)
		{
			//return rad * (180 / Math.PI);
			return Robocode.Util.Utils.ToDegrees(rad);
		}

		public static double DegToRad(double deg)
		{
			//return deg * (Math.PI / 180);
			return Robocode.Util.Utils.ToRadians(deg);
		}

		public static Vector2D ClampToScreen(this Vector2D v)
		{
			return v;
		}
	}
}
