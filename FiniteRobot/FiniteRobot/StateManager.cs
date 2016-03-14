using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.States;
using PG4500_2016_Exam1;

namespace Drot
{
	// Holds all states, so we only have one instance of each class
	public class StateManager
	{
		public const string StateIdle = "Idle";
		public const string StateAttack = "Attack";
		public const string StateDodge = "Dodge";
		public const string StatePursuit = "Pursuit";
		public const string StateCircleEnemy = "CircleEnemy";
		public const string StateScanLock = "ScanLock";
		public const string StateRadarSweep = "RadarSweep";

		private readonly Dictionary<string, State> states;
		private trotor14 robot;

		public StateManager(trotor14 robot)
		{
			this.robot = robot;

			states = new Dictionary<string, State>
			{
				{ StateIdle, new StateIdle() },
				{ StateAttack, new GunStateLinearAttack() },
				{ StateDodge, new BodyStateDodge() },
				{ StatePursuit, new BodyStatePursuit() },
				{ StateCircleEnemy, new BodyStateCircleEnemy() },
				{ StateScanLock, new RadarStateScanLock() },
				{ StateRadarSweep, new RadarStateScanSweep() }
			};
			// Initialize the states with the dictionary entry's key and a reference to the robot
			foreach (var item in states)
			{
				item.Value.Initialize(item.Key, robot);
			}
		}

		public State GetState(string stateId)
		{
			State state = null;
			if (HasState(stateId))
			{
				state = states[stateId];
			}
			else
			{
				robot.Out.WriteLine(string.Format("Couldn't find the state '{0}'", stateId));	
			}
			return state;
		}

		public bool HasState(string stateId)
		{
			return states.ContainsKey(stateId);
		}
	}
}
