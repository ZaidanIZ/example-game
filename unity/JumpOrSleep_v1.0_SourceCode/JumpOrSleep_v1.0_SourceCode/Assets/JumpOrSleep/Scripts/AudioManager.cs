using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	public AudioSource _audio;
	public AudioClip jumpSound;
	public AudioClip hitGroundSound;
	public AudioClip dieSound;
	public AudioClip coinSound;

	public Image soundImage;
	public Sprite[] soundIcon;

	private static AudioManager m_Instance;
	public static AudioManager Instance
	{
		get
		{
			return m_Instance;
		}
	}

	public void Awake()
	{
		if(Instance == null)
        {
			m_Instance = this;
		}
        else
        {
			Destroy(this.gameObject);
        }
		//m_Instance = this;
		//DontDestroyOnLoad(this.gameObject);
		SetSound ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(string soundName)
	{
		if(soundName == "jump")
			_audio.PlayOneShot (jumpSound);
		else if(soundName == "hit")
			_audio.PlayOneShot  (hitGroundSound);
		else if(soundName == "die")
			_audio.PlayOneShot  (dieSound);
		else if(soundName == "coin")
			_audio.PlayOneShot  (coinSound);
		else
			_audio.PlayOneShot  (jumpSound);
	}

	public void ToggleSound()
	{
		int sound = PlayerPrefs.GetInt ("Sound");
		if (sound == 0) {
			PlayerPrefs.SetInt ("Sound", 1);
			soundImage.sprite = soundIcon [1];
			_audio.mute = true;
		}
		else
		{
			PlayerPrefs.SetInt ("Sound", 0);
			soundImage.sprite = soundIcon [0];
			_audio.mute = false;
		}
	}

	public void SetSound()
	{
		int sound = PlayerPrefs.GetInt ("Sound");
		if (sound == 0) {
			soundImage.sprite = soundIcon [0];
			_audio.mute = false;
		}
		else
		{
			soundImage.sprite = soundIcon [1];
			_audio.mute = true;
		}
	}
}
