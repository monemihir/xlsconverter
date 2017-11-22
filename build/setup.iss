; -- setup.iss --
;
; Installer for MMVIC Report Generator
;
[Setup]
AppName=MMVIC Report Generator
AppCopyright=Mihir Mone
AppVersion=1.1
DefaultDirName={pf32}\MMVIC Report Generator
DefaultGroupName=MMVIC
UninstallFilesDir={app}
UninstallDisplayIcon={app}\mmvic-logo.ico

[Files]
Source: "..\src\bin\*.*"; Excludes: *.vshost.*, *.pdb, *.xml; DestDir: "{app}\bin"; Flags: ignoreversion recursesubdirs
Source: "..\src\data\*.*"; DestDir: "{app}\data"; Flags: recursesubdirs
Source: "..\src\lib\*.exe"; DestDir: "{app}\lib"
Source: "..\src\mmvic-logo.ico"; DestDir: "{app}"

[Icons]
Name: "{group}\MMVIC Report Generator"; Filename: "{app}\bin\MMVICReportGenerator.exe"
