net stop "Particular ServiceControl"
REM RD C:\programdata\particular\ServiceControl\localhost-33333 /S
RD C:\ProgramData\Particular\ServiceControl\localhost-33333 /S
net start "Particular ServiceControl"