@echo off
echo Building solution...
dotnet build machi_coro\MachiCoro.sln --verbosity quiet
if %errorlevel% neq 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo Killing old processes...
taskkill /F /IM Server.exe 2>nul
taskkill /F /IM MachiCoroUI.exe 2>nul
timeout /t 2 /nobreak >nul

echo Starting Server...
pushd "%~dp0machi_coro\Server\bin\Debug\net8.0"
start "" Server.exe
popd

echo Waiting for server to start...
timeout /t 4 /nobreak >nul

echo Starting 4 clients...
pushd "%~dp0machi_coro\MachiCoroUI\bin\Debug\net8.0-windows"
start "" MachiCoroUI.exe
timeout /t 2 /nobreak >nul
start "" MachiCoroUI.exe
timeout /t 2 /nobreak >nul
start "" MachiCoroUI.exe
timeout /t 2 /nobreak >nul
start "" MachiCoroUI.exe
popd

echo All started!
