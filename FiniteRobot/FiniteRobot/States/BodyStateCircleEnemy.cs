using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;

namespace Drot.States
{
	public class BodyStateCircleEnemy : State
	{
		public override string OnUpdate()
		{
			string ret = null;

			// TODO This will just make it circle rightways(?), so if it is to the left of the enemy, it will circle away from the enemy...
			robot.SetTurnRight(robot.enemyData.Bearing + 90);
			robot.SetAhead(100 * robot.WallHitMovementDir);
			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.Preffered)
			{
				ret = StateManager.StateMovementSelect;
			}

			return ret;
		}
	}
}
