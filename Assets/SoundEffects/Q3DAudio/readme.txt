Quick Start:
Import the 3D Audio Unity package like any other Unity Plugin.

Navigate to Edit > Project Settings and select Audio.

Set Spatializer Plugin to QObjectsSpatializer and Ambisonic Decoder Plugin to QSoundfieldSpatializer.

Place a Q3D AudioListener on the same object that has your AudioListener.

Navigate to Window -> Audio Mixer to set up Q3D Audio Groups for sound objects and soundfields.

Add a Group by clicking on the "+" icon as shown below.

You can name Groups whatever you like, for example, "Q3D Sound Object" and "Q3D Soundfield."

Add the 'Q3DAudioObjectOut' Mixer Effect on the "Q3D Sound Object" Group.

Add the "Q3DAudioSoundFieldOut" Mixer Effect on the "Q3DSoundfield" Group.

The Q3D Audio Mixer Effects ignores mixer effect inputs, so it makes sense to place them as the first effect on their Groups, since they ignore the outputs of any mixer effects above them. Instead of getting their inputs from the Audio Mixer, they take inputs from the Spatializer and Ambisonic Decoder Plugins that were selected in the Audio Project Settings. Processing these inputs in parallel rather than using the Audio Mixer's serial model can achieve better performance on supported platforms. However, to make this work in Unity, you must create a sound that outputs to each Group - even though these sounds should never actually play. These "dummy sounds" makes Unity activate these Groups, which in turn, lets the 3D Audio Plugin use less processor time for more sounds.

Create two Audio Sources.

Output one Audio Source to the Q3D Sound Object Group.

Output another Audio Source to the Q3D Soundfield Group.

The Unity project is now set to create spatialized audio.

For a sound object, create an Audio Source as usual.

Add a Q3D Audio Source to it.

Since this is a sound object, click the Spatialize checkbox on the Audio Source to be consistent with Unity's standard workflow. The Q3D Audio Source inherits its Audio Source's Transform and some other Audio Source parameters, which are grayed out next to the Q3D Audio Source-specific parameters.

Soundfields are authored just like sound objects, except you must adhere to Unity's workflow by unchecking the Spatialize checkbox.

Check the Ambisonic checkbox on the Audio Source's AudioClip.

If you now launch your Unity title, the sound object and soundfield that you just created will be spatialized.

To use the 3D Audio Plugin's reverb functionality, first create an Audio Reverb Zone as usual.

Add a Q3D Audio Room to the Audio Reverb Zone.

As with the Q3D Audio Source, the Q3D Audio Room inherits its Audio Reverb Zone's Transform and some of its parameters, which are displayed grayed out next to Q3D Audio Room-specific parameters.

Q3D Audio Rooms behave just like Unity's Audio Reverb Zones, except that the reverb effect's default values can be set on the 'Q3DAudioObjectOut' Mixer Effect itself. So, if the Listener isn't in any Q3D Audio Room, then the reverb effect will be set to the Mixer Effect's values.

We hope you enjoy using the Qualcomm 3D Audio Plugin.


For more information, see:
https://developer.qualcomm.com/software/3d-audio-plugin-unity

For support, please contact nfrost@qti.qualcomm.com


Release Notes from 1.1 to 1.2:
* Unity 5.x supported again out of the box (1.1 broke this).  This was tested on 5.2.0f3, the earliest Unity version that supports spatialization plugins
* q3d_audio_update_1.2.bat now works out of the box (1.1 only worked with tedious user-path creation)
* logarithmic falloff is now the default for sound objects, not linear (to mirror Unity's defaults)
* Brightness parameter is now normalized between 0 and 1 (which is what the underlying code expects)
* additional in-Editor tooltips for clarity

Release Notes from 1.0 (which was released only Qualcomm® Developer Network) to 1.1:
* supports Unity 2018.x (tested on 2018.3.9f1) in addition to Unity 5.x and 2017.x
* allows user to choose, in the Editor, whether reverb runs on the DSP (for Snapdragon 835 or Snapdragon 845 hardware) or the CPU
* fixed bug where 32 samples of silence was sometimes inserted in the middle of the second 256 sample block of a sound
* reverb is now never processed if wetmix=0, saving up to ~1ms per audio frame on the Snapdragon 845 (with reverb on CDSP) on the (recommended) Best Latency setting
* reverb default settings are now correctly set when pushing "play" in the Editor rather than often keeping previous settings
* fixed typos in tooltips
* miscellaneous, minor CPU performance optimization
* various spurious asserts in debug/logging builds fixed