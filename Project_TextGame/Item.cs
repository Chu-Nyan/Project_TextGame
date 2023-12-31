﻿enum ItemType
{
    // 무기 (왼손,양손)
    SteelSword = 1000, ShortBow, LongLance,
    // 무기 (오른손)
    WoodShield, SteelShield,
    // 상의 방어구
    LeatherArmour, PlateArmour,
    // 하의 방어구
    LeatherPants,
    // 신발 방어구
    LeatherShoes
}

enum EquipmentPart
{
    //무기 
    Left = 0, Right, TwoHand,
    //방어구
    Head = 3,
    Top,
    Pants,
    Shoes
}

//모든 아이템
class Item
{
    protected string name = "";
    protected int id = 0;
    protected int gold = 0;

    public string Name { get { return name; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int ID { get { return id; } }
}

//장비 아이템
class Equipment : Item
{
    protected int part = 0;
    bool isEquip = false;

    protected int atk = 0;
    protected int def = 0;
    public int Atk { get { return atk; } }
    public int Def { get { return def; } }
    public int Part { get { return part; } }
    public bool IsEquip { get { return isEquip; } set { isEquip = value; } }
}

class Postion : Item
{

}

class SteelSword : Equipment
{
    public SteelSword() 
    {
        name = "철검";
        id = (int)ItemType.SteelSword;
        part = (int)EquipmentPart.Right;

        gold = 500;
        atk = 15;
    }
}

class ShortBow : Equipment
{
    public ShortBow()
    {
        name = "숏보우";
        id = (int)ItemType.ShortBow;
        part = (int)EquipmentPart.TwoHand;

        gold = 700;
        atk = 20;
        def = -3;
    }
}

class LongLance : Equipment
{
    public LongLance()
    {
        name = "롱 랜스";
        id = (int)ItemType.LongLance;
        part = (int)EquipmentPart.TwoHand;

        gold = 1500;
        atk = 40;
    }
}

class SteelShield : Equipment
{
    public SteelShield()
    {
        name = "금속 방패";
        id = (int)ItemType.SteelShield;
        part = (int)EquipmentPart.Left;

        gold = 1500;
        def = 10;
    }
}

class WoodShield : Equipment
{
    public WoodShield()
    {
        name = "나무 방패";
        id = (int)ItemType.WoodShield;
        part = (int)EquipmentPart.Left;

        gold = 500;
        def = 8;
    }
}

class LeatherArmour : Equipment
{
    public LeatherArmour()
    {
        name = "가죽 갑옷";
        id = (int)ItemType.LeatherArmour;
        part = (int)EquipmentPart.Top;

        gold = 1000;
        def = 10;
    }
}

class PlateArmour : Equipment
{
    public PlateArmour()
    {
        name = "플레이트 갑옷";
        id = (int)ItemType.PlateArmour;
        part = (int)EquipmentPart.Top;

        gold = 5000;
        atk = -5;
        def = 20;
    }
}

class LeatherPants : Equipment
{
    public LeatherPants()
    {
        name = "가죽 바지";
        id = (int)ItemType.LeatherPants;
        part = (int)EquipmentPart.Pants;

        gold = 700;
        def = 7;
    }
}

class LeatherShoes : Equipment
{
    public LeatherShoes()
    {
        name = "가죽 신발";
        id = (int)ItemType.LeatherShoes;
        part = (int)EquipmentPart.Shoes;

        gold = 300;
        def = 5;
    }
}

