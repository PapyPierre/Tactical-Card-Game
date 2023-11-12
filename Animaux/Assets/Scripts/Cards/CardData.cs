using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        public CardManager.Cards thisCard;
        public GameObject prefab;
        public Sprite sprite;
        public bool hasSpecialEffectOnPose;
    }
}