using System;
using System.Threading;

Random rand = new Random();

static void Wait(double seconds)
{
    Thread.Sleep((int)(seconds * 1000));
}

static int GetChoice(string prompt, string[] options)
{
    int selectedChoice = -1;

    while (selectedChoice < 1 || selectedChoice > options.Length)
    {
        Typewriter(prompt);
        
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        Console.Write("> ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out selectedChoice) || selectedChoice < 1 || selectedChoice > options.Length)
        {
            Console.WriteLine($"\n[Invalid choice. Please enter a number between 1 and {options.Length}]\n");
            selectedChoice = -1; 
        }
    }

    return selectedChoice;
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

static void GameOver(string message)
{
    Console.WriteLine();
    Typewriter($"=== GAME OVER ===");
    Typewriter(message);
    Wait(2);
    
    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey(true);
    
    Environment.Exit(0); 
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
int playerHP = 20;

Wait(3);
Console.WriteLine();
Typewriter("You find yourself in a dark forest.... ALONE. The trees tower above you, and the path ahead is unclear.\n");

string[] forestChoices = {
    "Explore the forest",
    "Scavenge for Resources",
    "Call for help"
};

int choice = GetChoice("What will you do?", forestChoices);

switch(choice)
{
    case 1:
        Typewriter("You step deeper into the forest, pushing through the thick undergrowth");
        Typewriter("the howls of distant creatures echo through the trees. You feel a sense");
        Typewriter("of unease as you continue forward, unsure of what lies ahead.");
        Wait(3);
        Console.WriteLine();
        Typewriter("Suddenly, a wild beast jumps before you, it's howls fill the air. You must act quickly!");
        Console.WriteLine();
        string[] beastChoices = {
            "Fight the beast",
            "Run away",
            "Try to tame the beast"
        };
        choice = GetChoice("What will you do?", beastChoices);
        Console.WriteLine();
        switch(choice)
        {
            case 1:
                Typewriter("You awkwardly approach the beast, it's teeth bared and eyes glowing. You swing your");
                Typewriter("fists wildly, making you look like the fool, the beast eats your head off... You are dead.");
                Wait(3);
                GameOver("Who brings their fists to a wild fight? You apparently...");
                break;
            case 2:
                Typewriter("You turn and run, stumbling over rocks and branches, the beast growls menacinly behind you...");
                Wait(3);
                Console.WriteLine();
                int escapeRoll = rand.Next(1,6);
                if (escapeRoll <= 3)
                {
                    Typewriter("As you rush through the woods, the beast catches up to you and tears you apart. You lay on the");
                    Typewriter("ground helplessly, until it fades to black... You are dead.");
                    Wait(3);
                    GameOver("You should have fought the beast or tried to tame it. Running away was not the best choice.");
                }
                else
                {
                    Typewriter("You manage to escape the beast, your heart pounding with adrenaline. You find a small clearing with");
                    Typewriter("a small rock, and stop for a rest.");
                    Console.WriteLine();
                    Wait(2);
                    Typewriter("You restored +5 HP");
                    playerHP += 5;
                    Clearing();
                }
                break;
            case 3:
                Typewriter("You cautiously approach the beast, speaking softly and extending your hand. The beast tilts its head");
                Typewriter("in confusion, but after a tense momenet, it...");
                Wait(3);
                Typewriter(".");
                Wait(1);
                Typewriter("..");
                Wait(1);
                Typewriter("...");
                Wait(1);
                Typewriter("eats your head off. You are dead.");
                Wait(3);
                GameOver("Mercy does not work on the wild...");
                break;
        }
        break;
    case 2:
        Typewriter("You wander through the forest, looking for anything of value, you push");
        Typewriter("through branches and leaves, but come up empty-handed. As you start to");
        Typewriter("turn back, you spot a empty clearing up ahead...");
        Wait(3);
        Clearing();
        break;
    case 3: 
        Typewriter("You cry out for help, desperately, but the forest remains silent. The howls of the");
        Typewriter("wild creatures call to you... This was a bad idea, you think to yourself. You start");
        Typewriter("to run, but the howls grow louder... Your not getting out of this...");
        Wait(3);
        GameOver("What part of \"Alone\" did you not understand?");
        break;
}

static void Clearing()
{
    Typewriter("The clearing gives you a beautiful view of the darkening sky. and as you wait there, you");
    Typewriter("you feel almost at peace, but than you hear a rustling... Was it the wind, a beast, or");
    Typewriter("something else? You can't be sure, but you know you need to be ready for anything.");
    Wait(3);
    Typewriter("Suddenly, a pain in your stomach makes you dizzy, you need to find some food... fast. You");
    Typewriter("scan the surroundings, and spot a small animal, which could be a source of food, but it could");
    Typewriter("also be your demise...");
    Console.WriteLine();
    Wait(1);
    string[] animalChoices = {
        "Hunt the animal",
        "Ignore it and keep searching for food",
        "Give up..."
    };
    int choice = GetChoice("What do you do?", animalChoices);
}