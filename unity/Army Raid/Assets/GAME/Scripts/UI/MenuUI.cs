using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button[] tombol;
  [SerializeField] private GameObject menuUIWrapper;
  [SerializeField] private AudioSource startAudioSource;
  [SerializeField] private GameObject TutorialWrapper;
  private bool _isPressed;

  private void Awake()
  {
    if (PlayerPrefs.HasKey("Tutorial"))
      Destroy(TutorialWrapper);
  }

  public void ActivateMenu(bool status)
  {
    if (status)
    {
      startAudioSource.Play();

      if (ComponentsManager.Tutorial)
      {
        if (ComponentsManager.Tutorial.GetStep == 0)
          ComponentsManager.Tutorial.NextStep(1, true);
      }
    }

    menuUIWrapper.SetActive(status);
        Debug.Log("pe");
        ADsummoner.adSummoner.LoadReward();
        foreach (Button item in tombol)
        {
            item.interactable = true;
            Debug.Log("nyala");
        }
    }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      ComponentsManager.PlayerWallet.AddMoney(144);
    }
  }

  public void StartBattleButton()
  {
    ComponentsManager.BattleManager.StartBattle();
    ActivateMenu(false);

    if (!_isPressed)
    {
      _isPressed = true;
    }

    if (ComponentsManager.Tutorial)
    {
      if (ComponentsManager.Tutorial.GetStep == 0)
        ComponentsManager.Tutorial.NextStep(0, false);
      
      if (ComponentsManager.Tutorial.GetStep == 2)
        ComponentsManager.Tutorial.NextStep(2, false);
      
      if (ComponentsManager.Tutorial.GetStep == 4)
        ComponentsManager.Tutorial.NextStep(5, false);
    }
  }
}