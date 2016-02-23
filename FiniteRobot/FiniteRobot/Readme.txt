### Why
- I made a StateManager that holds all availible states, so we only have one instace of each state 
	(instead of having all availible states in the FiniteStateMachine which has several instances)
- The container that holds all the states is a Dictionary, so we can easily get the states by simply providing the name string.