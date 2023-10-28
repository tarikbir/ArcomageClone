using UnityEngine;

namespace ArcomageClone.Cards
{
    [CreateAssetMenu(fileName = "New CardData", menuName = "CardData", order = 0)]
    public class CardSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public CardType Type;
        public int Cost;
        public TypedEffect<EffectType>[] Effects;
    }
}