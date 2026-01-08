using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Runtime State")]
    public PlayerStats stats = new PlayerStats();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewRun()
    {
        stats = new PlayerStats(); // 初期値に戻す
    }
}
