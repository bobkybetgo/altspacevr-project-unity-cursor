# AltspaceVR Unity Cursor Programming Project - Steve Kim
- Email: wyskim93@stanford.edu   OR   steve.kim1993@gmail.com

## Part 1 - 3D Cursor 

# Files 
	/Assets/Altspace/Scripts/SphericalCursorModule.cs
	/Assets/Altspace/Scripts/MyShader.shader

# Notes
1) Changed value of DefaultCursorScale to be much higher (500, 500, 500), since the cursor was too small when there was no collision. Not sure if this is due to a mathematical error when calculating collision with the 'virtual' sphere. However, my implementation seems to set the position correctly. 

2) Did not explicitly calculate the intersection point between raycast and 'virtual' sphere. Instead, I added the ray's origin to the product of SphereRadius and the ray's normal. This way, we find the point the ray would've hit the 'virtual' sphere of radius 1000 without actually having to use spherical coordinates.

3) MyShader ensures cursor to be drawn on top of all geometry with the line 'ZTest Off'.

4) 'Lighting Off' was meant to disable lighting, though the vert/frag functions already ignore lights.

5) Commented out 'LockScreen' code, as it was causing problems in my editor.

## Part 2 - Enhancement

# Files (in addition to files above)
	/Assets/Altspace/Scripts/SphericalCursorModule.cs (modified)
	/Assets/EnhancementScene
	/Assets/Aquarium (3rd party assets)


# Notes
1) New map containing enhancements are in /Assets/EnhancementScene

2) Cursor will turn red when mouse is over interactable portion of the aquarium (i.e., tapping on the glass). 

3) Clicking on the aquarium will prompt the fish to hide. 

4) User can only tap on the glass once, and the cursor will no longer turn red when hovering over interactable portion.

5) All aquarium assets (fish models, default animations, particle effects) are 3rd party assets, EXCEPT for the 'hiding' animations of the fish (i.e., when the fish swim away) - a Unity technique I've tried out for the first time!

6) SphericalCursorModule now takes in an array of GameObjects (for fish) and AnimationClips (for hiding animations). Each fish's animation is played when user interacts with aquarium. I was hoping to directly access the clips from the animation component, but from what I could find online, it does not seem possible.