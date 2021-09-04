<h1> GD50 Final Project </h1> 

<font size="5"> Description: </font> 

This game is a simple platformer with a dash of FPS elements that steals inspiration from games like Doom and Portal. The game consists of three levels with an increasing number of mechanics that utilize Unity's various systems. Like the video game Portal, the player controls a character in the first-person view and must navigate through a series of obstacles and platforms to reach the end objective. Besides being a platformer, in the later levels, the player is given a gun (that uses Ray Casts) which they can utilize to damage enemies that can fight back. This is where the inspiration from doom comes in. Thus, the result is a game in which the player must precisely navigate the environment while taking care of various enemies who can damage the player and reset the level. This combination of FPS and platforming mechanics ensures that the player is actively engaged in the game and also that the player gains a sense of triumph as they complete each stage.

<font size="5"> Justification of Game Complexity and Distinctiveness: </font> 

Here is an explanation of the major mechanics of the game as well as an abstraction of how these mechanics were programmed

<font size = "4"> Mechanics: </font>

**FPS Player Controller:** <br/>
The character that the player controls is a capsule that uses a modified version of Unity's character controller. The player can move around using the WASD keys and has the ability to perform a double jump with the space bar as well as a short dash forward by pressing the E key. The code for wall jumping is also present in this character controller. Whenever the player makes contact with an object that is tagged as a wall, the boolean value that determines whether or not the player has double jumped gets reset. Therefore the player gains the ability to jump again.

**Player UI:** <br/>
The player UI is a canvas that is spread across the screen which displays information such as the player's health, a crosshair and the victory text that shows up when the player reaches the end objective. The player's health is controlled by the character controller which changes the value as the player takes damage.

**Music:** <br/>
The music in each level is controlled by a simple audio source with infinite range. The soundtrack in each level is unique and enriches the player's experience.

**Static and Mobile Platforms:** <br/>
All the levels in this game were made with Unity Probuilder. As a result, each object comes pre-equipped with a rigid body and collider which made programming collisions in the levels easy. There are some platforms in the game that essentially tween from one position to the other so as to give the impression that these platforms are moving. Whenever the player steps on these moving platforms, the player controller gets parented to the moving platform so that the position of the player controller also moves along with the platform. Whenever the player jumps of, it's parent becomes null and is no longer affected by the movement of the platform

**FPS Shooter Controller:** <br/>
This is the character that the player uses in the second and the third level. It is similar to the above mentioned character controller. The major difference in this controller is that the player is given a given that uses Unity's ray cast system to hit a particular target. The point that the gun hits is the centre of the cross hair in the player's UI. Whenever the fire button is pressed, the audio source that is present in this controller plays a gunshot sound and a bullet impact particle effect is created at the place where the ray cast intersects with another object. There is also a muzzle flash particle effect that is player whenever the player shoots. All of these effects combined gives some satisfaction to the player and adds some realism to the game. One thing to note is that the amount of times that the player can shoot at a time is determined by the fire rate of the gun. 

**Enemies:** <br/>
The enemies in this game are capsules and consist of two types. Movers are essentially just dummies that pose no threat to the player. They exist just to demonstrate the damaging potential of the player's gun. When the player shoots at them, their health gets reduced by a set amount. When their health reaches zero, an explosion particle effect is instantiated at their position and their bodies are deleted from the level. Gunners, like Movers, posses the same target script that permits them to take damage, however, unlike Movers they stand still and fire bullets at the player. If these bullets collide with the player's character, them the player's health is reduced by 25%. If the player's health reaches zero, then the level gets reset. This is the only way the player is capable of resetting a level as falling off a platform into the void just respawns them at the nearest blue platform. This type of enemy forces the player to play aggressively and adds to the gameplay loop.

<font size="5"> Layout of Levels: </font> 

This game consists of three levels with increasing complexity in each level

**Level 1:** <br/>
The player has to navigate through a combination of static and moving platforms using the abilities described above. At the end of the stage, the player must perform some consecutive wall jumps to complete the level. The hardest part of this level has to be the wall jumps because the player must perform a rapid sequence of coordinated controls to get past this section. This level serves as an introduction to the platforming aspect of the game.

**Level 2:** <br/>
The player can use a hit-scan pistol on a series of targets to practice their aim. Once the targets take sufficient damage, they gain ragdoll physics. The dummies are then deleted and leave behind an explosion that makes extensive use of Unity's particle system. This level serves primarily as a tutorial for the basic gunplay in the game and possesses nearly no platforming.

**Level 3:** <br/>
Enemies that have simple AI are introduced. There are two kinds of enemies: Movers and Gunners. The movers exist primarily as moving targets for the player to destroy. However, the gunners are capable of sending projectiles that can damage the player. If the player's HP reaches 0, then the level is reset. To complete this level, the player must use both the platforming and FPS mechanics of the game. The player must take notice of the range from which the gunners can attack them. Since the player's weapon has no damage fall-off, it is possible to snipe some of the gunners from a distance. Doing this can make a section of the level trivial as the player eliminates the need to dodge enemy gunfire while platforming. I feel like such a level design rewards the player for being preceptive and taking the time to understand the game mechanics.

<font size="5"> Afterword: </font> 

This README is an abstract summary of the game that describes it in general gaming and programming terms. I have added comments to the script present in the game's source code that explains how various mechanics and objects are programmed. I referred to multiple online sources such as the Unity forums, Youtube, and the past lectures of the course to program the game. 
Video games have given me many memories and experiences. Likewise, I hope that this simple game can make someone's day just a bit better.

Final Build link: https://drive.google.com/file/d/1Nr2mDAfYrOxVRxQPs900kZGwNS5rQ7Nz/view?usp=sharing
