using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
  public Material DeathMaterial;
  public Texture[] CharacterTextures;
  public GameObject PlayerWarriorPrefab;
  public GameObject PlayerArcherPrefab;
  public GameObject EnemyWarriorPrefab;
  public GameObject EnemyArcherPrefab;
  public GameObject GoldForDeadPrefab;
  public UpgradeLevels[] WarriorUpgradeLevel;
  [HideInInspector] public List<Warrior> EnemyStageWarriors = new List<Warrior>();
  [HideInInspector] public List<Warrior> PlayerStageWarriors = new List<Warrior>();
  private int _counter;
  private bool _isStageEnd;

  public void StartBattle()
  {
    // EnemyStageWarriors.Clear();
    _isStageEnd = false;
    EnemyStageWarriors = ComponentsManager.StagesManager.GetCurrentStageItem.Enemys;
    PlayerStageWarriors = ComponentsManager.PlayerArmyManager.PlayerWarriors;

    for (int i = 0; i < EnemyStageWarriors.Count; i++)
    {
      EnemyStageWarriors[i].IsStop = false;
      EnemyStageWarriors[i].SearchTarget();
    }

    for (int i = 0; i < PlayerStageWarriors.Count; i++)
    {
      PlayerStageWarriors[i].IsStop = false;
      PlayerStageWarriors[i].SearchTarget();
    }
  }

  public void CheckStatus()
  {
    EnemyStageWarriors = ComponentsManager.StagesManager.GetCurrentStageItem.Enemys;
    PlayerStageWarriors = ComponentsManager.PlayerArmyManager.PlayerWarriors;

    //Player Win
    if (EnemyStageWarriors.Count <= 0)
    {
      if (!_isStageEnd)
      {
        _isStageEnd = true;

        if (ComponentsManager.StagesManager.NextStage())
        {
          _counter = 0;

          for (int i = 0; i < PlayerStageWarriors.Count; i++)
          {
            PlayerStageWarriors[i].IsStop = true;

            PlayerStageWarriors[i]
              .GoToNextPoint(ComponentsManager.StagesManager.GetCurrentStageItem.GetPlayerPoint(_counter).position);

            _counter++;
          }

          StartCoroutine(StageWin());
        }
        else
        {
          for (int i = 0; i < PlayerStageWarriors.Count; i++)
            PlayerStageWarriors[i].PlayerWinner();

          ComponentsManager.GameUI.PlayerWinner();
        }
      }
    }

    //Player Lose
    if (PlayerStageWarriors.Count <= 0)
    {
      if (!_isStageEnd)
      {
        _isStageEnd = true;

        for (int i = 0; i < EnemyStageWarriors.Count; i++)
        {
          EnemyStageWarriors[i].IsStop = true;
          EnemyStageWarriors[i].PlayerWinner();
        }

        ComponentsManager.GameUI.PlayerLose();
      }
    }
  }

  IEnumerator StageWin()
  {
    yield return new WaitForSeconds(1.5f);
    ComponentsManager.CameraScript.SetCameraPosition(new Vector3(-3.75f, 13f,
      -16f + (ComponentsManager.StagesManager.StageLevel * 23)));
    yield return new WaitForSeconds(4f);
    ComponentsManager.PlayerArmyManager.ResetPlayerWarriors();
    ComponentsManager.MenuUI.ActivateMenu(true);
  }
}