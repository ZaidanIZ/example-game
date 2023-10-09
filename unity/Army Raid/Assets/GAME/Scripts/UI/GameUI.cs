using Player.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
  [SerializeField] private GameObject winnerWrapper, loseWrapper;

  [SerializeField]
  private TextMeshProUGUI moneyDisplayText, rewardLoseDisplayText, rewardWinDisplayText, levelDisplayText;

  [SerializeField] private AudioSource coinAudioSource;
  private Wallet _wallet;
  private int _rewardLose;
  private int _rewardWin;
  private bool _isStop;
  private int _tempSettingID;
  private int _settingID;

  private void OnEnable()
  {
    _wallet = FindObjectOfType<Wallet>();
    _wallet.PlayerMoneyEvent += UpdateMoneyText;
  }

  private void OnDisable()
  {
    _wallet.PlayerMoneyEvent -= UpdateMoneyText;
  }

  private void Start()
  {
    _settingID = PlayerPrefs.GetInt("_settingID");
    _tempSettingID = PlayerPrefs.GetInt("_tempSettingID");
    print("SETTING ID - " + _settingID);
    print("_tempSetting ID - " + _tempSettingID);
    levelDisplayText.SetText(I2.Loc.LocalizationManager.GetTranslation("Level") + " " + (ComponentsManager.PlayerData.GetLevel + 1));
  }

  public void CoiAudioPlay()
  {
    coinAudioSource.pitch = Random.Range(.9f, 1.1f);
    coinAudioSource.Play();
  }

  public void SetRewards(int win, int lose)
  {
    _rewardLose = lose;
    _rewardWin = win;

    rewardLoseDisplayText.SetText("+" + lose);
    rewardWinDisplayText.SetText("+" + win);
  }

  public void PlayerWinner()
  {
        Debug.Log("menang");
    if (!_isStop)
    {
      winnerWrapper.SetActive(true);
      SetEndEvent();
      PlayerPrefs.DeleteKey("_stageLevel");
      _isStop = true;
            Debug.Log("menang if");
    }
  }

  public void PlayerLose()
  {
    if (!_isStop)
    {
      loseWrapper.SetActive(true);
      _isStop = true;
      SetEndEvent();
    }
  }

  private void UpdateMoneyText(int value)
  {
    moneyDisplayText.SetText(value.ToString());
  }

  public void WinButton()
  {
    _wallet.AddMoney(_rewardWin);
    ComponentsManager.PlayerData.AddLevel();
    ChangeSetting();
    SceneManager.LoadScene(0);
  }

  public void LoseButton()
  {
    _wallet.AddMoney(_rewardLose);
    SceneManager.LoadScene(0);
  }

  private void SetEndEvent()
  {
  }

  private void ChangeSetting()
  {
    _tempSettingID++;

    if (_tempSettingID >= 2)
    {
      _tempSettingID = 0;

      if (_settingID >= 2)
        _settingID = 0;
      else
        _settingID++;

      PlayerPrefs.SetInt("_settingID", _settingID);
    }

    PlayerPrefs.SetInt("_tempSettingID", _tempSettingID);
  }
}