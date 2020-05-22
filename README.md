# Game_project

A derivative work of Touhou series. The project is under development currently.

## Screenshots

<img id="ss01" src="https://github.com/dark9ive/Game_project/blob/master/pics/Screenshots1.png" width="853" height="480">
<img id="ss02" src="https://github.com/dark9ive/Game_project/blob/master/pics/Screenshots2.png" width="853" height="480">  

## Building enviornments

This project is written in the following enviornment:

 - Unity3D version: 2019.2.0b10

## Usage

Please make sure you have [Unity3D](https://unity.com/) installed on your PC, and use the following command to clone this project.  

```
git clone git@github.com:dark9ive/Game_project.git
```

## Instructions

 - Use <kbd>↑</kbd> and <kbd>↓</kbd> keys to jump and glide.  
 - Use <kbd>z</kbd> to switch different lanes.


## ChangeLog

### v1.1(Uploaded on May 21, 2020)

 - Slightly change the Auto-aim algorithm (Bug #1 fixed)
 - Fixed the floating terrains. (Bug #2 fixed)
 - Remake floors and add the grass texture on.
 - Remove some "terrain" tags to prevent a bug.
 - Hook running speed to Real-world time.

#### Auto-aim algorithm update

In the last version, auto-aim algorithm wasn't very satisfying.  
The old algorithm would aim on the target with lowest x-value, despite there may have other target which is nearer to the character.  
  
The new algorithm will choose the nearest enemy only from those who are in front of the character.  

#### Floating terrains

Just some simple position adjust.  

#### Remake floors & add grass texture

#### Remove some "terrain" tag

#### running speed

### v1.0
  
Including all the stuffs been worked before this GitHub repository was made, which is as follows:
  
 - Basic controls
 - Two BGMs(One of them is still WIP)
 - Basic artworks(Main character, Stage1 Boss)
 - Basic scripts(still a little buggy)

## Known Bugs
  
Here're the bugs has been known by the developers.

### Bug #1
  
The auto-aim function will aim on the enemy with the lowest x-value, despite that the target might be behind the Character.

Fixed on: v1.1  

### Bug #2

Some terrains are floating in the air.

Fixed on: v1.1
