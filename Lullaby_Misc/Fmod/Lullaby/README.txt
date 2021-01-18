*** Update 03/11 2020, 12:37 by Mark Poulsen ***
- I have added a new parameter to skip build-up of tone while singing other tones. The two parameters may be combined into one, but I do not know how.
- I have changed names of one or two elements inside the fmod file, but I do not think it will conflict with Unity program (timelines have the same name)
***

*** Update 09/11 2020, 12:24 by Mark Poulsen ***
- Added E4 tone using same system setup
***

*** Update 12/11 2020, 00:24 by Mark Poulsen ***
- Added Dungeon ambiance
- Added character sounds
***

*** Update 16/11 2020, 18:06 by Mark Poulsen ***
- Changed structure for tones. Still using timelines but using integrated envelope-logic instead of timeline logic
- The two parameters "isSinging" and "wasSingingOtherTone" are redundant and should be deleted
- Wrote post on Discord with documentation for ensuring evelope-logic / AHDSR will play out
***

*** Update 22/11 2020, 20:12 by Mark Poulsen ***
- Have made various implementations to sound. There should be a lot more environment sounds in relation to boxes, torches and bells and such.
***

*** Update 24/11 2020, 17:05 by Mark Poulsen ***
- Have added new tones to replace prototype tones. Instead of one event, Unity needs to reference both _Synth and _Voice at the same time.
- Order is important: A2 (top in wheel), B2, C#3, E, F#3 and A3.
***

*** Update 28/11 2020, 23:18 by Mark Pouslen ***
- Lots of adjustments and mixing, things should generally sound better but will need to test
- Added new tone events to be replace previous ones (at least for trying out a fix to delay problem)
- Added lock sound for lock combinations puzzle, added bump for draw bridge, added whole elevator sequence, added closeToObject for doors, 
***

*** Update 1/12 2020, 12:07 by Mark Poulsen ***
- Added new platform impact sound for tutorial platforms
- Added perc sound for activation whenever a player activates any note. This is to mask the hard cut from short fade-ins while sounding rhythmical
- Mixed the sounds in general
***