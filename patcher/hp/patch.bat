@echo off
title Need for Speed III: Hot Pursuit Patcher by KilLo445 [v1.0]
:checkinst
IF EXIST "fedata/config/platcfg.dat" (
  goto checkinst2
) ELSE (
  goto noinst
)
:checkinst2
IF EXIST "gamedata/render/pc/cone.tga" (
  goto patchinfo
) ELSE (
  goto noinst
)
:noinst
echo Please copy this file into your Need for Speed III: Hot Pursuit install location.
echo Then copy the fedata and gamedata folders into the install location.
pause
exit
:patchinfo
echo To patch Need for Speed III: Hot Pursuit, you need a blank folder with the following files from the disc.
echo fedata folder
echo gamedata folder
echo And finally, patch.bat
echo.
echo Once you only have those files, press any key.
pause >nul
goto startpatch
:startpatch
echo Patching...
echo.
timeout 1 /nobreak >nul
md temp
echo Downloading file... [readme_en.txt]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/hp/ModernPatch/readme_en.txt', 'readme_en.txt')"
echo Downloading file... [readme_ru.txt]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/hp/ModernPatch/readme_ru.txt', 'readme_ru.txt')"
echo Downloading file... [eacsnd.dll]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/eacsnd.dll', 'eacsnd.dll')"
echo Downloading file... [nfs3.exe]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/nfs3.exe', 'nfs3.exe')"
echo Downloading file... [nfs3.ini]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/patcher/hp/ModernPatch/nfs3.ini', 'nfs3.ini')"
echo Downloading file... [fedata.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/fedata.zip', 'fedata.zip')"
move fedata.zip temp >nul
echo Downloading file... [gamedata.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/gamedata.zip', 'gamedata.zip')"
move gamedata.zip temp >nul
echo Downloading file... [drivers.zip]
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/drivers.zip', 'drivers.zip')"
move drivers.zip temp >nul
timeout 1 /nobreak >nul
powershell Expand-Archive temp/fedata.zip -DestinationPath temp/modernpatch/fedata
powershell Expand-Archive temp/gamedata.zip -DestinationPath temp/modernpatch/gamedata
powershell Expand-Archive temp/drivers.zip -DestinationPath temp/modernpatch/drivers
timeout 1 /nobreak >nul
echo.
echo Moving Files...
move /y temp/modernpatch/fedata fedata >nul
move /y temp/modernpatch/gamedata gamedata >nul
move /y temp/modernpatch/drivers drivers >nul
echo.
goto patchcomplete
:patchcomplete
@RD /S /Q "temp"
echo Patch complete!
echo Press any key to exit...
pause >nul
exit