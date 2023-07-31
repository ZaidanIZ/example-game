/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script sirve para cuando el jugador haga clic en un objeto que sea un sprite y contenga un collider, se ponga transparente y haga sonar
/// el sonido de clic, luego cuando suelte el clic se ponga de nuevo de la transparencia que tenia. In this way have an effect button.
/// 
///----------- ENGLISH -----------
/// 
/// This script is for when the player clicks on an object that is an sprite and contains a collider, this object set transparent and play the sound of click.
/// When the player release the click, the object will be back the transprency that had. In this way have an effect button.
/// 
/// </summary>

using UnityEngine;

public class Effect_Button : MonoBehaviour 
{
	
	RaycastHit2D hitMouseClic;
	GameObject cameraAudio;

	void Start ()
	{

		cameraAudio = GameObject.Find("Main Camera").gameObject;

	}

	void Update () 
	{
	
		ClicDetectionAndSetEffect ();

	}

	void ClicDetectionAndSetEffect () 
	{

		if (Input.GetMouseButtonDown (0)) 
		{
			
			hitMouseClic = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			
			if (hitMouseClic.collider != null) 
			{
				
				if (hitMouseClic.collider.GetComponent<SpriteRenderer>() != null) 
				{
					
					hitMouseClic.collider.GetComponent<SpriteRenderer>().color = new Vector4 (hitMouseClic.collider.GetComponent<SpriteRenderer>().color.r, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.g, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.b, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.a / 1.5f);
					
					if (PlayerPrefs.GetInt("sounds") == 1) 
					{
						cameraAudio.GetComponent<AudioSource>().PlayOneShot(cameraAudio.GetComponent<Sounds>().buttonClic);
					}
					
				}
			}
			
		} 
		
		if (Input.GetMouseButtonUp (0)) 
		{
			
			if (hitMouseClic.collider != null)
			{
				
				if (hitMouseClic.collider.GetComponent<SpriteRenderer>() != null) 
				{
					
					hitMouseClic.collider.GetComponent<SpriteRenderer>().color = new Vector4 (hitMouseClic.collider.GetComponent<SpriteRenderer>().color.r, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.g, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.b, hitMouseClic.collider.GetComponent<SpriteRenderer>().color.a * 1.5f);
					
				}
				
			}
		}

	}
}
