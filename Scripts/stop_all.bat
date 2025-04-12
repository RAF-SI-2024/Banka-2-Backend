@echo off

echo Stopping all services...
docker-compose down

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to services.
    echo Press any key to continue...
    pause >nul
    exit /b %ERRORLEVEL%
)

echo All services were stopped successfully.
echo Press any key to continue...
pause >nul
exit /b 0