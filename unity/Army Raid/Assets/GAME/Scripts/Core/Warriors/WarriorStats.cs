using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WarriorStats : MonoBehaviour
{
  public int Health;
  public int Damage;
  public float AttackTimer;
  public NavMeshAgent NavMeshAgent;
  public Animator AgentAnimator, ShadowAnimator;
  public ParticleSystem ArcherParticle;
  public Slider HealthProgress;
  public Image HealthProgressAnim;
  public GameObject InnerCapsuleCollider;
  public SkinnedMeshRenderer CharacterMesh;
  public bool IsDead;
  [HideInInspector] public bool IsEnemy;
  [HideInInspector] public int HealthStartValue;

  public void TakeDamage(int value)
  {
    Health -= value;
    HealthProgress.value = (float) Health / HealthStartValue;

    if (IsEnemy)
      CharacterMesh.material.mainTexture = ComponentsManager.BattleManager.CharacterTextures[1];
    else
      CharacterMesh.material.mainTexture = ComponentsManager.BattleManager.CharacterTextures[3];

    StartCoroutine(TakeDamageView());

    if (!IsDead)
      HealthProgress.gameObject.SetActive(true);

    if (Health <= 0 && !IsDead)
    {
      AgentAnimator.SetTrigger("death");
      ComponentsManager.StagesManager.GetCurrentStageItem.DeleteEnemyFromStageList(this.GetComponent<Warrior>());
      ComponentsManager.PlayerArmyManager.DeletePlayerFromStageList(this.GetComponent<Warrior>());

      ComponentsManager.BattleManager.CheckStatus();
      NavMeshAgent.isStopped = true;
      NavMeshAgent.enabled = false;
      GetComponent<CapsuleCollider>().enabled = false;
      InnerCapsuleCollider.SetActive(false);
      CharacterMesh.material = ComponentsManager.BattleManager.DeathMaterial;

      if (ShadowAnimator)
        ShadowAnimator.SetTrigger("Hide");

      if (IsEnemy)
      {
        GameObject _clone = Instantiate(ComponentsManager.BattleManager.GoldForDeadPrefab, transform.position,
          Quaternion.identity);
        ComponentsManager.PlayerWallet.AddMoney(5);
        ComponentsManager.GameUI.CoiAudioPlay();
        Destroy(_clone, 2);
      }

      Destroy(gameObject, 12);

      HealthProgress.gameObject.SetActive(false);
      IsDead = true;
    }
  }

  public void ApplyDamage(Warrior _warrior)
  {
    StartCoroutine(DelayApplyDamage(_warrior));
  }

  IEnumerator DelayApplyDamage(Warrior _warrior)
  {
    yield return new WaitForSeconds(0.2f);
    _warrior.TakeDamage(Damage);
  }

  IEnumerator TakeDamageView()
  {
    yield return new WaitForSeconds(.15f);
    if (IsEnemy)
      CharacterMesh.material.mainTexture = ComponentsManager.BattleManager.CharacterTextures[0];
    else
      CharacterMesh.material.mainTexture = ComponentsManager.BattleManager.CharacterTextures[2];

    if (IsDead)
      CharacterMesh.material.mainTexture = null;
  }
}