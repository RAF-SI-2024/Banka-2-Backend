@echo off

set SERVICE_NAME=user_database

echo Starting %SERVICE_NAME% service and its dependencies...
docker-compose up %SERVICE_NAME% --build

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to start %SERVICE_NAME% service or its dependencies.
    echo Check the logs for more details.
    echo Press any key to continue...
    pause >nul
    exit /b %ERRORLEVEL%
)

echo %SERVICE_NAME% service and its dependencies started successfully.
echo Press any key to continue...
pause >nul
exit /b 0