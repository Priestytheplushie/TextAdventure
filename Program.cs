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
GameState.PlayerName = Console.ReadLine() ?? "Player";
Console.WriteLine();
Typewriter("Welcome, " + GameState.PlayerName + "! Let's begin your adventure.");

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
                    GameState.PlayerHP += 5;
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
    switch (choice)
    {
        case 1: 
            Typewriter("You approach the beast with malice in your eyes, and as you step over it, you reach");
            Typewriter("for your knife, but... you don't have any knife. The animal, seeing yoour weakness, bites");
            Typewriter("you...");
            Console.WriteLine();
            Typewriter("You took 5 damage!");
            GameState.PlayerHP -= 5;
            if (GameState.PlayerHP <= 0)
            {
                Wait(2);
                GameOver("Stop bullying defenseless animals, you monster.");
            }
            else
            {
                Typewriter("You manage to scare the animal away, but you are left with a wound and a sense of disappointment.");
                Typewriter("how could you have lost to a simple creature. Oh well, be greatful you survived at all...");
                Wait(2);
                Clearing();
            }
            break;
        case 2:
            Typewriter("You decide to ignore the animal, and contiune your everlasting thirst for food. You search the clearing");
            Typewriter("and finally find a small berry bush, and eat the berries, restoring your health.");
            GameState.PlayerHP += 5;
            Typewriter("You restored +5 HP");
            Wait(2);
            Typewriter("But as you finish eating, you hear a rustling in the bushes, and a wild beast jumps out at you!");
            Typewriter("Its fangs glint in the moonlight... This doesn't look good, you need to act fast!");
            Wait(2);
            Console.WriteLine();
            BeastCombat();
            break;
        case 3:
            Typewriter("You give up, and sit down on the ground, feeling hopeless. The forest seems to close in around you,");
            Typewriter("and you feel a sense of despair. You hear the howls of the wild creatures, and you know that your");
            Typewriter("time is up...");
            Wait(3);
            GameOver("What a pussy, imagine giving up in a forest, you should be ashamed of yourself.");
            break;

    }
}

static void BeastCombat()
{
    int beastHP = 100;
    int beastAttack = 5;
    int beastDefense = 2;
    int turnCount = 0;
    int turnLimit = 10;
    Random rand = new Random();
    string[] fightChoices =
    {
        "Attack",
        "Defend",
        "Flee",
    };
    Typewriter("The forest beast appears...");
    Wait(3);
    while (beastHP > 0 && GameState.PlayerHP > 0)
    {
        turnCount++;
        Console.WriteLine($"Turn {turnCount}: (Death in {turnLimit - turnCount})");
        int choice = GetChoice("What do you do?", fightChoices);
        switch(choice)
        {
            case 1:
                int playerAttack = rand.Next(1, 10);
                int damageToBeast = Math.Max(playerAttack - beastDefense, 0);
                beastHP -= damageToBeast;
                Typewriter("You pucnh the beast, dealing " + damageToBeast + " damage! Beast HP: " + beastHP);
                break;
            case 2:
                Typewriter("You brace yourself for the beast's attack, reducing the damage taken.");
                beastAttack /= 2;
                break;
            case 3:
                Typewriter("You attempt to flee from the beast...");
                int fleeRoll = rand.Next(1, 6);
                if (fleeRoll <= 3)
                {
                    Typewriter("You successfully escape from the beast!");
                    Clearing();
                    return;
                }
                else
                {
                    Typewriter("You fail to escape, and the beast attacks you!");
                }
                break;
        }
        if (GameState.PlayerHP > 0)
        {
            int damageToPlayer = Math.Max(beastAttack - rand.Next(1, 5), 0);
            GameState.PlayerHP -= damageToPlayer;
            Typewriter("The beast bites you, dealing " + damageToPlayer + " damage! Your HP: " + GameState.PlayerHP);
        }
        else
        {
            Typewriter("You fall to the ground, and the beast stands over you, victorious. You have been defeated.");
            Wait(3);
            GameOver("Ouch... That's gotta hurt (both physiucally and emotionally).");
        }

        if (turnCount >= turnLimit)
        {
            Typewriter("You feeel exhausted, but foul beasts do not care... They circle in");
            Typewriter("for the kill, and you know your time is up...");
            Wait(3);
            GameOver("Gotta go fast next time");
        }
    }
    Console.WriteLine();
    Typewriter("You have defeated the beast, and it lies on the ground, lifeless. You just");
    Typewriter("stand there... the journeys over (in this demo lol)");
    Console.WriteLine();
    Wait(3);
    Typewriter("Congratulations, " + GameState.PlayerName + "! You have survived the forest and defeated the beast.");
    Console.WriteLine();
    Wait(2);
    Typewriter("Thanks for playing, ig...");
    Environment.Exit(0);

}
public static class GameState
{
    public static int PlayerHP = 20;
    public static string PlayerName = "Player";
}