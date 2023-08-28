class GameManager
{
    public static GameManager GM;

    public GameManager()
    {
        GM = this;
    }

    // 키 읽어오기
    public ConsoleKey ReadNunberKeyInfo(int numberOfOption)
    {
        ConsoleKey InputKey = Console.ReadKey(true).Key;
        {
            if ((int)InputKey >= 48 && (int)InputKey < (int)ConsoleKey.D1 + numberOfOption)
            {
                return InputKey;
            }
        }
        Console.WriteLine("선택지를 다시 선택해주세요.");
        Thread.Sleep(1000);
        return ConsoleKey.F24;

    }
    
    // 엔터키 눌러주세요
    public void PressEnterKey()
    {
        Console.WriteLine("\nPress 'Enter' Key To Continue");
        Console.ReadLine();
    }

    public void OneMoreChance()
    {
        Console.Clear();
        Console.WriteLine("\n\n\n");
        string deathTxt = "진짜 이번 딱 한번만 살려드립니다.";
        Console.Write("\t\t");
        foreach (var txt in deathTxt)
        {
            Console.Write(txt);
            Thread.Sleep(200);
        }
        Thread.Sleep(1000);
    }

    public Item CopyItem(int code)
    {
        Item item;
        switch ((ItemType)code)
        {
            case ItemType.SteelSword:
                item = new SteelSword();
                break;
            case ItemType.ShortBow:
                item = new ShortBow();
                break;
            case ItemType.LongLance:
                item = new LongLance();
                break;
            case ItemType.WoodShield:
                item = new WoodShield();
                break;
            case ItemType.SteelShield:
                item = new SteelShield();
                break;
            case ItemType.LeatherArmour:
                item = new LeatherArmour();
                break;
            case ItemType.PlateArmour:
                item = new PlateArmour();
                break;
            case ItemType.LeatherPants:
                item = new LeatherPants();
                break;
            case ItemType.LeatherShoes:
                item = new LeatherShoes();
                break;
            default:
                item = new Item();
                Console.WriteLine("public Item CopyItem(ItemID code)");
                break;
        }
        return item;
    }
}