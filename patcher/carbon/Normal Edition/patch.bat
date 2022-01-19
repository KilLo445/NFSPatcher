@echo off
title Need for Speed: Carbon Patcher by KilLo445
:checkinst
IF EXIST "NFSC.exe" (
  goto warning
) ELSE (
  echo Please copy this file into your Need For Speed: Carbon install location.
  pause
  exit
)
:warning
echo Warning!
echo.
echo This version of the patcher has not been tested, so I am not sure if it works as I have the Collector's Edition
echo If this does not work for the normal edition, sorry!
pause
goto startpatch
:startpatch
echo Patching...
md temp
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/carbon/NoCD/nocd.bat', 'nocd.bat')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/nfsc_v1.4_en.zip', 'nfsc_v1.4_en.zip')"
move nfsc_v1.4_en.zip temp >nul
powershell Expand-Archive temp/nfsc_v1.4_en.zip -DestinationPath temp
start temp/patch_1.2_1.3_1.4.exe
timeout 5 /nobreak >nul
echo Press any key after the patcher closes.
pause >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/WidescreenFix/dinput8.dll', 'dinput8.dll')"
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/WidescreenFix/scripts.zip', 'scripts.zip')"
move scripts.zip temp >nul
powershell Expand-Archive temp/scripts.zip -DestinationPath scripts
echo Would you like to open the Widescreen Fix configuration? [Y/N]
set /p widescreencfg=""
if '%widescreencfg%'=='Y' start scripts/NFSCarbon.WidescreenFix.ini
if '%widescreencfg%'=='y' start scripts/NFSCarbon.WidescreenFix.ini
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