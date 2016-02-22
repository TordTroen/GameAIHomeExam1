using System.Collections.Generic;
using Drot.States;

namespace Drot
{
	public class FiniteStateMachine
	{
		public State curState;
		public Queue<State> stateQueue = new Queue<State>();
		private readonly State[] _allStates; // Contains all possible states, so we don't have to make several instances of the same state
		private const int MaxTransitionsPerFrame = 10;
		private Dictionary<string, State> states;
		 
		public FiniteStateMachine(FSMRobot robot)
		{
			states = new Dictionary<string, State>()
			{
				{ "Idle", new StateIdle() },
				{ "Attack", new StateAttack() }
			};
			foreach (var item in states)
			{
				//item.Value.Id = item.Key;
				item.Value.Initialize(item.Key, robot);
			}

			_allStates = new State[] { new StateIdle(), new StateAttack(), new StateEscape() };
			foreach (var state in _allStates)
			{
				//state.Initialize(robot);
			}
			//SetCurrentState(_allStates[0]);
			SetCurrentState(states["Idle"]);
		}

		/// <summary>
		/// Enqueues a state with the gives StateID. Won't do anything if the StateID is StateID.None.
		/// </summary>
		/// <param name="stateId">The State to enqueue.</param>
		public void EnqueueState(StateID stateId)
		{
			if (stateId != StateID.None)
			{
				int id = (int)stateId;
				stateQueue.Enqueue(_allStates[id]);
			}
		}

		public void EnqueueState(string id)
		{
			if (states.ContainsKey(id))
			{
				stateQueue.Enqueue(states[id]);
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

				StateID queuedStateId = curState.OnUpdate();
				EnqueueState(queuedStateId);

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
