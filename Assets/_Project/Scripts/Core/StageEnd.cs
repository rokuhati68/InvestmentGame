using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEnd : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private int goldEarned = 20;

    public void ClearStage()
    {
        var s = GameState.Instance.stats;

        // 残HPを持ち越すなど
        s.gold += goldEarned;

        SceneManager.LoadScene("Home");
    }

    public void GameOver()
    {
        var s = GameState.Instance.stats;
        SceneManager.LoadScene("Result");
    }
}
