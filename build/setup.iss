; -- setup.iss --
;
; Installer for MMVIC Report Generator
;

#define AppVer GetFileVersion('..\src\bin\MMVICReportGenerator.exe')

[Setup]
AppName=MMVIC Report Generator
AppCopyright=Mihir Mone
AppVersion={#AppVer}
OutputBaseFilename=mmvic-report-generator-{#AppVer}
DefaultDirName={pf32}\MMVIC Report Generator
DefaultGroupName=MMVIC
UninstallFilesDir={app}
UninstallDisplayIcon={app}\mmvic-logo.ico

[Files]
Source: "..\src\bin\*.*"; Excludes: *.vshost.*, *.pdb, *.xml; DestDir: "{app}\bin"; Flags: ignoreversion recursesubdirs
Source: "..\src\content\*.*"; DestDir: "{app}\content"; Flags: recursesubdirs
Source: "..\src\lib\*.exe"; DestDir: "{app}\lib"
Source: "..\src\mmvic-logo.ico"; DestDir: "{app}"

[Icons]
Name: "{group}\MMVIC Report Generator"; Filename: "{app}\bin\MMVICReportGenerator.exe"