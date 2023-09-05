using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrickshotArena
{
    public class OneUpManager : MonoBehaviour
    {
        /// <summary>
        /// 1Up is a special (reward) game object which gives player a new life. Player starts with 3 life at the beginning of the game.
        /// </summary>
        /// 
        public GameObject circleEffect;

        /// <summary>
        /// Detect collision with the player object
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Player":
                    //tell game manager that we eat an 1up
                    GlobalGameManager.instance.Eat1Up();

                    //Untag this object to prevent null reference error
                    this.gameObject.tag = "Untagged";

                    //move it towards the UI health
                    StartCoroutine(MoveToHealthUI());

                    //No more collision
                    GetComponent<SphereCollider>().enabled = false;
                    break;
            }
        }


        /// <summary>
        /// move this 1Up towards the playerLife indicator object on UI/HUD
        /// </summary>
        /// <returns></returns>
        public IEnumerator MoveToHealthUI()
        {
            GameObject hbUI = GameObject.FindGameObjectWithTag("healthBarUI");
            if (!hbUI)
                yield break;

            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(-11, 10, 0);

            print("startPos: " + startPos);
            print("targetPos: " + targetPos);

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 1.0f;
                transform.position = new Vector3(Mathf.SmoothStep(startPos.x, targetPos.x, t),
                                                   Mathf.SmoothStep(startPos.y, targetPos.y, t),
                                                   -0.5f);
                yield return 0;
            }

            if (t >= 1)
                Destroy(gameObject);
        }


        /// <summary>
        /// Animate the scale of this object
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public IEnumerator scaleAnimator(int dir)
        {
            Vector3 startingScale = new Vector3();
            Vector3 targetScale = new Vector3();

            if (dir == 1)
            {
                startingScale = new Vector3(0.1f, 0.1f, 0.1f);
                targetScale = transform.localScale;
            }
            else if (dir == -1)
            {
                startingScale = transform.localScale;
                targetScale = new Vector3(0.1f, 0.1f, 0.1f);
            }

            transform.localScale = startingScale;
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 1.0f;
                transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, targetScale.x, t),
                                                   Mathf.SmoothStep(startingScale.y, targetScale.y, t),
                                                   Mathf.SmoothStep(startingScale.z, targetScale.z, t));
                yield return 0;
            }

            if (t >= 1)
            {
                //this.gameObject.GetComponent<SphereCollider>().enabled = true;

                if (dir == -1)
                {
                    this.gameObject.tag = "Untagged";
                    Destroy(gameObject, 5);
                    this.gameObject.GetComponent<SphereCollider>().enabled = false;
                    GetComponent<Renderer>().enabled = false;
                    circleEffect.GetComponent<Renderer>().enabled = false;
                }
            }
        }

    }
}