using System.Collections.Generic;
using Board;
using Cards;
using UnityEngine;

public static class Helpers
{
    public static List<Tile> AdjacentTile(this Tile tile)
    {
        Vector3 tilePos = tile.transform.position;

        List<Tile> adjacentTile = new List<Tile>();

        // Above
        if (tilePos.z < 5) adjacentTile.Add(BoardManager.instance.tileMatrix[(int)tilePos.x, (int)tilePos.z + 1]);

        // Left
        if (tilePos.x > 0) adjacentTile.Add(BoardManager.instance.tileMatrix[(int)tilePos.x - 1, (int)tilePos.z]);

        // Right
        if (tilePos.x < 5) adjacentTile.Add(BoardManager.instance.tileMatrix[(int)tilePos.x + 1, (int)tilePos.z]);

        // Below
        if (tilePos.z > 0) adjacentTile.Add(BoardManager.instance.tileMatrix[(int)tilePos.x, (int)tilePos.z - 1]);

        return adjacentTile;
    }
    
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.Cards card)
    {
        List<Tile> tileHavingTheCardOn = new List<Tile>();

        foreach (var tile in tiles)
        {
            if (tile.cardOnThisTile == null) continue;
            
            if (tile.cardOnThisTile.data.thisCard == card)
            {
                tileHavingTheCardOn.Add(tile);
            }
        }
        
        return tileHavingTheCardOn;
    }
    
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.CardType type)
    {
        List<Tile> tileHavingTheCardOn = new List<Tile>();

        foreach (var tile in tiles)
        {
            if (tile.cardOnThisTile == null) continue;
            
            if (tile.cardOnThisTile.data.type == type)
            {
                tileHavingTheCardOn.Add(tile);
            }
        }
        
        return tileHavingTheCardOn;
    }
    
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.CardBiomes biome)
    {
        List<Tile> tileBeingThisBiome = new List<Tile>();

        foreach (var tile in tiles)
        {
            if (tile.cardOnThisTile == null) continue;
            
            if (tile.cardOnThisTile.data.biome == biome)
            {
                tileBeingThisBiome.Add(tile);
            }
        }
        
        return tileBeingThisBiome;
    }

    public static List<Tile> GetAllLinkedGivenCard(this Tile startTile, CardManager.Cards givenCard)
    {
        List<Tile> linkedTiles = new List<Tile>();

        List<Tile> alreadyChekedTiles = new List<Tile>();
        alreadyChekedTiles.Add(startTile);

        FindLinkedTiles(startTile, givenCard, ref alreadyChekedTiles, ref linkedTiles);
        
        return linkedTiles;
    }

    private static void FindLinkedTiles(Tile startTile, CardManager.Cards givenCard, ref List<Tile> alreadyChekedTiles, ref List<Tile> linkedTiles)
    {
        foreach (var tile in startTile.AdjacentTile())
        {
            if (tile.cardOnThisTile == null) continue;
            if (tile.cardOnThisTile.data.thisCard != givenCard) continue;
            
            if (!alreadyChekedTiles.Contains(tile))
            {   
                linkedTiles.Add(tile);
                alreadyChekedTiles.Add(tile);
                
                // Continue Searching from new startTile
                FindLinkedTiles(tile, givenCard, ref alreadyChekedTiles, ref linkedTiles);
            }
        }
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