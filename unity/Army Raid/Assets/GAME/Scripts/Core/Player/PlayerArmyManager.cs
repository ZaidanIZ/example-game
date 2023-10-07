using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerArmyManager : MonoBehaviour
{
  private int _playerWarriorCount;
  private int _playerArcherCount;

  [HideInInspector] public List<Warrior> PlayerWarriors = new List<Warrior>();
  private int _counter;

  private void Awake()
  {
    LoadData();
  }

  private void Start()
  {
    Init();
  }

  public void DeletePlayerFromStageList(Warrior _warrior)
  {
    PlayerWarriors.Remove(_warrior);
  }

  public Warrior GetRandomPlayerWarrior()
  {
    return PlayerWarriors[Random.Range(0, PlayerWarriors.Count)];
  }

  public void AddNewWarrior()
  {
    _playerWarriorCount++;

    PlayerWarriors.Add(Instantiate(ComponentsManager.BattleManager.PlayerWarriorPrefab.GetComponent<Warrior>(),
      ComponentsManager.StagesManager.GetCurrentStageItem.GetPlayerPoint(_counter).position, Quaternion.identity));

    _counter++;
    SaveData();
  }

  public void AddNewArcher()
  {
    _playerArcherCount++;


    PlayerWarriors.Add(Instantiate(ComponentsManager.BattleManager.PlayerArcherPrefab.GetComponent<Warrior>(),
      ComponentsManager.StagesManager.GetCurrentStageItem.GetPlayerPoint(_counter).position, Quaternion.identity));

    _counter++;
    SaveData();
  }

  public void ResetPlayerWarriors()
  {
    for (int i = 0; i < PlayerWarriors.Count; i++)
    {
      Destroy(PlayerWarriors[i].gameObject);
    }

    PlayerWarriors.Clear();
    Init();
  }

  private void Init()
  {
    _counter = 0;

    InitWarrior();
    InitArcher();
  }

  private void InitWarrior()
  {
    for (int i = 0; i < _playerWarriorCount; i++)
    {
      PlayerWarriors.Add(Instantiate(ComponentsManager.BattleManager.PlayerWarriorPrefab.GetComponent<Warrior>(),
        ComponentsManager.StagesManager.GetCurrentStageItem.GetPlayerPoint(_counter).position, Quaternion.identity));
      _counter++;
    }
  }

  private void InitArcher()
  {
    for (int i = 0; i < _playerArcherCount; i++)
    {
      PlayerWarriors.Add(Instantiate(ComponentsManager.BattleManager.PlayerArcherPrefab.GetComponent<Warrior>(),
        ComponentsManager.StagesManager.GetCurrentStageItem.GetPlayerPoint(_counter).position, Quaternion.identity));
      _counter++;
    }
  }

  private void LoadData()
  {
    if (PlayerPrefs.HasKey("playerWarriorCount"))
      _playerWarriorCount = PlayerPrefs.GetInt("playerWarriorCount");
    else
      _playerWarriorCount = 2;

    _playerArcherCount = PlayerPrefs.GetInt("playerArcherCount");
  }

  private void SaveData()
  {
    PlayerPrefs.SetInt("playerArcherCount", _playerArcherCount);
    PlayerPrefs.SetInt("playerWarriorCount", _playerWarriorCount);
  }
}