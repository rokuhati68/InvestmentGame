using System;

[Serializable]
public class PlayerStats
{
    public int maxHp = 10;
    public int Atk = 1;
    public float CoolDown = 0.5f;
    public float BulletSize = 0.1f;
    public int gold = 0;
    public PlayerStats Clone()
    {
        return (PlayerStats)MemberwiseClone();
    }
}