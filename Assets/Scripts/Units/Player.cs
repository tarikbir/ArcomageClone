using ArcomageClone.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace ArcomageClone.Units
{
    public class Player : MonoBehaviour
    {
        //TODO: Starting values Scriptable Object
        public Castle Castle;
        public Wall Wall;
        public int BuilderGain = 3;
        public int Builder = 8;
        public int MightGain = 3;
        public int Might = 8;
        public int MagicGain = 3;
        public int Magic = 8;

        public List<Card> Hand = new();

        //public CardPile Deck;

        public void DrawCard()
        {
            Hand.Add(LevelManager.Instance.DrawCard(this));
        }

        public void RemoveCard(Card card)
        {
            Hand.Remove(card);
        }

        public void Damage(int amount)
        {
            if (Wall.Health > 0)
            {
                int wall = Wall.Health;
                Wall.Health = Mathf.Max(Wall.Health - amount, 0);
                amount -= wall;
            }
            
            if (amount > 0)
            {
                Castle.Health -= amount;
            }
        }
    }
}