using UnityEngine;

public class AmmunitionWarrior : MonoBehaviour
{
  [SerializeField] private GameObject[] weapons;
  [SerializeField] private GameObject helm;
  [SerializeField] private GameObject shield;
  [SerializeField] private GameObject bows;
  private Warrior _warrior;
  private int _weaponLevel;

  private void Start()
  {
    _warrior = GetComponent<Warrior>();

    if (_warrior.IsEnemy)
      _weaponLevel = ComponentsManager.StagesManager.EnemyStageLevel;
    else
    {
      if(_warrior.isArcher)
        _weaponLevel = ComponentsManager.UpgradeUI.GetArcherLevelUpgrade();
      else
        _weaponLevel = ComponentsManager.UpgradeUI.GetWarriorLevelUpgrade();
    }
    
    CheckWarriorLevelAmmunition();
  }

  private void CheckWarriorLevelAmmunition()
  {
    if (!_warrior.isArcher)
    {
      for (int i = 0; i < weapons.Length; i++)
      {
        weapons[i].SetActive(false);
      }
      
      switch (_weaponLevel)
      {
        case 0:
          weapons[0].SetActive(true);
          break;
        
        case 1:
          weapons[0].SetActive(true);
          shield.SetActive(true);
          break;
        
        case 2:
          weapons[0].SetActive(true);
          shield.SetActive(true);
          break;
        
        case 3:
          weapons[1].SetActive(true);
          shield.SetActive(true);
          break;
        
        case 4:
          weapons[1].SetActive(true);
          shield.SetActive(true);
          break;
        
        case 5:
          weapons[1].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 6:
          weapons[1].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 7:
          weapons[2].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 8:
          weapons[2].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 9:
          weapons[3].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 10:
          weapons[3].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 11:
          weapons[3].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 12:
          weapons[4].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 13:
          weapons[4].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 14:
          weapons[4].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 15:
          weapons[4].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        case 16:
          weapons[5].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
        
        default:
          weapons[5].SetActive(true);
          shield.SetActive(true);
          helm.SetActive(true);
          break;
      }
    }
    else
    {
      if(_weaponLevel>0)
        helm.SetActive(true);
    }
  }
}