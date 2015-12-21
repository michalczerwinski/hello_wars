Set oWS = WScript.CreateObject("WScript.Shell")
Set objShell = Wscript.CreateObject("Wscript.Shell")
desktopPath = objShell.SpecialFolders("Desktop")

arenaLinkFilePath = desktopPath + "\Arena.LNK"
configLinkFilePath = desktopPath + "\Config.LNK"

Set arenaLink = oWS.CreateShortcut(arenaLinkFilePath) 
arenaLink.TargetPath = "C:\hello_wars\Arena\bin\Arena.EXE"
arenaLink.Description = "Heelo-Wars Arena"
arenaLink.WindowStyle = "1"
arenaLink.WorkingDirectory = "C:\hello_wars\Arena\bin"
arenaLink.Save

Set configLink = oWS.CreateShortcut(configLinkFilePath)
configLink.TargetPath = "C:\hello_wars\Arena\bin\ArenaConfiguration.xml"
configLink.Description = "Heelo-Wars Arena configuration"
configLink.WindowStyle = "1"
configLink.WorkingDirectory = "C:\hello_wars\Arena\bin"
configLink.Save