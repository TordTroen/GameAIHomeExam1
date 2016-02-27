using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;

namespace Drot.States
{
	public class StateCircleEnemy : State
	{
		private double screenMargin = 100;

		public override string OnUpdate()
		{
			string ret = null;

			if (robot.enemyData.Distance > robot.prefferedEnemyDistance * 1.2)
			{
				ret = "Pursuit";
			}
			else
			{
				// TODO This will just make it circle rightways(?), so if it is to the left of the enemy, it will circle away from the enemy...
				robot.SetTurnRight(robot.enemyData.Bearing + 90);
				robot.SetAhead(100 * robot.WallHitMovementDir);
			}

			return ret;
		}
	}
}
