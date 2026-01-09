using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHp;
    private int _hp;

    public void SetMaxHp(int maxHp)
    {
        _maxHp = maxHp;
        _hp = maxHp;
    }
    public void Damaged(int damage)
    {
        _hp -= damage;
        Debug.Log(_hp);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collide");
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            var damage = enemyStats.Atk;
            Damaged(damage);
        }
    }
}