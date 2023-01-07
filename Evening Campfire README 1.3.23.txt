Description:

My project is a scene representing a campsite in a wooded area. In it there are various tree assets, rocks, logs, shrubs, etc. The player will spawn near a small campsite with a tent, campfire, cooking setup, and a barrel with a flashlight on top of it. The flashlight has an XR grab interactable component on it, and can be picked up with the grip button, and turned on and off by using the trigger button while held. The player can also smooth-move or teleport. I built and tested the environment using a Valve Index, however the project runs on OpenXR, so a user with a Quest should be able to run it as well, although it may need to be plugged into a VR capable PC to properly experience the scene. I personally haven't been able to test on a Quest 2 yet.

Requirements:

- VR HMD capable of rotational and positional tracking
- VR controllers with a grip and trigger button (oculus wands, Index knuckles, etc.)
- Headphones or built in audio in the HMD

Instructions:

Please unzip the file into your chosen directory, then import the project into Unity (Note: Unity must be version 2021.3.7f1, the scene will not load properly without the correct editor version). On opening the project, run it and hop into your VR gear.

Controls:

- Left hand joystick to smooth-move
- Hold down the left hand "primary button" ('A' button on Index knuckles, unsure of the mapping for other controllers) to bring up teleport location selection, release to teleport to highlighted location.
- Use either hand to pick up the flashlight that is on the barrel next to the tent using the grip button, and while holding the flashlight with the grip button, press the trigger button to toggle the light on and off.