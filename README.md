# beertapped
If theres two beers, which one you should drink? Based on Untappd data. Every beer makes this smarter.

```powershell
Invoke-RestMethod https://api.untappd.com/v4/user/beers/dopson | ConvertTo-Json -Depth 10 > dopson.json

$a = invoke-restmethod https://api.untappd.com/v4/user/beers/dopson
$a.response.beers.items.beer | select bid, beer_name, beer_abv, beer_ibu, beer_slug, beer_type, rating_score, rating_count | export-csv -Delimiter ";" out.csv -NoTypeInformation
```
