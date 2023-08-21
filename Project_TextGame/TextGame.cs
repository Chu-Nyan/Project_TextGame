using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

class Town : IInventory
{
    public Town(Player player)
    {
        this.player = player;
    }

    Player player;
    List<Item> inventory = new List<Item>() { new SteelSword(), new WoodShield(),new LeatherArmour(),new LeatherPants(), new LeatherShoes() };

    public List<Item> Inventory { get { return inventory; } }

    public void VisitTown()
    {
        while (true)
        {
            RenderTownUI();
        }
    }

    void RenderTownUI()
    {
        Console.Clear();
        Console.WriteLine("라면 마을 ____________\n"); //1,2번 줄
        Console.WriteLine
            (
            "이 곳 사람들은 반들반들 피부에서 윤기가 흐른다.\n" +
            "질 좋은 라면이 공급되고 있는 모양이니 나가기 전에 한그릇 해야겠다.\n"
            );
        Console.WriteLine("어떤 행동을 하시겠습니까?");
        Console.WriteLine("1. 나의 정보 확인");
        Console.WriteLine("2. 가방 열기");
        Console.WriteLine("3. 상점 방문\n");

        ConsoleKey key = GameManager.GM.ReadNunberKeyInfo(3);

        if (key == ConsoleKey.D1) // 나의 정보 확인
        {
            player.RenderStatus();
        }

        if (key == ConsoleKey.D2) // 가방 열기
        {
            player.InventoriUI();
        }

        if (key == ConsoleKey.D3) // 상점 방문
        {
            VisitShop();
        }


    }

    void VisitShop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점 __________");

            Console.WriteLine("이름\t\t\t금화\n");
            for (int num = 0; num < inventory.Count; num++)
            {
                StringBuilder invenText = new StringBuilder();

                invenText.Append($"{num + 1}. {inventory[num].Name}{(String.Format("{0,15}", "\t" + inventory[num].Gold))}");
                Console.WriteLine(invenText);
            }
            Console.WriteLine("___________________\n");

            Console.WriteLine($"구매할 물품의 번호를 입력해주세요. (0. 돌아가기) | 가진 금화 : {player.Gold}\n");
            ConsoleKey inputKey = GameManager.GM.ReadNunberKeyInfo(inventory.Count);

            if (inputKey == ConsoleKey.D0) // 돌아가기
            {
                return;
            }
            else if ((int)inputKey-48 >inventory.Count) // 입력 오류
            {
                continue;
            }
            else if(inventory[(int)inputKey - 49].Gold > player.Gold) // 금화 부족
            {
                Console.WriteLine("가진 금화가 부족합니다.");
                GameManager.GM.PressAnyKey();
            }else // 구매 성공
            {
                player.Gold -= inventory[(int)inputKey - 49].Gold;
                player.AddItem(inventory[(int)inputKey - 49]);

            }
        }
    }
}


class Dungeon
{

}

internal class TextGame
{
    static void Main()
    {
        new GameManager();
        Player newPlayer = new Player();
        Town town = new Town(newPlayer);
        // 스타트 씬
        //StartScene startScene = new StartScene(newPlayer);
        //startScene.IntroStartScene();
        //startScene.SettingBackGround();
        //startScene.FinishScene();

        // 타운 방문으로 게임 시작
        town.VisitTown();

        Console.ReadKey();


    }
}
