using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drot.States;

namespace Drot
{
	// Holds all states, so we only have one instance of each class
	public class StateManager
	{
		private readonly Dictionary<string, State> states;

		public StateManager(FSMRobot robot)
		{
			states = new Dictionary<string, State>
			{
				{ "Idle", new StateIdle() },
				{ "Attack", new StateAttack() },
				{ "Escape", new StateEscape() },
				{ "Pursuit", new StatePursuit() }
			};
			// Initialize the states with the dictionary entry's key and a reference to the robot
			foreach (var item in states)
			{
				//item.Value.Id = item.Key;
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
			return state;
		}

		public bool HasState(string stateId)
		{
			return states.ContainsKey(stateId);
		}
	}
}
