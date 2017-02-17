:: @if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

:: Variables
:: ---------

SET WEB_PROJ=Api\Api.csproj
SET CONFIG=Release
SET SOLUTION=solutions\Performance.AutoRest.sln
SET SCRIPT_PATH=DeployScripts

call "%DEPLOYMENT_SOURCE%\%SCRIPT_PATH%\deploy-common.cmd"
