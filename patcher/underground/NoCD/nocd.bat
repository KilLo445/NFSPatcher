@echo off
title Need for Speed: Underground No CD Patch
del Speed.exe >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/NoCD/Speed.exe', 'Speed.exe')"
exit