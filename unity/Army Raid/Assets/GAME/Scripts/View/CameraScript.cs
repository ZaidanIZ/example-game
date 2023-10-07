using UnityEngine;
using UnityEngine.Serialization;

public class CameraScript : MonoBehaviour
{
  public float damping = 1.5f;
  public Vector3 offset = new Vector3(0f, 0f, 0f);
  public Transform player;
  private int lastX;
  [SerializeField] private bool showCursor;
  private Animator _cameraAnimator;

  public Texture2D cursorTexture;
  public CursorMode cursorMode = CursorMode.Auto;
  public Vector2 hotSpot = Vector2.zero;

  void Start()
  {
    if (showCursor)
    {
      Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
      Cursor.visible = true;
    }

    //_isActive = false;
    _cameraAnimator = GetComponent<Animator>();
    lastX = Mathf.RoundToInt(player.position.x);
  }

  public void ShakeCamera()
  {
    _cameraAnimator.SetTrigger("shake");
  }

  public void PlayerWinner()
  {
    offset = new Vector3(0f, 4f, -3f);
  }

  public void SetCameraPosition(Vector3 camPosition)
  {
    offset = camPosition;
  }

  public void StartGame()
  {
    offset = new Vector3(0f, 9f, -7.5f);
    //_isActive = true;
  }

  void FixedUpdate()
  {
    int currentX = Mathf.RoundToInt(player.position.x);
    lastX = Mathf.RoundToInt(player.position.x);

    Vector3 target;
    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
    Vector3 currentPosition;


    //  if (_isActive)
    // {
    currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
    transform.position = currentPosition;
    //  }
  }
}