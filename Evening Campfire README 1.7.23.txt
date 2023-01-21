ATTENTION:
Due to the file size limitations on Coursera, The file submitted in the submission area here is a .zip file with a Windows and Android builds. For the full project .zip file, please use this dropbox link.

Description:

My project is a scene representing a clearing in a wooded area, with various assets designed to represent a small campsite (. In it there are various tree assets, rocks, logs, shrubs, etc. The player will spawn near a tent, campfire, cooking setup, a barrel with a flashlight on top of it, and an intriguing glowing stone with a hand shape on it. The flashlight has an XR grab interactable component on it, and can be picked up with the grip button, and turned on and off by using the trigger button while held. There is also an interactable component on the glowing obelisk, but it's intended as an easter-egg, so if you want to try to figure it out yourself, don't read the final instruction at the bottom of this text! (it's pretty straightforward though) The player can also smooth-move (for those who are comfortable doing so) or teleport (for those who haven't established their VR legs). I built and tested the environment using a Valve Index, however the project runs on OpenXR, so a user with a Quest should in theory be able to run it as well, although it may need to be plugged into a VR capable PC to properly experience the scene. I personally haven't been able to test on a Quest 2 yet, so I can't confirm or deny its playability there.

Requirements:

- VR HMD capable of rotational and positional tracking (I tested on Index)
- VR controllers with a grip and trigger button (oculus wands, Index knuckles, etc.)
- Headphones or built in audio in the HMD

Instructions:

Please unzip the file into your chosen directory, then import the project into Unity (Note: Unity must be version 2021.3.7f1, the scene will not load properly without the correct editor version). On opening the project, run the scene (there is only one Scene, and I couldn't figure out how to rename it, so it's called "SampleScene") and hop into your VR gear.

Controls:

- Left hand joystick to smooth-move
- Hold down the left hand "primary button" ('A' button on Index knuckles, unsure of the mapping for other controllers) to bring up teleport location selection, release to teleport to highlighted location.
- Use either hand to pick up the flashlight that is on the barrel next to the tent using the grip button, and while holding the flashlight with the grip button, press the trigger button to toggle the light on and off.
- EASTER EGG SPOILER AHEAD: Place your hand on the hand shape on the magical stone for a special easter egg (that I put way too much time into)! (Once the visitor arrives you can place your hand on the obelisk again to make it go away).