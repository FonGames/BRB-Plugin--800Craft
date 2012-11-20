using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fCraft;
using System.IO;

namespace brb
{
    public class Init : Plugin
    {
        public void Initialize()
        {
            Logger.Log(
                LogType.ConsoleOutput,
                Name + "(v " + Version + "): Registering /brb");

            CommandManager.RegisterCustomCommand(new CommandDescriptor
            {
                Name = "brb",
                Category = CommandCategory.Chat,
                Permissions = new Permission[] { Permission.Chat },
                RepeatableSelection = true,
                IsConsoleSafe = true,
                Help = "type /brb [message]",
                Handler = BRB,
            });
        }
        public string Name
        {
            get
            {
                return "brb";
            }
            set
            {
                Name = value;
            }
        }
        public string Version
        {
            get
            {
                return "1.0";
            }
            set
            {
                Version = value;
            }
        }
        internal static void BRB(Player player, Command cmd)
        {
            StreamReader streamReader = new StreamReader("plugins/brbMessage.txt");
            string message = streamReader.ReadToEnd();
            streamReader.Close();

            string msg = cmd.NextAll().Trim();
            if (player.Info.IsMuted)
            {
                player.MessageMuted();
                return;
            }
            if (msg.Length > 0)
            {
                Server.Message("{0}&S &EWill Brb &9({1})",
                                  player.ClassyName, msg);
                player.IsAway = true;
                return;
            }
            else
            {
                Server.Players.Message("&S{0} &EWill Brb &9(" + message + ")", player.ClassyName);
                player.IsAway = true;
            }
        }
    }
}