using System;

namespace ArcomageClone.Cards
{
    [Serializable]
    public struct TypedEffect<T>
    {
        public SideTargeter Side;
        public T Type;
        public int Amount;
    }
}
