-- CFX Details
lua54 'on'
fx_version 'cerulean'
games {'gta5'}

-- Resource stuff
name 'PingStrike'
description 'A simple FiveM resource that check player pings every so often and assigns them strikes. After a certain amount of strikes received, the player is kicked for high ping usage!'
version 'v1'
author 'ReckerXF'

files {
    "Newtonsoft.Json.dll"
}

server_script 'PingStrike.Server.net.dll'