using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class GameSceneDirector : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    public Vector2 WorldStart;
    public Vector2 WorldEnd;

    public PlayerController Player;

    [SerializeField] Transform parentTextDamage;
    [SerializeField] GameObject prefabTextDamage;

    [SerializeField] Text textTimer;
    public float GameTimer;
    public float OldSeconds;

    [SerializeField] EnemySpawnerController enemySpawner;

    [SerializeField] Slider sliderXP;
    [SerializeField] Slider sliderHP;
    [SerializeField] Text textLv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int playerId = 0;
        Player = CharacterSettings.Instance.CreatePlayer(playerId, this, enemySpawner, textLv,sliderHP,sliderXP);
        OldSeconds -= 1;
        enemySpawner.Init(this,tilemapCollider);
        foreach(Transform item in grid.GetComponentInChildren<Transform>())
        {
            //開始位置
            if(TileMapStart.x > item.position.x)
            {
                TileMapStart.x = item.position.x;
            }
            if(TileMapStart.y > item.position.y)
            {
                TileMapStart.y = item.position.y;
            }
            //終了位置
            if(TileMapEnd.x < item.position.x)
            {
                TileMapEnd.x = item.position.x;
            }
            if(TileMapEnd.y < item.position.y)
            {
                TileMapEnd.y = item.position.y;
            }
        }
        //画面縦半分の描画範囲
        float camerasize = Camera.main.orthographicSize;
        float aspect = (float)Screen.width/(float)Screen.height;
        WorldStart = new Vector2(TileMapStart.x - camerasize * aspect, TileMapStart.y - camerasize);
        WorldEnd = new Vector2(TileMapEnd.x + camerasize * aspect, TileMapEnd.y + camerasize);
    }

    // Update is called once per frame
    void Update()
    {
        updateGameTimer();
    }

    public void DispDamage(GameObject target, float damage)
    {
        GameObject obj = Instantiate(prefabTextDamage, parentTextDamage);
        obj.GetComponent<TextDamageController>().Init(target,damage);
    }
    void updateGameTimer()
    {
        GameTimer += Time.deltaTime;
        int seconds = (int)GameTimer % 60;
        if(seconds == OldSeconds) return;

        textTimer.text = Utils.GetTextTimer(GameTimer);
        OldSeconds = seconds;
    }
}
