[CmdletBinding()]
Param(
    [Parameter()][string]$UntappdUser = "ajaska",
    [Parameter()][string]$ApiKey = "13A8DEAF7B8F0272256990BB7F72169C1F37E7C3"
)

$ErrorActionPreference = "stop"

function InvokeApi($offset) {
    $url = "https://api.untappd.com/v4/user/beers/$($UntappdUser)?access_token=$ApiKey&offset=$offset&limit=50"
    Write-Host "Reading '$url'"
    return Invoke-RestMethod $url
}

function FormatData($result) {
    return $result.response.beers.items.beer |
        Select-Object bid, beer_name, beer_abv, beer_ibu, beer_slug, beer_style, rating_score, rating_count, auth_rating |
        foreach {$_.rating_score = $_.rating_score -replace ",","."; $_ }
}

$first = InvokeApi(0);
$data = @(FormatData($first))
$raw = @($first)

for($offset = 50; ($offset -lt $first.response.total_count); $offset += 50) {
    $result = InvokeApi($offset)
    $raw += $result
    $data += (FormatData($result))
}

$data | Export-Csv -NoTypeInformation "$UntappdUser.csv"
$raw | Export-Clixml "$UntappdUser.raw.xml"