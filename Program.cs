using System;

namespace weapon
{
    class Weapon
    {
        private readonly int _damage;

        public Weapon(int damage, int bullets)
        {
            if (_damage < 0)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} нелетальное оружие");

            if (bullets < 0)
                throw new ArgumentOutOfRangeException($"подайте {nameof(bullets)} поручик Галицин");

            _damage = damage;
            Bullets = bullets;
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
            if (health < 0)
                throw new ArgumentOutOfRangeException($"{_health} уже не то");

            _health = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException($"отрицательный {nameof(damage)}");

            _health = Math.Max(0, _health - damage);
        }
    }

    class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            if (weapon is null)
                throw new ArgumentNullException($"раз в 100 лет и {nameof(weapon)} стреляет");

            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (player is null)
                throw new ArgumentNullException($"{nameof(player)} null-объект");

            _weapon.Fire(player);
        }
    }
}
