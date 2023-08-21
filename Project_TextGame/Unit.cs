
using System.Reflection;
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
        gold = 10000;

        inventory.Add(new SteelSword());
        inventory.Add(new WoodShield());
        inventory.Add(new PlateArmour());

        equipmentParts[0] = (Equipment)Inventory[1];
        equipmentParts[1] = (Equipment)Inventory[0];
        ScanItemStatus();
    }
    // Player 스테이터스
    string job;
    int level = 1;
    int levelUpExp = 20;

    int itemHp = 0;
    int itemAtk = 0;
    int itemDef = 0;



    List<Item> inventory = new List<Item>(10);
    List<Equipment> equipmentParts = new List<Equipment>(7) { new Equipment(), new Equipment(), new Equipment(), new Equipment(),new Equipment(), new Equipment(), new Equipment()};

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
        set { inventory = value; }
    }

    public List<Equipment> EquipmentParts
    {
        get { return equipmentParts; }
        set { equipmentParts = value; }
    }

    //아이템 추가
    public void AddItem(Item newItem)
    {
        while (true)
        {
            if (inventory.Count < 10)
            {
                Inventory.Add(newItem);
                break;
            }
            Console.WriteLine("\n가방이 가득 찼습니다.");
            Console.WriteLine("1. 교체한다 2. 버린다");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(2);
            if (inputKey == ConsoleKey.D2) // 2. 버린다
            {
                return;
            }

            RenderInventori(true);
            Console.WriteLine("\n교체할 아이템을 선택해주세요.");
            inputKey = GameManager.GM.ReadNunberKeyInfo(Inventory.Count + 1);
            if (inputKey == ConsoleKey.D1)
            {
                StringBuilder printAdditem = new StringBuilder($"{Inventory[1 - 1].Name}을 ");
                Inventory[1 - 1] = newItem;
                printAdditem.Append($"{newItem.Name}으로 교체하였습니다.");
                break;
            }
            else if (inputKey == ConsoleKey.D2)
            {
                Inventory[2 - 1] = newItem;
                break;
            }
            else if (inputKey == ConsoleKey.D3)
            {
                Inventory[3 - 1] = newItem;
                break;
            }
            else if (inputKey == ConsoleKey.D4)
            {
                Inventory[4 - 1] = newItem;
                break;
            }
            else if (inputKey == ConsoleKey.D5)
            {
                Inventory[5 - 1] = newItem;
                break;
            }
            else
            {
                break;
            }

        }

        Console.WriteLine($"\n{newItem.Name}을 획득하였습니다.");
        GameManager.GM.PressAnyKey();
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


    // 인벤토리 UI
    public void InventoriUI()
    {
        while (true)
        {
            RenderInventori(false);
            Console.WriteLine("1. 장착 관리 2. 정렬 3. 닫기");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(3);
            if (inputKey == ConsoleKey.D1)
            {
                ChangeEquipmentUI();
            }
            else if (inputKey == ConsoleKey.D2)
            {
                Inventory = Inventory.OrderBy(item => item.ID).ToList();
                Inventory = Inventory.OrderBy(item => item.ID).ToList();
            }
            else if (inputKey == ConsoleKey.D3)
            {
                return;
            }
        }
    }

    // 아이템 장착 UI
    public void ChangeEquipmentUI()
    {
        RenderInventori(true);
        Console.WriteLine("장착 또는 해제할 아이템을 골라주세요.");
        ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(Inventory.Count);
        // 49 = 1번
        if ((int)inputKey >= 49 && (int)inputKey <= 57)
        {
            ChangeParts((Equipment)Inventory[(int)inputKey - 49]);
        }

       
    }


    // 아이템 장착 및 해제
    public void ChangeParts(Equipment item)
    {
        // 장착 아이템 선택시 해제
        for (int num = 0; num < equipmentParts.Count; num++)
        {
            if (item == equipmentParts[num])
            {
                equipmentParts[num] = null;
                ScanItemStatus();
                return;
            }
        }

        // 선택 아이템을 장착
        switch ((EquipmentPart)item.Part)
        {
            case EquipmentPart.Left:
                equipmentParts[(int)EquipmentPart.Left] = item;
                break;
            case EquipmentPart.Right:
                equipmentParts[(int)EquipmentPart.Right] = item;
                break;
            case EquipmentPart.TwoHand:
                equipmentParts[(int)EquipmentPart.Left] = null;
                equipmentParts[(int)EquipmentPart.Right] = item;
                break;
            case EquipmentPart.Head:
                equipmentParts[(int)EquipmentPart.Head] = item;
                break;
            case EquipmentPart.Top:
                equipmentParts[(int)EquipmentPart.Top] = item;
                break;
            case EquipmentPart.Pants:
                equipmentParts[(int)EquipmentPart.Pants] = item;
                break;
            case EquipmentPart.Shoes:
                equipmentParts[(int)EquipmentPart.Shoes] = item;
                break;
            default:
                break;
        }

        ScanItemStatus();
    }

    // 아이템 스테이터스 검사
    public void ScanItemStatus()
    {
        itemHp = 0;
        itemAtk = 0;
        itemDef = 0;
        foreach (var item in equipmentParts)
        {
            if (item == null)
            {
                continue;
            }
            itemAtk += item.Atk;
            itemDef += item.Def;
        }
    }

    // 스테이터스 내용 출력
    public override void RenderStatus()
    {
        Console.WriteLine($"{Name} ___________");
        Console.WriteLine($"직업 : {Job}");
        Console.WriteLine($"체력 : {Hp} / {MaxHp}");
        Console.WriteLine($"경험치 : {Exp} / {LevelUpExp}");
        Console.WriteLine($"공격력 : {Atk} + {itemAtk}");
        Console.WriteLine($"방어력 : {Def} + {itemDef}\n");

        Console.WriteLine("1. 돌아가기");
        ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(1);
        if (inputKey == ConsoleKey.D1)
        {
            return;
        }
    }

    // 인벤토리 내용 출력
    public void RenderInventori(bool isSeclet)
    {
        Console.Clear();
        Console.WriteLine($"[가방] ______________ 가진 금화 : {Gold}\n");
        for (int num = 0; num < Inventory.Count; num++)
        {
            StringBuilder invenText = new StringBuilder();

            if (isSeclet == true)
            {
                invenText.Append($"{num + 1}. ");

            }
            foreach (var item in equipmentParts)
            {
                if (item == Inventory[num])
                {
                    invenText.Append("[E]");
                }

            }
            invenText.Append($"{Inventory[num].Name}\t\t");

            if (Inventory[num] is Equipment)
            {
                if (((Equipment)Inventory[num]).Atk != 0)
                {
                    invenText.Append($"ATK : {((Equipment)Inventory[num]).Atk}\t");
                }
                if (((Equipment)Inventory[num]).Def != 0)
                {
                    invenText.Append($"DEF : {((Equipment)Inventory[num]).Def}");

                }

            }

            Console.WriteLine(invenText);
        }
        Console.WriteLine("___________________\n");
    }
}


class Monster : Unit
{

}