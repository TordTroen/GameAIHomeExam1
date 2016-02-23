using System;

namespace Drot
{
	public static class Utils
	{
		public static bool IsZero(this double val, double offByTolerance = 0.00001)
		{
			return Math.Abs(val) < offByTolerance;
		}
	}
}
