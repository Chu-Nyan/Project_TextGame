using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

enum Region
{
    Town,
    Dungeon
}

class Town : IInventory
{
    public Town(Player player)
    {
        this.player = player;
    }

    Region moveRegion;
    Player player;
    List<Item> inventory = new List<Item>() { new SteelSword(), new WoodShield(), new LeatherArmour(), new LeatherPants(), new LeatherShoes(), new PlateArmour() };

    public List<Item> Inventory { get { return inventory; } }

    public Region VisitTown()
    {
        RenderTownUI();
        return moveRegion;
    }

    void RenderTownUI()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("라면 마을 ____________\n"); //1,2번 줄
            Console.WriteLine
                (
                "이 곳 사람들은 반들반들 피부에서 윤기가 흐른다.\n" +
                "질 좋은 라면이 공급되고 있는 모양이니 나가기 전에 한그릇 해야겠다.\n"
                );
            Console.WriteLine("어떤 행동을 하시겠습니까?");
            Console.WriteLine("1. 정보 확인");
            Console.WriteLine("2. 상점 방문");
            Console.WriteLine("3. 숲으로 향한다.\n");

            ConsoleKey key = GameManager.GM.ReadNunberKeyInfo(4);

            if (key == ConsoleKey.D1) // 나의 정보 확인
            {
                player.InventoriUI();

            }
            else if (key == ConsoleKey.D2) // 상점 방문
            {
                VisitShop();
            }
            else if (key == ConsoleKey.D3) // 던전
            {
                moveRegion = Region.Dungeon;
                return;
            }
        }
    }

    void RenderVisitShop()
    {
        Console.Clear();
        Console.WriteLine($"[상점] __________ 가진 금화 : {player.Gold}\n");

        for (int num = 0; num < inventory.Count; num++)
        {
            StringBuilder invenText = new StringBuilder();
            if (Inventory[num].Gold == 0)
            {
                invenText.Append($"{num + 1}. {inventory[num].Name}{(String.Format("{0,15}", "\t" + "구매 완료"))}");
            }
            else
            {
                invenText.Append($"{num + 1}. {inventory[num].Name}{(String.Format("{0,15}", "\t" + inventory[num].Gold))}");
            }

            Console.WriteLine(invenText);
        }
        Console.WriteLine("\n___________________\n");
    }

    void VisitShop()
    {
        while (true)
        {
            RenderVisitShop();

            Console.WriteLine("1. 구매하기 2. 판매하기 3. 돌아가기)\n");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(inventory.Count);

            if (inputKey == ConsoleKey.D1)
            {
                BuyShopItem();
            }
            else if (inputKey == ConsoleKey.D2)
            {
                SellShopItem();
            }
            else if (inputKey == ConsoleKey.D3)
            {
                return;
            }
        }
    }

    void BuyShopItem()
    {
        while (true)
        {
            RenderVisitShop();
            Console.WriteLine($"구매할 물품의 번호를 입력해주세요. (0. 돌아가기)\n");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(inventory.Count);

            if (inputKey == ConsoleKey.D0) // 돌아가기
            {
                return;
            }
            else if ((int)inputKey - 48 > inventory.Count) // 입력 오류
            {
                continue;
            }
            else if (inventory[(int)inputKey - 49].Gold > player.Gold) // 금화 부족
            {
                Console.WriteLine("가진 금화가 부족합니다.");
                GameManager.GM.PressAnyKey();
            }
            else if (player.Inventory.Count >= 10) // 입력 오류
            {
                Console.WriteLine("가방이 가득 찼습니다.");
                GameManager.GM.PressAnyKey();
            }
            else if (inventory[(int)inputKey - 49].Gold == 0)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                GameManager.GM.PressAnyKey();
            }
            else // 구매 성공
            {
                player.Gold -= inventory[(int)inputKey - 49].Gold;
                player.AddItem(inventory[(int)inputKey - 49]);
                inventory[(int)inputKey - 49].Gold = 0;
            }
        }

    }

    void SellShopItem()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[가방] ______________ 가진 금화 : {player.Gold}\n");
            for (int num = 0; num < player.Inventory.Count; num++)
            {
                StringBuilder invenText = new StringBuilder();

                invenText.Append($"{num + 1}. ");
                foreach (var item in player.EquipmentParts)
                {
                    if (item == player.Inventory[num])
                    {
                        invenText.Append("[E]");
                    }

                }
                invenText.Append($"{player.Inventory[num].Name}\t\t");

                if (player.Inventory[num] is Equipment)
                {
                    invenText.Append($"금화 : {player.Inventory[num].Gold * 0.8f}");
                }

                Console.WriteLine(invenText);
            }
            Console.WriteLine("___________________\n");
            Console.WriteLine($"판매할 물품의 번호를 입력해주세요. (0. 돌아가기)\n");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(player.Inventory.Count);

            if (inputKey == ConsoleKey.D0) // 돌아가기
            {
                return;
            }
            else if ((int)inputKey - 48 > inventory.Count) // 입력 오류
            {
                continue;
            }
            else // 판매 성공
            {
                Item sellItem = player.Inventory[(int)inputKey - 49];
                int sellGold = (int)(sellItem.Gold * 0.8f);
                player.Gold += sellGold;
                player.Inventory.Remove(sellItem);
                // 장착한 아이템인지 확인 후 제거
                if (sellItem is Equipment && sellItem == player.EquipmentParts[((Equipment)sellItem).Part])
                {
                    player.ChangeParts((Equipment)sellItem);
                }

                StringBuilder sellTxt = new StringBuilder($"{sellItem.Name}을(를) {sellGold}금화에 판매하였습니다.");
                Console.WriteLine(sellTxt);
                GameManager.GM.PressAnyKey();
            }
        }
    }

}


