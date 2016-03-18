using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	public class BodySelectMovementState : State
	{
		/// <summary>
		/// The number of times we want to sample the states.
		/// </summary>
		public const int SampleIterations = 2;

		private string nextState = null;
		private readonly Random rnd = new Random();

		public override void OnEnter()
		{
			DistanceLevel distLvl = robot.enemyData.GetDistanceLevel();
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
				//var statesToUse = robot.gameData.GetBestStates();
				//int ranIndex = rnd.Next(0, statesToUse.Count);
				//nextState = statesToUse[ranIndex];
				nextState = robot.CurrentBodyMovementState;
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
