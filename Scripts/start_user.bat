@echo off
REM Script to run docker-compose for the bank.user service and handle failures

REM Define the service name
set SERVICE_NAME=bank.user

REM Run docker-compose up for the specified service
echo Starting %SERVICE_NAME% service and its dependencies...
docker-compose up %SERVICE_NAME%

REM Check if the command failed
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to start %SERVICE_NAME% service or its dependencies.
    echo Check the logs for more details.
    exit /b %ERRORLEVEL%
)

REM If successful
echo %SERVICE_NAME% service and its dependencies started successfully.
echo Press any key to continue...
timeout /T 10 /NOBREAK >nul
exit /b 0