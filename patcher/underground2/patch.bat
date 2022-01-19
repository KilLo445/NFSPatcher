@echo off
title Need for Speed: Underground 2 Patcher by KilLo445
:checkinst
IF EXIST "SPEED2.exe" (
  goto startpatch
) ELSE (
  echo Please copy this file into your Need For Speed: Underground 2 install location.
  pause
  exit
)
:startpatch
echo Patching...
md temp
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/underground2/NoCD/nocd.bat', 'nocd.bat')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/nfsu2_v1.2_us.zip', 'nfsu2_v1.2_us.zip')"
move nfsu2_v1.2_us.zip temp >nul
powershell Expand-Archive temp/nfsu2_v1.2_us.zip -DestinationPath temp
start temp/nfsu2_v1.2_us.exe
timeout 5 /nobreak >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/DirectInputMapper/dimap.dll', 'dimap.dll')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/WidescreenFix/dinput8.dll', 'dinput8.dll')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/WidescreenFix/scripts.zip', 'scripts.zip')"
move scripts.zip temp >nul
powershell Expand-Archive temp/scripts.zip -DestinationPath scripts
echo Would you like to open the Widescreen Fix configuration? [Y/N]
set /p widescreencfg=""
if '%widescreencfg%'=='Y' start scripts/NFSUnderground2.WidescreenFix.ini
if '%widescreencfg%'=='y' start scripts/NFSUnderground2.WidescreenFix.ini
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