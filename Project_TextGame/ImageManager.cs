using System.Text;

class ImageManager
{
    public static ImageManager IM;
    ConsoleColor monsterColor;
    ConsoleColor playerColor;
    string Town =
        "┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓\n" +
        "┃                                              _.-^-._    .--.      ┃\n" +
        "┃                       라 면 마 을         .-'   _   '-. |__|      ┃\n" +
        "┃                                          /     |_|     \\|  |      ┃\n" +
        "┃          _____A_                        /               \\  |      ┃\n" +
        "┃         /      /\\                      /|     _____     |\\ |      ┃\n" +
        "┃      __/__/\\__/  \\___                   |    |==|==|    |  |      ┃\n" +
        "┃     /__|\" '' \"| /___/\\      |---|---|---|    |--|--|    |  |      ┃\n" +
        "┃     |''|\"'||'\"| |' '||      |---|---|---|    |==|==|    |  |      ┃\n" +
        "┃     `\"\"`\"\"))\"\"`\"`\"\"\"\"`    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^          ┃\n" +
        "┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n";

    string Dungeon =
    "┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓\n" +
    "┃          /\\                                                       ┃\n" +
    "┃         /**\\                        어두운 숲                     ┃\n" +
    "┃        /****\\   /\\                                                ┃\n" +
    "┃       /      \\ /**\\                                               ┃\n" +
    "┃      /  /\\    /    \\        /\\    /\\  /\\      /\\            /\\/\\/\\┃\n" +
    "┃     /  /  \\  /      \\      /  \\/\\/  \\/  \\  /\\/  \\/\\  /\\  /\\/ / /  ┃\n" +
    "┃    /  /    \\/ /\\     \\    /    \\ \\  /    \\/ /   /  \\/  \\/  \\  /   ┃\n" +
    "┃   /  /      \\/  \\/\\   \\  /      \\    /   /    \\                   ┃\n" +
    "┃__/__/_______/___/__\\___\\__________________________________________┃\n" +
    "┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n";

    string Shop =
        "┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓\n" +
        "┃          ______                                                   ┃\n" +
        "┃    ＿＿／      ＼                                                 ┃\n" +
        "┃   (  ＿     -ω- |                                                 ┃\n" +
        "┃    ＼\\          ｜              ※주의 사항※                       ┃\n" +
        "┃    ∠ノ        / ｜               환불 불가                        ┃\n" +
        "┃    (         ／／                교환 불가                        ┃\n" +
        "┃    ｜  ＿＿ノ￣￣)               카드 거절                        ┃\n" +
        "┃    ｜ /    ￣/ ／                                                 ┃\n" +
        "┃    ｜/      /／                                                   ┃\n" +
        "┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n";

    public void vsMonster(Unit player, Unit monster)
    {
        float playerHP = (float)player.Hp / player.MaxHp;
        float monsyerHp = (float)monster.Hp / monster.MaxHp;

        if (playerHP >= 0.7f)
        {
            playerColor = ConsoleColor.Green;
        }
        else if (playerHP >= 0.3f)
        {
            playerColor = ConsoleColor.Yellow;
        }
        else 
        { 
            playerColor = ConsoleColor.Red;
        }

        if (monsyerHp >= 0.7f)
        {
            monsterColor = ConsoleColor.Green;
        }
        else if (monsyerHp >= 0.3f)
        {
            monsterColor = ConsoleColor.Yellow;
        }
        else
        {
            monsterColor = ConsoleColor.Red;
        }

        StringBuilder monsterLvtxt = new StringBuilder("┏━━━━━━━━━━━━━━━━━━━━━━━━  < 괴 물 출 현 >  ━━━━━━━━━━━━━━━━━━━━━━━━┓");
        if (((Monster)monster).isRevision == true)
        {
            monsterLvtxt.Replace("괴 물", "강 적");
        }
        Console.WriteLine(monsterLvtxt);
        Console.WriteLine("┃                                                                   ┃");
        Console.Write("┃                    ");
        Console.ForegroundColor= playerColor;
        Console.Write("∧_∧                  ");
        Console.ForegroundColor= monsterColor;
        Console.Write(" ∧∧                       ");
        Console.ResetColor();
        Console.Write("┃\n");
        Console.Write("┃                   ");
        Console.ForegroundColor= playerColor;
        Console.Write("(r' ')r              ");
        Console.ForegroundColor= monsterColor;
        Console.Write("('' )/                     ");
        Console.ResetColor();
        Console.Write("┃\n");
        Console.Write("┃                    ");
        Console.ForegroundColor= playerColor;
        Console.Write("/ /       ");
        Console.ForegroundColor= ConsoleColor.White;
        Console.Write("VS      ");
        Console.ForegroundColor= monsterColor;
        Console.Write("ノ/  /                       ");
        Console.ResetColor();
        Console.Write("┃\n");
        Console.Write("┃                  ");
        Console.ForegroundColor= playerColor;
        Console.Write("ノ￣>                ");
        Console.ForegroundColor= monsterColor;
        Console.Write("ノ￣＞                      ");
        Console.ResetColor();
        Console.Write ("┃\n");
        Console.WriteLine("┃                                                                   ┃");
        Console.WriteLine("┃                    나                  상대                       ┃");
        Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
    }

    public ImageManager() 
    {
        IM = this;
    }

    public void RenderImage(string place)
    {
        switch (place)
        {
            case "Town":
                Console.WriteLine(Town);
                break;
            case "Dungeon":
                Console.WriteLine(Dungeon);
                break;
            case "Shop":
                Console.WriteLine(Shop);
                break;


            default:
                break;
        }
    }
}

