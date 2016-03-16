using System;
using System.Drawing;
using Drot.Helpers;
using Robocode;
using Robocode.Util;
using Drot;

namespace PG4500_2016_Exam1
{
    public class Trotor14 : AdvancedRobot
    {
		public const double Mass = 1;
		public const double MaxSpeed = 8;

		public EnemyData enemyData;
		public BulletData bulletData;
		private FiniteStateMachine bodyFSM;
		private FiniteStateMachine gunFSM;
		private FiniteStateMachine radarFSM;
		//private int radarDir = 1;
		//private bool hitEnemy = false;
		//private bool hitByEnemy = false;
	    public int ConsecutiveHits { get; set; }
	    public int ConsecutiveMisses { get; set; }

		/// <summary>
		/// An int that flips between 1 and -1 when the robot hits a wall
		/// </summary>
	    public int WallHitMovementDir { get; private set; }

	    public Drawing drawing;
		public Vector2D Position { get { return new Vector2D(X, Y); } }
		public Vector2D VelocityVector {
			get {
				if (Velocity.IsZero()) return new Vector2D();
				return new Vector2D(Velocity * Math.Cos(HeadingRadians), Velocity * Math.Sin(HeadingRadians));
			}
		}
	    public double prefferedEnemyDistance = 250;

		public override void Run()
		{
			InitializeBot();

			radarFSM.EnqueueState(StateManager.StateRadarSweep);

			//SetHeading(0);
			//SetTurnRadarRight(360);//double.PositiveInfinity);// * radarDir);
			//RadarSweep();

			while (true)
			{
				bodyFSM.Update();
				gunFSM.Update();
				radarFSM.Update();
				drawing.DrawLine(Color.White, Position, Position.ProjectForTime(HeadingRadians, Velocity, 10));

				//SetTurnRadarLeft(double.PositiveInfinity * radarDir);

				drawing.DrawString(Color.Black, "Hits: " + ConsecutiveHits, new Vector2D(0, -70));
				drawing.DrawCircle(Color.BlueViolet, enemyData.Position, (float)prefferedEnemyDistance*2, (float)prefferedEnemyDistance*2);
				drawing.DrawString(Color.Black, "Body  : " + bodyFSM.CurrentStateID, new Vector2D(0, -100));
				drawing.DrawString(Color.Black, "Gun   : " + gunFSM.CurrentStateID, new Vector2D(0, -130));
				drawing.DrawString(Color.Black, "Radar : " + radarFSM.CurrentStateID, new Vector2D(0, -160));
				//bool dodge = false;
				//if (!hitEnemy && enemyData.EnergyChanged && hitByEnemy)
				//{
				//	// Assume enemy bullet will hit where we are now
				//	bodyFSM.EnqueueState("Dodge");
				//	dodge = true;
				//}
				//drawing.DrawString(Color.Red, "Dodge: " + dodge, new Vector2D(0, -30));

				//hitByEnemy = false;
				//hitEnemy = false;
				//Scan();

				//if (!enemyData.ValidData() && enemyData.LastPosition == enemyData.Position)
				//{
				//	RadarSweep();
				//}

				if (ConsecutiveHits > ConsecutiveMisses)
				{
					enemyData.ValidDataTime = EnemyData.ValidDataTimeOnHits;
				}
				else
				{
					enemyData.ValidDataTime = EnemyData.ValidDataTimeOnMisses;
				}

				Execute();
			}
		}

	    public void SetHeading(double degrees)
	    {
		    //SetTurnRight((-Heading) + degrees);
	    }

		private void InitializeBot()
		{
			drawing = new Drawing(this);
			bodyFSM = new FiniteStateMachine(this);
			gunFSM = new FiniteStateMachine(this);
			radarFSM = new FiniteStateMachine(this);

			enemyData = new EnemyData(this);
			bulletData = new BulletData();
			WallHitMovementDir = 1;

			IsAdjustRadarForGunTurn = false;
			IsAdjustGunForRobotTurn = false;
			IsAdjustRadarForRobotTurn = false;
		}

	    private void RadarSweep()
	    {
		    SetTurnRadarRight(double.PositiveInfinity);
	    }

		// ROBOCODE EVENTS // 

		public override void OnScannedRobot(ScannedRobotEvent evnt)
		{
			//enemyData.SetData(evnt.Name, evnt.Distance, evnt.Bearing, Time);
			enemyData.SetData(evnt);
			gunFSM.EnqueueState(StateManager.StateAttack);
			bodyFSM.EnqueueState(StateManager.StatePursuit);
			radarFSM.EnqueueState(StateManager.StateScanLock);


			//double turn = HeadingRadians + evnt.BearingRadians - RadarHeadingRadians;
			//SetTurnRadarRightRadians(2 * Utils.NormalRelativeAngle(turn));
		}

	    public override void OnHitByBullet(HitByBulletEvent evnt)
	    {
		    bulletData.SetData(evnt.Heading, Time);
			//bodyFSM.EnqueueState("Dodge");
		}

	    public override void OnBulletHit(BulletHitEvent evnt)
	    {
		    ConsecutiveHits ++;
			ConsecutiveMisses = 0;
		    //hitEnemy = true;
	    }

	    public override void OnBulletMissed(BulletMissedEvent evnt)
	    {
		    ConsecutiveHits = 0;
			ConsecutiveMisses ++;
	    }

	    public override void OnHitWall(HitWallEvent evnt)
	    {
			WallHitMovementDir *= -1;
	    }

	    public override void OnRobotDeath(RobotDeathEvent evnt)
	    {
		    if (evnt.Name == enemyData.Name)
		    {
			    enemyData.Reset();
		    }
	    }

	    public override void OnDeath(DeathEvent evnt)
	    {
		    Out.WriteLine("I'll be back.");
	    }
    }
}
