using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CharacterSettings", menuName = "ScriptableObjects/CharacterSettings")]
public class CharacterSettings : ScriptableObject
{
    public List<CharacterStats> datas;
    static CharacterSettings instance;
    public static CharacterSettings Instance
    {
        get
        {
            if(! instance)
            {
                instance = Resources.Load<CharacterSettings>(nameof(CharacterSettings));
            }
            return instance;
        }
    }
    public CharacterStats Get(int id)
    {
        return (CharacterStats)datas.Find(item => item.Id == id).GetCopy();
    }

    public EnemyController CreateEnemy(int id, GameSceneDirector sceneDirector, Vector3 position)
    {
        CharacterStats stats = Instance.Get(id);
        GameObject obj = Instantiate(stats.Prefab, position,Quaternion.identity);
        EnemyController ctrl = obj.GetComponent<EnemyController>();
        ctrl.Init(sceneDirector, stats);
        return ctrl;
    }

    //プレイヤー生成
    public PlayerController CreatePlayer(int id,GameSceneDirector sceneDirector,
        EnemySpawnerController enemySpawner, Text textLv, Slider sliderHP, Slider sliderXP)
    {
        CharacterStats stats = Instance.Get(id);
        GameObject obj = Instantiate(stats.Prefab,Vector3.zero, Quaternion.identity);
        PlayerController ctrl = obj.GetComponent<PlayerController>();
        ctrl.Init(sceneDirector, enemySpawner, stats, textLv,sliderHP, sliderXP);
        return ctrl;
    }
    
}
public enum MoveType
{
    TargetPlayer,
    TargetDirection,    
}
[System.Serializable]
public class CharacterStats : BaseStats
{
    public GameObject Prefab;
    public List<int> DefaultWeaponIds;
    public List<int> UsableWeaponIds;
    public int UsableWeaponMax;
    public MoveType MoveType;
}
