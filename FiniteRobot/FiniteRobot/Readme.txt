### Why
- I made a StateManager that holds all availible states, so we only have one instace of each state 
	(instead of having all availible states in the FiniteStateMachine which has several instances)
- The container that holds all the states is a Dictionary, so we can easily get the states by simply providing the name string. 
	- I also made a constant field for all the state names, so I don't have to worry about me misspelling a statename.
- I added several states for movement that are chosen randomly to make it a bit more difficult to predict what the robot is going to do. 
	- This also might make it so the robot uses tactics that works better against certain opponents. A dumb learining robot of sorts.
- Since what state the body is in depends on the distance to the enemy, I made a state that selects what body state to transition to instead of having the almost same logic in multiple states.
- In the charge state I decided to use Arrival so it has a bit more interesting behavior, but Seek would probably have been a better fit there.

### More time
- With the current setup, you can queue Gunstates to the body State Machine and vica versa. 
	This isn't that big of a problem when it's just me, but it would be nice to make it so that it's not possible to e.g. queue StateAttack to the body FSM.
- The system for selecting a random state for movement could have been better. It currently just uses the state that had the most success after only trying the states once, but it could have been a bit more finetuned so it coulkd better select the state to use the next round.
- The wander state could be tuned to work much better
- Could maybe use CollisionAvoidance when trying to get as far away as possible from the enemy???