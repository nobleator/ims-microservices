# TODO: Pass container name as parameter?
param (
    [string]$version = "0.0.0.0",
    [int]$offset = 0
)

Write-Output ("Version: {0} Offset: {1}" -f $version, $offset)
# Set working directory explicitly
Set-Location ("{0}/SQL Scripts/"-f (Split-Path $MyInvocation.MyCommand.Path))

[array]$folderList = @((Get-ChildItem -Directory).Name |
    ForEach-Object{[System.Version]$_} |
    Sort-Object |
    ForEach-Object{("{0}.{1}.{2}.{3}" -f $_.Major, $_.Minor, $_.Build, $_.Revision)})

[int]$start = $folderList.IndexOf($version)
[int]$end = $start - $offset
for ([int]$i = $start; $i -le $end; $i++) {
    [string]$folder = $folderList[$i]
    Write-Output ("Processing {0}" -f $folder)

    # Create folder on Docker container
    & docker exec -i db_service mkdir -p /tmp/sql/$folder

    Write-Output ("Copying {0} into Dockerized DB" -f $folder)
    # Appending "/." will copy the contents of the directory
    # docker cp <source file> <container_name>:<destination file>
    & docker cp "./$folder/." db_service:/tmp/sql/$folder/ | Out-Null

    [array]$sqlScripts = @(Get-ChildItem -Path $folder -Filter *.sql)
    foreach ($script in $sqlScripts) {
        $file = $script.Name
        Write-Output ("Executing {0}" -f $file)
        # docker exec -it <container name> psql -U <valid username> -a <database name> -f <SQL file>
        & docker exec -it db_service psql -U postgres -a imsdb -f "/tmp/sql/$folder/$file" | Out-Null
    }

    # Clean up container
    & docker exec -i db_service rm -rf /tmp/sql/$folder
}