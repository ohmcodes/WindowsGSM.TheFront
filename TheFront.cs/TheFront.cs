using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Engine;
using WindowsGSM.GameServer.Query;
using System.Security.Cryptography;

namespace WindowsGSM.Plugins
{
    public class TheFront : SteamCMDAgent
    {
        // - Plugin Details
        public Plugin Plugin = new Plugin
        {
            name = "WindowsGSM.TheFront", // WindowsGSM.XXXX
            author = "ohmcodes",
            description = "WindowsGSM plugin for supporting The Front Dedicated Server",
            version = "1.0",
            url = "https://github.com/ohmcodes/WindowsGSM.TheFront", // Github repository link (Best practice)
            color = "#f5bd1f" // Color Hex
        };

        // - Standard Constructor and properties
        public TheFront(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;
        public string Error, Notice;

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "2612550"; /* taken via https://steamdb.info/app/2612550/info/ */

        // - Game server Fixed variables
        public override string StartPath => @"ProjectWar\Binaries\Win64\TheFrontServer.exe"; // Game server start path
        public string FullName = "The Front Dedicated Server"; // Game server FullName
        public bool AllowsEmbedConsole = true;  // Does this server support output redirect?
        public int PortIncrements = 0; // This tells WindowsGSM how many ports should skip after installation
        public object QueryMethod = new A2S(); // Query method should be use on current server type. Accepted value: null or new A2S() or new FIVEM() or new UT3()

        public static string ConfigServerName = RandomNumberGenerator.Generate12DigitRandomNumber();

        // - Game server default values
        public string ServerName = "wgsm_thefront_dedicated";
        public string Defaultmap = "DedicatedServer"; // Original (MapName)
        public string Maxplayers = "100"; // WGSM reads this as string but originally it is number or int (MaxPlayers)
        public string Port = "27047"; // WGSM reads this as string but originally it is number or int
        public string QueryPort = "27048"; // WGSM reads this as string but originally it is number or int (SteamQueryPort)
        public string Additional = $"-EnableParallelCharacterMovementTickFunction -EnableParallelCharacterTickFunction -UseDynamicPhysicsScene -fullcrashdumpalways -Game.PhysicsVehicle=false -ansimalloc -Game.MaxFrameRate=35 -QueueValidTime=120 -BeaconPort=\"27049\" -ShutDownServicePort=\"27050\" -ConfigServerName=\"{ConfigServerName}\"";

        // - Create a default cfg for the game server after installation
        public async void CreateServerCFG()
        {
            createBaseConfig();
        }

        // - Start server function, return its Process to WindowsGSM
        public async Task<Process> Start()
        {
            string shipExePath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);
            if (!File.Exists(shipExePath))
            {
                Error = $"{Path.GetFileName(shipExePath)} not found ({shipExePath})";
                return null;
            }

            string param =  $"ProjectWar_Start?{_serverData.ServerMap}?MaxPlayers={_serverData.ServerMaxPlayer}?udrs=steam -server -game -log -UserDir=\"{ServerPath.GetServersServerFiles(_serverData.ServerID)}\" {_serverData.ServerParam}";

            param += $" -OutIPAddress={_serverData.ServerIP}";
            param += $" -ServerName=\"{_serverData.ServerName}\"";
            param += $" -Port=\"{_serverData.ServerPort}\"";
            param += $" -QueryPort=\"{_serverData.ServerQueryPort}\"";
            param += $" -MaxQueueSize={_serverData.ServerMaxPlayer}";
            param += $" -QueueThreshold={_serverData.ServerMaxPlayer}";

            // Prepare Process
            var p = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = shipExePath,
                    Arguments = param.ToString(),
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            // Set up Redirect Input and Output to WindowsGSM Console if EmbedConsole is on
            if (AllowsEmbedConsole)
            {
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                var serverConsole = new ServerConsole(_serverData.ServerID);
                p.OutputDataReceived += serverConsole.AddOutput;
                p.ErrorDataReceived += serverConsole.AddOutput;
            }

            // Start Process
            try
            {
                p.Start();
                if (AllowsEmbedConsole)
                {
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                }

                return p;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null; // return null if fail to start
            }
        }

        // - Stop server function
        public async Task Stop(Process p)
        {
			await Task.Run(() =>
			{
				 Functions.ServerConsole.SetMainWindow(p.MainWindowHandle);
				 Functions.ServerConsole.SendWaitToMainWindow("^c");
			});
			await Task.Delay(20000);
        }

        // - Update server function
        public async Task<Process> Update(bool validate = false, string custom = null)
        {
            var (p, error) = await Installer.SteamCMD.UpdateEx(serverData.ServerID, AppId, validate, custom: custom, loginAnonymous: loginAnonymous);
            Error = error;
            await Task.Run(() => { p.WaitForExit(); });
            return p;
        }

