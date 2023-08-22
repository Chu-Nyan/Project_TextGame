class Dungeon
{
    Player player;
    Region moveRegion = Region.Dungeon;

    int dungeonMinDef;
    int dungeonMinAft;
    public Dungeon(Player player)
    {
        this.player = player;
    }

    public Region VisitDungeon()
    {
        RenderEntryDungeon();
        return moveRegion;
    }

    void RenderEntryDungeon()
    {
        while (true)
        {
            if (player.IsDead == true)
            {
                moveRegion = Region.Town;
                return;
            }
            Console.Clear();
            ImageManager.IM.RenderImage("Dungeon");
            Console.WriteLine
                (
                "한때는 질 좋은 라면 재료를 제공했던 숲이었으나\n" +
                "지금은 알 수 없는 이유로 폐쇄되었다.\n"
                );
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("어떤 행동을 하시겠습니까?\n");
            Console.ResetColor();
            Console.WriteLine("1. 입구 근처를 수색한다.");
            Console.WriteLine("2. 숲에서 재료를 채취한다.");
            Console.WriteLine("3. 깊숙한 곳으로 탐험을 떠난다.");
            Console.WriteLine("4. 마을로 돌아간다.\n");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(4);
            if (inputKey == ConsoleKey.D1)
            {
                DungeonLevel1();
            }
            else if (inputKey == ConsoleKey.D2)
            {
                DungeonLevel2();
            }
            else if (inputKey == ConsoleKey.D3)
            {
                DungeonLevel3();
            }
            else if (inputKey == ConsoleKey.D4)
            {
                moveRegion = Region.Town;
                return;
            }
        }
    }

    void DungeonLevel1()
    {
        float revisionVaule = 1.0f;
        dungeonMinDef = 10;

        if (player.Def < dungeonMinDef) 
        {
            revisionVaule = 1.3f;
        }

        Monster newMonster = new Monster(MonsterCode.Goblin,revisionVaule);
        BattlePhase battlePhase = new BattlePhase(player, newMonster);
        battlePhase.BattleScene();
    }

    void DungeonLevel2()
    {
        float revisionVaule = 1.0f;
        dungeonMinDef = 20;

        if (player.Def < dungeonMinDef)
        {
            revisionVaule = 1.3f;
        }

        Monster newMonster = new Monster(MonsterCode.Orc, revisionVaule);
        BattlePhase battlePhase = new BattlePhase(player, newMonster);
        battlePhase.BattleScene();
    }
    void DungeonLevel3()
    {
        float revisionVaule = 1.0f;
        dungeonMinAft = 50;

        if (player.Atk < dungeonMinAft)
        {
            revisionVaule = 1.3f;
        }

        Monster newMonster = new Monster(MonsterCode.Golem, revisionVaule);
        BattlePhase battlePhase = new BattlePhase(player, newMonster);
        battlePhase.BattleScene();
    }
}
