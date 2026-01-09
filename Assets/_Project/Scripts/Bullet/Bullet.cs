using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;

    private Rigidbody2D rb;
    private float lifeTimer;

    public int Damage { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        lifeTimer = 0f;
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 弾の初期化（速度・向き・ダメージ）
    /// </summary>
    public void Init(Vector2 direction, float speed, int damage)
    {
        Damage = damage;
        rb.linearVelocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        // Enemy側に "IDamageable" を実装する想定（後で作る）
        Debug.Log("Bullet Hit");
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(Damage);
        }
        
    }
}

