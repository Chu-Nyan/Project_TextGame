
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


    public virtual void RenderStatus() { }
}

class Player : Unit, IInventory
{
    public Player()
    {
        maxHp = 100;
        hp = 100;
        atk = 20;
        def = 3;
        exp = 0;
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

    public void RenderStatus()
    {
        Console.WriteLine($"정보 {Name} ___________");
        Console.WriteLine($"직업 : {Job}");
        Console.WriteLine($"체력 : {Hp} \t\\{MaxHp}");
        Console.WriteLine($"경험치 : {Exp} \t\\{LevelUpExp}");
        Console.WriteLine($"공격력 : {Atk} \t\\방어력 : {Def}\n");

        Console.WriteLine("1. 장비 교체 \n2. 아이템 사용\n3.돌아가기");
        ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(3);
        if (inputKey == ConsoleKey.D1)
        {
            Console.WriteLine("장비 교체");
        }
        else if (inputKey == ConsoleKey.D2)
        {
            Console.WriteLine("아이팀 사용");
        }
        else if (inputKey == ConsoleKey.D3)
        {
            return;
        }
    }

    public void RenderInventori()
    {

    }

    public void OpenInventori()
    {

    }

}

class Monster : Unit
{

}