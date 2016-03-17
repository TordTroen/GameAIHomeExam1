using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class BodyStateFlee : State
	{
		private FleeBehavior flee;

		public override void OnStart()
		{
			flee = new FleeBehavior(robot);
		}

		public override string OnUpdate()
		{
			string retState = base.OnUpdate();

			// TODO Flee
			flee.Steer(robot.enemyData.Position);

			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.TooClose || robot.IsStuck)
			{
				retState = StateManager.StateMovementSelect;
			}
			return retState;
		}
	}
}
