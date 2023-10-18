using System;
using Player.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI[] upgradePriceDisplayText;
  [SerializeField] private TextMeshProUGUI[] levelDisplayText;
  [SerializeField] private AudioSource upgradeAudioSource;
    public Button[] Tombol;
  private int[] _playerUpgradesLevel = new int[4];

  private Wallet _wallet;

  private void Awake()
  {
    _playerUpgradesLevel[0] = PlayerPrefs.GetInt("_playerWarriorLevel");
    _playerUpgradesLevel[1] = PlayerPrefs.GetInt("_playerArcherLevel");
    _playerUpgradesLevel[2] = PlayerPrefs.GetInt("_playerUpgradeWarriorLevel");
    _playerUpgradesLevel[3] = PlayerPrefs.GetInt("_playerUpgradeArcherLevel");

    _wallet = FindObjectOfType<Wallet>();

    UpdateText();
  }

    public void panggilRewardOpWarrior()
    {
        ADsummoner.adSummoner.ShowReward(UpgradeWarriors);
    }

    public void panggilRewardUpArcher()
    {
        ADsummoner.adSummoner.ShowReward(UpgradeArchers);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpgradeWarriors();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            UpgradeArchers();
        }
    }

    public void BuyWarrior()
  {
    if (_wallet.GetMoney >= GetPrice(0))
    {
      _wallet.UseMoney(GetPrice(0));
      _playerUpgradesLevel[0]++;
      ComponentsManager.PlayerArmyManager.AddNewWarrior();
      SaveDatas();
      UpdateText();
    }
  }

  public void BuyArcher()
  {
    if (_wallet.GetMoney >= GetPrice(1))
    {
      _wallet.UseMoney(GetPrice(1));
      _playerUpgradesLevel[1]++;
      ComponentsManager.PlayerArmyManager.AddNewArcher();
      SaveDatas();
      UpdateText();

      if (ComponentsManager.Tutorial)
      {
        if (ComponentsManager.Tutorial.GetStep == 1)
          ComponentsManager.Tutorial.NextStep(2, true);
      }
    }
  }

  public void UpgradeWarriors()
  {
        //  if (_wallet.GetMoney >= GetPrice(2))
        //  {
        //  _wallet.UseMoney(GetPrice(2));
        foreach (Button item in Tombol)
        {
            item.interactable = false;
        }

        _playerUpgradesLevel[2]++;
    ComponentsManager.PlayerArmyManager.ResetPlayerWarriors();

    SaveDatas();
    UpdateText();
    //  }
  }

  public void UpgradeArchers()
  {
        // if (_wallet.GetMoney >= GetPrice(3))
        // {
        // _wallet.UseMoney(GetPrice(3));
        foreach (Button item in Tombol)
        {
            item.interactable = false;
        }


        _playerUpgradesLevel[3]++;
    ComponentsManager.PlayerArmyManager.ResetPlayerWarriors();

    SaveDatas();
    UpdateText();

    if (ComponentsManager.Tutorial)
    {
      if (ComponentsManager.Tutorial.GetStep == 3)
        ComponentsManager.Tutorial.NextStep(4, true);
    }
    //}
  }

  public int GetWarriorLevelUpgrade()
  {
    return _playerUpgradesLevel[2];
  }


  public int GetArcherLevelUpgrade()
  {
    return _playerUpgradesLevel[3];
  }

  private int GetPrice(int id)
  {
    return (int) (GameBalance.UpgradeBase[id].BasePrice *
                  Math.Pow(GameBalance.UpgradeBase[id].BasePriceCoefficient, _playerUpgradesLevel[id]));
  }

  private void UpdateText()
  {
    for (int i = 0; i < upgradePriceDisplayText.Length; i++)
    {
      upgradePriceDisplayText[i].SetText(GetPrice(i).ToString());
      levelDisplayText[i].SetText((_playerUpgradesLevel[i] + 1).ToString());
    }

    levelDisplayText[0].SetText((_playerUpgradesLevel[0] + 2).ToString());
    levelDisplayText[1].SetText((_playerUpgradesLevel[1]).ToString());
  }

  private void SaveDatas()
  {
    PlayerPrefs.SetInt("_playerWarriorLevel", _playerUpgradesLevel[0]);
    PlayerPrefs.SetInt("_playerArcherLevel", _playerUpgradesLevel[1]);
    PlayerPrefs.SetInt("_playerUpgradeWarriorLevel", _playerUpgradesLevel[2]);
    PlayerPrefs.SetInt("_playerUpgradeArcherLevel", _playerUpgradesLevel[3]);
    upgradeAudioSource.Play();
  }
}