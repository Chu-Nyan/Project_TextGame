using System.Text;

class Town
{
    public Town(Player player)
    {
        this.player = player;
    }

    Player player;

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

        ConsoleKey key =  GameManager.GM.ReadNunberKeyInfo(3);

        if (key == ConsoleKey.D1) // 나의 정보 확인
        {
            player.RenderStatus();
        }

        if (key == ConsoleKey.D2) // 가방 열기
        {
            player.OpenInventori();
        }

        if (key == ConsoleKey.D3) // 상점 방문
        {
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
        Player newPlayer = new Player();
        Town town = new Town(newPlayer);
        new GameManager();
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
