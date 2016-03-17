using PG4500_2016_Exam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.Helpers;
using Robocode.Util;

namespace Drot
{
	public class WanderBehavior : SteeringBehaviour
	{
		private double wanderDist = 100;
		private double wanderRadius = 500;
		private double wanderAngle = 0.0;
		private double wanderVariance = 10.0;
		private readonly Random rnd = new Random();

		public WanderBehavior(Trotor14 robot)
			: base(robot)
		{

		}

		public override void Steer(Vector2D targetPos)
		{
			// TODO See here --> http://www.slideshare.net/cartwright_samuel/steering-behaviours-wander-15308522

			//Vector2D desiredVelocity = Vector2D.Normalize(targetPos - robot.Position) * Trotor14.MaxSpeed;
			Vector2D desiredVelocity = new Vector2D();

			Vector2D wanderControl = new Vector2D(robot.VelocityVector);
			wanderControl.Normalize();
			wanderControl *= wanderDist;
			robot.Drawing.DrawCircle(System.Drawing.Color.Red, robot.Position + wanderControl, (float)wanderRadius * 2f, (float)wanderRadius * 2f);

			Vector2D displacement = new Vector2D(0, -1);// targetPos);
			//displacement.Normalize();
			displacement *= wanderRadius;

			//displacement = SetAngle(displacement, wanderAngle);
			displacement = SetAngle(displacement, wanderAngle);

			//wanderAngle += rnd.RandomRange(-wanderVariance, wanderVariance);
			wanderAngle += rnd.NextDouble() * wanderVariance - wanderVariance * .5;

			desiredVelocity = wanderControl + displacement;

			ApplySteering(desiredVelocity, new Vector2D(0, 0));

		}

		private Vector2D SetAngle(Vector2D v, double a)
		{
			double l = v.Length;
			return new Vector2D(Math.Cos(a) * l, Math.Sin(a) * l);
		}
	}
}
