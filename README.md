# beertapped
If theres two beers, which one you should drink? Based on Untappd data. Every beer makes this smarter.

```powershell
Invoke-RestMethod https://api.untappd.com/v4/user/beers/dopson | ConvertTo-Json -Depth 10 > dopson.json
```
