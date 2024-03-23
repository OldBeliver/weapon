using System;

namespace weapon
{
    /*
     * 
     *  
        1.   public void Fire(Player player) - в каждом публичном методе нужна проверка аргументов  на null/outOfRange и бросать исключение в случае ошибки
        
        2.   _bullets > 0 - это лучше вынести в свойство
        
        3.   if (damage < 0)
                return; - нужно бросать исключение, это исключительная ситуация когда урон отрицательный
        
        4.   if (damage >= _health)
                _health = 0;
            else
                _health -= damage; - лучше использовать Math.Max
        
        5.   if (player != null)
                _weapon.Fire(player); - тут так же будет тихо глючить если мы передадим null, нужно исключние
        
        6. Для всех классов нужны конструкторы с проверкой аргументов на null/outOfRange
    *
    */

    //class Program 
    //{
    //    private  static void Main()
    //    {
    //    }
    //}


    class Weapon
    {
        private readonly int _damage;

        public Weapon(int damage, int bullets)
        {
            if (_damage < 0)
                throw new ArgumentOutOfRangeException($"{nameof(damage)} неожиданно стал отрицательным");

            if (bullets < 0)
                throw new ArgumentOutOfRangeException($"{nameof(bullets)} неожиданно стали отрицательными");

            _damage = damage;
            Bullets = bullets;
        }

        public int Bullets { get; private set; }

        public void Fire(Player player)
        {
            if (Bullets > 0)
            {
                Bullets--;
                player.TakeDamage(_damage);
            }
        }
    }

    class Player
    {
        private int _health;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException($"отрицательный {nameof(damage)}");

            //if (damage >= _health)
            //    _health = 0;
            //else
            //    _health -= damage;

            _health = Math.Max(0, _health - damage);
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            if (player is null)
                throw new ArgumentNullException($"{nameof(player)} null-объект");

            _weapon.Fire(player);
        }
    }
}
