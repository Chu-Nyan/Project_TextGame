using System.Text;

enum MonsterCode
{
    Goblin,
    Orc
}

interface IInventory
{
    public List<Item> Inventory { get; }
}


struct BattlePhase
{
    Player player;
    Monster monster;
    public BattlePhase(Player player, Monster monster)
    {
        this.player = player;
        this.monster = monster;
    }

    public void BattleScene()
    {
        RenderBattleScene();
    }

    void RenderBattleScene()
    {
        while (player.IsDead ==false && monster.IsDead ==false)
        {
            Console.Clear();
            Console.WriteLine($"\t{player.Name}  VS  {monster.Name}    ");
            Console.WriteLine($"{player.Hp}\t  VS  {monster.Hp}");

            BattleUnits();
        }
    }

    void BattleUnits()
    {
        StringBuilder playerAtkTxt = new StringBuilder($"{player.Name}의 공격!!");
        StringBuilder playerDefTxt = new StringBuilder();
        StringBuilder MonsterAtkTxt = new StringBuilder();
        StringBuilder MonsterDefTxt = new StringBuilder();
        AttckUnit(player, monster);
    }

    void AttckUnit(Unit atkUnit, Unit defUnit)
    {
        int damage = atkUnit.Atk - defUnit.Def;
        if (damage < 1)
        {
            damage = 1;
        }
        defUnit.Hp -= damage;

        StringBuilder playerAtkTxt = new StringBuilder($"{atkUnit.Name}의 공격!!");
        StringBuilder playerDefTxt = new StringBuilder($"{defUnit.Name}은 {damage}의 피해를 입었다!!");
        Thread.Sleep(1000);
        Console.WriteLine(playerAtkTxt);
        Thread.Sleep(1000);
        Console.WriteLine(playerDefTxt);
        Thread.Sleep(1000);
    }

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
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    public virtual void RenderStatus() { }
}

class Player : Unit, IInventory
{
    public Player()
    {
        name = "츄냥";
        job = "디버그";
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
    List<Equipment> equipmentParts = new List<Equipment>(7) { new Equipment(), new Equipment(), new Equipment(), new Equipment(), new Equipment(), new Equipment(), new Equipment() };

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
        bool switchSeclectInven = false;
        while (true)
        {
            Console.Clear();
            RenderStatus();

            if (switchSeclectInven ==false)
            {
                RenderInventori(switchSeclectInven);
                Console.WriteLine("1. 장착 관리 2. 정렬 3. 닫기");
                ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(3);
                if (inputKey == ConsoleKey.D1)
                {
                    switchSeclectInven = true;
                    continue;
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
            else
            {
                ChangeEquipmentUI();
                switchSeclectInven = false;

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
        StringBuilder topTextBar = new StringBuilder("┌───────────────────────────────────────────────┐");
        topTextBar.Remove(2, 4 + name.Length*2);
        topTextBar.Insert(2, "　"+name+ "　");

        Console.WriteLine(topTextBar);
        Console.WriteLine($"│   직업 : {Job}             \t\t\t│");
        Console.Write($"│   체력 : {Hp} / {MaxHp}\t 공격력 : {Atk} + {itemAtk}");
        Console.WriteLine("\t│");
        Console.Write($"│   경험치 : {Exp} / {LevelUpExp}\t 방어력 : {Def} + {itemDef}");
        Console.WriteLine("　\t│");
        Console.WriteLine("└───────────────────────────────────────────────┘");


    }

    public void RenderChoiceOne()
    {

    }

    // 인벤토리 출력
    public void RenderInventori(bool isSeclet)
    {
        Console.WriteLine($"[가방]                         가진 금화 : {Gold}");
        Console.WriteLine("┌───────────────────────────────────────────────┐");

        for (int num = 0; num < Inventory.Count; num++)
        {
            StringBuilder invenText = new StringBuilder("│ ");
            if (isSeclet == true) // 선택형 인벤토리일경우 숫자 추가
            {
                invenText.Append($"{num + 1}. ");

            }
            foreach (var item in equipmentParts) // 장착한 아이템일 경우 [E]추가
            {
                if (item == Inventory[num])
                {
                    invenText.Append("[E]");
                }

            }
            invenText.Append($"{Inventory[num].Name}");

            int twoSpacebarChar = 0;

            for (int i = 0; i < Inventory[num].Name.Length; i++) // 인벤토리의 한글(스페이스바 두 개 크기)은 몇개인가?
            {
                if (Inventory[num].Name[i] == ' ')
                {
                    continue;
                }
                twoSpacebarChar ++;
            }
            int nameSpaceNum = 25 - (invenText.Length+twoSpacebarChar);
            for (int i = 0; i < nameSpaceNum; i++) // 한글까지 감안하여 빈공간 추가
            {
                invenText.Append(' ');
            }
            invenText.Append("│  ");
            int hereLength = invenText.Length;
            if (Inventory[num] is Equipment) // 능력치 UI 추가
            {
                if (((Equipment)Inventory[num]).Atk != 0)
                {
                    invenText.Append($"ATK : {((Equipment)Inventory[num]).Atk}".PadRight(10,' '));
                }
                if (((Equipment)Inventory[num]).Def != 0)
                {
                    invenText.Append($"DEF : {((Equipment)Inventory[num]).Def}".PadRight(10, ' '));
                }
                invenText.Append("                                     ");
                invenText.Insert(hereLength + 20, '│');
            }
            for (int i = 0; i < invenText.Length; i++) // [E] 색깔 칠하기
            {
                if (invenText[i] == '[')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }else if (invenText[i] == ']')
                {
                    Console.Write(invenText[i]);
                    Console.ResetColor();
                    continue;
                }

                Console.Write(invenText[i]);

            }
            Console.WriteLine();
        }
        Console.WriteLine("└───────────────────────────────────────────────┘");
    }
}


struct MonsterDB
{
    public MonsterDB(MonsterCode code, out string name, out int hp, out int atk, out int def)
    {
        switch (code)
        {
            case MonsterCode.Goblin:
                name = "고블린";
                hp = 40;
                atk = 5;
                def = 2;
                break;
            case MonsterCode.Orc:
                name = "오크";
                hp = 60;
                atk = 10;
                def = 5;
                break;
            default:
                name = "오류";
                hp = 1;
                atk = 0;
                def = 0;
                break;
        }
    }
}

class Monster : Unit
{
    public Monster(MonsterCode code)
    {
        MonsterDB monsterDB = new MonsterDB(MonsterCode.Goblin, out name, out hp, out atk, out def);
    }
}