class Dungeon
{
    Player player;
    Region moveRegion;

    int dungeonMinDef;
    int dungeonMaxAft;
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
            Console.Clear();
            Console.WriteLine("어두운 숲 ____________\n"); //1,2번 줄
            Console.WriteLine
                (
                "한때는 질 좋은 라면 재료를 제공했던 숲이었으나\n" +
                "지금은 알 수 없는 이유로 폐쇄되었다.\n"
                );
            Console.WriteLine("어떤 행동을 하시겠습니까?");
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
            }
            else if (inputKey == ConsoleKey.D3)
            {
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
        Monster newMonster = new Monster(MonsterCode.Goblin);
        BattlePhase battlePhase = new BattlePhase(player, newMonster);
        battlePhase.BattleScene();
    }


}

internal class TextGame
{
    static void Main()
    {
        //SteelSword steelSword = new SteelSword();
        //StringBuilder border = new StringBuilder("|                    |");// 스페이스바 20개
        //Console.WriteLine(border);
        //int spacebar = 0;
        //foreach (char c in steelSword.Name) 
        //{ 
        //    if (c == ' ')
        //    {
        //        spacebar++;
        //    }
        //}
        //border.Remove(1, steelSword.Name.Length*2-spacebar);
        //border.Insert(1, steelSword.Name);
        //Console.WriteLine(border);

        new GameManager();
        Player newPlayer = new Player();
        Town town = new Town(newPlayer);
        Dungeon dungeon = new Dungeon(newPlayer);
        // 스타트 씬
        //StartScene startScene = new StartScene(newPlayer);
        //startScene.IntroStartScene();
        //startScene.SettingBackGround();
        //startScene.FinishScene();

        // 타운 방문으로 게임 시작
        Region whereIGo = town.VisitTown();

        while (true)
        {
            switch (whereIGo)
            {
                case Region.Town:
                    whereIGo = town.VisitTown();
                    break;
                case Region.Dungeon:
                    whereIGo = dungeon.VisitDungeon();
                    break;
            }
        }
    }
}
