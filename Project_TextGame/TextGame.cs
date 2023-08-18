using System.Text;

namespace Project_TextGame
{
    interface IInventory
    {
        public List<Item> Inventory { get; }
    }

    class Unit
    {
        protected string name;
        protected int hp;
        protected int atk;
        protected int def;
        protected int exp;
        protected bool isDead = false;

        public string Name { get { return name; } set { name = value; } }
        public virtual int HP
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

    }

    class Player : Unit, IInventory
    {
        public Player()
        {
            hp = 100;
            atk = 20;
            def = 3;
            exp = 0;
        }
        // Player 스테이터스
        string job;
        int level = 1;
        int levelUpExp = 20;

        List<Item> inventory = new List<Item>();

        // 스테이터스 접근 함수
        public string Job { get { return job; } set { job = value; } }
        public int Level { get { return level; } set { level = value; } }
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

        }

        //레벨 업 함수
        void LevelUp()
        {
            exp -= levelUpExp;
            level++;
            atk += 2;
            def += 1;
            Console.WriteLine("레벨업");
        }
    }



    class Item
    {
        protected string name;
        protected int gold;

        public string Name { get { return name; } }
        public int Gold { get { return gold; } }
    }

    class Weapon : Item
    {
    }

    class Ammor : Item
    {
    }

    class Postion : Item
    {
    }

    // 타이틀 화면 플레이어 세팅
    struct StartScene
    {
        string? newName = null;
        string? newJob = null;
        int addHp = 0;
        int addAtk = 0;
        int addDef = 0;
        Player newPlayer;

        public StartScene(Player newPlayer)
        {
            string gameName = "Game Name";
            this.newPlayer = newPlayer;
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

        void EnterPlayerStatus()
        {
        }
        enum HomeTown
        {
            BigHouse,
            SmallHouse,
            empty
        }

        public void SettingBackGround()
        {
            bool isAskHomeTown = true;
            bool isAskWeapon = true;
            bool isSelectName = false;

            HomeTown playerHomeTown = HomeTown.empty;
            string text1 = "1023년 여름...";
            StringBuilder sayHomeTown = new StringBuilder("나는 (   )에서 태어났습니다.");
            string? text2 = null; // HomeTown에 대한 선택값
            StringBuilder sayWhereRUgoing = new StringBuilder("당신은 잠시 생각에 빠졌습니다....");


            // 당신은 어디서 태어났습니까?
            while (isAskHomeTown)
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
                        isAskHomeTown = false;
                        break;
                    case ConsoleKey.D2:
                        sayHomeTown.Replace("(   )", "빈민가");
                        text2 = "어느날, 배가 고파 빵을 훔치려고 했으나 주인에게 들키고 말았습니다.";

                        AddStatus(-10, 10, 0);
                        playerHomeTown = HomeTown.SmallHouse;
                        isAskHomeTown = false;
                        break;
                    default:
                        Console.WriteLine("\n올바른 숫자를 입력해주세요.");
                        Thread.Sleep(2000);
                        break;
                }
            }

            // 공격할래 도망갈래?
            while (isAskHomeTown == false && isAskWeapon == true)
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
                            newJob = "기사";
                        }
                        else if (playerHomeTown == HomeTown.SmallHouse)
                        {
                            AddStatus(0, 10, 0);
                            sayWhereRUgoing.Append("품 속에서 작은 단검을 꺼내 들고 가게 주인을 공격했습니다.");
                            newJob = "중범죄자";
                        }
                        isAskWeapon = false;
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
                        isAskWeapon = false;
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
            FinishScene();
        }

        

        void FinishSetting(Player newPlayer)
        {
            newPlayer.Name = newName;
            newPlayer.Job = newJob;
            newPlayer.HP += addHp;
            newPlayer.Atk += addAtk;
            newPlayer.Def += addDef;
        }

        void FinishScene()
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
            Console.WriteLine("\n\n옛 생각을 하다 보니 어느샌가 마을에 도착했다.");
            Thread.Sleep(2000);
        }

        void AddStatus(int hp, int atk, int def)
        {
            addHp += hp;
            addAtk += atk;
            addDef += def;
        }
    }

    class Town
    {

    }

    class Dungeon
    {

    }

    internal class TextGame
    {
        static void Main(string[] args)
        {
            Player newPlayer = new Player();
            StartScene startScene = new StartScene(newPlayer);
            startScene.SettingBackGround();
            Console.WriteLine(newPlayer.Name);
            Console.WriteLine(newPlayer.Job);
            Console.WriteLine(newPlayer.HP);
            Console.WriteLine(newPlayer.Atk);
            Console.WriteLine(newPlayer.Def);
        }
    }


}