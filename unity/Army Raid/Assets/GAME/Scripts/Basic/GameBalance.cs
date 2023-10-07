using System;

public static class GameBalance
{
  public static int InitialMoney = 5;
  public static int InitialGem = 0;

  public static UpgradeBalance[] UpgradeBase = new UpgradeBalance[4]
  {
    new UpgradeBalance(10, 1.4f, 1),
    new UpgradeBalance(10, 1.4f, 1),
    new UpgradeBalance(10, 1.4f, 1),
    new UpgradeBalance(10, 1.4f, 1),
  };
  
  public class UpgradeBalance
  {
    public int BasePrice;
    public float BasePriceCoefficient;
    public float BaseValue;

    public UpgradeBalance(int basePrice, float basePriceCoefficient, float baseValue)
    {
      BasePrice = basePrice;
      BasePriceCoefficient = basePriceCoefficient;
      BaseValue = baseValue;
    }
  }
}

