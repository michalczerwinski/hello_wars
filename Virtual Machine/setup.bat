SET url=https://az412801.vo.msecnd.net/vhd/VMBuild_20141027/VirtualBox/IE10/Windows/IE10.Win7.For.Windows.VirtualBox.zip
SET currPath=%~dp0%
@powershell -NoProfile -ExecutionPolicy Bypass -Command "iex (new-object net.webclient).DownloadFile('%url%','%currPath%win7.zip')"
"%ProgramFiles%\7-Zip\7z.exe" e %currPath%win7.zip -o%currPath%
@powershell -NoProfile -ExecutionPolicy Bypass -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
choco install virtualbox --yes
VBoxManage import "%currPath%IE10 - Win7.ova"
VBoxManage sharedfolder add "IE10 - Win7" --automount --name "HelloWarsSetup" --hostpath "%currPath%HelloWarsSetup"
VBoxManage startvm "IE10 - Win7"
