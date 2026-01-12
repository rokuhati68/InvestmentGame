using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
public static class Utils
{
    public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }
    //当たり判定のあるタイルかどうか調べる
    public static bool IsCollisionTile(Tilemap tileMapCollision, Vector2 position)
    {
        Vector3Int cellPosition = tileMapCollision.WorldToCell(position);
        if(tileMapCollision.GetTile(cellPosition))
        {
            return true;
        }
        return false;
    }
}