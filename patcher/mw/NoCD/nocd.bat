@echo off
title Need for Speed: Most Wanted No CD Patch
del Speed.exe >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/NoCD/speed.exe', 'speed.exe')"
exit