## PingStrike
PingStrike is a simple FiveM resource that check player pings every so often and assigns them strikes. After a certain amount of strikes received, the player is kicked for high ping usage!

## Config
You can modify the config values in ``config.json`` to your liking. Below you will find a list of config values and what they mean.
```
"pingThreshold": The amount of ping a player needs to be given a strike.
"pollingRate": How often in ms to check player pings. (Def. 15000)
"refreshRate": How long in ms before a player's strikes are removed? (Def. 1800000)
"maxStrikes:" How many strikes before the player gets kicked for high ping? (Def. 3)
```
