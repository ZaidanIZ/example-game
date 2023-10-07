using UnityEngine;
using Player.Wallet;

/* Static Class to access game managers */
public static class ComponentsManager
{
  private static Storage<Wallet> _wallet = new Storage<Wallet>();
  private static Storage<MenuUI> _menuUI = new Storage<MenuUI>();
  private static Storage<PlayerData> _playerData = new Storage<PlayerData>();
  private static Storage<GameUI> _gameUI = new Storage<GameUI>();
  private static Storage<StagesManager> _stagesManager = new Storage<StagesManager>();
  private static Storage<PlayerArmyManager> _playerArmyManager = new Storage<PlayerArmyManager>();
  private static Storage<BattleManager> _battleManager = new Storage<BattleManager>();
  private static Storage<CameraScript> _cameraScript = new Storage<CameraScript>();
  private static Storage<UpgradeUI> _upgradeUI = new Storage<UpgradeUI>();
  private static Storage<Tutorial> _tutorial = new Storage<Tutorial>();

  public static Wallet PlayerWallet => _wallet.GetItem();
  public static MenuUI MenuUI => _menuUI.GetItem();
  public static PlayerData PlayerData => _playerData.GetItem();
  public static GameUI GameUI => _gameUI.GetItem();
  public static StagesManager StagesManager => _stagesManager.GetItem();
  public static PlayerArmyManager PlayerArmyManager => _playerArmyManager.GetItem();
  public static BattleManager BattleManager => _battleManager.GetItem();
  public static CameraScript CameraScript => _cameraScript.GetItem();
  public static UpgradeUI UpgradeUI => _upgradeUI.GetItem();
  public static Tutorial Tutorial => _tutorial.GetItem();

  private class Storage<T> where T : Object
  {
    private T _item;

    public T GetItem()
    {
      if (_item == null)
      {
        _item = Object.FindObjectOfType<T>();
      }

      return _item;
    }
  }
}