using System;
/*
 * 
 * 
1.   public void Fire(Player player) - тут нужна проверка player на null

2. HaveBullets - лучше именовать CanFire

3.             if (health < 0)
            {
                throw new ArgumentOutOfRangeException($"{_health} уже не то");
            } - придерживайся одного стиля, либо со скобками, либо без

4.             if (HaveBullets)
            { - лучше инвертировать if

 * 
 * 
 * 
1.         public void Fire(Player player) - в каждом публичном методе нужна проверка аргументов  на null/outOfRange и бросать исключение в случае ошибки

2. _bullets > 0 - это лучше вынести в свойство

3.             if (damage < 0)
                return; - нужно бросать исключение, это исключительная ситуация когда урон отрицательный

4.             if (damage >= _health)
                _health = 0;
            else
                _health -= damage; - лучше использовать Math.Max

5.             if (player != null)
                _weapon.Fire(player); - тут так же будет тихо глючить если мы передадим null, нужно исключние

6. Для всех классов нужны конструкторы с проверкой аргументов на null/outOfRange
 */

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

        public bool HaveBullets => Bullets > 0;

        public void Fire(Player player)
        {
            if (HaveBullets)
            {
                Bullets--;
                player.TakeDamage(_damage);
            }
        }
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            if (health < 0)
            {
                throw new ArgumentOutOfRangeException($"{_health} уже не то");
            }

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
