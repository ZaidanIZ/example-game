/// <summary>

///----------- ESPAÑOL -----------
/// 
/// 
/// Este script sirve para dar una resolucion fija a la pantalla independientemente de los pixeles o aspect ratio que posea el dispositivo.
/// Si el dispositivo es mas ancho o mas largo de lo establecido en el aspecto ratio se llenara con un fondo negro para tener
/// una resolucion fija. Lo que se ve en la camara de Unity es exactamente lo que severa en el dispositivo movil.
/// La variable _wantedAspectRatio se obtiene dividiendo el aspect ratio que se desea, en este caso es 10/16 = 0.625f
/// 
///----------- ENGLISH -----------
/// 
/// This script serves to give a fixed resolution to the screen, regardless of the number of screen pixels or aspect ratio of the mobile device.
/// If the resolution of the device is wider or longer than the aspect ratio established in the "_wantedAspectRatio" variable, the remaining pixels
/// will be fill with black background. It that you see in the camera of Unity is exactly it that you will see in the mobile device.
/// The "_wantedAspectRatio" variable is obtain dividing the aspect ratio that you want, in this case is: 10/16 = 0.625f
///
/// </summary>

using UnityEngine;

public class AspectUtility : MonoBehaviour 
{
	
	public float _wantedAspectRatio = 0.625f;
	static float wantedAspectRatio;
	static Camera cam;
	static Camera backgroundCam;

	void Awake () 
	{

		cam = GetComponent<Camera>();
		if (!cam) 
		{
			cam = Camera.main;
		}
		if (!cam) 
		{
			Debug.LogError ("No camera available");
			return;
		}
		wantedAspectRatio = _wantedAspectRatio;
		SetCamera();
	}
	
	public static void SetCamera () 
	{
		float currentAspectRatio = (float)Screen.width / Screen.height;
		// If the current aspect ratio is already approximately equal to the desired aspect ratio,
		// use a full-screen Rect (in case it was set to something else previously)
		if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f) 
		{
			cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
			if (backgroundCam) 
			{
				Destroy(backgroundCam.gameObject);
			}
			return;
		}
		// Pillarbox
		if (currentAspectRatio > wantedAspectRatio) 
		{
			float inset = 1.0f - wantedAspectRatio/currentAspectRatio;
			cam.rect = new Rect(inset/2, 0.0f, 1.0f-inset, 1.0f);
		}
		// Letterbox
		else 
		{
			float inset = 1.0f - currentAspectRatio/wantedAspectRatio;
			cam.rect = new Rect(0.0f, inset/2, 1.0f, 1.0f-inset);
		}
		if (!backgroundCam) 
		{
			// Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
			backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
			backgroundCam.depth = int.MinValue;
			backgroundCam.clearFlags = CameraClearFlags.SolidColor;
			backgroundCam.backgroundColor = Color.black;
			backgroundCam.cullingMask = 0;
		}
	}
	
	public static int screenHeight 
	{
		get 
		{
			return (int)(Screen.height * cam.rect.height);
		}
	}
	
	public static int screenWidth 
	{
		get 
		{
			return (int)(Screen.width * cam.rect.width);
		}
	}
	
	public static int xOffset 
	{
		get 
		{
			return (int)(Screen.width * cam.rect.x);
		}
	}
	
	public static int yOffset 
	{
		get 
		{
			return (int)(Screen.height * cam.rect.y);
		}
	}
	
	public static Rect screenRect 
	{
		get 
		{
			return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
		}
	}
	
	public static Vector3 mousePosition 
	{
		get 
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.y -= (int)(cam.rect.y * Screen.height);
			mousePos.x -= (int)(cam.rect.x * Screen.width);
			return mousePos;
		}
	}
	
	public static Vector2 guiMousePosition
	{
		get 
		{
			Vector2 mousePos = Event.current.mousePosition;
			mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
			mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
			return mousePos;
		}
	}
}