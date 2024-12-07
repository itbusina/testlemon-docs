param(
    [string]$user,
    [string]$pass,
    [string]$jira_base_url,
    [string]$test_results
)

try {
    $pair = "$($user):$($pass)"
    $encodedCreds = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes($pair))
    $basicAuthValue = "Basic $encodedCreds"
    
    $uri = "$($jira_base_url)/rest/raven/1.0/import/execution"
    $res = Invoke-WebRequest -Uri $uri -Body $test_results -Method POST -ContentType "application/json" -Headers @{"Authorization" = $basicAuthValue } 
    write-host $res
}
catch {    
    write-host $_.Exception.Message
    write-host $_.Exception
    exit 1
}