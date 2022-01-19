@echo off
title Need for Speed: Carbon Collector's Edition No CD Patch
del NFSC.exe >nul
powershell -Command "(New-Object Net.WebClient).DownloadFile('https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/NoCD/NFSC.exe', 'NFSC.exe')"
exit