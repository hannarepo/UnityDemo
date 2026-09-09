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

## Third party
All art assets are from third parties, downloaded from Unity Asset Store.

Environment art: Oode Studios | Low Poly Nature | [Asset Store Page](https://assetstore.unity.com/packages/3d/environments/low-poly-nature-260306)

Player character art: Dungeon Mason | RPG Tiny Hero Duo PBR Polyart | [Asset Store Page](https://assetstore.unity.com/packages/3d/characters/humanoids/rpg-tiny-hero-duo-pbr-polyart-225148)

Enemy character art: Dungeon Mason | RPG Monster Duo PBR Polyart | [Asset Store Page](https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbr-polyart-157762)
