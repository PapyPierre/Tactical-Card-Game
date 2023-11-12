using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "DeckList", menuName = "ScriptableObjects/DeckList", order = 1)]
    public class DeckList : ScriptableObject
    {
        public List<CardManager.Cards> list;
        
        //TODO faire un systeme pour checker le nombre d'exemplaire de chaque carte pour savoir si la decklist est légal
    }
}