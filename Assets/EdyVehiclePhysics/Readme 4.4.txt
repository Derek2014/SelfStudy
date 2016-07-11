Edy's Vehicle Physics is a set of scripts for developing realistic and fun vehicles in your game. Designed for gameplay, ease of use, and physically realistic behavior. Features:

- Drag & Play! 4 pre-configured vehicle prefabs with different features and behaviors
- Vehicles work out of the box by dragging the prefabs into any existing scene
- Full-featured sound controller, from engine and wind to scratches and wheel bump effects
- Complete, fully working demo project
- Version archive an download via GIT repository (http://projects.edy.es/trac/edy_vehicle-physics)


How to upgrade:

1. In Unity, File -> New Scene
2. Delete the folder EdyVehiclePhysics
3. Import Edy's Vehicle Physics from the Asset Store or the updated Unity Package

NOTE: If you're upgrading from version 4.3 or earlier, at the stage 2 remove also any other file or folder related to the package (such as the folders "World" and "Vehicles" that came in the original package).


How to use the vehicles in your project:

1. Import the package in your project.
2. Drag any of the vehicle prefabs from Prefabs\Vehicles into your scene.
3. Click Play. 

The vehicle is already working with the standard input. You might want to configure your camera for chasing it.


Using the scripts from C#:

1. Create a Plugins folder in the root of your project (if you don't have it already)
2. Move the folder Scripts from EdyVehiclePhysics to the Plugins folder

Now the classes and types can be referenced directly from your C# scripts.


Input Manager settings:

You can find a nicely configured Input Manager settings file at the folder InputManager. Unzip the InputManager.asset file and move it to the ProjectSettings folders overriding the existing file.


Features, videos, live demo:
http://www.edy.es/dev/vehicle-physics

Feedback & support:
Contact me at edytado@gmail.com



--------

Edy's Vehicle Physics 4.4

- Project updated to Unity 4.5.3f3
- Performance improvement on visual effects (smoke / skidmarks)
- New simple example scenes to help you getting started
- FIX: damage calculation for deformable nodes
- FIX: antiroll bar force calculation in strict mode
- FIX: null pointer when instantiating vehicle prefabs (Bill Pfeil)
- FIX: conflicting GUIDs with the camera scripts of the Standard Assets
- FIX: terrain friction doesn't work due to Unity removing physic material from TerrainColliders

Upgrade notes: 

- The GUIDs in the camera scripts have changed for solving conflicts with the Standard Assets. You might find "missing script" errors in your camera objects. You must manually re-assing the camera scripts to the components in your objects.
- The movement in the FreeView and SmothLookAt cameras is now disabled by default. This is because the movement uses three extra axis that must be set up in the Input Manager. A nicely configured InputManager.asset file is provided in the InputManager folder.