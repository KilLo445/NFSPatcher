Need For Speed III Modern Patch v1.6.1 [2016/10/28]
(C) Evgeny Vrublevsky <me@veg.by>
http://veg.by/en/projects/nfs3/

------------------------------------------------------------------------------------------------------------------------
  Key Features
------------------------------------------------------------------------------------------------------------------------

- Full HD and widescreen resolutions support.
- Improved graphics. No more cars with non-rotating wheels, better textures and models for other's cars, much better graphics in the rear view, etc.
- Fog effect and light beams support in the DirectX 6/7 (they were exclusive effects for the 3dfx Voodoo).
- Portability. All system settings are loaded from the ini file. No more registry!
- Compatibility. Most problems with modern Windows were fixed. New system settings were added.
- Full support of multi-core processors. Original game could work correctly only on one core. This change dramatically improves performance of the nGlide and the dgVoodoo.
- Alt+Tab support. You can safely minimize the game when nGlide or dgVoodoo renderer is used.
- Built-in screenshoter. Just press Print Screen key, and a screenshot will be saved into the screenshots subdirectory in the JPG/PNG/BMP format.
- Improved keyboard support in the menu. Now it is much more consistent. You can also use the Tab and Shift+Tab to switch between elements.
- Optimizations. 10 times faster gameplay loading on modern systems. To skip loading animation just press any key.
- Bugfixes. More than 200 changes at all!

------------------------------------------------------------------------------------------------------------------------
  How To Install
------------------------------------------------------------------------------------------------------------------------

1. Copy FEDATA and GAMEDATA directories from the NFS3 CD to a new empty directory.
2. Extract contents of this archive into that directory with replacement of files.
3. Done!

