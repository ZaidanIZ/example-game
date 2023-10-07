using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
  [SerializeField] private RectTransform tutorialPointer;
  public static int _steps;
  [SerializeField] private RectTransform[] objectSteps;
  [SerializeField] private Button[] allButtons;
  [SerializeField] private Button[] disableButtons1;
  [SerializeField] private Button[] disableButtons2;
  [SerializeField] private Button[] disableButtons3;

  public int GetStep => _steps;

  private void Start()
  {
    if (_steps == 2)
    {
      NextStep(3, true);
    }
  }

  public void NextStep(int stepNumber, bool pointerStatus)
  {
    EnableButtons();
    tutorialPointer.gameObject.SetActive(pointerStatus);
    _steps = stepNumber;

    switch (_steps)
    {
      case 1:
        for (int i = 0; i < disableButtons1.Length; i++)
          disableButtons1[i].interactable = false;
        break;

      case 2:
        for (int i = 0; i < disableButtons1.Length; i++)
          disableButtons2[i].interactable = false;
        break;

      case 3:
        for (int i = 0; i < disableButtons1.Length; i++)
          disableButtons3[i].interactable = false;
        ComponentsManager.Tutorial.NextStep(4, true);
        break;

      case 5:
        PlayerPrefs.SetInt("Tutorial", 1);
        Destroy(gameObject);
        break;
    }
  }

  private void EnableButtons()
  {
    for (int i = 0; i < allButtons.Length; i++)
    {
      allButtons[i].interactable = true;
    }
  }

  private void Update()
  {
    if (_steps < 5)
      tutorialPointer.position = Vector3.Lerp(tutorialPointer.position, objectSteps[_steps].position, .05f);
  }
}