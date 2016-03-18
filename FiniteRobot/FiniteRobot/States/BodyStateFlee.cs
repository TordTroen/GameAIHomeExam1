using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	/// <summary>
	/// State that uses the Flee steeringbehavior to move away from the enemy.
	/// </summary>
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

			flee.Steer(robot.enemyData.Position);

			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.TooClose || robot.IsStuck)
			{
				retState = StateManager.StateMovementSelect;
			}
			return retState;
		}
	}
}
