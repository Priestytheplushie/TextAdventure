using System;
using System.Threading;

Random rand = new Random();

Console.Write("\u001b[0m");

Console.WriteLine($"{Colors.Bold}{Colors.Magenta}Welcome to TextAdventure{Colors.Reset}"); 
Console.WriteLine();
Console.WriteLine($"{Colors.DarkGray}Disclaimer: This game is pretty trash lol{Colors.Reset}");
Console.Write($"{Colors.Yellow}Proceed (y/n)? {Colors.Reset}");
string proceed = Console.ReadLine() ?? "n";
if (proceed.ToLower() != "y")
{
    Console.WriteLine($"{Colors.Red}Exiting game...{Colors.Reset}");
    return;
}
Console.WriteLine();
Wait(1);
Console.Write($"{Colors.Cyan}Enter your name: {Colors.Reset}");
GameState.PlayerName = Console.ReadLine() ?? "Player";
if (string.IsNullOrWhiteSpace(GameState.PlayerName)) GameState.PlayerName = "Player";

Console.WriteLine();
Typewriter($"Welcome, {Colors.Green}{GameState.PlayerName}{Colors.Reset}! Let's begin your adventure.");

Wait(3);
Console.WriteLine();
Typewriter($"You find yourself in a dark forest.... {Colors.Bold}{Colors.Red}ALONE.{Colors.Reset} The trees tower above you, and the path ahead is unclear.\n");

string[] forestChoices = {
    "Explore the forest",
    "Scavenge for Resources",
    "Call for help"
};

int choice = GetChoice("What will you do?", forestChoices);

switch(choice)
{
    case 1:
        Typewriter("You step deeper into the forest, pushing through the thick undergrowth.");
        Typewriter($"The howls of {Colors.Red}distant creatures{Colors.Reset} echo through the trees. You feel a sense");
        Typewriter("of unease as you continue forward, unsure of what lies ahead.");
        Wait(3);
        Console.WriteLine();
        Typewriter($"Suddenly, a {Colors.Red}{Colors.Bold}wild beast{Colors.Reset} jumps before you! Its howls fill the air. You must act quickly!");
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
                Typewriter("You awkwardly approach the beast, its teeth bared and eyes glowing. You swing your");
                Typewriter($"fists wildly, making you look like a fool. The beast {Colors.Red}eats your head off...{Colors.Reset} You are dead.");
                Wait(3);
                GameOver("Who brings their fists to a wild fight? You apparently...");
                break;
            case 2:
                Typewriter("You turn and run, stumbling over rocks and branches, the beast growls menacingly behind you...");
                Wait(3);
                Console.WriteLine();
                int escapeRoll = rand.Next(1, 6);
                if (escapeRoll <= 3)
                {
                    Typewriter("As you rush through the woods, the beast catches up to you and tears you apart.");
                    Typewriter($"You lay on the ground helplessly, until it fades to black... {Colors.Red}You are dead.{Colors.Reset}");
                    Wait(3);
                    GameOver("You should have fought the beast or tried to tame it. Running away was not the best choice.");
                }
                else
                {
                    Typewriter("You manage to escape the beast, your heart pounding with adrenaline. You find a small clearing with");
                    Typewriter("a small rock, and stop for a rest.");
                    Console.WriteLine();
                    Wait(2);
                    Typewriter($"{Colors.Green}You restored +5 HP{Colors.Reset}");
                    GameState.PlayerHP += 5;
                    Clearing();
                }
                break;
            case 3:
                Typewriter("You cautiously approach the beast, speaking softly and extending your hand. The beast tilts its head");
                Typewriter("in confusion, but after a tense moment, it...");
                Wait(3);
                Typewriter(".");
                Wait(1);
                Typewriter("..");
                Wait(1);
                Typewriter("...");
                Wait(1);
                Typewriter($"{Colors.Red}eats your head off.{Colors.Reset} You are dead.");
                Wait(3);
                GameOver("Mercy does not work on the wild...");
                break;
        }
        break;
    case 2:
        Typewriter("You wander through the forest, looking for anything of value. You push");
        Typewriter("through branches and leaves, but come up empty-handed. As you start to");
        Typewriter("turn back, you spot an empty clearing up ahead...");
        Wait(3);
        Clearing();
        break;
    case 3: 
        Typewriter("You cry out for help desperately, but the forest remains silent. The howls of the");
        Typewriter("wild creatures call to you... This was a bad idea, you think to yourself. You start");
        Typewriter($"to run, but the howls grow louder... {Colors.Red}You're not getting out of this...{Colors.Reset}");
        Wait(3);
        GameOver("What part of \"Alone\" did you not understand?");
        break;
}

