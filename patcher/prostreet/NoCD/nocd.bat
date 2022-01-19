@echo off
title Need for Speed: ProStreet No CD Patch
del nfs.exe >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/prostreet/NoCD/nfs.exe', 'nfs.exe')"
exit