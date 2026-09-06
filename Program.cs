using System;
using System.Threading;

static void Wait(double seconds)
{
    Thread.Sleep((int)(seconds * 1000));
}

static void Typewriter(string text, int delayMs = 40)
{
    foreach (char c in text)
    {
        Console.Write(c);
        Thread.Sleep(delayMs);
    }
    Console.WriteLine();
}

Console.WriteLine("Welcome to TextAdventure"); 
Console.WriteLine();
Console.WriteLine("Disclaimer: This game is pretty trash lol");
Console.Write("Proceed (y/n)? ");
string proceed = Console.ReadLine() ?? "n";
if (proceed.ToLower() != "y")
{
    Console.WriteLine("Exiting game...");
    return;
}
Console.WriteLine();
Wait(1);
Console.Write("Enter your name: ");
string playerName = Console.ReadLine() ?? "Player";
Console.WriteLine();
Typewriter("Welcome, " + playerName + "! Let's begin your adventure.");
Wait(3);