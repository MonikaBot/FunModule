﻿using System;
using MonikaBot.Commands;


public class ModuleEntryPoint : IModuleEntryPoint
{
    public IModule GetModule()
    {
        return new MonikaBot.FunModule.FunModule();
    }
}

namespace MonikaBot.FunModule
{
    public class FunModule : IModule
    {
        private string[] EightballMessages = new string[]
        {
            "Signs point to yes.",
            "Yes.",
            "Reply hazy, try again.",
            "Without a doubt",
            "My sources say no",
            "As I see it, yes.",
            "You may rely on it.",
            "Concentrate and ask again",
            "Outlook not so good",
            "It is decidedly so",
            "Better not tell you now.",
            "Very doubtful",
            "Yes - definitely",
            "It is certain",
            "Cannot predict now",
            "Most likely",
            "Ask again later",
            "My reply is no",
            "Outlook good",
            "Don't count on it"
        };
        private string[] FEmojis = new string[]
        {
            "💩","🍆","👌","`lol`","😛","💀","🎆", "😏", "🖕", "💀🎺🎺"
        };

        private Random rng = new Random((int)DateTime.Now.Ticks);

        public FunModule()
        {
            Name = "Fun Module";
            Description = "Has some fun stuff in it I suppose.";
            ModuleKind = ModuleType.External; //NECESSARY for a DLL.
        }

        public override void Install(CommandsManager manager)
        {
            manager.AddCommand(new CommandStub("f", "Pay respect.", "Press f", PermissionType.User, 0, cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Username} has paid their respects {FEmojis[rng.Next(0, FEmojis.Length - 1)]}");
            }), this);
            manager.AddCommand(new CommandStub("orange", "Orangifies your text.", "Discord only.", PermissionType.User, 1, cmdArgs =>
            {
                if (cmdArgs.FromIntegration.ToLower().Trim() == "discord")
                {
                    cmdArgs.Channel.SendMessageAsync($"```fix\n{cmdArgs.Args[0]}\n```");
                }
                else
                    cmdArgs.Channel.SendMessageAsync($"This command is only available on Discord!");
            }), this);
            manager.AddCommand(new CommandStub("nf", "Pay no respect.", "Press nf", PermissionType.User, cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Username} refuses to pay respect. {FEmojis[manager.rng.Next(0, FEmojis.Length - 1)]}");
            }), this);
            manager.AddCommand(new CommandStub("8ball", "Have your fortune told.", "8ball <your message here>", PermissionType.User, cmdArgs =>
            {
                manager.rng.Next(0, EightballMessages.Length);
                manager.rng.Next(0, EightballMessages.Length);
                int index = manager.rng.Next(0, EightballMessages.Length);
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Mention}: **{EightballMessages[index]}**");
            }, trigger: CommandTrigger.BotMentioned), this);
            manager.AddCommand(new CommandStub("42", "..", "...", PermissionType.User, 0, cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync("The answer to life, the universe, and everything.");
            }, trigger: CommandTrigger.BotMentioned), this);
        }
    }
}