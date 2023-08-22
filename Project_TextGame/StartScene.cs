using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct StartScene
{
    // 첫번째 선택지 종류
    enum HomeTown
    {
        BigHouse,
        SmallHouse,
        empty
    }

    string newName = "";
    string newJob = "";
    int addMaxHp = 0;
    int addAtk = 0;
    int addDef = 0;
    Player newPlayer;

    public StartScene(Player newPlayer)
    {
        this.newPlayer = newPlayer;
    }

    // 스타트씬 인트로
    public void IntroStartScene()
    {
        string gameName = "Game Name";
        Console.Clear();
        Console.WriteLine("\n\n\n");
        Console.Write("\t");
        foreach (var text in gameName)
        {
            Console.Write($"{text}   ");
            Thread.Sleep(200);
        }
        Console.WriteLine("\n\n\n");

        Thread.Sleep(500);
        Console.WriteLine("\n\t\t    게임 시작");
        Console.ReadLine();
        Console.Clear();
    }

    // 신규 플레이어 스테이터스 설정
    public void SettingBackGround()
    {
        bool isSelectHomeTown = false;
        bool isSelectWeapon = false;
        bool isSelectName = false;

        HomeTown playerHomeTown = HomeTown.empty;
        string text1 = "1023년 여름...";
        StringBuilder sayHomeTown = new StringBuilder("나는 (   )에서 태어났습니다.");
        string? text2 = null; // HomeTown에 대한 선택값
        StringBuilder sayWhereRUgoing = new StringBuilder("당신은 잠시 생각에 빠졌습니다....");

        // 당신은 어디서 태어났습니까?
        while (isSelectHomeTown == false)
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.WriteLine($"{text1}\n{sayHomeTown}");
            Console.WriteLine("\n");
            Console.WriteLine("1. 대저택\n2. 빈민가");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    sayHomeTown.Replace("(   )", "대저택");
                    text2 = "어느날, 굶주린 도적들이 집을 습격하였습니다.";

                    AddStatus(20, 0, 5);
                    playerHomeTown = HomeTown.BigHouse;
                    isSelectHomeTown = true;
                    break;
                case ConsoleKey.D2:
                    sayHomeTown.Replace("(   )", "빈민가");
                    text2 = "어느날, 배가 고파 빵을 훔치려고 했으나 주인에게 들키고 말았습니다.";

                    AddStatus(-10, 10, 0);
                    playerHomeTown = HomeTown.SmallHouse;
                    isSelectHomeTown = true;
                    break;
                default:
                    Console.WriteLine("\n올바른 숫자를 입력해주세요.");
                    Thread.Sleep(2000);
                    break;
            }
        }

        // 공격할래 도망갈래?
        while (isSelectHomeTown == true && isSelectWeapon == false)
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.WriteLine($"{text1}\n{sayHomeTown}\n{text2}");
            Console.WriteLine("\n");

            Console.WriteLine(sayWhereRUgoing);
            Console.WriteLine("1. 공격한다.\n2. 도망간다.");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    sayWhereRUgoing.Remove(0, 20);
                    if (playerHomeTown == HomeTown.BigHouse)
                    {
                        sayWhereRUgoing.Append("가족들을 지키기 위해 도적들의 앞을 가로막았습니다.");
                        AddStatus(0, 5, 5);
                        newJob = "영웅";
                    }
                    else if (playerHomeTown == HomeTown.SmallHouse)
                    {
                        AddStatus(0, 10, 0);
                        sayWhereRUgoing.Append("품 속에서 작은 단검을 꺼내 들고 가게 주인을 공격했습니다.");
                        newJob = "범죄자";
                    }
                    isSelectWeapon = true;
                    break;
                case ConsoleKey.D2:
                    sayWhereRUgoing.Remove(0, 20);
                    if (playerHomeTown == HomeTown.BigHouse)
                    {
                        AddStatus(20, -3, 3);
                        sayWhereRUgoing.Append("살기위해 가족들을 버리고 도망갔습니다.");
                        newJob = "쓰레기";
                    }
                    else if (playerHomeTown == HomeTown.SmallHouse)
                    {
                        AddStatus(20, -3, 3);
                        sayWhereRUgoing.Append("재빠르게 도망 갔지만 결국 잡히고 말았습니다..");
                        newJob = "좀도둑";
                    }
                    isSelectWeapon = true;
                    break;
                default:
                    Console.WriteLine("\n올바른 숫자를 입력해주세요.");
                    Thread.Sleep(2000);
                    break;
            }
        }

        // 이름 선택
        while (isSelectName == false)
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.WriteLine($"{text1}\n{sayHomeTown}\n{text2}\n{sayWhereRUgoing}\n");

            Console.WriteLine("그런 당신의 이름은 무엇인가요?");
            newName = Console.ReadLine();
            if (newName == "")
            {
                Console.WriteLine("이름을 공백으로 할 수 없습니다.");
                Thread.Sleep(2000);
                continue;
            }
            StringBuilder playerNameJab = new StringBuilder($"\n나는 {newJob} {newName}이(가) 맞습니다.");
            Console.WriteLine(playerNameJab);
            Console.WriteLine("1. 맞다 2. 아니다");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    isSelectName = true;
                    playerNameJab.Replace("나는", "이제부터 당신은");
                    playerNameJab.Replace("이(가) 맞습니다", "입니다");
                    Console.WriteLine();
                    for (int num = 0; num < playerNameJab.Length; num++)
                    {
                        Console.Write(playerNameJab[num]);
                        Thread.Sleep(200);
                    }
                    break;
                case ConsoleKey.D2:
                    isSelectName = false;
                    break;
                default:
                    Console.WriteLine("\n올바른 숫자를 입력해주세요.");
                    Thread.Sleep(2000);
                    break;
            }

            Thread.Sleep(2000);
        }

        FinishSetting(newPlayer);
    }

    // 스타트씬 마무리
    public void FinishScene()
    {
        string text = "달 그 락";
        for (int num = 0; num < 2; num++)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.Write("\t\t");
            foreach (var item in text)
            {
                Console.Write(item);
                Thread.Sleep(300);
            }
        }
        Console.WriteLine("\n\n\n옛 생각을 하다 보니 어느샌가 마을에 도착했다.");
        GameManager.GM.PressAnyKey();
    }

    // 신규 플레이어 스테이터스 추가
    void AddStatus(int hp, int atk, int def)
    {
        addMaxHp += hp;
        addAtk += atk;
        addDef += def;
    }

    // 신규 플레이어 스테이터스 최종 결정
    void FinishSetting(Player newPlayer)
    {
        newPlayer.Name = newName;
        newPlayer.Job = newJob;
        newPlayer.MaxHp += addMaxHp;
        newPlayer.Hp = newPlayer.MaxHp;
        newPlayer.Atk += addAtk;
        newPlayer.Def += addDef;
    }
}
