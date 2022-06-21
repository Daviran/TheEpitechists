using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class EnnemiesInstance
    {
        private string _name;
        public virtual string Name
        {
            get => _name;
            set => _name = value;
        }

        private float _maxHp = 10f;
        public virtual float MaxHp
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        private float _currentHp;
        public virtual float CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        private float _damage;
        public virtual float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        private bool _canAttack = true;
        public virtual bool CanAttack
        {
            get => _canAttack;
            set => _canAttack = value;
        }

        public void TakeDamage(int damage)
        {
            CurrentHp -= damage;
        }

        public void InflictDamage(PlayerInstance player)
        {
            int damage = (int)Damage;
            player.TakeDamage(damage);
        }
    }

}
