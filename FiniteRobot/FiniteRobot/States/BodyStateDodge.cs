using System;
using System.Drawing;
using Drot.Helpers;
using Robocode.Util;

namespace Drot.States
{
	public class BodyStateDodge : State
	{
		private Random rnd = new Random();

		public override void OnEnter()
		{
			base.OnEnter();

			// TODO Figure out if we are too close to a wall to dodge (else just pick a random direction)
			// TODO Save if we were hit while dodging last time (if so; dodge the other way)??
			int direction = Utility.RandomSign(rnd);
			// TODO Maybe figure out if we are aprox. perpendicular to the enemy robot so we know 
			//		if we should turn so we can properly dodge

			// Distance to closest edge (normalized to range 0-1)
			// 

			//Vector2D pos = new Vector2D(robot.X, robot.Y);
			//Vector2D toMove = new Vector2D((pos.X / robot.BattleFieldWidth - 0.5) * 2,
			//							  (pos.Y / robot.BattleFieldHeight - 0.5) * 2);
			//double h = robot.Heading;
			//double rotDir = 0;
			//if (h > 45*1 && h < 45*3)
			//{
			//	rotDir = -1;
			//}
			//else if (h > 45 * 5 && h < 45 * 7)
			//{
			//	rotDir = 1;
			//}
			////robot.SetTurnRight(90*rotDir);

			//robot.drawing.DrawString(Color.Red, "ToMove: " + (36 + 36 * toMove.X), new Vector2D(0, -50));
			//robot.SetAhead(36 + 36 * toMove.X);
			//if (Math.Abs(toMove.X) < Math.Abs(toMove.Y))
			//{
			//	// use tomove.x

			//}
			//else
			//{
			//	// use tomove.y
			//}
			robot.SetAhead(100 * Utility.RandomSign(rnd));
		}

		public override string OnUpdate()
		{
			string ret = base.OnUpdate();

			//robot.SetAhead(30);
			if (robot.DistanceRemaining.IsZero())
			{
				ret = StateManager.StatePursuit;
			}

			return ret;
		}
	}
}
