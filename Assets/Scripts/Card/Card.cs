using ArcomageClone.Units;
using System;
using UnityEngine;

namespace ArcomageClone.Cards
{
    public class Card
    {
        public CardSO Data;
        public string Name;
        public string Description;
        public Sprite Icon;
        public int Cost;
        public CardType Type;
        public Player Owner;
        public Action Effect;

        public Card(CardSO data, Player owner)
        {
            Data = data;
            Name = data.Name;
            Description = data.Description;
            Icon = data.Icon;
            Type = data.Type;
            Cost = data.Cost;
            Owner = owner;
            Effect = BuildEffect(data.Effects);
        }

        public Action BuildEffect(TypedEffect<EffectType>[] effects)
        {
            return () => {
                foreach (var effect in effects)
                {
                    GetEffect(effect.Side, effect.Type, effect.Amount);
                }
            };
        }

        public void GetEffect(SideTargeter side, EffectType type, int amount)
        {
            Player target = side == SideTargeter.Owner ? Owner : PlayerManager.Instance.GetEnemy(Owner);
            switch (type)
            {
                case EffectType.Damage:
                    target.Damage(amount);
                    break;
                case EffectType.AddCastle:
                    target.Castle.Health += amount;
                    break;
                case EffectType.AddWall:
                    target.Wall.Health += amount;
                    break;
                case EffectType.BuildGain:
                    target.BuilderGain += amount;
                    break;
                case EffectType.Build:
                    target.Builder += amount;
                    break;
                case EffectType.MightGain:
                    target.MightGain += amount;
                    break;
                case EffectType.Might:
                    target.Might += amount;
                    break;
                case EffectType.MagicGain:
                    target.MagicGain += amount;
                    break;
                case EffectType.Magic:
                    target.Magic += amount;
                    break;
                case EffectType.BuildEncampment:
                    break;
                case EffectType.BuildForceField:
                    break;
                case EffectType.GrowForest:
                    break;
                case EffectType.DigMoat:
                    break;
                case EffectType.MagicWall:
                    break;
            }
        }

        public bool CanOwnerPayCost()
        {
            return Type switch
            {
                CardType.Build => Owner.Builder >= Cost,
                CardType.Might => Owner.Might >= Cost,
                CardType.Magic => Owner.Magic >= Cost,
                _ => false,
            };
        }

        public void PayCost()
        {
            switch (Type)
            {
                case CardType.Build:
                    Owner.Builder -= Cost;
                    break;
                case CardType.Might:
                    Owner.Might -= Cost;
                    break;
                case CardType.Magic:
                    Owner.Magic -= Cost;
                    break;
                default:
                    break;
            }
        }
    }
}
