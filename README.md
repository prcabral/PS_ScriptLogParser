# PS_ScriptLogParser
Powershell ScriptLogParser is basically a simple binary to parse and export Microsoft-Windows-PowerShell Operational logs where the only focus is the event ID 4104 (Execute Remote Command)

### Example how to run
* `PS_ScriptlogParser.exe -f "C:\Windows\System32\winevt\Logs\Microsoft-Windows-PowerShell%4Operational.evtx" -o "c:\temp\out"`

### TO DO
* Validate the evtx file provided are in reality Microsoft-Windows-PowerShell Operational
* Concatenate properly scripts that are split accross multiple entries (instead of a single \n)

