# COSC4397-Team-Assignment-2
This is the final repository for Immersive Spatial Tech

# Contributions
## Kevin Do (Team Lead)
1. Designed the front UI and look of buttons and icons.
2. Prepared presentation.
3. Providing the Vuforia database and targets
4. Made the enemy wave system script
5. Made a map texture and tower icons for spawning
6. Set up arrangement for global health and money ui
## Gal Egozi
1. Setup the asset loading from Azure, including the logic for switching between the different models.
2. Implemented the loading for the properties as individual JSON files (one for each object) from Azure into a C# object.
3. Added a new scene and implemented a scene transition button.
4. Implemented a buy button for the shop to prototype the purchasing process.
5. Condensed the shop into a fully-operational dock.
  1. Added the dock buttons into the UI
  2. Uploaded the dock buttons to Azure, and added the functionality to load them in.
  3. Added the functionality to generate a copy of the model (the tower) when the button is clicked.
  4. Added the functionality to move the model the first time it is clicked, then it is locked in place.
  5. The damage is determined by the model's attributes.
6. Added the global state (for health and money), bound the dock-shop to the global state, and added the textbox that shows the health and money in the UI.
7. Added the button to switch between the game and the tower viewer (the scene that we called the shop when originally submitting the assignment)
## Khuong Nguyen
1. Created the UI Layout for shop scene
2. Wrote the logic to rotate active object 
3. Wrote the UIController.cs to programatically control the UI
4. Brought the active object to ImageTarget on ARCam
5. Helped with the ShopManager.cs
6. Added tower controller
7. Added bullet controller
8. Make tower shoots at enemy object if in range
9. Implemented a game manager to store player health and when enemy touches the trigger, player health will be deducted.
## Henry Hite
1. Presented video of our working shop.
2. Added rotate/scale for loaded objects
## Phuc Vo
1. Help in anchoring UI elements.
2. Implement sound system.
3. Adding health bar on top of enemies.
4. Assisting other teammates.
