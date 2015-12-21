@powershell -NoProfile -ExecutionPolicy Bypass -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
choco install git --yes
"%ProgramFiles%\Git\bin\git.exe" clone https://github.com/michalczerwinski/hello_wars C:\hello_wars
CSCRIPT "%~dp0%create shortcut.vbs"
choco install dotnet4.5 --yes