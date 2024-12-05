param(
    [string]$client_id,
    [string]$client_secret,
    [string]$jira_base_url,
    [string]$test_results
)

$uri = "$($jira_base_url)/authenticate"
$json = @"
    {
     "client_id": "$($client_id)", "client_secret": "$($client_secret)"
    }
"@

try {
    $res = Invoke-WebRequest -Uri $uri -Body $json -Method POST -ContentType "application/json"
    $token = $res -replace '"', ''
    
    $uri = "$($jira_base_url)/import/execution"
    $res = Invoke-WebRequest -Uri $uri -UseBasicParsing -ContentType "application/json" -Body $test_results -Method POST -Headers @{"Authorization" = "Bearer $token" }
    write-host $res
}
catch {
    write-host $_.Exception.Message
    write-host $_.Exception
}