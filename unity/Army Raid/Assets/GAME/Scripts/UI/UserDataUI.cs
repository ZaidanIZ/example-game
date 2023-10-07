using TMPro;
using UnityEngine;
using Player.Wallet;

public class UserDataUI : MonoBehaviour
{
  [SerializeField]private TextMeshProUGUI moneyDisplayText;
  private Wallet _playerWallet;
  
  private void OnEnable()
  {
    _playerWallet = FindObjectOfType<Wallet>();
    _playerWallet.PlayerMoneyEvent += UpdatePlayerMoney;
  }

  private void OnDisable()
  {
    _playerWallet.PlayerMoneyEvent -= UpdatePlayerMoney;
  }
  
  private void UpdatePlayerMoney(int value)
  {
    moneyDisplayText.text = value.ToString();
  }
}
