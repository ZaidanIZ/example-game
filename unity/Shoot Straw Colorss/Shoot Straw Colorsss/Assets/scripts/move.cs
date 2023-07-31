using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour {
    public ShSfx thisSfx;
    Rigidbody rb;
    public GameObject[] obstacles,obs;
    string[] anims = new string[] { "enemy1","enemy2", "enemy3", "enemy4", "enemy5", "enemy6", "enemy7", "enemy8" };
    GameObject temp;
    GameObject[] temps,tempo;
    public GameObject bullets, keys, fireParticles, winParticles, winPanel;
    float speed=30;
    Animator anim,enemyanim;
    int n = 0,sub,level, b=0;
    float time;
    bool win=false, a=true,mute;
    public  rotate[] rotateObj;
    public Text l;
    AudioSource sound;
    public AudioClip[] sClip;
    bool even;
    public Material keyMat, key1Mat, propMat,standMat;
    // Use this for initialization
	void Start () {
        Score.ScoreValue = 0;
        colours();
        if (!PlayerPrefs.HasKey("mute"))
            PlayerPrefs.SetInt("mute", 0);
        if (PlayerPrefs.GetInt("mute") == 0)
            mute = false;
        else
            mute = true;
        sound = GetComponent<AudioSource>();
        if (levelSelect.call)
        {
            level = int.Parse(levelSelect.level) - 1;
            l.text = "LEVEL " + (level + 1);
        }
        else
        {
            level = (int)PlayerPrefs.GetInt("level");
            l.text = "LEVEL " + (level + 1);
        }
        rotateObj = new rotate[3];
        temps = new GameObject[3];
        tempo = new GameObject[3];
        anim = transform.GetChild(1).gameObject.GetComponent<Animator>();
        levels(0);
        temps[0] = Instantiate(keys, new Vector3(0, 0, obs[0].transform.position.z), Quaternion.identity);
        rotateObj[0] = temps[0].transform.GetChild(0).gameObject.GetComponent<rotate>();
        tempo[0] = Instantiate(obstacles[Random.Range(1, 2)], new Vector3(0, 0, obs[0].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
        tempo[0].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
    }
    // Update is called once per frame
    void Update () {

        if (rotateObj[0])
        {
            if (rotateObj[0].go)
            {
                a = false;
                tempo[0].SetActive(false);
                Vector3 desiredPos = new Vector3(transform.position.x, transform.position.y, rotateObj[0].pos + 3);
                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime *5);
                if (transform.position == desiredPos)
                {
                    levels(1);
                    temps[1] = Instantiate(keys, new Vector3(0, 0, obs[1].transform.position.z), Quaternion.identity);
                    rotateObj[1] = temps[1].transform.GetChild(0).gameObject.GetComponent<rotate>();
                    tempo[1] = Instantiate(obstacles[Random.Range(1, 2)], new Vector3(0, 0, obs[1].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
                    tempo[1].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
                    a = true;
                    rotateObj[0] = null;
                }
            }
        }
        else if (rotateObj[1])
        {
            if (rotateObj[1].go)
            {
                a = false;
                tempo[1].SetActive(false);
                Vector3 desiredPos = new Vector3(transform.position.x, transform.position.y, rotateObj[1].pos + 3);
                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * 5);
                if (transform.position == desiredPos)
                {
                    levels(2);
                    temps[2] = Instantiate(keys, new Vector3(0, 0, obs[2].transform.position.z), Quaternion.identity);
                    rotateObj[2] = temps[2].transform.GetChild(0).gameObject.GetComponent<rotate>();
                    tempo[2] = Instantiate(obstacles[Random.Range(1, 2)], new Vector3(0, 0, obs[2].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
                    a = true;
                    rotateObj[1] = null;
                }
            }
        }
        else if (rotateObj[2])
        {
            if (rotateObj[2].go)
            {
                if (a)
                {
                    if (!mute)
                    {
                        sound.clip = sClip[0];
                        sound.Play();
                    }
                    Invoke("Win", 0.5f);
                    a = false;
                }
                tempo[2].SetActive(false);
                Vector3 desiredPos = new Vector3(transform.position.x, transform.position.y + 2, rotateObj[2].pos + 30);
                transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);
                winParticles.SetActive(true);
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            if (Time.timeSinceLevelLoad - time > 0.1f && a)
            {
                fireParticles.SetActive(true);
                if (!mute)
                {
                    sound.clip = sClip[1];
                    sound.Play();
                }
                anim.Play("shoot");
                thisSfx.playSfx();
                temp = Instantiate(bullets, transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).transform.rotation);
                if (even)
                {
                    temp.GetComponent<MeshRenderer>().material = key1Mat;
                    even = false;
                }
                else
                {
                    temp.GetComponent<MeshRenderer>().material = keyMat;
                    even = true;
                }
                rb = temp.GetComponent<Rigidbody>();
                rb.velocity = transform.GetChild(0).transform.forward * speed;
                time = Time.timeSinceLevelLoad;
            }
        }
        else
        {
            anim.Play("player");
            fireParticles.SetActive(false);
        }

        if (Input.GetButton("Jump"))
        {
            if (Time.timeSinceLevelLoad - time > 0.1f && a)
            {
                fireParticles.SetActive(true);
                if (!mute)
                {
                    sound.clip = sClip[1];
                    sound.Play();
                }
                anim.Play("shoot");
                thisSfx.playSfx();
                temp = Instantiate(bullets, transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).transform.rotation);
                if (even)
                {
                    temp.GetComponent<MeshRenderer>().material = key1Mat;
                    even = false;
                }
                else
                {
                    temp.GetComponent<MeshRenderer>().material = keyMat;
                    even = true;
                }
                rb = temp.GetComponent<Rigidbody>();
                rb.velocity = transform.GetChild(0).transform.forward * speed;
                time = Time.timeSinceLevelLoad;
            }
        }
        else
        {
            anim.Play("player");
            fireParticles.SetActive(false);
        }
    }
    void Win()
    {
        levelSelect.call = false;
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        AdManager.instance.showInterstitial();
        winPanel.SetActive(true);
        Invoke("colours", 0.5f);
     }
    void colours()
    {
        propMat.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
        standMat.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
        keyMat.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
        //bulletMat.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
        key1Mat.color = keyMat.color + Color.grey;
    }
    void levels(int i) {
        if (level > 5 && level < 20)
        {
            tempo[i] = Instantiate(obstacles[Random.Range(1, 3)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
        }
        if (level > 20 && level < 40)
        {
            tempo[i] = Instantiate(obstacles[Random.Range(1, 4)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
        }
        if (level > 40 && level < 70)
        {
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
        }
        if (level > 70)
        {
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
        }
        if (level > 200)
        {
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
            tempo[i] = Instantiate(obstacles[Random.Range(1, 5)], new Vector3(0, 0, obs[i].transform.position.z), new Quaternion(0, 0, Random.Range(0, 360), 0));
            tempo[i].GetComponent<Animator>().Play(anims[Random.Range(0, 8)]);
        }
    }
}

