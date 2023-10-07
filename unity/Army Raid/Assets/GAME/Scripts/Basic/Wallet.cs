using System;
using UnityEngine;

namespace Player.Wallet
{
  public class Wallet : MonoBehaviour
  {
    private int _playerMoney;
    public event Action<int> PlayerMoneyEvent;
    public int GetMoney => _playerMoney;

    void Awake()
    {
      _playerMoney = PlayerPrefs.HasKey("_playerMoney")
        ? PlayerPrefs.GetInt("_playerMoney")
        : GameBalance.InitialMoney;
    }

    private void Start()
    {
      AddMoney(2220);
    }

    public void AddMoney(int value)
    {
      _playerMoney += value;
      WalletFunc();
    }
    
    public void UseMoney(int value)
    {
      _playerMoney -= value;
      WalletFunc();
    }

    private void WalletFunc()
    {
      PlayerPrefs.SetInt("_playerMoney", _playerMoney);
      PlayerMoneyEvent?.Invoke(_playerMoney);
    }
  }
}