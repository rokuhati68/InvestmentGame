using UnityEngine;

public class PlayerStatusApplier : MonoBehaviour
{
    [SerializeField] private PlayerCombat combat; // 自動射撃など
    [SerializeField] private PlayerHealth health;

    void Start()
    {
        var s = GameState.Instance.stats;

        health.SetMaxHp(s.maxHp);
        health.SetHp(s.hp);

        combat.SetAttack(s.attack);
        combat.SetDirections(s.fireDirections);
        combat.SetCooldown(s.cooldown);
    }
}
