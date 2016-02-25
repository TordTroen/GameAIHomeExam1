using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class StatePursuit : State
	{
		private const double prefferedEnemyDistance = 250.0;

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			// TODO improve this
			double diff = robot.enemyData.Distance - prefferedEnemyDistance - 36; // 36 to account for the robot size
			robot.SetAhead(1000 * robot.moveDir);
			robot.SetTurnRight(robot.enemyData.Bearing + 90);
			

			return ret;
		}
	}
}
