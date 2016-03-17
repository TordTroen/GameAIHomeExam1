using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class BodySelectMovementState : State
	{
		private string nextState = null;
		private readonly Random rnd = new Random();

		public override void OnEnter()
		{
			// TODO Enqueue random offensive state
			DistanceLevel distLvl = robot.enemyData.GetDistanceLevel();
			//if (distLvl == DistanceLevel.Preffered)
			//{
			//	double r = rnd.NextDouble();
			//	if (r < 0.5)
			//	{
			//		nextState = StateManager.StateCircleEnemy;
			//	}
			//	else if (r >= 0.5)
			//	{
			//		nextState = StateManager.StateCircleWander;
			//	}
			//}
			if (distLvl == DistanceLevel.TooFar && !robot.IsStuck)
			{
				nextState = StateManager.StateFollow;
			}
			else if (distLvl == DistanceLevel.TooClose && !robot.IsStuck)
			{
				nextState = StateManager.StateFlee;
			}
			else
			{
				double r = rnd.NextDouble();
				if (r < 0.5)
				{
					nextState = StateManager.StateCircleEnemy;
				}
				else if (r >= 0.5)
				{
					nextState = StateManager.StateCircleWander;
				}
			}
		}

		public override string OnUpdate()
		{
			return nextState;
		}

		//public string GetRandomOffensiveBodyState()
		//{
		//	int rNum = rnd.Next(3);
		//	switch (rNum)
		//	{
		//		case 0:
		//			return StateWander;
		//		case 1:
		//			return StateCircleEnemy;
		//		case 2:
		//			return StateCircleWander;
		//	}
		//	return null;
		//}
	}
}