        public void createBaseConfig()
        {
            string baseConfig = $"[BaseServerConfig]\r\nServerName={_serverData.ServerName}\r\nServerPassword=\r\nQueueThreshold=24\r\nServerFightModeType=1\r\nIsCanSelfDamage=1\r\nIsCanFriendDamage=1\r\nClearSeverTime=\r\nUseSteamSocket=1\r\nPort={_serverData.ServerPort}\r\nBeaconPort=27049\r\nShutDownServicePort=27050\r\nQueryPort={_serverData.ServerQueryPort}\r\nSaveWorldInterval=300\r\nGMOverlapRatio=1\r\nIsUnLockAllTalentAndRecipe=0\r\nGMBagInitGirdNum=40\r\nGreenHand=1\r\nCharacterInitItem=\r\nGMDeathDropMode=1\r\nGMDeathInventoryLifeSpan=1800\r\nCorpsePickAuthority=2\r\nGMCanDropItem=1\r\nGMCanDiscardItem=1\r\nGMDiscardBoxLifeSpan=300\r\nGMRebirthBaseCD=10\r\nGMRebirthExtraCD=1\r\nGMPenaltiesMaxNum=5\r\nGMPenaltiesCD=600\r\nConstructEnableRot=1\r\nGMAttackCityCdRatio=1\r\nOpenAllHouseFlag=0\r\nIsCanChat=1\r\nIsShowBlood=1\r\nSensitiveWords=1\r\nHealthDyingState=1\r\nUseACE=1\r\nServerAdminAccounts=\r\nIsShowGmTitle=1\r\nPlayerHotDefAddRate=1\r\nPlayerIceDefAddRate=1\r\nHeadNameDisplayDist_Team=200\r\nHeadNameDisplayDist_Enemy=20\r\nPlayerDeathAvatarItemDurableRate=0\r\nPlayerDeatShortcutItemDurableRate=0\r\nGMCraftTimeRate=1\r\nPlayerAddExpRate=1\r\nPlayerKillAddExpRate=1\r\nPlayerFarmAddExpRate=1\r\nPlayerCraftAddExpRate=1\r\nMoveSpeedRate=1\r\nJumpRate=1\r\nPlayerLandedDamageRate=1\r\nPlayerMaxHealthRate=1\r\nHealthRecoverRate=1\r\nPlayerMaxStaminaRate=1\r\nStaminaRecoverRate=1\r\nPlayerStaminaCostRate=1\r\nPlayerMaxHungerRate=1\r\nGMHungerDecRatio=1\r\nGMBodyHungerAddRate=1\r\nMaxBodyWaterRate=1\r\nGMWaterDecRatio=1\r\nGMBodyWaterAddRate=1\r\nMaxBreathRate=1\r\nBreathRecoverRate=1\r\nPlayerBreathCostRate=1\r\nGMPlayerHealthRate=1\r\nGMFoodDragDurationRate=1\r\nNpcRespawnRatio=1\r\nAnimalBodyStayTime=300\r\nHumanBodyStayTime=10\r\nGMNPCLootableItemRatio=1\r\nNpcSpawnLevelRatio=1\r\nWildNPCDamageRate=1\r\nWildNPCHealthRate=1\r\nWildNPCSpeedRate=1\r\nCityNPCLevelRate=1\r\nCityNPCDamageRate=1\r\nCityNPCHealthRate=1\r\nCityNPCSpeedRate=1\r\nCityNPCNumRate=1\r\nNpcDisplayDistance=50\r\nGMInventoryGainRate=1\r\nGMCityATKNPCLootItemRatio=1\r\nGMMaxHouseFlagNumber=1\r\nGMSetGJConstructMaxNumRatio=1\r\nGMHFTrapMaxNum=0\r\nGMHFTurretMaxNum=0\r\nGMConstructDefenseRatio=1\r\nGMTrapDefenseRatio=1\r\nGMTurretDefenseRatio=1\r\nGMTrapDamageRatio=1\r\nGMTurretDamageRatio=1\r\nGMConstructMaxHealthRatio=1\r\nGMConstructReturnHPRatio=1\r\nGMHouseFlagRepairHealthRatio=1\r\nGMTTC_Oil_Rate=1\r\nGMWaterCollecter_Rate=1\r\nGMTTC_Ore_Rate=1\r\nGMTTC_Fish_Rate=1\r\nCHFDamagedByPlayer=1\r\nCHFDamagedByVehicle=1\r\nCHFDamagedByNpc=1\r\nGMHouseFlagExcitantTime=1\r\nGMMaxRetrieveProductsRate=1\r\nGMTreeGainRate=1\r\nGMBushGainRate=1\r\nGMOreGainRate=1\r\nGMCropVegetableReapRatio=1\r\nGMFleshGainRate=1\r\nGMCropVegetableGrowRatio=1\r\nGMMeleeNpcDamageRatio=1\r\nGMRangedNpcDamageRatio=1\r\nGMMeleePlayerDamageRatio=1\r\nGMRangedPlayerDamageRatio=1\r\nGMMeleeConstructDamageRatio=1\r\nGMRangedConstructDamageRatio=1\r\nGMToolDamageRate=1\r\nGMDurabilityCostRatio=1\r\nGMVehiclePlayerDamageRatio=1\r\nGMVehicleConstructDamageRatio=1\r\nGMVehicleDamageRate=1\r\nIsCanMail=1.000000\r\nServerTags=1,2,3";
            
            Directory.CreateDirectory(ServerPath.GetServersServerFiles(_serverData.ServerID, "TheFrontManager"));
            File.WriteAllText(ServerPath.GetServersServerFiles(_serverData.ServerID, "TheFrontManager", $"ServerConfig_{ConfigServerName}.ini"), baseConfig);
        }
    }

    public class RandomNumberGenerator
    {
        public static string Generate12DigitRandomNumber()
        {
            Random random = new Random();
            string twelveDigitNumber = GenerateRandom12Digits(random);
            return twelveDigitNumber;
        }

        private static string GenerateRandom12Digits(Random random)
        {
            string result = "";
            for (int i = 0; i < 12; i++)
            {
                result += random.Next(0, 10).ToString(); // Generates a random digit between 0 and 9
            }
            return result;
        }
    }
}