void Wait(double seconds)
{
    Thread.Sleep((int)(seconds * 1000));
}

int GetChoice(string prompt, string[] options)
{
    int selectedChoice = -1;

    while (selectedChoice < 1 || selectedChoice > options.Length)
    {
        Typewriter($"{Colors.Cyan}{prompt}{Colors.Reset}");
        
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"  {Colors.Yellow}{i + 1}.{Colors.Reset} {options[i]}");
        }

        Console.Write($"{Colors.Bold}> {Colors.Reset}");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out selectedChoice) || selectedChoice < 1 || selectedChoice > options.Length)
        {
            Console.WriteLine($"\n{Colors.Red}[Invalid choice. Please enter a number between 1 and {options.Length}]{Colors.Reset}\n");
            selectedChoice = -1; 
        }
    }

    return selectedChoice;
}

void Typewriter(string text, int delayMs = 30)
{
    bool inAnsi = false;
    foreach (char c in text)
    {
        if (c == '\u001b') inAnsi = true;
        
        Console.Write(c);
        
        if (inAnsi)
        {
            if (c == 'm') inAnsi = false;
        }
        else
        {
            Thread.Sleep(delayMs);
        }
    }
    Console.WriteLine();
}

void GameOver(string message)
{
    Console.WriteLine();
    Typewriter($"{Colors.Red}{Colors.Bold}=== GAME OVER ==={Colors.Reset}");
    Typewriter($"{Colors.Red}{message}{Colors.Reset}");
    Wait(2);
    
    Console.WriteLine($"\n{Colors.DarkGray}Press any key to exit...{Colors.Reset}");
    Console.ReadKey(true);
    
    Environment.Exit(0); 
}

void Clearing()
{
    Typewriter("The clearing gives you a beautiful view of the darkening sky, and as you wait there,");
    Typewriter("you feel almost at peace, but then you hear a rustling... Was it the wind, a beast, or");
    Typewriter("something else? You can't be sure, but you know you need to be ready for anything.");
    Wait(3);
    Typewriter($"Suddenly, a pain in your stomach makes you dizzy, you need to find some food... {Colors.Yellow}fast.{Colors.Reset}");
    Typewriter("You scan the surroundings and spot a small animal, which could be a source of food, but it could");
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
            Typewriter($"for your knife, but... {Colors.Yellow}you don't have any knife.{Colors.Reset} The animal, seeing your weakness, bites you!");
            Console.WriteLine();
            Typewriter($"{Colors.Red}You took 5 damage!{Colors.Reset}");
            GameState.PlayerHP -= 5;
            if (GameState.PlayerHP <= 0)
            {
                Wait(2);
                GameOver("Stop bullying defenseless animals, you monster.");
            }
            else
            {
                Typewriter("You manage to scare the animal away, but you are left with a wound and a sense of disappointment.");
                Typewriter("How could you have lost to a simple creature? Oh well, be grateful you survived at all...");
                Wait(2);
                Clearing();
            }
            break;
        case 2:
            Typewriter("You decide to ignore the animal, and continue your everlasting thirst for food. You search the clearing");
            Typewriter($"and finally find a small berry bush, and eat the berries, restoring your health.");
            GameState.PlayerHP += 5;
            Typewriter($"{Colors.Green}You restored +5 HP{Colors.Reset}");
            Wait(2);
            Typewriter($"But as you finish eating, you hear a rustling in the bushes, and a {Colors.Red}wild beast{Colors.Reset} jumps out at you!");
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
            GameOver("What a coward, imagine giving up in a forest. You should be ashamed of yourself.");
            break;
    }
}

