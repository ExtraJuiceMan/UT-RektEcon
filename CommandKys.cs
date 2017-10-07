using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandKys : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public static string[] kysmsg = {"{0} fucking DIED.", "{0} hung themselves.", "{0} put a bullet through their own head.",
            "{0} drank some Windex.", "{0} had some rat poison.", "{0} overdosed on cocaine.", "{0} hung themselves after finding out that anime isn't real.",
            "{0} overdosed on heroin.", "{0} overdosed on banana peels.", "{0} jumped off a rooftop and died.", "{0} had a sudden heart attack.",
        "{0} died of gay.", "{0} died from a gang of blacks.", "{0} got his butt pounded by a gorilla.", "{0} smoked too much crack and died.",
        "{0} took too much acid, tripped balls, and died.", "{0} shot himself after he found out that he was cucked by Jamal.", "{0} shot up an entire preschool, then themselves." };

        public string Name => "kys";

        public string Help => "Kills you";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "rektecon.kys" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, kysmsg.Length);
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;
            uCaller.Suicide();
            string msg = kysmsg[index];
            UnturnedChat.Say(String.Format(msg, caller.DisplayName), UnityEngine.Color.red);
        }
    }
}