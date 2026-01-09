using UnityEngine;

public class PlayerCombat:MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint; // nullならプレイヤー中心から撃つ
    [SerializeField] private float bulletSpeed = 10f;

    [Header("Stats")]
    private int _Atk;
    private float _CoolDown = 1f;
    
    [Header("Fire Pattern")]
    [Tooltip("4方向固定（上/右/下/左）。後で8/16へ拡張する前提。")]
    [SerializeField] private bool fireImmediatelyOnStart = true;
    [SerializeField]
    private float timer;
    private static readonly Vector2[] DIR4 = new Vector2[]
    {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left,
    };

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _CoolDown)
        {
            timer = 0f;
            Fire4Directions();
        }
    }

    private void Fire4Directions()
    {
        Vector3 origin = firePoint != null ? firePoint.position : transform.position;

        for (int i = 0; i < DIR4.Length; i++)
        {
            SpawnBullet(origin, DIR4[i]);
        }
    }

    private void SpawnBullet(Vector3 origin, Vector2 dir)
    {
        Bullet b = Instantiate(bulletPrefab, origin, Quaternion.identity);
        b.Init(dir, bulletSpeed, _Atk);
    }
    public void SetAttack(int Atk)
    {
        _Atk = Atk;
    }
    public void SetCooldown(float CoolDown)
    {
        _CoolDown = CoolDown;
    }

}