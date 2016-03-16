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

		public override void Steer(Vector2D targetPos)
		{
			Vector2D desiredVelocity = Vector2D.Normalize(targetPos - robot.Position) * Trotor14.MaxSpeed;
			ApplySteering(desiredVelocity, robot.VelocityVector);
		}
	}
}
