using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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
        
    }
}