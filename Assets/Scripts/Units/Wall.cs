namespace ArcomageClone.Units
{
    public class Wall : Building
    {
        protected override void Awake()
        {
            base.Awake();

            _startingHealth = 0;
            _health = _startingHealth;
        }
    }
}
