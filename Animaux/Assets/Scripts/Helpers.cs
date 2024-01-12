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
    
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.CardBiome biome)
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
    
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.CardCategory category)
    {
        List<Tile> tileBeingThisBiome = new List<Tile>();

        foreach (var tile in tiles)
        {
            if (tile.cardOnThisTile == null) continue;
            
            if (tile.cardOnThisTile.data.category == category)
            {
                tileBeingThisBiome.Add(tile);
            }
        }
        
        return tileBeingThisBiome;
    }
    
    // Check for flags if they contain the given values
    private static bool BiomeFilter(CardBehaviour card, CardManager.CardBiome biome) => (card.data.biome & biome) != 0;
    private static bool TypeFilter(CardBehaviour card, CardManager.CardType type) => (card.data.type & type) != 0;
    private static bool CategoryFilter(CardBehaviour card, CardManager.CardCategory category) => (card.data.category & category) != 0;
    
    // return the tiles of a given list that contain given values for each flags
    public static List<Tile> WhichIs(this List<Tile> tiles, CardManager.CardBiome? biome, 
        CardManager.CardType? type, CardManager.CardCategory? category)
    {
        List<Tile> result = new List<Tile>();

        // Check if the flags are not equals to "nothing" in which case it should not be considered
        bool biomeHasValue = biome != 0;
        bool typeHasValue = type != 0;
        bool categoryHasValue = category != 0;

        foreach (var tile in tiles)
        {
            if (tile.cardOnThisTile == null) continue;
            
            // if the flags are set to nothing or if they contain the wanted values, they match
            bool biomeMatches = !biomeHasValue || BiomeFilter(tile.cardOnThisTile, biome.Value);
            bool typeMatches = !typeHasValue || TypeFilter(tile.cardOnThisTile, type.Value);
            bool categoryMatches = !categoryHasValue || CategoryFilter(tile.cardOnThisTile, category.Value);

            // if all flags matches, add to result
            if (biomeMatches && typeMatches && categoryMatches)
            {
                result.Add(tile);
            }
        }

        return result;
    }

    // Return true if the card matches the given criteria
    public static bool Is(this Tile tile, CardManager.CardBiome? biome, 
        CardManager.CardType? type, CardManager.CardCategory? category)
    {
        if (tile.cardOnThisTile == null) return false;
        
        // Check if the flags are not equals to "nothing" in which case it should not be considered
        bool biomeHasValue = biome != 0;
        bool typeHasValue = type != 0;
        bool categoryHasValue = category != 0;
        
        // if the flags are set to nothing or if they contain the wanted values, they match
        bool biomeMatches = !biomeHasValue || BiomeFilter(tile.cardOnThisTile, biome.Value);
        bool typeMatches = !typeHasValue || TypeFilter(tile.cardOnThisTile, type.Value);
        bool categoryMatches = !categoryHasValue || CategoryFilter(tile.cardOnThisTile, category.Value);

        // if all flags matches, return true
        return biomeMatches && typeMatches && categoryMatches;
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