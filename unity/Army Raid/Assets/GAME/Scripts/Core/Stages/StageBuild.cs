using System;
using UnityEngine;

[Serializable]
public class StageBuild
{
  public StageBuildItem[] _stageBuildItems;
  public GameObject BossPrefab;
  public int EnemyLevel;
  public int EnemyFinalWarriors;
  public int EnemyFinalArchers;
  public int RewardLoseMoney;
  public int RewardWinMoney;
 
}
