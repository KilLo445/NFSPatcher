@echo off
title Need for Speed: Underground Patcher by KilLo445 [v2.0]
:checkinst
IF EXIST "Speed.exe" (
  goto startpatch
) ELSE (
  echo Please copy this file into your Need For Speed: Underground install location.
  pause
  exit
)
:startpatch
echo Patching...
timeout 1 /nobreak >nul
md temp
echo.
echo Downloading file... [nocd.bat]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/underground/NoCD/nocd.bat', 'nocd.bat')"
echo Downloading file... [US_PATCH_4.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/US_PATCH_4.zip', 'US_PATCH_4.zip')"
move US_PATCH_4.zip temp >nul
powershell Expand-Archive temp/US_PATCH_4.zip -DestinationPath temp
start temp/US_PATCH_4.exe
timeout 3 /nobreak >nul
echo Downloading file... [dimap.dll]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/DirectInputMapper/dimap.dll', 'dimap.dll')"
echo Downloading file... [scripts.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/WidescreenFix/scripts.zip', 'scripts.zip')"
move scripts.zip temp >nul
powershell Expand-Archive temp/scripts.zip -DestinationPath scripts
echo Downloading file... [dinput8.dll]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/WidescreenFix/dinput8.dll', 'dinput8.dll')"
echo.
echo Would you like to open the Widescreen Fix configuration? [Y/N]
set /p widescreencfg=""
if '%widescreencfg%'=='Y' start scripts/NFSUnderground.WidescreenFix.ini
if '%widescreencfg%'=='y' start scripts/NFSUnderground.WidescreenFix.ini
if '%widescreencfg%'=='N' goto finishpatch
if '%widescreencfg%'=='n' goto finishpatch
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