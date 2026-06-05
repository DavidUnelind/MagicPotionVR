# MagicPotion

## Overview

MagicPotion is a Unity-based VR potion-mixing game built around a fantasy tavern experience. Players use motion controls and hand interactions to prepare and serve magical potions to a growing queue of customers.

## Gameplay

- Customers arrive and line up at the bar.
- Each guest requests a randomized potion order: **Love**, **Luck**, **Strength**, or **Health**.
- Players collect the correct ingredients and add them to the cauldron.
- Once the required ingredients are added, the player must shake the cauldron to finish the potion.
- After the potion is fully shaken, the player presses the serve button to deliver the drink.
- Each successful serve awards points.
- The game runs on a timed session, and the objective is to serve as many customers as possible before time runs out.

## Notes

- The game uses Unity XR and Oculus interaction components for immersive VR hand controls.
- Potion recipes are defined by ingredient colors/shapes and are resolved in the `Recipe` system.
- The player must complete a shaking action before the potion can be served.

## Running the Project

1. Open `MagicPotionVR.slnx` or the Unity project folder in Unity.
2. Load the main scene from `Assets/Scenes/`.
3. Ensure XR and Oculus support packages are installed and configured.
4. Enter Play mode to test the VR potion-serving gameplay.

## Installing and Playing the APK on Meta Quest

1. Build the APK from Unity or use the provided APK file.
2. Open SideQuest on your PC.
3. Connect your Meta Quest headset to the PC via USB.
4. Use SideQuest to transfer the APK to the headset.
5. Put on the Quest headset.
6. Open the `Library` app.
7. Go to the menu and select `Unknown Sources`.
8. Find and launch `MagicPotion` from the list.
