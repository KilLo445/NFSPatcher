@echo OFF

echo This batch file MUST be run as an admin (right click, Run as administrator).

reg Query "HKLM\Hardware\Description\System\CentralProcessor\0" | find /i "x86" > NUL && set PCGWver=32BIT || set PCGWver=64BIT

if %PCGWver%==32BIT goto 32BIT
if %PCGWver%==64BIT goto 64BIT

:32BIT
echo n|copy /-y "%~dp0\diactfrm.dll" %SystemRoot%\System32\
echo n|copy /-y "%~dp0\dimap.dll" %SystemRoot%\System32\
regsvr32.exe %SystemRoot%\System32\diactfrm.dll

:64BIT
echo n|copy /-y "%~dp0\diactfrm.dll" %SystemRoot%\SysWoW64\
echo n|copy /-y "%~dp0\dimap.dll" %SystemRoot%\SysWoW64\

regsvr32.exe %SystemRoot%\SysWoW64\diactfrm.dll