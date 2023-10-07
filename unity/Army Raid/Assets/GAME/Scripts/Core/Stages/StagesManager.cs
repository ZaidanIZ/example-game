using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StagesManager : MonoBehaviour
{
  private int _stageLevel;
  [SerializeField] private StageSetting[] _stageSetting;
  [FormerlySerializedAs("_stageBuild")] [SerializeField] private StageBuild[] levels;
  public GameObject WarriorPrefab_1;
  [HideInInspector]public List<StageItem> stageItems;
  private Transform _clone;
  private NavMeshSurface _navMeshSurface;
  private StageManagerUI _stageManagerUI;
  private int _settingID;

  public StageItem GetCurrentStageItem => stageItems[_stageLevel];
  public int EnemyStageLevel => levels[_loadLevel].EnemyLevel;
  public int StageLevel => _stageLevel;
  private int _loadLevel;

  private void Awake()
  {
    _navMeshSurface = GetComponent<NavMeshSurface>();
    _stageManagerUI = FindObjectOfType<StageManagerUI>();

    _stageLevel = PlayerPrefs.GetInt("_stageLevel");
    ComponentsManager.CameraScript.SetCameraPosition(new Vector3(-3.75f, 12f,
      -13f + (_stageLevel * 23)));

    _settingID = PlayerPrefs.GetInt("_settingID");
  }

  private void OnEnable()
  {
    CheckLoadLevelAvailable();
    
    Init();
    _stageManagerUI.Init(levels[_loadLevel]._stageBuildItems.Length + 1);
    _stageManagerUI.SetActiveItemUI(_stageLevel);
  }

  public void Init()
  {
    for (int i = 0; i < levels[_loadLevel]._stageBuildItems.Length; i++)
    {
      _clone = Instantiate(_stageSetting[_settingID].StagePrefab.transform, Vector3.zero, Quaternion.identity);
      _clone.SetParent(transform);
      _clone.localPosition = new Vector3(0, 0, i * 22.6f);
      stageItems.Add(_clone.GetComponent<StageItem>());
      _clone.GetComponent<StageItem>()
        .Init(levels[_loadLevel]._stageBuildItems[i].EnemyWarriorQty,
          levels[_loadLevel]._stageBuildItems[i].EnemyArcherQty, null);
    }

    BuildFinishStage();

    _navMeshSurface.BuildNavMesh();

    ComponentsManager.GameUI.SetRewards(levels[_loadLevel].RewardWinMoney,
      levels[_loadLevel].RewardLoseMoney);
  }

  public bool NextStage()
  {
    _stageLevel++;
    PlayerPrefs.SetInt("_stageLevel", _stageLevel);
    _stageManagerUI.SetActiveItemUI(_stageLevel);
    return _stageLevel <= levels[_loadLevel]._stageBuildItems.Length;
  }

  private void BuildFinishStage()
  {
    _clone = Instantiate(_stageSetting[_settingID].StageBuildFinishPrefab.transform, Vector3.zero, Quaternion.identity);
    _clone.SetParent(transform);
    _clone.localPosition = new Vector3(0, 0,
      levels[_loadLevel]._stageBuildItems.Length * 22.6f);
    stageItems.Add(_clone.GetComponent<StageItem>());
    _clone.GetComponent<StageItem>().Init(levels[_loadLevel].EnemyFinalWarriors,
      levels[_loadLevel].EnemyFinalArchers,
      levels[_loadLevel].BossPrefab);
  }

  private void CheckLoadLevelAvailable()
  {
    if (ComponentsManager.PlayerData.GetLevel >= levels.Length)
      _loadLevel = Random.Range(17, levels.Length);
    else
      _loadLevel = ComponentsManager.PlayerData.GetLevel;
  }
}