using System;
using Drot.Helpers;

namespace Drot
{
	public static class Utility
	{
		private static Random rnd = new Random();

		public static bool IsZero(this double val, double offByTolerance = 0.00001)
		{
			return Math.Abs(val) < offByTolerance;
		}

		public static double RadToDeg(double rad)
		{
			return rad * (180 / Math.PI);
		}

		public static double DegToRad(double deg)
		{
			return deg * (Math.PI / 180);
		}

		public static int RandomSign()
		{
			return rnd.Next(0, 2) * 2 - 1;
		}
	}
}
