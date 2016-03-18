using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drot.States
{
	/// <summary>
	/// State that selects a state for the robot body to use when inside the preffered range of the enemy.
	/// </summary>
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
				nextState = robot.CurrentBodyMovementState;
			}
		}

		public override string OnUpdate()
		{
			return nextState;
		}
	}
}
