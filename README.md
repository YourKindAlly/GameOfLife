# Conway's **modified** Game of Life

## Rules

* Any live cell with fewer than two live neighbours dies, as if by underpopulation.
* Any live cell with two or three live neighbours lives on to the next generation.
* Any live cell with more than three live neighbours dies, as if by overpopulation.
* Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

## Navigating through the game

When you launch the game you will be able to choose the grid size of the game.
You can choose between 50 to 200 cells per axis in the grid.

When you are in the game, you can change the tick time between generations by pressing
A or D. The A key decreases time per tick while the D key increases. You have to press 
per time you want the step to increase, which is 0.1 per step by default. The generation 
tick can be between 0.1 to 3 seconds by default.

You can use the scroll wheel on the mouse to zoom the camera closer or further away from
the grid.

* Start new: Brings back you to the menu.
* Restart: Restarts the game using the same settings as before.
* Exit: Exits the application.

## Navigating the game objects (if you want to try to break something)

--- MANAGERS --- → GameManager
There is nothing to break here really. It holds the other game objects.

--- MANAGERS --- → Ticks
* Tick interval: The starting point and variable for how long each generation tick is.
* Tick step: Holds how much the tick interval will change per key press (A and D).
* Min tick and Max tick: The lowest and highest the tick interval can be.

--- LAYOUT --- → Grid
* Cell size: How big a cell is in units.