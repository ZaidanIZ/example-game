using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public GameObject[] characters;

    [Space(20)]
    public float smoothTime = 0.45f;

    public float
        maxYPos,
        minYPos;

    [Space(20)]
    public TextMesh[] getCoinEffect;
    public TextMesh[] getDiamondEffect;

    private SpriteRenderer spriteRender;

    private Vector3 m_CurrentVelocity;

    private Vector2 posOffset = Vector2.zero;

    private float
        maxXPos,
        minXPos;

    private bool touchedOnScreen = false;

    public SpriteRenderer SpriteRender { get { return spriteRender; } set { spriteRender = value; } }

    public int HP { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        HP = 3;

        // Set Layer for textMesh
        for (var i = 0; i < getCoinEffect.Length; i++)
        {
            getCoinEffect[i].GetComponent<Renderer>().sortingLayerName = "Bonus";
            getDiamondEffect[i].GetComponent<Renderer>().sortingLayerName = "Bonus";
        }
    }

    public void GetSpriteMainChar()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetAxisX_Boundary()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float offset = spriteRender.bounds.size.x / 2;

        minXPos = worldPoint.x + offset;
        maxXPos = worldPoint.x + worldScreenWidth - offset;
    }

    void Update()
    {
        if (!GameController.Instance.GameOver && UIManager.Instance.inGame.activeInHierarchy)
        {
            if (touchedOnScreen)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 targetPosition =
                    new Vector3(Mathf.Clamp(mousePos.x - posOffset.x, minXPos, maxXPos),
                        Mathf.Clamp(mousePos.y - posOffset.y, minYPos, maxYPos));

                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_CurrentVelocity, smoothTime);
            }

            GameController.Instance.UpdateDistance();
        }
    }

    public void PointerDown()
    {
        touchedOnScreen = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posOffset = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
    }

    public void PointerUp()
    {
        touchedOnScreen = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!GameController.Instance.GameOver)
        {
            if (col.tag == "Check" || col.tag == "ItemCheck")
            {
                if (col.transform.position.y <= transform.position.y)
                {
                    spriteRender.sortingOrder = col.GetComponentInParent<SpriteRenderer>().sortingOrder - 1;
                }
                else
                {
                    spriteRender.sortingOrder = col.GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!GameController.Instance.GameOver)
        {
            if (col.tag == "Check" || col.tag == "ItemCheck")
            {
                SpriteRenderer otherRender = col.GetComponentInParent<SpriteRenderer>();

                if (otherRender != null)
                {
                    if (col.transform.position.y <= transform.position.y)
                    {
                        spriteRender.sortingOrder = otherRender.sortingOrder - 1;
                    }
                    else
                    {
                        spriteRender.sortingOrder = otherRender.sortingOrder + 1;
                    }
                }
            }
        }
    }
}
