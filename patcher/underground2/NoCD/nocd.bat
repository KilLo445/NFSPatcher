@echo off
title Need for Speed: Underground 2 No CD Patch
del SPEED2.exe >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/NoCD/SPEED2.EXE', 'SPEED2.EXE')"
exit