Warning! If you had used original NFS3 before, it is very likely that some compatibility patches are installed in your system. Usually these patches disallow NFS3 to use all cores of your processor. It will be a cause of performance degradation of Modern NFS3, because these patches are installed globally and they affect all copies of NFS3 on your PC. As a result you will have Modern NFS3 with poor performance of original NFS3. If you would like to remove these patches, download and install Microsoft Application Compatibility Toolkit (http://go.microsoft.com/fwlink/?LinkID=82101). Run Compatibility Administrator (32-bit) and see what you have in the Installed Databases. If you will find some patches according to NFS3, just remove them.

------------------------------------------------------------------------------------------------------------------------
  Network multiplayer
------------------------------------------------------------------------------------------------------------------------

Find other NFS3 fans for the network games in our Skype group chat: https://join.skype.com/xLP4dCluGg1s

It is strongly recommended to use the latest version of the game and port 9803 for every player. Port 1030 is just for compatibility with the original NFS3. Modern versions of Windows often occupy port 1030, so Modern Patch suggests you to use 9803 by default.

Every player have to use the same port! If you are a server, ports 1030 and 9803 have to be accessible from the network, so you have to add the appropriate permit rule in your firewall for the nfs3.exe and configure a port forwarding properly. If you're a client, port forwarding isn't required.

If you're using default Windows firewall, run the game and try to create a new network game. Windows firewall will ask you if you wish to allow network access for the nfs3.exe (the dialog will be under the window of the game). So, minimize the game and agree with your system. To configure port forwarding please read the manual of your router and ask your internet service provider if it is available for you at all.

If “The port is already occupied” error occurs when you're trying to create a new network game, possibly, the port is occupied by some program or system service. Use TCPView or “netstat -bn” command to find out which program occupied this port. Probably, you have to close the program or reboot your system to fix the problem.

If the port 1030 is occupied by your system, the most probable cause is Windows RPC services. You can use “netsh int ipv4 set dynamicport tcp start=49152 num=16384” command to change the dynamic client port range for outgoing connections, which is used by the Windows RPC services (don't forget to reboot after it!). Use “netsh int ipv4 show dynamicport tcp” command to view your current values. But these manipulations are strongly not recommended. It is much better to use an updated version of the game and port 9803 everywhere.

------------------------------------------------------------------------------------------------------------------------
  Renderers (thrash drivers)
------------------------------------------------------------------------------------------------------------------------

Recommended:
- nGlide (Glide 3x API emulator) supports fog effect, renders sharp fonts in the menu, uses DirectX 9, supports minimizing and windowed mode (Alt+Enter to switch). Recommended for modern computers with Windows XP/7+.
- dgVoodoo (Glide 3x API emulator) supports fog effect, renders sharp fonts in the menu, supports minimizing and windowed mode (Alt+Enter to switch), uses DirectX 11, but requires very powerful GPU.
- DX7 supports fog effect, but doesn't support minimizing, and FPS may be a little lower than with nGlide or DX8 on the Windows 7+ because of limited Direct3D 7 support. Recommended for old computers with Windows 98/Me/XP.

Other:
- DX8 works smoothly supports fog effect, but doesn't support minimizing, can't display intro video, and on some systems doesn't render dashboard on “In Car” camera view.
- DX6 the same as DX7, but utilizes Direct3D 6. Just for a collection :)
- DX5 is based on the original thrash driver of the NFS3. However, it doesn't support fog effect, it may not work in the Windows 8+, looks worse than others in some details (e.g. artifacts when a large number of spray or dust from under the wheels).
- SoftTri doesn't use hardware acceleration and predictably it looks worst. May not work on Windows 8+. Useful for running the game on virtual machines.
- Glide3x is intended for using with the real 3dfx Voodoo hardware through the Glide 3x API.

------------------------------------------------------------------------------------------------------------------------
  Ini file settings
------------------------------------------------------------------------------------------------------------------------

Language            — language which is officially supported by NFS3 (English/German/French/Spanish/Italian/Swedish). Language files have to be already installed.
NoMovies            — don't display opening movie and demo movies when idle in the menu.
ThrashDriver        — renderer name. For DX7 and DX8 renderers you can specify (without spaces!) @ and number of the needed device (e.g. dx7@0).
IntroSplashTime     — time of displaying of the intro splash image before main menu (in seconds). Use 0 to disable it.
LoadingSplashTime   — gameplay starts too quickly on modern computers, so you can set desired time of loading in seconds. Use 0 to get maximum possible speed. It can be skipped by pressing any key.
Hide16bitModes      — hides 16 bit resolutions if similar 32 bit resolutions are available.
Use32bitModeInMenu  — enables 32-bit rendering in the menu. But movies can't be rendered in the 32-bit modes, so it is recommended to use NoMovie setting also.
AllowHugeTextures   — experimental setting which enables huge texture support for track elements and cars during gameplay. 1 is for 512×512 size limit, 2 is for 1024×1024, and 3 is for 2048×2048.
SingleProcAffinity  — bind game's process to one core, disabled by default, not recommended.
OwnHeapLimitMb      — heap size limit in megabytes. Might be from 16 to 512, 32 by default, in the original version it was 16. Helps to get rid of “Out Of Memory” error.
NoErrorReporting    — enables suppressing of Windows Error Reporting dialog on fatal errors. It isn't recommended.
PreventMinimize     — disables Win key, Alt+Tab, Alt+Esc, and Ctrl+Esc for preventing accidental minimizing of the game. Useful for renderers which don't support restoring after minimizing. Disabled by default.
ScreenshoterEnabled — enables built-in screenshoter feature on Print Screen key.
ScreenshoterFormat  — format of screenshots. Possible values: BMP, PNG, JPG. The JPG allows to specify quality from 0 to 100 just after @ (e.g. jpg@90). PNG and JPG require GDI+ (included in Windows XP and newer, for older systems the gdiplus.dll is needed).

------------------------------------------------------------------------------------------------------------------------
  Command line arguments
------------------------------------------------------------------------------------------------------------------------

-driver=  — renderer name, overwrites ThrashDriver ini setting, format of value is exactly the same as format of ThrashDriver ini setting. Spaces before and after “=” symbol aren't allowed.
-nomovies — don't display opening movie and demo movies when idle in the menu.

------------------------------------------------------------------------------------------------------------------------
  thrash.ini settings (a file inside each driver subdirectory)
------------------------------------------------------------------------------------------------------------------------

[THRASH] section:
File — driver DLL filename.
Type — driver type (d3d/voodoo/software/none).
FogSupport — have to be “1” if the renderer supports the fog effect.

[DDRAW] section (acceptable only for the DirectX 5/6/7 renderers):
DisableMaxWindowedMode — enables “DXPrimaryEmulation -DisableMaxWindowedMode” compatibility mode, may fix performance problems on Windows 8+.
NoDwmOffForPrimaryLock — prevents turning off of the DWM when the game is started in the windowed mode, but in this case the screenshoter will not work.

------------------------------------------------------------------------------------------------------------------------
  nGlide settings in the ./drivers/nglide/thrash.ini
------------------------------------------------------------------------------------------------------------------------

nGlide renderer has additional settings in the according thrash.ini file. This is done by setting NGLIDE_* environment variables before glide3x.dll loading. Every setting is gotten from the [ENV] section. If you would like to use global nGlide settings, just remove [ENV] section in the ./drivers/nglide/thrash.ini file.

nGlide v1.05 settings:
- NGLIDE_RESOLUTION — always use desktop resolution instead of game's resolution (0 or 1).
- NGLIDE_ASPECT — preserve aspect ratio when desktop resolution is used (0 or 1).
- NGLIDE_VSYNC — enable vertical synchronization (0 or 1).
- NGLIDE_GAMMA — gamma setting (from 0 to 10, 5 by default).
- Additional info can be found at the official nGlide forum: http://www.zeus-software.com/forum/viewtopic.php?f=9&t=796

------------------------------------------------------------------------------------------------------------------------
  Interesting statistics
------------------------------------------------------------------------------------------------------------------------

- 25000+ lines of assembly code were written for this patch.
- 181+ days of reverse-engineering and coding.
- 40 sheets of A4 paper were covered with writing during development.

------------------------------------------------------------------------------------------------------------------------
  Known Problems
------------------------------------------------------------------------------------------------------------------------

Original code of the Need For Speed III is very old and it completely not adopted for modern Windows. All of these problems are from the original game.

- The game have to have write access to its directory because NFS3 uses it to save all data. So, you can't install this game into Program Files on modern Windows with UAC.
- If you have graphical glitches with headlights in night driving mode, go to advanced graphics settings and switch “Headlights” setting from “Projected” to “Vertex”. This problem is occurs on some GPUs.
- Fog doesn't work with the DX6 renderer when the game is runned in Wine/Linux. Instead of this horizon is corrupted. Disable the fog effect in the graphics settings or use nGlide renderer.

------------------------------------------------------------------------------------------------------------------------
  Thanks
------------------------------------------------------------------------------------------------------------------------

- Thanks Zeus (http://www.zeus-software.com/) for the nGlide.
- Thanks Dege (http://dege.fw.hu/) for debugging the DX6 renderer, and for the dgVoodoo.
- Thanks kruto (http://kruto.deviantart.com/) for a great icon.
- Thanks Kirill Diduk, Gonzalo's, and kgt_jp for help with translation of the new strings.
- Thanks BrainRipper, Goblinit, Ivan_83, kai, Kerouha, Manticore, and RejZoR for testing.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.6.1 [2016/10/28]
------------------------------------------------------------------------------------------------------------------------

- Fixed the problem with Texel Alignment in the DX6, DX7 and DX8 renderers, so everything looks sharp, like when Glide3x is used.
- DX8 supports fog effect. Thanks to Verok for investigation.
- The DX7 graphical glitches like the black polygon near the Old Church were workarounded.
- DX7 doesn't output 640×480×16 if this mode isn't supported. Also it returns sorted list of resolutions now.
- A small optimization of texture loading. The thrash_about isn't called when it is not really needed.
- More strict checking of validity of a thrash driver (a renderer).
- New error messages for some problematic situations: too long path to the root directory (it has not to be longer than 210 characters); thrash driver not found.
- DirectDrawLagFix was renamed to DisableMaxWindowedMode in the thrash.ini files. It is disabled by default now.
- A new DirectDraw compatibility flag NoDwmOffForPrimaryLock was added to the thrash.ini files.
- The game asks a renderer if it supports the fog effect. DX5 and SoftTri always report that they do not support the fog effect.
- Refactoring of the video mode init code. Video mode restore function will not be called twice at exit.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.6.0 [2016/09/22]
------------------------------------------------------------------------------------------------------------------------

- Automatic View Angle (FOV H+ mode).
- More accurate vertical view angle.
- Camera doesn't shift down when frame is wide (to match the FOV H+ better), the horizon is always in the middle of the screen.
- Frame Size setting was extended to be able to return this camera shift.
- Proportions of the image in the mirror are the same regardless of the aspect ratio of the selected resolution.
- Size of the borders and map points are calculated based on the vertical resolution.
- More accurate positions for the following HUD elements: Rear View Mirror, Cop Detector, Analog Speedometer and Tachometer.
- Cabin image in “In Car” view now rendered 0-3 pixels wider (depending on the resolution) for get rid of chinks on the edges of screen.
- Ferrari, Lister and Spectre logos were added to the flying logos animation in the menu.
- Ford, HSV, Lister and Spectre logos will not be used in the animation if these addon cars aren't installed.
- When gameplay loading is aborted using Esc key, "Aborted" string will be displayed for 200ms (it was just a frame before).
- FPU always uses extended precision for all calculations.
- The game checks if proper version of language files is used.
- dgVoodoo v2.5.3 (Napalm).

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.5.3 [2016/05/03]
------------------------------------------------------------------------------------------------------------------------

- Fixed a bug of the original game which was a cause of different volume of the race music each race.
- Fixed a bug of the original game which was a cause of ignoring of the latest Rock track and all Techno tracks when Random race music is choosed.
- Added two additional random race music modes: Random (Rock) and Random (Techno).
- Added two additional random menu music modes: Random (Fast) and Random (Slow). Fast: Romulus 3, Triton, Pi. Slow: Minotaur, Whipped, Whacked.
- Random race music actually works as a playlist (each race a new track). It prevents from repeating same music tracks frequently.
- Prevented double music fading in the audio settings when user presses Race button.
- Fixed a regression of the v1.5.2 which was a cause of problems with windowed mode of the nGlide.
- Fixed a regression of the v1.5.1 in the fallback code for the unupdated language files.
- Menu music volume will be used for the movies.
- Proper handling of the case when EAX was presented in the system and choosed by user, but for some reason had become unavailable.
- Sound Volume and Dead Zone slider values are rounded correctly.
- More accurate time measure function is used (timeGetTime instead of GetTickCount).
- HSV and Ford logos from the LOGO2.QFS aren't used in the menu animation if there is no cars from the Australian Edition.
- Fixed a problem which was a cause of the inability to start the game on some systems.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.5.2 [2016/04/10]
------------------------------------------------------------------------------------------------------------------------

- Better Rear View Detail setting. Light from the car headlights and cop flashers will be rendered even on Medium value of this setting. View Distance and Car Detail in the Rear View Mirror now depends on Rear View Detail also. So, if you have performance problems with High level of the Rear View Detail setting, just use Medium. It have to be much more usable now.
- Better compatibility with the Thrash API v107 according to fog. So, the DX7 renderer can render fog in the NFS3 now.
- Fixed a bug which was a cause of config reset after loading of ghost files from the other versions of the NFS3.
- Fixed a bug which was a cause of using player name from the loaded ghost file instead of the current player name.
- Step size for the Alpha Intensity and sound volume sliders is 5%.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.5.1 [2016/03/25]
------------------------------------------------------------------------------------------------------------------------

- “Rear View Camera” setting which allows to lock the rear view camera in the car. It greatly increases visibility and reduces the blind spot when the current main camera is not “In Car”.
- Fixed a bug which had allowed to save duplicated cameras when the user presses "Race", "Credits", or "Exit game" buttons instead of the "Done" button.
- Experimental ini setting AllowHugeTextures which enables huge texture (512×512, 1024×1024, and 2048×2048) support for track elements and cars during gameplay. Useful for the game modding.
- Fixed a small bug in the DX5, DX6, and SoftTri renderers in the thrash_lockwindow function.
- Fixed mistakes in new strings in German, Spanish and French languages. Thanks Gonzalo's and Kirill Diduk for hints.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.5.0 [2016/03/20]
------------------------------------------------------------------------------------------------------------------------

General:
- No more non-rotating wheels in the rear view mirror and in the split screen mode (when Car Detail is High).
- Rear View Mirror can display clouds, lightnings, and light from the headlights of cars and from the flashers of cops.
- Rear View Mirror can use better 3D models of the track, so the problem with the holes in the objects (displayed in the mirror) is solved now.
- “Rear View Mirror” detail setting. Low is equivalent to the original; Medium is for better track 3D model and view distance; High is for the best track 3D model, clouds, lightnings, and light from the car headlights and cop flashers rendering.
- Better proportions of the objects in the rear view mirror.
- Fixed direction of the camera of the rear view mirror. The horizon is on the middle of the mirror now (for a better view).
- Fixed a small horizon glitch at the edge of a screen when the wide view angle is enabled.
- An ability to enable or disable the fog effect directly from the graphics settings menu.
- An ability to set volume of menu sounds separately from the volume of the ingame special effects.

Engline limits:
- No more "Render_GetTm() raM out of raN" error for high poly cars. Corresponding buffer is increased from 0.8 MiB to 8 MiB.
- 512×512, 1024×1024 and 2048×2048 textures support in the voodoo2a.dll. Support of such textures was introduced in the 3dfx Voodoo 4 and 5.
- The limit of texture size for track elements was expanded from 128×128 to 256×256 pixels in this release.
- Support of the big 512×512 and 1024×1024 textures for cars and track elements still available only in beta versions of the patch.

HUD:
- Smooth edges of the cop/player points on the map.
- Gear numbers are right-aligned now, like the digital speedometer.
- Gear numbers use smaller font if there is not enough space for them.
- Opponents List and Speeds List have standard bevel when SoftTri is used (as it was for other renderers).

Menu:
- An ability to enter digits from the Num Pad when Num Lock is on.
- Commas are treated as dots when parsing an IP address.
- HUD preview in the settings now arranged well.
- Volume and Alpha sliders display own value (in percents).
- Logos from the LOGO2.QFS (if it exists) are used for animation in the menu (together with logos from the LOGO.QFS). Original version of the game have this file, but it is unused.
- Animated logos of automobile manufacturers are disappearing more smoothly.
- Fixed rendering piority errors for the top dropdown lists in the the “Location” and “Opponents” menus.
- “Car Showcase” subtitle and “Compare All Cars” subtitle are displayed using the same font as the “Race Results” subtitle.
- Other insignificant improvements.

DX7 renderer:
- DX7 renderer is included into this patch. Just for a collection :)
- Based on the dx7z.dll from the Motor City Online.
- Dithering is disabled by default.
- Resolutions under 640×480 are hidden.

Other:
- Gameplay loading animation takes into account time of preliminary synchronization of a network game.
- IntroSplashTime ini setting, which allows you to set duration of the intro splash image.
- Fixed a problem with discolored light from the taillights in the menu when DX6 renderer is used and fog support is enabled.
- Fixed a bug which cause a crash when using the screenshoter feature on Windows XP.
- The game doesn't change resolution to 640×480 when “exit to system” is used during the gameplay, so closing the game is faster in this case.
- Updated version of nGlide with fixes according to the NFS3 is included.
- dgVoodoo v2.5.1 (Napalm).
- Fixed a bug in the SoftTri renderer. Now it outputs all available resolutions.
- DX8 renderer outputs 16-bit resolutions also. However, it still can't play movies at all.
- nfs3.exe sorts resolutions that it gets from the renderer. Refactoring of the graphics initialization and resolution choosing code.
- If ini file with the same name as a executable file name (e.g. nfs3test.ini for nfs3test.exe) is not found, nfs3.ini will be used.
- Hide16bitModes ini setting which hides 16 bit resolutions if similar 32 bit resolutions are available.
- Use32bitModeInMenu ini setting which enables 32-bit video mode for the menu. However movies can't be played in 32-bit video modes.
- All renderers can use any DLL wrappers from the own subdirectories on the Windows XP SP1+.
- DDRAW.DLL is removed from imports of the nfs3.exe and voodoo2a.dll.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.4.0 [2016/02/16]
------------------------------------------------------------------------------------------------------------------------

Multiplayer:
- TCP port 9803 is used by default, because original port 1030 often is occupied by Windows, so a network game can't start.
- You can change the port in the “TCP connection” menu. You can choose between 1030 (legacy) и 9803 (modern). Every player have to use the same value!
- You can specify a port after “:” symbol when joining IP game. If not specified, a value from the previous dialog will be used.
- When you're trying to open TCP multiplayer game menu, “The port is already occupied” error message will be shown if needed port is occupied by other application.
- You can join to other's game even when the same port on your system is occupied. Original game doesn't allow to do this.
- For compatibility with the original NFS3 use 1030, but it is not recommended because this port often is occupied by Windows.

Menu:
- Don't allows to send empty chat messages.
- Tab and Shift+Tab can be used to switch between controls of the menu.
- Tab, Shift+Tab, and Esc work even when multiplayer chat is in the focus, so you can switch focus to another control without using your mouse (original game doesn't allow it).
- All menu sounds have same volume.
- Consistent sounds for the mini dialogs when using the keyboard.
- Consistent sounds for the top dropdown lists when using the keyboard (the same as for general dropdown lists).
- A car color dropdown list uses consistent (with other controls) sounds and has a normal padding.
- List of replays works correctly using keyboard (fixed an invalid positioning of the mouse cursor). Also it uses consistent sounds (like others).
- Fixed errors of element switching order (when keyboard is used).
- 3D view of a car interior can be stopped using Space key. Arrow keys change rotating speed more smoothly now.
- Options button is placed near Exit button on the first menu screen (Game Setup).
- “Connect Players”, “Connect IPX” and “View Car Interior” menu screens also have standard set of buttons (Exit and Options).
- This set of buttons don't disappear in the Options when they are opened directy from the race results screen.
- This set of buttons is smart: when user opens Options from the first screen without choosing game mode, the Race button are hidden.
- Drop down list of cars displays the class of the current car not only for the 1st player, but also for the 2nd player.
- You can use Left and Right keys to switch slides in the car slideshow and in the credits (inclusive 3D panoramas of the developers).
- Noticeable “More...” button in the credits (opens the 3D panoramas of the developers).
- Credits page with the 3D panoramas of the developers has same controls, as general credits (with Back and Next buttons).
- Increased quantity of visible elements in some long drop down lists (resolution list, car lists).
- Fixed unaligned paddings of the all drop down lists.
- “Reset To Defaults” button in the Controllers menu was shifted one position down.
- “Predefined Keys”, “View Car Interior”, “Load”, and “IPX connection” are aligned well.
- NoMovie ini setting also disables demo movies on menu idle.
- Demo movies aren't starting when the game is minimized in the menu mode (useful for the nGlide driver).
- If demo video files are removed, the game doesn't try to play them.
- Length of a track in the track info is displayed in units, which is choosed in the HUD of the 1st player.
- “Wide Screen” setting was renamed to “2:1 Field of View”, just to avoid ambiguity.
- The top dropdown with the player type selection is always displayed in the HUD settings.
- Fixed bug, when previous text was lost on first key press when entering player name or IP address.
- A hint about an ability to abort using Esc button in the “Connecting...” window.

Thrash drivers:
- Refactoring of the Voodoo thrash driver. 32bpp support for 3dfx Voodoo 4 or 5 was added.
- DX6 thrash driver outputs 16bpp resolutions and sorts them.
- DX5 thrash driver outputs 16bpp resolutions, limit of resolutions count is expanded from 30 to 100.
- Refactoring of the thrash driver initialization code.
- No more hardcoded environment variables for the 3dfx Voodoo.
- Instead of this the game sets environment variables from the [ENV] section of the current thrash.ini file.
- The nGlide uses general voodoo2a.dll now, its settings can be changed using the [ENV] section of the thrash.ini.
- Added separate Glide3x thrash driver for using on the real 3dfx Voodoo hardware.
- VideoDriver ini setting was renamed to ThrashDriver (original name). Filenames of all thrash drivers also were renamed using original format.
- Included glide3x.dll from the gGlide v1.05.
- Included glide3x.dll from the dgVoodoo v2.5.1 (Napalm, WIP16).

Screenshoter:
- Uses Print Screen key instead of inconvenient combination Alt+P.
- Does one screenshot per second when Print Screen is held for long time instead of hundreds of screnshots without delays.
- Saves screenshots in the BMP format (instead of rare TGA).
- Better quality of 16bpp screenshots (original code converts RGB565 to RGB555 because of limitation of the TGA).
- Smaller files for 32bpp screenshots because BMP allows to drop alpha channel, so the result is stored as 24bpp.
- Ability to save screenshots in the PNG or JPG formats (use ScreenshoterFormat ini setting).
- Saves screenshots to the screenshots subdirectory instead of the Desktop directory.
- Remembers last screenshot number. If you would like to reset this counter, just empty screenshots directory.
- Makes shutter sound while screenshoting.
- Saving a screenshot file was moved to a separate thread, so the game have to freeze for less time while the screenshot is taking.
- Ability of making screenshots of the gameplay loading screen.
- Screenshoting doesn't stop intro movie.

Other:
- Super quick gameplay loading on modern systems (thanks Zeus for a hint).
- LoadingSplashTime ini setting for slowing down gameplay loading, set desired time in seconds or 0 to disable this feature (and get maximum possible speed).
- If this “splash” is enabled, it can be skipped just by pressing any key (but Esc aborts loading).
- The View Angle setting allows to enable wider view angle, which is useful for the widescreen resolutions. New set of options: Narrow, Normal (former Wide), Wide.
- It is possible to show rear view mirror when viewing replays (using F7).
- Autimatic creation of empty directories for saving game data.
- Working directory is set automatically to the directory of the exe file.
- Useless RemoteOnly ini setting was removed.
- The KeyboardHook ini setting was replaced by the PreventMinimize. Now it works on Windows NT also.
- Better code for the system cursor hiding, as result the cursor isn't hidden in the standard system menu of the window.
- Language files with text strings are included, so the patch is compatible with rare prerelease version of the game now.
- Opponent Name HUD element was moved to the top of screen, just under the rear view mirror.
- Reset HUD just after installation of this patch over the original version of NFS3.
- Zoom of cop's minimap will be the same as zoom of racer's minimap (by default, can be changed).
- i586 and newer are recommended. The game displays a warning for older systems.
- nfs3.exe has an application manifest now.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.3.1 [2015/07/14]
------------------------------------------------------------------------------------------------------------------------

- DirectDrawLagFix is readed from a thrash.ini of according driver, not from the nfs3.ini. So, this setting isn't applied for drivers that doesn't require it. This option is useful only for drivers that use DirectX 5/6/7 on Windows 8+.
- A small optimization in the voodoo2.dll and nglide.dll: grGlideInit and grGlideShutdown functions aren't called when display mode is changing.
- Windows 98/Me support was returned.
- If you choose the biggest available resolution, when resolutions list is changed (e.g. when you are using external display with notebook), the biggest resolution will be choosed automatically from the new list.
- KEY_READ access right is used instead of KEY_ALL_ACCESS for reading path to desktop from the registry for saving a screenshot (Alt+P).

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.3.0 [2015/06/22]
------------------------------------------------------------------------------------------------------------------------

Changes in the nfs3.exe:
- Hangs on multicore systems were completely fixed, so you don't have to use SingleProcAffinity kludge now. The game works much smoother without this kludge (expecially with nGlide and dgVoodoo drivers).
- Unexpected game termination at the end of gameplay loading was fixed. (Thanks Zeus for help with debugging.)
- Multiplayer game bug were fixed. It had been introduced by the first version of this patch. (Thanks Ivan_83 for testing.)
- HUD was updated a little.

Menu changes:
- The Wide Screen setting has no any relation to proper widescreen support, so its location next to the Resolution setting is confusing. This is why this setting was moved from the Graphics to the Advanced Graphics settings. Now it's placed near the View Angle setting because it is more logical. The View Distance setting was moved to freed space in the Graphics settings.
- Menu subitems in the Controller and in the Connect menus aligned vertically like in the other menus.

Other changes:
- nGlide is updated to version 1.04.3. Now you can safely minimize and restore NFS3. (Thanks Zeus for it.)

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.2.0 [2015/06/10]
------------------------------------------------------------------------------------------------------------------------

Changes in the nfs3.exe:
- If ini file hasn't some setting, program tries to read environment variable with name like SECTION_SETTINGNAME, e.g. for setting NoMovie in the [NFS3] section environment variable name will be NFS3_NOMOVIE. If environment also isn't present default value will be used.
- Fixed a bug according to race type autoselection in the race selection dialog (Hot Pursuit and Tournament were mixed up).
- Mercedes and Ferrari cars aren't locked in the Hot Pursuit mode. It seems that it had been done because of licensing restrictions. Thanks to Manticore for a hint.
- Structure of thrash video drivers was changed. All drivers stored in “drivers” directory. Every driver has own subdirectory with name which is used in VideoDriver ini setting. Inside this subdirectory have to be dll file of the driver and file thrash.ini with information about this driver (File and Type are required fields, type may be null, d3d, voodoo, or software). This change allows to save several different Glide API emulators to neighbour directories and use them when needed.
- Some thrash driver capabilities could be set in thrash.ini: FogSupport and LightBeams. For example, it allows to override hardcoded values and allow to use fog for d3d thrash driver (if this driver supports it). It allows to use fog in the dx6 thrash driver from NFS4/3DSETUP.
- Thrash driver is loaded only once.
- When game can't init thrash driver error message box is displayed. In the original version there was no error reporting.
- Resolutions count limit was extended up to 100.
- Fixed a bug of the first release of this patch: returning to the main setup menu instead of a menu of a current game mode.
- Added an ini setting DirectDrawLagFix which fixes performance problems of DirectX 5/6/7 in Windows 8+ via enabling “DXPrimaryEmulation -DisableMaxWindowedMode” compatibility mode (enabled by default).
- Added an ini setting KeyboardHook which allows to disable keyboard hook that has been used by the game for blocking Win key and keystrokes like Ctrl+Esc, but this code doesn't work in Windows 7+ so it is better to disable it (disabled by default).
- Slightly better multilanguage support. When language directory is absent in the ./GameData/Audio/Speech/ directory the game doesn't crash and uses sound files from the English subdirectory. Also Swedish language can use own speech sounds (original game uses English sounds for it) now. The name of the language directory have to be Swedish, and the one-letter code in file names is W.
- config.dat was moved to its original destination (./FeData/Config/) for preserving settings of original NFS3 when updating. However, graphics settings are reset to their defaults in this case (because format is changed a little).

Changes in the dx6.dll thrash driver:
- Based on the 3dsetup/d3da.dll from the NFS4, supports fog effect in the NFS3.
- Fixed a bug according to non-working fog effect on modern systems. (Thanks Dege for debugging this driver and much valuable information about Direct 3D.)
- Fixed a crash when system supports too many texture formats. (Also thanks Dege.)
- Resolutions count limit was extended up to 100.
- The driver hides resolutions with height lower than 480 pixels now.
- 640×480 always reported as 16-bit because intro movie doesn't works in 32-bit mode. Other resolutions are reported as 32-bit if they are available.

Changes in the voodoo2.dll thrash driver:
- Firstly file glide3x.dll is searched in the same directory from where voodoo2.dll is loaded, then it is searched in current directory, and then in system directory. This change allows to save several different Glide API emulators to neighbour directories and use them when needed.
- Thrash driver voodoo2.dll distributed with two different Glide API emulators: nGlide and dgVoodoo. You have to choose nglide or dgvoodoo instead of voodoo2 in the VideoDriver ini setting.
- If you would like to play NFS3 on the real 3dfx Voodoo or 3dfx Voodoo 2 hardware, create new subdirectory with name voodoo2 in the drivers directory, copy into this subdirectory thrash.ini and voodoo2.dll from the dgvoodoo subdirectory, set VideoDriver ini setting to voodoo2.
- glide3x.dll is loaded only once.
- Resolutions count limit was extended up to 100.

Changes in the nglide.dll thrash driver:
- It's a slightly changed version of voodoo2.dll with special code for the nGlide wrapper. It's created to prevent unwanted using global settings of nGlide.
- This is done by setting NGLIDE_* environment variables before glide3x.dll loading. Every setting is gotten from the [NGLIDE] section of according thrash.ini file. Each setting of this section is formatted like NGLIDE_SETTINGNAME and used as name of an environment variable. Names of settings and their formats can be found at the official nGlide forum: http://www.zeus-software.com/forum/viewtopic.php?f=9&t=796
- If you would like to use global nGlide settings, just remove [NGLIDE] section in the ./drivers/nglide/thrash.ini file.
- Bundled with nGlide v1.04.2 (this version also fixes possible problems with projected headlights).

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.1.0 [2015/05/22]
------------------------------------------------------------------------------------------------------------------------

Changes in the nfs3.exe:
- VideoDriver ini setting now accepts a name of a driver file, not one of the predefined values. Thus, you can put several thrash video drivers of the same type in the game's directory and switch between them with a simple changing of the ini setting, without renaming files. It is important to understand that the game uses drivers in different ways depending on the type. The type of driver is determined by the first letter of the name of the driver file (d — d3d; v — voodoo; s — software; the rest — none).
- VideoDeviceId ini setting became a part of VideoDriver. If it is necessary to specify device number, just put “@” and device number just after the driver name (e.g. dx8@0).
- -voodoo2, -d3d and -softtri command line arguments are replaced by one universal -driver=. This argument has the same format as VideoDriver ini setting.
- Improved validation of thrash video drivers.

Changes in the dx8.dll thrash driver:
- Original dx5.dll not always works on modern operating systems (e.g. Windows 8.1). That's why dx8.dll was created.
- Based on the dx8z.dll from the Motor City Online (uses DirectX 8).
- The driver hides resolutions lower than 640×480 pixels.
- The driver reports resolutions in a slightly different order.
- The problem with ugly 16-bit menu was fixed by using 32-bit mode.
- The problem with non-working video still not solved. It's recommended to disable the intro video by the NoMovie ini setting.

Changes in the voodoo2.dll thrash driver:
- Renamed from voodoo2a.dll to voodoo2.dll.
- Always reports that all resolutios are 16-bit even when nGlide is used. Otherwise making screenshots by pressing Alt+P will not work because in low-level all Glide resolutions are still 16-bit modes from the game's side.

Changes in the dx5.dll thrash driver:
- Renamed from d3da.dll to dx5.dll similar to dx8.dll, because it uses DirectX 5.

Changes in the softtri.dll thrash driver:
- Renamed from softtria.dll to softtri.dll.

------------------------------------------------------------------------------------------------------------------------
  What's new in v1.0.0 [2015/05/17]
------------------------------------------------------------------------------------------------------------------------

Support of widescreen and other resolutions:
- All resolutions with aspect ratio from 5:4 to 16:9 (inclusive 4:3 and 16:10) are supported.
- All objects will be properly scaled for every aspect ratio and you will see them in correct proportions.
- There is no unreasonable limits for maximum resolution anymore (e.g. for D3D it was 800×600), you can use almost every resolution which is supported by your system (e.g. 1920×1200).
- Font size is choosed on the basis of screen height, not screen width. It prevents from choosing too big font for widescreen resolutions.
- HUD will be completely recalculated with the purpose of different aspect ratio support when resolution is changed in settings. Besides the result looks more accurate than original one even on 4:3 resolutions. Files ./GameData/DashHud/*.POS are unnecessary now and can be removed.
- HUD borders was reduced twice to look better on modern huge resolutions. Without this change they look too fat.
- Compact cop's speeds table. Now it looks like the standard opponents list.
- As a whole positions of all HUD elements were adjusted a little to look better on all supported resolutions.
- Resolutions list limit was extended from 20 to 50.
- The games uses the best possible graphics settings by default when it detects that the game is runned on the modern computer.
- On first run game tries to choose gameplay resolution nearest to current Windows default resolution.

Better graphics:
- New “High” car detail level. New quality of graphics for cars other than 1st player. There is no non-rotating wheels and disappearing side-view mirrors! Also the game uses twice better textures for all cars. Even traffic cars looks twice better. Also it fixes unfairness when 2nd player's car has worse graphics. Old “High” car detail is “Medium” now, old “Medium” is “Low” now, old “Low” was removed. Warning! You have to restart the game when you've changed car detail level to see full effect because the game loads textures only once when loading gameplay.
- Much better “Full” view distance level. Now it also affects rear view mirror (in “Far” and “Full” levels).
- View distance in “Full” level renders farther in splitscreen and night driving modes, the same as in singleplayer day driving mode.
- “Alpha Intensity” setting works for Voodoo2 mode now. Cars look too shining without it.
- A little better rendering of an image when loading gameplay. An attempt to improve quality of scaling of this image.
- An attempt to get rid of glitch lines near some sprites (e.g. in exit dialog near NFS logo).
- Cabin image in “In Car” view now rendered for a pixel bigger in every dimensions for get rid of chinks on the edges of screen.
- Because of fixed hardware detection bug quality of the starting movie will be much better (in comparison to original version) on modern computers.

Portability:
- The game reads system settings from ini file now. No more registry! Name of the ini file have to be exactly like the name of game's exe file, but with *.ini extension. For example, for nfs3.exe it have to be nfs3.ini.
- The game automatically binds own process to one core for avoiding hang, so there is no more reasons to install any SDB compatibility files to run this game. This behaviour might be disabled, see SingleProcAffinity setting in ini file.
- The game doesn't require file install.win which was created by NFS3 installation utility. All settings which were in this file were moved to ini file (Language and RemoteOnly options).
- You can choose thrash video driver from ini file, simply set VideoDriver setting to D3D, Voodoo2, or SoftTri. You can set device number in VideoDeviceId setting (but it affects only D3D mode, as in original version of NFS3).
- You can disable all intro movies by NoMovie setting in ini file.
- Suppressing of displaying Windows Error Reporting dialog on fatal errors is disabled by default. So if something go wrong you will know about it. But you can enable suppressing of error reporting via NoErrorReporting setting in the ini file.

Bugfixes:
- Internal heap memory limit was extended from 16 to 32 MiB. It have to solve “Out of memory” problem on some rare cases. Also you can control it by OwnHeapLimitMb ini setting (it may be from 16 to 512).
- Bugfix for situation when you are cancelled editing audio setting, Engine Volume wasn't restored and set to maximum value, and Sound Effects also sometimes wasn't restored to original value.
- Fixes for hardware detection function. Original game by mistake tries to simplify intro video graphics for modern computers because internal benchmark thinks that it is 166MHz processor (signed integer, result of benchmark too big and it is treated as negative). This benchmark was replaced to cpuid which sees processor family (i586/i686 or newer), support of MMX/SSE/SSE2, and in result calculates class of processor which is used by game for choosing default settings and deciding which level of video quality user will see.
- Bugfix for error when game says that on your HDD is less than 1 MiB is free which is not true.

Default settings:
- Use km/h for speedometers by default.
- Two camera views: Heli Cam and In Car (you can restore original set in camera settings).
- Mini map has 1x scaling for cops and 3x for racers by default.
- Opponent list is displayed by default in splitscreen mode (instead of opponent name which looked a little confusing).
- “Romulus 3” is used by default for menu soundtrack (but you can change it in audio settings).

Other changes:
- New icon in variants from 16×16 to 256×256 pixels. Old icon also available and you can set it for your shortcut in properties.
- Smooth loading indication. Original game does almost nothing between 58% and 100%.
- Original game makes a copy of music file when loading gameplay, maybe to solve problem with slow CD-ROMs in 90's. Now game plays gameplay music directly from source file. It saves a little time and 10 MiB of resource of your SSD on every gameplay start.
- Num Lock, Caps Lock и Scroll Lock aren't turned off automatically on every game start.
- The game doesn't requires CD anymore.
- The game doesn't try to revert focus every time when it was loosed because it might be a cause of impossibility to kill game process in some situations when the game continuously steals the focus from Task Manager.
- Smart installation checking (it checks existance of some critical game data directories).
- Some error message boxes were fixed a little. All errors have “error” icon, all warnings have “warning” icon, all fatal errors are displayed with one button “Ok”, not “Ok” and “Cancel” where both buttons do same thing.
- Information about all unworked debug arguments were hidden in command line help dialog (you can run it with -h argument).
- Game's config was moved from ./fedata/config/config.dat to root directory (you can delete ./fedata/config/).
- The game doesn't removes config.dat, player1.spc, and player2.spc files when exe file is updated.
- The game doesn't resets video settings when video driver is changed.
- “About” slide was added to Credits.

Menu changes:
- Cone Stats and Tuturial Icon widgets are hidden in HUD editor for cops because these widgets can't be used in this mode.
- Buggy background animation in graphics settings and opponents choosing screens were fixed.
- Title in pause menu is more aligned now. In original version of the game it was a little misplaced.
- Flying brands effect added to all slides without large amount of text for consistency. For example, in original version this effect is present on main singleplayer game screen, but it is absent on main splitscreen game screen.
- On the first menu screen irrelevant link to Need For Speed website was replaced to “Credits...” button. Really, you can't find any mention about NFS3 on this website for 10 years or even more.
- In choose car menu “Download car...” button is hidden because it doesn't work properly anymore.
- Yellow rect which indicates current player in HUD settings screen doesn't overlaps other controls.
- Standard bottom set of buttons was added to camera settings screen.

Changes in the voodoo2a.dll thrash driver:
- Based on voodoo2a.dll from the Future Cop L.A.P.D. game. It seems that it is a slightly updated NFS3 driver which uses Glide3x API instead of Glide2x API. So it seems that these drivers are highly compatible and all special effects are present. Original voodoo2a.dll from NFS3 uses Glide 2x API which doesn't have ability do detect supported resolutions. Official update of this file which uses Glide 3x API is based on NFS4 and it seems that it lacks fog effect. So it doesn't fit us.
- Added support of huge Glide resolutions with ability to use Z-buffer. Now list of supported Glide resolutions are: 640×480, 800×600, 960×720, 1024×768, 1280×1024, 1600×1200. Widescreen resolutions aren't possible with standard Glide API. This list is used when extended Glide API isn't supported.
- Jointly with the author of nGlide wrapper we are extended Glide API to support all possible resolutions. So if voodoo2a.dll detects that this extended Glide API is supported then it displays all possible resolutions (inclusive widescreen ones).
- nGlide 1.04.1 with support of extended API is included in this archive.
- If you would like to use other Glide wrapper with support of all resolutions, please write to author of this wrapper a suggestion to add support of extended resolutions Glide API. This extension is easy to implement and it should not take a long time.

Changes in the d3da.dll thrash driver:
- Based on the original d3da.dll from NFS3 (works on DirectX 5). Newer versions of this driver not fully compatible with NFS3 and may be a cause of graphical glitches like huge black polygon near Old Church in Hometown.
- The driver hides resolutions with height lower than 480 pixels now. It frees space in the list for modern resolutions.
- The driver works in 32-bit mode if it is available in the system. It improves quality of dust or splashes from wheels.
- 640×480 always reported as 16-bit. Original title movie doesn't works in 32-bit mode.
- Light beams effect now works in D3D mode. In original version this effect available in Voodoo2 mode only.

Changes in the softtria.dll thrash driver:
- Based on the original softtria.dll from NFS3. Newer versions of this driver render worse image or crash.
- The driver reports only 16-bit resolutions now. Original version was reported that 32-bit resolutions are supported, but it isn't true. This driver can't work in 32-bit modes.

------------------------------------------------------------------------------------------------------------------------

P.S. Sorry for my English, it isn't my native language. Please contact me if you wish to help to improve this readme.