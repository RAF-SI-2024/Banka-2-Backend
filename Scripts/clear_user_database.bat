@echo off

set DATABASE_VOLUME_NAME=user_database

echo Clearing %DATABASE_VOLUME_NAME% data...
docker volume rm %DATABASE_VOLUME_NAME%

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to clear %DATABASE_VOLUME_NAME% data.
    echo Press any key to continue...
    pause >nul
    exit /b %ERRORLEVEL%
)

echo %DATABASE_VOLUME_NAME% data are cleared successfully.
echo Press any key to continue...
pause >nul
exit /b 0