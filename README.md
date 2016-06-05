# Match3Swipe
## 0. Getting started
This project **Swiper** was made with Unity 5.3.3 using C# language for scripting. After cloning this repository you will need
to open the contents as a project in Unity. 

There is only one scene that represents a demo level of the game located in **Scenes/Level.unity**. 
You can run the game starting from that scene, but in order to play it inside Unity Editor, an app: Unity Remote has to be installed 
and running on an appropriate device.

This is a demo game that was created in a very short period. Have fun, enjoy the game and feel free to write to me about any suggestions about the code structure or the game.

## 1. Architecture

The game currently has one feature: LevelRun that represents a demo level of a match-3 swiper game.

For this feature a variation of MVC pattern was used:
 - **LevelRunModel** (Model) - stores the data for the level run 
 - **LevelRunManager** (Controller) - updates the model, knows how to execute the logic flow of the level run
 - **LevelRunComponent** (Controller/View) - keeps and executes an instance of LevelRunManager, responsible for all the components that shows game objects in the scene based on the logic objects, handles input 

The manager handles the logic flow of the game by creating logic objects that can live without visual objects (Game Objects).
The point is that each run can be executed independent of any visual representation. 
This can help in creating automated bots and tests for level run, avoiding rendering of the objects in the scene.  

Following the logic from presentation separation, all of the scripts that are created for presentational objects (Game Objects) have a suffix "Component" 
in the name and they are initialized with a logic object of the appropriate type. For example, for each tile 
a new logic object is created and then a Game Object having a TileComponent script is initialized with that object. 
When an object needs to be destroyed only the visual representation is destoyed.

All the logic objects that have time-dependant logic are tickable objects which are executed by a ticker. The presentational objects are just following the positions of the
logic objects. For example, the fall (move) animation of a tile is done to a logic tile object, and the presentational object just draws itself based on this position of the logic object.

Object pools were introduced for the objects that are dynamically instantiated multiple times during a level run. This was done to the tile objects, as 
they are constantly destroyed and new tiles are spawned.

