@echo off
title Need for Speed: Most Wanted Patcher by KilLo445 [v2.0]
:checkinst
IF EXIST "speed.exe" (
  goto startpatch
) ELSE (
  echo Please copy this file into your Need For Speed: Most Wanted install location.
  pause
  exit
)
:startpatch
echo Patching...
timeout 1 /nobreak >nul
md temp
echo.
echo Downloading file... [nocd.bat]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/mw/NoCD/nocd.bat', 'nocd.bat')"
echo Downloading file... [nfsmwpatch1.3.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/nfsmwpatch1.3.zip', 'nfsmwpatch1.3.zip')"
move nfsmwpatch1.3.zip temp >nul
powershell Expand-Archive temp/nfsmwpatch1.3.zip -DestinationPath temp
start temp/nfsmwpatch1.3.exe
timeout 5 /nobreak >nul
echo Downloading file... [dinput8.dll]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/WidescreenFix/dinput8.dll', 'dinput8.dll')"
echo Downloading file... [scripts.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/WidescreenFix/scripts.zip', 'scripts.zip')"
move scripts.zip temp >nul
powershell Expand-Archive temp/scripts.zip -DestinationPath scripts
echo.
echo Would you like to open the Widescreen Fix configuration? [Y/N]
set /p widescreencfg=""
if '%widescreencfg%'=='Y' start scripts/NFSMostWanted.WidescreenFix.ini
if '%widescreencfg%'=='y' start scripts/NFSMostWanted.WidescreenFix.ini
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