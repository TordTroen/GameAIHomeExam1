using Robocode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class RadarStateScanLock : State
	{
		public override void OnEnter()
		{
			RadarLock();
		}

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			if (!robot.enemyData.ValidData())
			{
				ret = "RadarSweep";
			}
			else
			{
				RadarLock();
			}
			return ret;
		}

		private void RadarLock()
		{
			double turn = robot.HeadingRadians + robot.enemyData.BearingRadians - robot.RadarHeadingRadians;
			robot.SetTurnRadarRightRadians(2 * Utils.NormalRelativeAngle(turn));
		}
	}
}
