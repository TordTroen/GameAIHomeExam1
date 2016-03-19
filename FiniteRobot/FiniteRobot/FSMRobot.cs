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
		public const double Mass = 2;
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
		public bool IsStuck { get { return (Time - lastMovementTime > 48); } }

		public Vector2D VelocityVector {
			get {
				if (Velocity.IsZero()) return new Vector2D();
				return new Vector2D(Velocity * Math.Cos(HeadingRadians), Velocity * Math.Sin(HeadingRadians));
			}
		}
		public Drawing Drawing { get; private set; }
		public EnemyData enemyData;
		private GameData gameData;
		private FiniteStateMachine bodyFSM;
		private FiniteStateMachine gunFSM;
		private FiniteStateMachine radarFSM;
		private long lastMovementTime;
		public string CurrentBodyMovementState { get; private set; }

		public override void Run()
		{
			InitializeBot();

			radarFSM.EnqueueState(StateManager.StateRadarSweep);

			CurrentBodyMovementState = gameData.GetBestState();
			bodyFSM.EnqueueState(StateManager.StateMovementSelect);

			while (true)
			{
				bodyFSM.Update();
				gunFSM.Update();
				radarFSM.Update();

				// Debug stuff
				Drawing.DrawString(Color.Black, "Hits: " + ConsecutiveHits, new Vector2D(0, -70));
				Drawing.DrawCircle(Color.BlueViolet, enemyData.Position, (float)PrefferedEnemyDistance*2, (float)PrefferedEnemyDistance*2);
				Drawing.DrawString(Color.Black, "Body  : " + bodyFSM.CurrentStateID, new Vector2D(0, -100));
				Drawing.DrawString(Color.Black, "Gun   : " + gunFSM.CurrentStateID, new Vector2D(0, -130));
				Drawing.DrawString(Color.Black, "Radar : " + radarFSM.CurrentStateID, new Vector2D(0, -160));
				Drawing.DrawString(Color.Black, string.Format("Stuck: {0} ({1} - {2}) Vel: {3}", IsStuck, Time, lastMovementTime, Velocity), new Vector2D(0, -40));

				Position.Set(X, Y);
				if (Math.Abs(Velocity) > 0.5)
				{
					lastMovementTime = Time;
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
			radarFSM.EnqueueState(StateManager.StateScanLock);
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
