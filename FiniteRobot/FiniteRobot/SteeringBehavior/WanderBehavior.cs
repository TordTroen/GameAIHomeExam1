using PG4500_2016_Exam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;

namespace Drot
{
	public class WanderBehavior : SteeringBehaviour
	{
		public WanderBehavior(Trotor14 robot)
			: base(robot)
		{

		}

		public override BehaviorResult GetBehavior(Vector2D targetPos)
		{
			return base.GetBehavior(targetPos);
		}
	}
}
