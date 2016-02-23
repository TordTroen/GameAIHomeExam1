using System.Collections.Generic;
using System.IO;
using Drot.States;

namespace Drot
{
	public class FiniteStateMachine
	{
		public State curState;
		public Queue<State> stateQueue = new Queue<State>();
		private readonly State[] _allStates; // Contains all possible states, so we don't have to make several instances of the same state
		private const int MaxTransitionsPerFrame = 10;
		//private Dictionary<string, State> states;
		private readonly StateManager states;
		private TextWriter io; // DEBUG

		public FiniteStateMachine(FSMRobot robot)
		{
			io = robot.Out; // DEBUG
			// Add the classes to the dictionary with a string id as key
			//states = new Dictionary<string, State>
			//{
			//	{ "Idle", new StateIdle() },
			//	{ "Attack", new StateAttack() },
			//	{ "Escape", new StateAttack() }
			//};
			//// Initialize the states with the dictionary entry's key and a reference to the robot
			//foreach (var item in states)
			//{
			//	//item.Value.Id = item.Key;
			//	item.Value.Initialize(item.Key, robot);
			//}

			//_allStates = new State[] { new StateIdle(), new StateAttack(), new StateEscape() };
			//foreach (var state in _allStates)
			//{
			//	state.Initialize(robot);
			//}
			//SetCurrentState(_allStates[0]);

			states = new StateManager(robot);

			// Start in the idle state
			SetCurrentState(states.GetState("Idle"));
			//SetCurrentState(states["Idle"]);
		}

		/// <summary>
		/// Enqueues a state with the gives stateID. 
		/// Checks to make sure stateID isn't null or the same as the current state.
		/// </summary>
		/// <param name="stateId">The State to enqueue.</param>
		public void EnqueueState(string stateId)
		{
			if (stateId != null && states.HasState(stateId) && curState != states.GetState(stateId))
			{
				stateQueue.Enqueue(states.GetState(stateId));
			}
		}

		public void Update()
		{
			int processCount = 0;

			do
			{
				// Make sure we don't spin outta control
				processCount++;
				if (processCount > MaxTransitionsPerFrame)
				{
					break;
				}

				if (stateQueue.Count > 0)
				{
					SetCurrentState(stateQueue.Dequeue());
				}

				//StateID queuedStateId = curState.OnUpdate();
				//EnqueueState(queuedStateId);
				string queuedState = curState.OnUpdate();
				//io.WriteLine("Queued: " + queuedState);
				EnqueueState(queuedState);

			} while (stateQueue.Count > 0);
		}

		private void SetCurrentState(State newState)
		{
			if (curState != null)
			{
				curState.OnExit();
			}

			curState = newState;

			if (curState != null)
			{
				curState.OnEnter();
			}
		}
	}
}
