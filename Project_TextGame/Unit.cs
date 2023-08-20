
using System.Text;

interface IInventory
{
    public List<Item> Inventory { get; }
}

class Unit
{
    protected string name = "";
    protected int maxHp;
    protected int hp;
    protected int atk;
    protected int def;
    protected int exp;
    protected int gold;
    protected bool isDead = false;

    public string Name { get { return name; } set { name = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public virtual int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
            }
        }
    }
    public int Atk { get { return atk; } set { atk = value; } }
    public int Def { get { return def; } set { def = value; } }
    public virtual int Exp { get { return exp; } set { exp = value; } }
    public int Gold { get { return gold; } set { gold = value; } }

    public virtual void RenderStatus() { }
}

class Player : Unit, IInventory
{
    public Player()
    {
        job = "";
        maxHp = 100;
        hp = 100;
        atk = 20;
        def = 3;
        exp = 0;
        gold = 0;
    }
    // Player 스테이터스
    string job;
    int level = 1;
    int levelUpExp = 20;

    List<Item> inventory = new List<Item>(10);

    // 스테이터스 접근 함수
    public string Job { get { return job; } set { job = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int LevelUpExp { get { return levelUpExp; } }
    public override int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            if (exp >= levelUpExp)
            {
                LevelUp();
            }
        }
    }

    public List<Item> Inventory
    {
        get { return inventory; }
    }

    public void AddItem(Item newItem)
    {
        if (inventory.Count >= 10)
        {
            Console.WriteLine("\n가방이 가득 찼습니다.");
            Console.WriteLine("1. 교체한다 2. 버린다");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(2);
            if (inputKey == ConsoleKey.D2)
            {
                return;
            }
        }
    }

    public void ChangeItem(Item newItem)
    {
    }

    //레벨 업 함수
    void LevelUp()
    {
        exp -= levelUpExp;
        level++;
        MaxHp += 10;
        Hp = MaxHp;
        atk += 2;
        def += 1;
        Console.WriteLine("레벨업");
    }

    // 스테이터스 출력
    public override void RenderStatus()
    {
        Console.WriteLine($"정보 {Name} ___________");
        Console.WriteLine($"직업 : {Job}");
        Console.WriteLine($"체력 : {Hp} / {MaxHp}");
        Console.WriteLine($"경험치 : {Exp} / {LevelUpExp}");
        Console.WriteLine($"공격력 : {Atk}");
        Console.WriteLine($"방어력 : {Def}\n");

        Console.WriteLine("1. 돌아가기");
        ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(1);
        if (inputKey == ConsoleKey.D1)
        {
            return;
        }
    }

    // 인벤토리 출력
    public void RenderInventori()
    {
        Console.WriteLine("가방 ______________");

        for (int num = 0; num < Inventory.Count; num++)
        {
            StringBuilder invenText = new StringBuilder();
            invenText.Append($"{num+1}. {Inventory[num].Name}\t{Inventory[num].Gold}");
            Console.WriteLine(invenText);
        }
        Console.WriteLine("___________________\n");
        Console.ReadLine();
    }


    public void OpenInventori()
    {
        RenderInventori();
        Console.WriteLine("1. 장착 관리 2. 닫기");
        ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(2);
        if (inputKey == ConsoleKey.D1) 
        {
            Console.WriteLine("장착 관리");
        }
        else if (inputKey == ConsoleKey.D2)
        {
            return;
        }

    }

}

class Monster : Unit
{

}