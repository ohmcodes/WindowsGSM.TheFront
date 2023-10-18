# WindowsGSM.TheFront
🧩 WindowsGSM plugin that provides The Front Dedicated server support!

# WindowsGSM Installation: 
1. Download  WindowsGSM https://windowsgsm.com/ 
2. Create a Folder at a Location you wan't all Server to be Installed and Run.
4. Drag WindowsGSM.Exe into previoulsy created folder and execute it.

# Plugin Installation:
1. Download [latest](https://github.com/ohmcodes/WindowsGSM.TheFront/releases/latest) release
2. Extract then Move **TheFront.cs** folder to **plugins** folder
3. OR Press on the Puzzle Icon in the left bottom side and install this plugin by navigating to it and select the Zip File.
4. Click **[RELOAD PLUGINS]** button or restart WindowsGSM
5. Navigate "Servers" and Click "Install Game Server" and find "TheFront Dedicated Server [TheFront.cs]

### Official Documentation
🗃️ https://youtu.be/xZT9PkN8dUI

### The Game
🕹️ https://store.steampowered.com/app/2285150/The_Front/

### Dedicated server info
🖥️ https://steamcommunity.com/app/2285150
🖥️ https://steamdb.info/app/2612550/info/

#### Change configuration manually
- Open WGSM Go to "Browse" Tab and Click "Server Files"
- Find "WGSM/servers/<ID>/serverfiles/TheFrontManager/ServerConfig_<autogeneratednumber>.ini" for game settings
- Recommendation for ports use 27047, 27048, 27048, 27050 (This is already setup but if you prefer different ports its up to you.)

#### NOTE
- All Available Config 
- ***<replaceme>*** replace this text with your desired config value
- add this config on your .bat file and add (negative sign) for example -ServerPassword="changeme" (NOTE: this .cs doesnt need bat file)
- Config guides [view](https://docs.google.com/spreadsheets/d/1Cea87x09rWuKjKuaqbMBSFdZjXigP5AkcVrT345RMfE/edit#gid=0)

```
[BaseServerConfig]
ServerName=<servername>
ServerPassword=
QueueThreshold=24
ServerFightModeType=1
IsCanSelfDamage=1
IsCanFriendDamage=1
ClearSeverTime=<yyyy-mm-dd>
UseSteamSocket=1
Port=<from 27000 - 27050>
BeaconPort=<from 27000 - 27050>
ShutDownServicePort=<from 27000 - 27050>
QueryPort=<from 27000 - 27050>
SaveWorldInterval=300
GMOverlapRatio=1
IsUnLockAllTalentAndRecipe=0
GMBagInitGirdNum=40
GreenHand=1
CharacterInitItem=
GMDeathDropMode=1
GMDeathInventoryLifeSpan=1800
CorpsePickAuthority=2
GMCanDropItem=1
GMCanDiscardItem=1
GMDiscardBoxLifeSpan=300
GMRebirthBaseCD=10
GMRebirthExtraCD=1
GMPenaltiesMaxNum=5
GMPenaltiesCD=600
ConstructEnableRot=1
GMAttackCityCdRatio=1
OpenAllHouseFlag=0
IsCanChat=1
IsShowBlood=1
SensitiveWords=1
HealthDyingState=1
UseACE=1
ServerAdminAccounts=<SteamID64>
IsShowGmTitle=1
PlayerHotDefAddRate=1
PlayerIceDefAddRate=1
HeadNameDisplayDist_Team=200
HeadNameDisplayDist_Enemy=20
PlayerDeathAvatarItemDurableRate=0
PlayerDeatShortcutItemDurableRate=0
GMCraftTimeRate=1
PlayerAddExpRate=1
PlayerKillAddExpRate=1
PlayerFarmAddExpRate=1
PlayerCraftAddExpRate=1
MoveSpeedRate=1
JumpRate=1
PlayerLandedDamageRate=1
PlayerMaxHealthRate=1
HealthRecoverRate=1
PlayerMaxStaminaRate=1
StaminaRecoverRate=1
PlayerStaminaCostRate=1
PlayerMaxHungerRate=1
GMHungerDecRatio=1
GMBodyHungerAddRate=1
MaxBodyWaterRate=1
GMWaterDecRatio=1
GMBodyWaterAddRate=1
MaxBreathRate=1
BreathRecoverRate=1
PlayerBreathCostRate=1
GMPlayerHealthRate=1
GMFoodDragDurationRate=1
NpcRespawnRatio=1
AnimalBodyStayTime=300
HumanBodyStayTime=10
GMNPCLootableItemRatio=1
NpcSpawnLevelRatio=1
WildNPCDamageRate=1
WildNPCHealthRate=1
WildNPCSpeedRate=1
CityNPCLevelRate=1
CityNPCDamageRate=1
CityNPCHealthRate=1
CityNPCSpeedRate=1
CityNPCNumRate=1
NpcDisplayDistance=50
GMInventoryGainRate=1
GMCityATKNPCLootItemRatio=1
GMMaxHouseFlagNumber=1
GMSetGJConstructMaxNumRatio=1
GMHFTrapMaxNum=0
GMHFTurretMaxNum=0
GMConstructDefenseRatio=1
GMTrapDefenseRatio=1
GMTurretDefenseRatio=1
GMTrapDamageRatio=1
GMTurretDamageRatio=1
GMConstructMaxHealthRatio=1
GMConstructReturnHPRatio=1
GMHouseFlagRepairHealthRatio=1
GMTTC_Oil_Rate=1
GMWaterCollecter_Rate=1
GMTTC_Ore_Rate=1
GMTTC_Fish_Rate=1
CHFDamagedByPlayer=1
CHFDamagedByVehicle=1
CHFDamagedByNpc=1
GMHouseFlagExcitantTime=1
GMMaxRetrieveProductsRate=1
GMTreeGainRate=1
GMBushGainRate=1
GMOreGainRate=1
GMCropVegetableReapRatio=1
GMFleshGainRate=1
GMCropVegetableGrowRatio=1
GMMeleeNpcDamageRatio=1
GMRangedNpcDamageRatio=1
GMMeleePlayerDamageRatio=1
GMRangedPlayerDamageRatio=1
GMMeleeConstructDamageRatio=1
GMRangedConstructDamageRatio=1
GMToolDamageRate=1
GMDurabilityCostRatio=1
GMVehiclePlayerDamageRatio=1
GMVehicleConstructDamageRatio=1
GMVehicleDamageRate=1
ServerTags=1,2,3,4,5 
```

NOTE:
ServerTags=0,1,2,3,4 The label has nothing to do with the server parameters, so you have to choose the one you can use among these:

```
0= PVP 
1= PVE
2= EXP Multiplikator
3= GatheringRate
4= KeepInventory
5= 45d wipe
6= 15d wipe
7= 30d wipe
8= 60d wipe
```


Adding Admin *ServerAdminAccounts*
- Edit Config and add parameters on the bottom
```
-ServerAdminAccounts=<SteamID64>
```
- How to get SteamID64 [Click Here](https://www.steamidfinder.com/)

- Unfortunately *-UserDir* can't be overriden
- Don't change *DedicatedServer* as Map

Auto read from Fields
Server Name == *ServerName*
Server IP Address == *OutIPAddress*
Server Port == *Port*
Server Query Port == *QueryPort*
Server Maxplayer == *MaxQueueSize*
Server Maxplayer == *QueueThreshold*

# License
This project is licensed under the MIT License - see the <a href="https://github.com/ohmcodes/WindowsGSM.TheFront/blob/main/LICENSE">LICENSE.md</a> file for details
