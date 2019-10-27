# TODO: Username and password?
param (
    [string]$version = "0.0.0.0",
    [int]$offset = 0,
    [string]$server,
    [string]$database
)

Write-Output ("Version: {0} Offset: {1}" -f $version, $offset)
# Set working directory explicitly
Set-Location ("{0}/SQL Scripts/"-f (Split-Path $MyInvocation.MyCommand.Path))

[array]$folderList = @((Get-ChildItem -Directory).Name |
    ForEach-Object{[System.Version]$_} |
    Sort-Object |
    ForEach-Object{("{0}.{1}.{2}.{3}" -f $_.Major, $_.Minor, $_.Build, $_.Revision)})

Write-Output ("Folder list: {0}" -f $folderList)

[int]$start = $folderList.IndexOf($version)
[int]$end = $start - $offset
for ([int]$i = $start; $i -le $end; $i++) {
    [string]$folder = $folderList[$i]

    Write-Output ("Script folder {0}" -f $folder)

    [array]$sqlScripts = @(Get-ChildItem -Path $folder -Filter *.sql)
    foreach ($script in $sqlScripts) {
        Write-Output ("Running (not really yet) {0}" -f $script) 
    }
}