void BeastCombat()
{
    int beastHP = 40;
    int beastAttack = 5;
    int beastDefense = 2;
    int turnCount = 0;
    int turnLimit = 10;
    string[] fightChoices =
    {
        "Attack",
        "Defend",
        "Flee",
    };
    Typewriter($"{Colors.Red}{Colors.Bold}The forest beast appears...{Colors.Reset}");
    Wait(3);
    while (beastHP > 0 && GameState.PlayerHP > 0)
    {
        turnCount++;
        Console.WriteLine($"\n{Colors.Magenta}Turn {turnCount}:{Colors.Reset} {Colors.DarkGray}(Death in {turnLimit - turnCount}){Colors.Reset}");
        int choice = GetChoice("What do you do?", fightChoices);
        switch(choice)
        {
            case 1:
                int playerAttack = rand.Next(1, 10);
                int damageToBeast = Math.Max(playerAttack - beastDefense, 0);
                beastHP -= damageToBeast;
                Typewriter($"You punch the beast, dealing {Colors.Yellow}{damageToBeast}{Colors.Reset} damage! Beast HP: {Colors.Red}{beastHP}{Colors.Reset}");
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
                    Typewriter($"{Colors.Green}You successfully escape from the beast!{Colors.Reset}");
                    Console.WriteLine();
                    Typewriter($" {Colors.Red} but than die from hunger... RIP");
                    GameOver("Stop being a coward!");
                    break;
                }
                else
                {
                    Typewriter($"{Colors.Red}You fail to escape, and the beast attacks you!{Colors.Reset}");
                }
                break;
        }
        if (GameState.PlayerHP > 0)
        {
            int damageToPlayer = Math.Max(beastAttack - rand.Next(1, 5), 0);
            GameState.PlayerHP -= damageToPlayer;
            Typewriter($"The beast bites you, dealing {Colors.Red}{damageToPlayer}{Colors.Reset} damage! Your HP: {Colors.Green}{GameState.PlayerHP}{Colors.Reset}");
        }

        if (GameState.PlayerHP <= 0)
        {
            Typewriter($"You fall to the ground, and the beast stands over you, victorious. {Colors.Red}You have been defeated.{Colors.Reset}");
            Wait(3);
            GameOver("Ouch... That's gotta hurt (both physically and emotionally).");
        }

        if (turnCount >= turnLimit)
        {
            Typewriter("You feel exhausted, but foul beasts do not care... They circle in");
            Typewriter($"for the kill, and you know your time is up...");
            Wait(3);
            GameOver("Gotta go fast next time!");
        }
    }
    Console.WriteLine();
    Typewriter("You have defeated the beast, and it lies on the ground, lifeless. You just");
    Typewriter("stand there... the journey's over (in this demo lol)");
    Console.WriteLine();
    Wait(3);
    Typewriter($"{Colors.Green}{Colors.Bold}Congratulations, {GameState.PlayerName}! You have survived the forest and defeated the beast.{Colors.Reset}");
    Console.WriteLine();
    Wait(2);
    Typewriter($"{Colors.DarkGray}Thanks for playing, ig...{Colors.Reset}");
    Environment.Exit(0);
}

public static class GameState
{
    public static int PlayerHP = 20;
    public static string PlayerName = "Player";
}

public static class Colors
{
    public const string Reset = "\u001b[0m";
    public const string Bold = "\u001b[1m";
    public const string Red = "\u001b[31m";
    public const string Green = "\u001b[32m";
    public const string Yellow = "\u001b[33m";
    public const string Cyan = "\u001b[36m";
    public const string Magenta = "\u001b[35m";
    public const string DarkGray = "\u001b[90m";
}