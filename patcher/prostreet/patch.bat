@echo off
title Need for Speed: ProStreet Patcher by KilLo445
:checkinst
IF EXIST "nfs.exe" (
  goto startpatch
) ELSE (
  echo Please copy this file into your Need For Speed: ProStreet install location.
  pause
  exit
)
:startpatch
echo Patching...
md temp
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/prostreet/NoCD/nocd.bat', 'nocd.bat')"
echo.
echo Would you like to install version 1.1? [Y/N]
echo This is only for retail copies of the game, and is optional.
echo The game should still run fine without this patch.
set /p retailupdate=""
if '%retailupdate%'=='Y' goto update
if '%retailupdate%'=='y' goto update
if '%retailupdate%'=='N' goto patch2
if '%retailupdate%'=='n' goto patch2
:update
echo.
echo To update the game to v1.1
echo You must download the updater and
echo Place it into your ProStreet install directory
echo.
echo Where would you like to download it from?
echo [1] MediaFire
echo [2] MEGA
set /p updatedl=""
if '%updatedl%'=='1' goto updatemediafire
if '%updatedl%'=='2' goto updatemega
:updatemediafire
start https://www.mediafire.com/file/ccblzmqvgzy0y4h/dlcpatchrelease_iso.zip/file
goto update2
:updatemega
start https://mega.nz/file/jBoC2SDC#YbqQ_CqnpdudJ8WlNgcmv_lF7HfeNgXX9WD2aAGaqGA
goto update2
:update2
echo.
echo Once the zip file downloads, place it inside your ProStreet install directory
echo (The location you put patch.bat)
echo Then press any key to continue . . .
pause >nul
move dlcpatchrelease_iso.zip temp >nul
powershell Expand-Archive temp/dlcpatchrelease_iso.zip -DestinationPath temp
start temp/dlcpatchrelease_iso.exe
echo.
echo Press any key after patch finishes . . .
pause >nul
echo.
echo Patching...
goto patch2
:patch2
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/prostreet/GenericFix/dinput8.dll', 'dinput8.dll')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/prostreet/GenericFix/scripts.zip', 'scripts.zip')"
move scripts.zip temp >nul
powershell Expand-Archive temp/scripts.zip -DestinationPath scripts
echo Would you like to open the Generic Fix configuration? [Y/N]
set /p genericcfg=""
if '%genericcfg%'=='Y' start scripts/NFSProStreet.GenericFix.ini
if '%genericcfg%'=='y' start scripts/NFSProStreet.GenericFix.ini
if '%genericcfg%'=='N' goto finishpatch
if '%genericcfg%'=='n' goto finishpatch
:finishpatch
echo Would you like to install a No CD Patch? [Y/N]
echo (I would recommend testing the game and making sure it will run before installing one)
set /p nocd=""
if '%nocd%'=='Y' goto nocddl
if '%nocd%'=='y' goto nocddl
if '%nocd%'=='N' goto nocdno
if '%nocd%'=='n' goto nocdno
:nocdno
echo You can install a No CD Patch at any time by running nocd.bat located in the game directory
goto patchcomplete
:nocddl
start nocd.bat
goto patchcomplete
:patchcomplete
@RD /S /Q "temp"
echo Patch complete!
echo Press any key to exit...
pause >nul
exit