using System.Collections.Generic;
using System.Linq;

namespace ArcomageClone.Cards
{
    /// <summary>Represents a bunch of card scripts piled together.</summary>
    public class CardPile
    {
        /// <summary>Returns if pile has any card.</summary>
        public bool HasCards => _pile.Count > 0;

        /// <summary>Returns the pile name.</summary>
        public string Name { get; private set; }

        private List<Card> _pile;

        /// <summary>Initializes the pile with a name.</summary>
        /// /// <param name="name">Pile name.</param>
        public CardPile(string name)
        {
            _pile = new List<Card>();
            Name = name;
        }

        /// <summary>Initializes the pile with another pile and a name.</summary>
        /// <param name="pile">Cards to add.</param>
        /// <param name="name">Pile name.</param>
        public CardPile(Card[] pile, string name)
        {
            _pile = new List<Card>(pile);
            Name = name;
        }

        /// <summary>Adds a card to the pile.</summary>
        /// <param name="card">Card to add.</param>
        public void AddCard(Card card)
        {
            _pile.Add(card);
        }

        /// <summary>Adds a bunch of cards to the pile.</summary>
        /// <param name="cards">Cards to add.</param>
        public void AddCards(IEnumerable<Card> cards)
        {
            foreach (var item in cards)
            {
                AddCard(item);
            }
        }

        /// <summary>Removes a specific card from the pile.</summary>
        /// <param name="card">Card to find.</param>
        public bool RemoveCard(Card card)
        {
            return _pile.Remove(card);
        }

        /// <summary>Removes a card at index from the pile.</summary>
        /// <param name="index">Indexes start at 0.</param>
        public bool RemoveCard(int index)
        {
            if (_pile.Count > index)
            {
                _pile.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>Removes the top card from the pile and returns its script.</summary>
        /// <returns>Returns null if pile has no cards.</returns>
        public Card GetTopCard()
        {
            if (!HasCards) return null;
            return GetCard(0);
        }

        /// <summary>Removes a random indexed card from the pile and returns its script. Uses Random.Range().</summary>
        /// <returns>Returns null if pile has no cards.</returns>
        public Card GetRandomCard()
        {
            if (!HasCards) return null;
            int index = UnityEngine.Random.Range(0, _pile.Count);
            return GetCard(index);
        }

        /// <summary>Removes all cards from the pile and returns them as an array.</summary>
        /// <returns>Returns null if pile has no cards.</returns>
        public Card[] GetAllCards()
        {
            if (!HasCards) return null;
            var cards = _pile.ToArray();
            Clear();
            return cards;
        }

        /// <summary>Shuffles the order of the pile randomly. Rotates cards by %52,2 chance.</summary>
        public void Shuffle()
        {
            _pile = _pile.OrderBy(c => UnityEngine.Random.Range(0, _pile.Count)).ToList();
        }

        /// <summary>Removes all cards from the pile.</summary>
        public void Clear()
        {
            for (int i = 0; i < _pile.Count; i++)
            {
                RemoveCard(i);
            }
        }

        private Card GetCard(int index)
        {
            var card = _pile[index];
            RemoveCard(index);
            return card;
        }
    }
}
