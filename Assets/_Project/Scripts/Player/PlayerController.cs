using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMouseMover : MonoBehaviour
{
    [Header("追従")]
    public bool instant = true;
    public float moveSpeed = 10f;

    [Header("画面内に制限")]
    public bool clampToCamera = true;

    [System.Serializable]
    public class CameraBounds
    {
        public float xMin, xMax, yMin, yMax;
    }
    [SerializeField] CameraBounds bounds;

    Rigidbody2D rb;
    Camera cam;
    Vector3 targetWorld;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        targetWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        targetWorld.z = 0f;

        if (clampToCamera)
            targetWorld = ClampToCamera(targetWorld);
    }

    void FixedUpdate()
    {
        if (instant)
        {
            rb.MovePosition(targetWorld);
        }
        else
        {
            Vector2 next = Vector2.MoveTowards(
                rb.position,
                targetWorld,
                moveSpeed * Time.fixedDeltaTime
            );
            rb.MovePosition(next);
        }
    }

    Vector3 ClampToCamera(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
        pos.y = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);
        return pos;
    }
}
