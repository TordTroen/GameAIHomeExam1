using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;

namespace Drot.States
{
	public class BodyStateFollow : State
	{
		//private const double prefferedEnemyDistance = 250.0;

		//private Vector2D velocity = new Vector2D(0, 0);
		//private double maxVelocity = 1;
		//private double maxSpeed = 8;
		//private double mass = 3;
		//private Random rnd = new Random();
		//private SeekBehavior seek;
		private ArrivalBehavior arrival;
		//private WanderBehavior wander;

		public override void OnEnter()
		{
			//velocity = new Vector2D(maxSpeed, maxSpeed);
			//seek = new SeekBehavior(robot);
			arrival = new ArrivalBehavior(robot, 150);
			//wander = new WanderBehavior(robot);
		}

		//private Vector2D ranPos = new Vector2D();
		//private long lastRndTime = -1000;

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			//if (robot.enemyData.HasPrefferedDistance() == DistanceLevel.Preffered)
			//{
			//	//ret = StateManager.StateCircleEnemy;
			//	ret = StateManager.StateOffensiveSelect;
			//}
			//else if (robot.enemyData.HasPrefferedDistance() == DistanceLevel.TooClose)
			//{
			//	// TODO Flee  
			//	ret = StateManager.StateFlee;
			//}
			//else
			//if (robot.Time - lastRndTime > 35)
			//{
			//	lastRndTime = robot.Time;
			//	ranPos = new Vector2D(rnd.Next(0, (int)robot.BattleFieldWidth), rnd.Next(0, (int)robot.BattleFieldHeight));
			//}
			//ranPos = new Vector2D(400, 400);

			arrival.Steer(robot.enemyData.Position);
			robot.Drawing.DrawBox(Color.Yellow, robot.enemyData.Position, 127);

			if (robot.enemyData.GetDistanceLevel() != DistanceLevel.TooFar || robot.IsStuck)
			{
				ret = StateManager.StateMovementSelect;
			}

			return ret;
		}
	}
}
