using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;

namespace Drot.States
{
	/// <summary>
	/// State that uses the Arrival steeringbehavior to move closer to the enemy. Could also use the Pursuit or the Seek behavior.
	/// </summary>
	public class BodyStateFollow : State
	{
		private ArrivalBehavior arrival;

		public override void OnStart()
		{
			arrival = new ArrivalBehavior(robot, 150);
		}


		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			arrival.Steer(robot.enemyData.Position);

			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.TooFar || robot.IsStuck)
			{
				ret = StateManager.StateMovementSelect;
			}

			return ret;
		}
	}
}
