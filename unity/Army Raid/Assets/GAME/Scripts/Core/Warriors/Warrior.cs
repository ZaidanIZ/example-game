using JetBrains.Annotations;
using UnityEngine;

public class Warrior : WarriorStats
{
  public bool isArcher;
  [SerializeField] [CanBeNull] private Transform _target;
  [SerializeField] private bool isBoss;
  private int _warriorLevel;
  private Warrior _targetWarrior;
  [SerializeField]private bool _isAttack;
  [HideInInspector] public bool IsStop;
  private float _attackTimerTemp;
  private AudioSource _audioSource;

  private void Start()
  {
    IsEnemy = GetComponent<Enemy>();
    _attackTimerTemp = Random.Range(0.4f, 1f);
    UpdateStats();
    HealthStartValue = Health;
    _audioSource = GetComponent<AudioSource>();
  }

  public void SearchTarget()
  {
    if (IsStop) return;

    if (IsEnemy)
    {
      SearchIfEnemy();
    }
    else
    {
      SearchIfPlayer();
    }

    if (_target)
      _targetWarrior = _target.GetComponent<Warrior>();
  }
  
  private void Update()
  {
    CheckRunAnimation();

    if (!isArcher)
    {
      if (_target && !IsDead)
      {
        NavMeshAgent.SetDestination(_target.position);
      }
    }
    
    if (HealthProgressAnim.fillAmount > HealthProgress.value )
    {
      HealthProgressAnim.fillAmount -= Time.deltaTime/1.5f;
    }

    if (_isAttack && !IsDead)
    {
      if (_attackTimerTemp <= 0)
      {
        _attackTimerTemp = AttackTimer;

        if (!isArcher)
          AgentAnimator.SetTrigger("attack_1");
        else
        {
          AgentAnimator.SetTrigger("attack_2");
          transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
          ArcherParticle.Play();
        }

        ApplyDamage(_targetWarrior);

        if (_audioSource)
        {
          _audioSource.pitch = Random.Range(.8f, 1.1f);
          _audioSource.Play();
        }

        if (_targetWarrior.IsDead)
          SearchTarget();
      }
      else
        _attackTimerTemp -= Time.deltaTime;
    }
  }

  public void GoToNextPoint(Vector3 _position)
  {
    _target = null;
    NavMeshAgent.stoppingDistance = 0;
    NavMeshAgent.SetDestination(_position);
    _isAttack = false;
  }

  public void PlayerWinner()
  {
    _target = null;
    _isAttack = false;
    NavMeshAgent.isStopped = true;
    AgentAnimator.Play("Winner_0", 0, Random.Range(0, 0.7f));
    AgentAnimator.SetTrigger("win");
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!IsEnemy)
    {
      if (other.CompareTag("Enemy"))
      {
        _target = other.transform;
        TriggerFunc();
      }
    }
    else
    {
      if (other.CompareTag("Player"))
      {
        _target = other.transform;
        TriggerFunc();
        if(isBoss)
          print("triggered");
      }
    }
  }

  private void TriggerFunc()
  {
    _isAttack = true;
    _targetWarrior = _target.GetComponent<Warrior>();
  }

  private void CheckRunAnimation()
  {
    if (NavMeshAgent.velocity.sqrMagnitude > 0)
      AgentAnimator.SetBool("run", true);
    else
      AgentAnimator.SetBool("run", false);
  }

  private void SearchIfEnemy()
  {
    if (ComponentsManager.PlayerArmyManager.GetRandomPlayerWarrior())
      _target = ComponentsManager.PlayerArmyManager.GetRandomPlayerWarrior().transform;

    IsTargetNull();
  }

  private void SearchIfPlayer()
  {
    if (ComponentsManager.StagesManager.GetCurrentStageItem.GetRandomEnemy())
      _target = ComponentsManager.StagesManager.GetCurrentStageItem.GetRandomEnemy().transform;

    IsTargetNull();
  }

  private void IsTargetNull()
  {
    if (_target == null)
    {
      _isAttack = false;
    }
    else
    {
      if (isArcher)
      {
        _isAttack = true;
      }
    }
  }

  private void UpdateStats()
  {
    int warriorLevel;
    int archerLevel;

    if (!IsEnemy)
    {
      warriorLevel = ComponentsManager.UpgradeUI.GetWarriorLevelUpgrade();
      archerLevel = ComponentsManager.UpgradeUI.GetArcherLevelUpgrade();
    }
    else
    {
      warriorLevel = ComponentsManager.StagesManager.EnemyStageLevel;
      archerLevel = ComponentsManager.StagesManager.EnemyStageLevel;

      if (isBoss)
      {
        warriorLevel = ComponentsManager.PlayerData.GetLevel * 3;
      }
    }

    if (isArcher)
    {
      Health = Health + archerLevel;
      Damage = Damage + archerLevel;
    }
    else
    {
      Health = Health + warriorLevel;
      Damage = Damage + warriorLevel;
    }
  }
}