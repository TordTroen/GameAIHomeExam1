### Why
- I made a StateManager that holds all availible states, so we only have one instace of each state 
	(instead of having all availible states in the FiniteStateMachine which has several instances)
- The container that holds all the states is a Dictionary, so we can easily get the states by simply providing the name string.


### More time
- With the current setup, you can queue Gunstates to the body State Machine and vica versa. 
	This isn't that big of a problem when it's just me, but it would be nice to make it so that it's not possible to e.g. queue StateAttack to the body FSM.