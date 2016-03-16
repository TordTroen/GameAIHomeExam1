using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;
using PG4500_2016_Exam1;

namespace Drot
{
	public class SeekBehavior : SteeringBehaviour
	{
		public SeekBehavior(Trotor14 robot)
			: base(robot)
		{
			
		}

		public override BehaviorResult GetBehavior(Vector2D targetPos)
		{
			// Store robot values
			Vector2D velocity = robot.VelocityVector;
			Vector2D curPos = robot.Position;

			Vector2D desiredVelocity = Vector2D.Normalize(targetPos - curPos) * Trotor14.VelocityMax;
			BehaviorResult behavior = ApplySteering(desiredVelocity, velocity, curPos);
			return behavior;
		}
	}
}
