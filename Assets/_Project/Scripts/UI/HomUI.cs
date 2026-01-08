using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HomeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI goldText;

    void Start()
    {
        var s = GameState.Instance.stats;
        statusText.text = $"HP {s.maxHp}\nATK {s.Atk}\nCD {s.CoolDown:0.00}s";
        goldText.text = s.gold.ToString();
    }
}
