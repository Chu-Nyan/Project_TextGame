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
        {
            if ((int)InputKey >= 48 && (int)InputKey < (int)ConsoleKey.D1 + numberOfOption)
            {
                return InputKey;
            }
            Console.WriteLine("올바르게 입력했다면 ReadNunberKeyInfo 스위치문 수정이 필요합니다.");
        }
        Console.WriteLine("선택지를 다시 선택해주세요.");
        Thread.Sleep(1000);
        return ConsoleKey.F24;

    }

    public void PressAnyKey()
    {
        Console.WriteLine("\nPress 'Enter' Key To Continue");
        Console.ReadLine();
    }

    public void MoveRegion()
    {


    }

}