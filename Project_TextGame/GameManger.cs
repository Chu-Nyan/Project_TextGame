class GameManager
{
    public static GameManager GM;

    public GameManager()
    {
        GM = this;
    }

    // 키 읽어오기
    public ConsoleKey ReadNunberKeyInfo(int numberOfOption)
    {
        ConsoleKey InputKey = Console.ReadKey(true).Key;

        switch (InputKey)
        {
            case ConsoleKey.D1:
            case ConsoleKey.D2:
                if (numberOfOption < 2)
                {
                    break;
                }
                return InputKey;
            case ConsoleKey.D3:
                if (numberOfOption < 3)
                {
                    break;
                }
                return InputKey;
            case ConsoleKey.D4:
                if (numberOfOption < 4)
                {
                    break;
                }
                return InputKey;
            default:
                Console.WriteLine("올바르게 입력했다면 ReadNunberKeyInfo 스위치문 수정이 필요합니다.");
                break;
        }
        Console.WriteLine("선택지를 다시 선택해주세요.");
        Thread.Sleep(1000);
        return InputKey;

    }

    public void PressAnyKey()
    {
        Console.WriteLine("\nPress 'Enter' Key To Continue");
        Console.ReadLine();
    }

}