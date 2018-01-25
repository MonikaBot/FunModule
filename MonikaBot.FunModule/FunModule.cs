using System;
using System.Threading;
using System.Threading.Tasks;
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
            manager.AddCommand(new CommandStub("f", "Pay respect.", "Press f", cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Username} has paid their respects {FEmojis[rng.Next(0, FEmojis.Length - 1)]}");
            }), this);
            manager.AddCommand(new CommandStub("orange", "Orangifies your text.", "Discord only.", cmdArgs =>
            {
                if (cmdArgs.FromIntegration.ToLower().Trim() == "discord")
                {
                    cmdArgs.Channel.SendMessageAsync($"```fix\n{cmdArgs.Args[0]}\n```");
                }
                else
                    cmdArgs.Channel.SendMessageAsync($"This command is only available on Discord!");
            }, argCount: 1), this);
            manager.AddCommand(new CommandStub("nf", "Pay no respect.", "Press nf", cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Username} refuses to pay respect. {FEmojis[manager.rng.Next(0, FEmojis.Length - 1)]}");
            }), this);
            manager.AddCommand(new CommandStub("8ball", "Have your fortune told.", "8ball <your message here>", cmdArgs =>
            {
                manager.rng.Next(0, EightballMessages.Length);
                manager.rng.Next(0, EightballMessages.Length);
                int index = manager.rng.Next(0, EightballMessages.Length);
                cmdArgs.Channel.SendMessageAsync($"{cmdArgs.Author.Mention}: **{EightballMessages[index]}**");
            }, trigger: CommandTrigger.BotMentioned), this);
            manager.AddCommand(new CommandStub("42", "..", "...", cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync("The answer to life, the universe, and everything.");
            }, trigger: CommandTrigger.BotMentioned), this);
            manager.AddCommand(new CommandStub("twitter", "Displays Monika's Twitter account", "Ask Monika about Twitter!", cmdArgs =>
            {
                cmdArgs.Channel.SendMessageAsync("I actually have a twitter account believe it or not! My username is @lilmonix3");
            }), this);

            manager.AddCommand(new CommandStub("natsuki", "Has Monika chat about Natsuki", "Ask Monika her thoughts on Natsuki!", cmdArgs =>
            {
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(5000);
                cmdArgs.Channel.SendMessageAsync("I love her cupcakes! She really can make some good food! I don't know what I was thinking when I deleted her... How could I survive without her baking?");
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("...Now that I think about it... Who needs her cupcakes when I have you? ❤️");
            }), this);
            manager.AddCommand(new CommandStub("yuri", "Has Monika chat about Yuri", "Ask Monika her thoughts on Yuri!", cmdArgs =>
            {
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("She may be quiet but trust me, she's one of the sweetest members of the literature club!");
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("...She has this weird thing with knives though...");
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(5000);
                cmdArgs.Channel.SendMessageAsync("She's still super sweet! Ask her about the book she's reading. That's one way to make her come out of her shell!");
                Thread.Sleep(3000);
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("Or you can continue to chat with me instead! 😉");
            }), this);
            manager.AddCommand(new CommandStub("sayori", "Has Monika talk about Sayori", "Ask Monika on her thoughts on Sayori!", cmdArgs =>
            {
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("My trusty vice president! She'll never leave you hanging when it comes to the club!");
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("She is kinda... *special* though...");
                cmdArgs.Channel.TriggerTypingAsync();
                Thread.Sleep(3000);
                cmdArgs.Channel.SendMessageAsync("But nothing usually brings her down! She's just a beaming ray of sunshine!");
            }), this);
        }

        public override void ShutdownModule(CommandsManager managers)
        {}
    }
}
