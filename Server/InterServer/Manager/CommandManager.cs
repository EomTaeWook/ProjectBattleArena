﻿using BA.InterServer.ServerModule;
using Kosher.Framework;
using Kosher.Log;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using System.Diagnostics;
using System.Text;

namespace BA.InterServer.Manager
{
    internal class CommandManager : Singleton<CommandManager>
    {
        private delegate Task Callback(params string[] args);
        private readonly string[] emptyOptions = new string[0];
        private Dictionary<string, string[]> _commandMap = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, Callback> _commandProtocolMap = new Dictionary<string, Callback>(StringComparer.OrdinalIgnoreCase);
        public CommandManager()
        {
            _commandMap.Add("gws", new string[] { "on","off" });
            _commandMap.Add("resetkey", emptyOptions);
            _commandMap.Add("close", emptyOptions);

            _commandProtocolMap.Add("gws", GameWebServerInspection);
            _commandProtocolMap.Add("resetkey", ChangedSecurityKeyAsync);
            _commandProtocolMap.Add("close", Close);
        }
        public async Task ChangedSecurityKeyAsync(params string[] args)
        {
            var _ = await SchedulerSecurityManager.Instance.CreateKey();
        }
        public Task GameWebServerInspection(params string[] args)
        {
            var packetData = new GameWebServerInspection();

            if (args[0].ToLower() == "on")
            {
                packetData.ServerOn = true;
            }
            else
            {
                packetData.ServerOn = false;
            }
            var packet = ServerModule.Packet.MakePacket(IGWSProtocol.GameWebServerInspection, packetData);

            InterServerModule.Instance.Broadcast(packet);

            return Task.CompletedTask;
        }
        public Task Close(params string[] args)
        {
            Process.GetCurrentProcess().Close();

            return Task.CompletedTask;
        }
        private bool RunCommnad(string command, params string[] args)
        {
            if(_commandMap.ContainsKey(command) == false)
            {
                return false;
            }
            var option = _commandMap[command];
            if(option.Length > 0 && args.Length == 0)
            {
                LogHelper.Error($"{command} need option");
                return false;
            }

            if (_commandProtocolMap.ContainsKey(command) == true)
            {
                _commandProtocolMap[command].Invoke(args);
            }            
            return true;
        }
        public void Run()
        {
            Task.Run(() =>
            {
                var sb = new StringBuilder();
                while (true)
                {
                    var line = Console.ReadLine();
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    var splits = line.Split(" ");

                    if (splits.Length < 2)
                    {
                        continue;
                    }
                    if (splits[0].ToLower().Equals("ba") == true)
                    {
                        for (int i = 2; i < splits.Length; ++i)
                        {
                            sb.Append($"{splits[i]} ");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        var excuted = RunCommnad(splits[1], sb.ToString());
                        if (excuted == false)
                        {
                            LogHelper.Error($"not found command {splits[1]}");
                        }
                        sb.Clear();
                    }
                }
            });
        }
    }
}
