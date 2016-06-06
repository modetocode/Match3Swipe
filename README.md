# Match3Swipe
## 0. Getting started
This project **Swiper** was made with Unity 5.3.3 using C# language for scripting. After cloning this repository you will need
to open the contents as a project in Unity. 

There is only one scene that represents a demo level of the game located in **Scenes/Level.unity**. 
You can run the game starting from that scene, but in order to play it inside Unity Editor, an app: Unity Remote has to be installed 
and running on an appropriate device.

This is a demo game that was created in a very short period. Have fun, enjoy the game and feel free to write to me about any suggestions about the code structure or the game.

[Download APK](https://www.dropbox.com/s/pt0p5539n72j6zx/2016.06.06.01%20match3swiper.apk?dl=0)

## 1. Game description

This is a match-3 based game where you need to connect more than 2 tiles with the same shape in a sequence. 

<img src="/Screenshots/how_to_play.jpg" width="225" height="400">

You have limited time and you get score for every sequence that you make. Also you get some bonus time for finishing a sequence of tiles. The more tiles there are in a sequence the more points and bonus time you score.

When the time passes the highscore is updated if a new score has been set. The highscore will be saved even after exiting the game.

## 2. Architecture

The game currently has one feature: LevelRun that represents a demo level of a match-3 swiper game.

For this feature a variation of MVC pattern was used:
 - **LevelRunModel** (Model) - stores the data for the level run 
 - **LevelRunManager** (Controller) - updates the model, knows how to execute the logic flow of the level run
 - **LevelRunComponent** (Controller/View) - keeps and executes an instance of LevelRunManager, responsible for all the components that shows game objects in the scene based on the logic objects, handles input 
 - **LevelRunGUIComponent** (View) - responsible for displaying the gui in the scene, gets the data from the model

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

## 3. Gameplay

All of the game variables that affect the gameplay are tweakable via the Unity Inspector. Everything connected to the progression and gameplay can be tweaked by a person which is preferably a game designer, and doesn't need to be a developer.

The tweakable variables can be found in the **Game Settings -> Game Constants** menu:

![Image](/Screenshots/game_constants.jpg)

## 4. Other

### 4.1. Gameboard size

The size of the board (number of rows x number of columns) can be changed and the board will be fully visible on a device. In order to achieve this, the camera was repositioned based on the size of the device and the size of the board. **LevelRunCameraComponent** script is responsible for achieveing this. 

<img src="/Screenshots/screenshot1.jpg" width="225" height="400">
<img src="/Screenshots/screenshot2.jpg" width="225" height="400">
<img src="/Screenshots/screenshot3.jpg" width="225" height="400">
<img src="/Screenshots/screenshot4.jpg" width="225" height="400">

### 4.2. No more moves detector

A situation when there are no more moves can happen. In that case a detector was developed that will activate on some time (currently 1 second) and randomly pop a tile.  

### 4.3. Player game data

The player game data currently is stored on the device and contains the highscore of the player. The file is serialized on disk when the game is saved, and deserialized when the game needs to be loaded.

## 5. License

The licence info can be found in the file [LICENSE.txt](/LICENSE.txt)
