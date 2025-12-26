$solutionRoot = "C:\kfu\oris\semestry\machi_coro\machi_coro"

$serverProj = Join-Path $solutionRoot "Server\Server.csproj"
$uiDll      = Join-Path $solutionRoot "MachiCoroUI\bin\Debug\net8.0\MachiCoroUI.exe"

Write-Host "=== Запуск сервера ==="
Start-Process powershell -ArgumentList "-NoExit", "-Command", "dotnet run --project `"$serverProj`""

Start-Sleep -Seconds 2

Write-Host "=== Запуск 4 клиентов UI ==="
1..4 | ForEach-Object {
    Start-Process "dotnet" "-NoLogo `"$uiDll`""
}

Read-Host "Нажми Enter для выхода"
