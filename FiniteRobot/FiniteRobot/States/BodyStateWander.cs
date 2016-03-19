using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class BodyStateWander : State
	{
		private WanderBehavior wander;

		public override void OnStart()
		{
			wander = new WanderBehavior(robot);
		}

		public override string OnUpdate()
		{
			wander.Steer(robot.enemyData.Position);
			return null;
		}
	}
}
