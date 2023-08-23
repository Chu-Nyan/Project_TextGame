using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;



internal class TextGame
{
    static void Main()
    {
        //폰트 : D2Coding

        new ImageManager();
        new GameManager();

        Player newPlayer = new Player();
        Town town = new Town(newPlayer);
        Dungeon dungeon = new Dungeon(newPlayer);
        //스타트 씬
        StartScene startScene = new StartScene(newPlayer);
        startScene.IntroStartScene();
        startScene.SettingBackGround();
        startScene.FinishScene();

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
