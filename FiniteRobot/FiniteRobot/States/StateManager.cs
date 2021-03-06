﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.States;
using PG4500_2016_Exam1;

namespace Drot
{
	/// <summary>
	/// Holds all states, so we only have one instance of each class.
	/// </summary>
	public class StateManager
	{
		public const string StateIdle = "Idle";
		public const string StateAttack = "Attack";
		public const string StateFollow = "Follow";
		public const string StateFlee = "Flee";
		public const string StateMovementSelect = "SelectMovementState";
		public const string StateCircleEnemy = "CircleEnemy";
		public const string StateWander = "Wander";
		public const string StateCircleWander = "CircleWander";
		public const string StateScanLock = "ScanLock";
		public const string StateRadarSweep = "RadarSweep";

		private readonly Dictionary<string, State> states;
		private readonly Trotor14 robot;

		public StateManager(Trotor14 robot)
		{
			this.robot = robot;

			// Add all the states to the dictionary and initialize them
			states = new Dictionary<string, State>
			{
				{ StateIdle, new StateIdle() },
				{ StateAttack, new GunStateLinearAttack() },
				{ StateFollow, new BodyStateFollow() },
				{ StateFlee, new BodyStateFlee() },
				{ StateMovementSelect, new BodySelectMovementState() },
				{ StateCircleEnemy, new BodyStateCircleEnemy() },
				{ StateWander, new BodyStateWander() },
				{ StateCircleWander, new BodyStateCircleWander() },
				{ StateScanLock, new RadarStateScanLock() },
				{ StateRadarSweep, new RadarStateScanSweep() }
			};
			// Initialize the states with the dictionary entry's key and a reference to the robot
			foreach (var item in states)
			{
				item.Value.Initialize(item.Key, robot);
			}
		}

		/// <summary>
		/// Returns a state with the specified stateID.
		/// </summary>
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

		/// <summary>
		/// Checks if the specified state exists.
		/// </summary>
		public bool HasState(string stateId)
		{
			return states.ContainsKey(stateId);
		}
	}
}
