# CTF
## Unity sample Capture The Flag project

A sample project in Unity with a basic implementation of a Capture The Flag game. The player's goal is to reach the enemy base, capture the flag and carry it to the starting point. The capture of the flag is obstructed by enemies which, when touched, cause the player to lose a life and start the board over again. The player also loses a life if he falls off the map. Enemies do not "attack" until the player is in their field of view.

Additionally, a simple system of the best results, that is, the shortest time needed to pass the board, was implemented.

#Issues/notes:
Enemy objects are not "destroyed" when they collide with the player's projectiles - only particle effects are created, and enemies also get a small recoil back as a result of the collision.

Enemy objects could pose more of a threat - it's a matter of adjusting their movement speed.

Player projectiles are Rigidbodies with a rather large collider and CollisionDetectionMode.ContinuousSpeculative instead of raycasts, which would perhaps fit better here.

## Used free assets:
UX Flat Icons [FREE] - https://assetstore.unity.com/packages/2d/gui/icons/ux-flat-icons-free-202525

Apocalyptic Props: Flag01 - https://assetstore.unity.com/packages/3d/props/apocalyptic-props-flag01-116763

M9 Knife - https://assetstore.unity.com/packages/3d/props/weapons/m9-knife-7597

Lean Pool - https://assetstore.unity.com/packages/tools/utilities/lean-pool-35666

Modern Guns: Handgun - https://assetstore.unity.com/packages/3d/props/guns/modern-guns-handgun-129821

NaughtyAttributes - https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996

easy UI emerald - default - https://assetstore.unity.com/packages/2d/gui/icons/easy-ui-emerald-default-112796

Unity Particle Pack - https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325

