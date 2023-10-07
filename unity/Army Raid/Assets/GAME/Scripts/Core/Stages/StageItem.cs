using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StageItem : MonoBehaviour
{
  [SerializeField] private Transform[] enemyPoints;
  [SerializeField] private Transform[] playerPoints;
  [SerializeField] private Transform bossPoint;
  public List<Warrior> Enemys = new List<Warrior>();
  int counter;
  
  public void Init(int warriorCount, int archerCount,[CanBeNull] GameObject BossPrefab)
  {
    for (int i = 0; i < warriorCount; i++)
    {
      Enemys.Add(Instantiate(ComponentsManager.BattleManager.EnemyWarriorPrefab.GetComponent<Warrior>(), enemyPoints[counter].position, Quaternion.Euler(0,180,0)));
      counter++;
    }

    for (int i = 0; i < archerCount; i++)
    {
      Enemys.Add(Instantiate(ComponentsManager.BattleManager.EnemyArcherPrefab.GetComponent<Warrior>(), enemyPoints[counter].position, Quaternion.Euler(0,180,0)));
      counter++;
    }

    if (BossPrefab)
    {
      Enemys.Add(Instantiate(BossPrefab.GetComponent<Warrior>(), bossPoint.position, Quaternion.Euler(0,180,0)));
      counter++;
    }
  }

  public Transform GetPlayerPoint(int index)
  {
    return playerPoints[index];
  }
  
  public Warrior GetRandomEnemy()
  {
    if (Enemys.Count > 0)
      return Enemys[Random.Range(0, Enemys.Count)];
    else
      return null;
  }

  public void DeleteEnemyFromStageList(Warrior _warrior)
  {
    Enemys.Remove(_warrior);
  }
}