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
		public const double PrefferedEnemyDistance = 250;

		/// <summary>
		/// Number of hits in a row. Resets when on miss.
		/// </summary>
		public int ConsecutiveHits { get; private set; }
		/// <summary>
		/// Number of misses in a row. Resets when on hit.
		/// </summary>
	    public int ConsecutiveMisses { get; private set; }

		/// <summary>
		/// An int that flips between 1 and -1 when the robot hits a wall
		/// </summary>
	    public int WallHitMovementDir { get; private set; }

		public Vector2D Position { get; private set; }
		/// <summary>
		/// Returns true if we havent moved for some time.
		/// </summary>
		public bool IsStuck { get { return (Time - lastDifferentPositionTime > 48); } }

		public Vector2D VelocityVector {
			get {
				if (Velocity.IsZero()) return new Vector2D();
				return new Vector2D(Velocity * Math.Cos(HeadingRadians), Velocity * Math.Sin(HeadingRadians));
			}
		}
		public Drawing Drawing { get; private set; }
		public EnemyData enemyData;
		public BulletData bulletData;
		public GameData gameData;
		private FiniteStateMachine bodyFSM;
		private FiniteStateMachine gunFSM;
		private FiniteStateMachine radarFSM;
		private long lastDifferentPositionTime;
		public string CurrentBodyMovementState { get; private set; }

		public override void Run()
		{
			InitializeBot();

			radarFSM.EnqueueState(StateManager.StateRadarSweep);

			CurrentBodyMovementState = gameData.GetBestState();
			bodyFSM.EnqueueState(StateManager.StateMovementSelect);
			Out.WriteLine("State: " + CurrentBodyMovementState);
			while (true)
			{
				bodyFSM.Update();
				gunFSM.Update();
				radarFSM.Update();
				Drawing.DrawLine(Color.White, Position, Position.ProjectForTime(HeadingRadians, Velocity, 10));

				//SetTurnRadarLeft(double.PositiveInfinity * radarDir);

				Drawing.DrawString(Color.Black, "Hits: " + ConsecutiveHits, new Vector2D(0, -70));
				Drawing.DrawCircle(Color.BlueViolet, enemyData.Position, (float)PrefferedEnemyDistance*2, (float)PrefferedEnemyDistance*2);
				Drawing.DrawString(Color.Black, "Body  : " + bodyFSM.CurrentStateID, new Vector2D(0, -100));
				Drawing.DrawString(Color.Black, "Gun   : " + gunFSM.CurrentStateID, new Vector2D(0, -130));
				Drawing.DrawString(Color.Black, "Radar : " + radarFSM.CurrentStateID, new Vector2D(0, -160));
				Drawing.DrawString(Color.Black, string.Format("Stuck: {0} ({1} - {2}) Vel: {3}", IsStuck, Time, lastDifferentPositionTime, Velocity), new Vector2D(0, -40));
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


				Position.Set(X, Y);
				if (Math.Abs(Velocity) > 0.5)
				{
					lastDifferentPositionTime = Time;
				}
				
				Execute();
			}
		}

		private void InitializeBot()
		{
			Position = new Vector2D();
			Drawing = new Drawing(this);
			bodyFSM = new FiniteStateMachine(this);
			gunFSM = new FiniteStateMachine(this);
			radarFSM = new FiniteStateMachine(this);

			enemyData = new EnemyData(this);
			bulletData = new BulletData();
			gameData = new GameData(this);
			WallHitMovementDir = 1;

			IsAdjustRadarForGunTurn = false;
			IsAdjustGunForRobotTurn = false;
			IsAdjustRadarForRobotTurn = false;
		}

		// ROBOCODE EVENTS // 
		public override void OnScannedRobot(ScannedRobotEvent evnt)
		{
			enemyData.SetData(evnt);
			gunFSM.EnqueueState(StateManager.StateAttack);
			//bodyFSM.EnqueueState(StateManager.StateFollow);
			radarFSM.EnqueueState(StateManager.StateScanLock);
		}

	    public override void OnHitByBullet(HitByBulletEvent evnt)
	    {
		    bulletData.SetData(evnt.Heading, Time); // TODO Remove??
			//bodyFSM.EnqueueState("Dodge");
		}

	    public override void OnBulletHit(BulletHitEvent evnt)
	    {
		    ConsecutiveHits ++;
			ConsecutiveMisses = 0;
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

	    //public override void OnRobotDeath(RobotDeathEvent evnt)
	    //{
		   // if (evnt.Name == enemyData.Name)
		   // {
			  //  enemyData.Reset();
		   // }
	    //}

		public override void OnRoundEnded(RoundEndedEvent evnt)
		{
			gameData.OnRoundOver(CurrentBodyMovementState);
		}

		public override void OnDeath(DeathEvent evnt)
	    {
			Out.WriteLine("I'll be back.");
	    }
	}
}
