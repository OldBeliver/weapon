using System;

namespace weapon
{
    class Weapon
    {
        private readonly int _damage;

        public Weapon(int damage, int bullets)
        {
            _damage = damage >= 0 ? damage : throw new ArgumentOutOfRangeException(nameof(damage));

            Bullets = bullets >= 0 ? bullets : throw new ArgumentOutOfRangeException(nameof(bullets));
        }

        public int Bullets { get; private set; }

        public bool CanNotFire => Bullets <= 0;

        public void Fire(Player player)
        {
            if (player is null)
                throw new ArgumentNullException($"цель {nameof(player)} не объявлена");

            if (CanNotFire)
                throw new ArgumentOutOfRangeException(nameof(Bullets));

            Bullets--;
            player.TakeDamage(_damage);
        }
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            _health = health >= 0 ? health : throw new ArgumentOutOfRangeException(nameof(health));
        }

        public void TakeDamage(int damage)
        {
            _health = damage >= 0 ? Math.Max(0, _health - damage) : throw new ArgumentOutOfRangeException(nameof(damage));
        }
    }

    class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon)); ;
        }

        public void OnSeePlayer(Player player)
        {
            if (player is null)
                throw new ArgumentNullException($"{nameof(player)} null-объект");

            _weapon.Fire(player);
        }
    }
}
