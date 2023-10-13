using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionScript : MonoBehaviour
{
    public GameObject smoke;

    private Animator anim;

    private int
        transformHash,
        dieHash;

    private bool
        isInvulnerable = false,
        zombieForm;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        transformHash = Animator.StringToHash("Transform");
        dieHash = Animator.StringToHash("Die");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerController.Instance.HP > 0)
        {
            if (col.tag == "Zombie")
            {
                if (!zombieForm)
                {
                    if (!isInvulnerable)
                    {
                        isInvulnerable = true;
                        StartCoroutine(ImmortalDuration(1.0f));

                        Hurt();
                    }
                }
            }

            if (col.tag == "HItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.getItems.Play();
                }

                col.gameObject.SetActive(false);

                if (PlayerController.Instance.HP < 3)
                {
                    UIManager.Instance.hearts[PlayerController.Instance.HP].SetActive(true);
                    PlayerController.Instance.HP++;
                }
            }

            if (col.tag == "ISItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.powerDown.Play();
                }

                col.gameObject.SetActive(false);

                GameController.Instance.SpeedUp();
            }

            if (col.tag == "DSItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.getItems.Play();
                }

                col.gameObject.SetActive(false);

                GameController.Instance.SpeedDown();
            }

            if (col.tag == "ZItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.getItems.Play();
                }

                col.gameObject.SetActive(false);

                zombieForm = true;
                smoke.SetActive(true);
                anim.SetBool(transformHash, true);

                StopCoroutine("ZombieDuration");
                StartCoroutine("ZombieDuration");
            }

            if (col.tag == "CItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.getCoinAndDiamond.Play();
                }

                col.gameObject.SetActive(false);
                GameController.Instance.Coin += 5.0f;

                var length = PlayerController.Instance.getCoinEffect.Length;
                for (var i = 0; i < length; i++)
                {
                    if (!PlayerController.Instance.getCoinEffect[i].gameObject.activeInHierarchy)
                    {
                        PlayerController.Instance.getCoinEffect[i].gameObject.SetActive(true);
                    }
                }
            }

            if (col.tag == "DItem")
            {
                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.getCoinAndDiamond.Play();
                }

                col.gameObject.SetActive(false);
                GameController.Instance.Diamond++;

                var length = PlayerController.Instance.getDiamondEffect.Length;
                for (var i = 0; i < length; i++)
                {
                    if (!PlayerController.Instance.getDiamondEffect[i].gameObject.activeInHierarchy)
                    {
                        PlayerController.Instance.getDiamondEffect[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void Hurt()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.hurt.Play();
        }

        PlayerController.Instance.HP--;
        UIManager.Instance.hearts[PlayerController.Instance.HP].SetActive(false);

        if (PlayerController.Instance.HP == 0)
        {
            if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
            {
                SoundManager.Instance.death.Play();
            }

            smoke.SetActive(true);
            anim.SetTrigger(dieHash);
            GameController.Instance.GameOver = true;
            GameController.Instance.backgroundSpeed = 0.0f;
            GameController.Instance.streetSpeed = 0.0f;

            GameController.Instance.ClearItems();
            GameController.Instance.Over();
        }
        else
        {
            if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
            {
                SoundManager.Instance.hurt.Play();
            }
        }
    }

    private IEnumerator ZombieDuration()
    {
        var timeWait = 2.0f + PlayerPrefs.GetInt(Constants.STATE_ZOMBIE_TIME, 1);

        // Show HUD
        UIManager.Instance.zItemTimer.gameObject.SetActive(true);
        UIManager.Instance.zItemTimer.fillAmount = 1.0f;
        UIManager.Instance.ZItemCoolingDown = true;
        UIManager.Instance.ZItemTimeDuration = timeWait;

        yield return new WaitForSeconds(timeWait);

        zombieForm = false;
        smoke.SetActive(true);
        anim.SetBool(transformHash, false);
    }

    private IEnumerator ImmortalDuration(float duration)
    {
        float time = 0.0f;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            PlayerController.Instance.SpriteRender.enabled = false;
            yield return new WaitForSeconds(0.05f);
            PlayerController.Instance.SpriteRender.enabled = true;

            time += 0.15f;
            if (time >= duration)
            {
                isInvulnerable = false;
                break;
            }
        }
    }

    void OnDisable()
    {
        isInvulnerable = zombieForm = false;
    }
}
