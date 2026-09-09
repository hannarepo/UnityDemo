# UnityDemo
A small demo of player and enemy state machines in Unity.

## Player
Player uses a pushdown automaton to keep track of previous states.
Player has the following states:
 - Idle
 - Walk
 - Sprint
 - Attack while idle
 - Attack while walking
 - Hurt
 - Dead

## Enemy
Enemy has a finite state machine with no state tracking.
The enemy has the following states:
 - Idle
 - Walk
 - Aggro
 - Attack
 - Cooldown
 - Hurt
 - Dead

The enemy patrols on a waypoint path that can be either looping or a ping pong path.
