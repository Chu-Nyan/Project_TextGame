using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

enum MonsterCode
{
    Goblin,
    Orc,
    Golem
}

interface IInventory
{
    public List<Item> Inventory { get; }
}

// 전투 페이즈
struct BattlePhase
{
    Player player;
    Monster monster;

    StringBuilder playerAtkTxt = new StringBuilder();
    StringBuilder playerDefTxt = new StringBuilder();

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
        bool isMyTurn = true;
        while (true)
        {
            Console.Clear();
            ImageManager.IM.vsMonster(player, monster);

            Console.WriteLine("전투 메세지 :");

            if (player.IsDead == true)
            {
                player.DeadTigger(monster);
                Thread.Sleep(2000);
                return;
            }
            if (isMyTurn == true)
            {
                AttckUnit(player, monster);
                isMyTurn = false;
            }
            else if (isMyTurn == false)
            {
                Console.WriteLine(playerAtkTxt);
                Console.WriteLine(playerDefTxt);
                if (monster.IsDead == true)
                {
                    monster.DeadTigger(player);
                    Thread.Sleep(2000);
                    return;
                }
                AttckUnit(monster, player);
                isMyTurn = true;
            }
        }
    }

    void AttckUnit(Unit atkUnit, Unit defUnit)
    {
        int damage = atkUnit.Atk - defUnit.Def;
        if (damage < 1)
        {
            damage = 1;
        }
        defUnit.Hp -= damage;

        playerAtkTxt = new StringBuilder($"\n{atkUnit.Name}{AddAttckText()}");
        playerDefTxt = new StringBuilder($"{defUnit.Name}은 {damage}의 피해를 입었다!!");
        Thread.Sleep(1000);
        Console.WriteLine(playerAtkTxt);
        Thread.Sleep(1000);
        Console.WriteLine(playerDefTxt);
        Thread.Sleep(1000);
    }

    StringBuilder AddAttckText()
    {
        Random randomText = new Random();
        StringBuilder attckText = new StringBuilder();

        switch (randomText.Next(0, 7))
        {
            case 0:
                attckText.Append("은(는) 왼쪽으로 파고 들었다");
                break;
            case 1:
                attckText.Append("은(는) 하늘로 날아올랐다.");
                break;
            case 2:
                attckText.Append("은(는) 사라졌다가 뒤에서 나타났다.");
                break;
            case 3:
                attckText.Append("이(가) 밥상을 뒤집었다!!");
                break;
            case 4:
                attckText.Append(" \"선라이트 옐로 오버드라이브!!\"");
                break;
            case 5:
                attckText.Append(" \"오라오라오라오라\"");
                break;
            case 6:
                attckText.Append("! 몸통 박치기!!");
                break;
            default:
                attckText.Append("는 공격하였다");
                break;
        }
        return attckText;

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
    public virtual int MaxHp { get { return maxHp; } set { maxHp = value; } }
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
    public virtual int Atk { get { return atk; } set { atk = value; } }
    public virtual int Def { get { return def; } set { def = value; } }
    public virtual int Exp { get { return exp; } set { exp = value; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    public virtual void RenderStatus() { }

    public virtual void DeadTigger(Unit Killer)
    {
        StringBuilder Text = new StringBuilder($"{Name}은 무참하게 죽었다.\n\n");
        Killer.Exp += Exp;
        Killer.Gold += gold;
        Text.AppendLine($"{Killer.Name}은 경험치 {Exp}를 획득하였습니다.");
        Text.AppendLine($"{gold}금화를 획득하였습니다.");
        Console.WriteLine("\n" + Text);

    }
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
    public override int Atk { get { return atk + itemAtk; } set { atk = value; } }
    public override int Def { get { return def + itemDef; } set { def = value; } }
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
        GameManager.GM.PressEnterKey();
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
        Console.WriteLine("\nL E V E L U P !!!");
        Thread.Sleep(2000);
    }

    // 죽었을 경우
    public override void DeadTigger(Unit Killer)
    {
        StringBuilder Text = new StringBuilder($"{Name}은 {Killer.Name}에게 비참하게 패배하였습니다.\n");
        Hp = 1;
        Text.AppendLine("마을로 돌아갑니다");
        Console.WriteLine("\n" + Text);

        GameManager.GM.PressEnterKey();
    }

    // 인벤토리 UI
    public void InventoriUI()
    {
        bool switchSeclectInven = false;
        while (true)
        {
            Console.Clear();
            RenderStatus();

            if (switchSeclectInven == false)
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
        topTextBar.Remove(2, 4 + name.Length * 2 + 5 + level.ToString().Length);
        topTextBar.Insert(2, "　" + name + " Lv. " + Level + "　");

        Console.WriteLine(topTextBar);
        Console.WriteLine($"│   직업 : {Job}             \t\t\t│");
        Console.Write($"│   체력 : {Hp} / {MaxHp}\t 공격력 : {Atk} + ({itemAtk})");
        Console.WriteLine("\t│");
        Console.Write($"│   경험치 : {Exp} / {LevelUpExp}\t 방어력 : {Def} + ({itemDef})");
        Console.WriteLine("　\t│");
        Console.WriteLine("└───────────────────────────────────────────────┘");


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
                twoSpacebarChar++;
            }
            int nameSpaceNum = 25 - (invenText.Length + twoSpacebarChar);
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
                    invenText.Append($"ATK : {((Equipment)Inventory[num]).Atk}".PadRight(10, ' '));
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
                }
                else if (invenText[i] == ']')
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
    public MonsterDB(MonsterCode code, out string name, out int hp, out int atk, out int def, out int exp, out int gold)
    {
        switch (code)
        {
            case MonsterCode.Goblin:
                name = "고블린";
                hp = 60;
                atk = 20;
                def = 5;
                exp = 10;
                gold = 200;
                break;
            case MonsterCode.Orc:
                name = "오크";
                hp = 120;
                atk = 30;
                def = 7;
                exp = 20;
                gold = 400;
                break;
            case MonsterCode.Golem:
                name = "골렘";
                hp = 200;
                atk = 40;
                def = 20;
                exp = 50;
                gold = 1000;
                break;
            default:
                name = "오류";
                hp = 1;
                atk = 0;
                def = 0;
                exp = 1;
                gold = 1;
                break;
        }
    }
}

class Monster : Unit
{
    public bool isRevision = false;

    public Monster(MonsterCode code, float revisionStatus)
    {
        new MonsterDB(code, out name, out hp, out atk, out def, out exp, out gold);
        if (revisionStatus > 1.0)
        {
            isRevision = true;
        }
        ReviseStatus(revisionStatus);
        maxHp = hp;

    }

    public override void DeadTigger(Unit Killer)
    {
        StringBuilder Text = new StringBuilder($"{Name}은 무참하게 죽었다.\n\n");
        Text.AppendLine($"{Killer.Name}은 경험치 {Exp}를 획득하였습니다.");
        Text.AppendLine($"{gold}금화를 획득하였습니다.");
        Console.WriteLine("\n" + Text);

        Killer.Exp += Exp;
        Killer.Gold += gold;

        GameManager.GM.PressEnterKey();

    }

    void ReviseStatus(float revisionVaule)
    {
        Hp = (int)(Hp * revisionVaule);
        Atk = (int)(Atk * revisionVaule);
        Def = (int)(Def * revisionVaule);
    }
}