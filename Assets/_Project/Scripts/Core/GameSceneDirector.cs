using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class GameSceneDirector : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    public Vector2 WorldStart;
    public Vector2 WorldEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        
    }
}
