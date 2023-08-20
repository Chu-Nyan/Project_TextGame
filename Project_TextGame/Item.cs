class Item
{
    protected string name = "";
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