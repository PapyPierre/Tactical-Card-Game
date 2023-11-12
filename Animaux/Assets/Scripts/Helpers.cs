using System;
using System.Collections.Generic;
using Board;
using UnityEngine;

public static class Helpers
{
    public static List<Tile> AdjacentTile(this Tile tile)
    {
        Vector3 tilePos = tile.transform.position;
        
        List<Tile> adjacentTile = new List<Tile>();
        
        // Above
        adjacentTile.Add(BoardManager.instance.tileMatrix[ (int) tilePos.x, (int) tilePos.z +1]);
        
        // Left
        adjacentTile.Add(BoardManager.instance.tileMatrix[ (int) tilePos.x -1, (int) tilePos.z]);

        // Right
        adjacentTile.Add(BoardManager.instance.tileMatrix[ (int) tilePos.x +1, (int) tilePos.z]);

        // Below
        adjacentTile.Add(BoardManager.instance.tileMatrix[ (int) tilePos.x, (int) tilePos.z -1]);

        return adjacentTile;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;

            int i = rng.Next(n + 1);
            (list[i], list[n]) = (list[n], list[i]);
        }
    }
}