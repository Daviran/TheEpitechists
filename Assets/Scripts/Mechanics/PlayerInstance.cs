using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class PlayerInstance
    {

        public PlayerInstance()
        {
            CurrentHp = MaxHp;
        }

        public PlayerController playerController;
        public enum PlayerState
        {
            escape,
            actionRPG
        }
        public PlayerState playerState;

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

        private float _maxSpecial = 100f;
        public virtual float MaxSpecial
        {
            get => _maxSpecial;
            set => _maxSpecial = value;
        }

        private float _currentSpecial = 0;
        public virtual float CurrentSpecial
        {
            get => _currentSpecial;
            set => _currentSpecial = value;
        }

        private float _physic = 5f;
        public virtual float Physic
        {
            get => _physic;
            set => _physic = value;
        }

        private float _techno = 5f;
        public virtual float Techno
        {
            get => _techno;
            set => _techno = value;
        }

        private float _social = 5f;
        public virtual float Social
        {
            get => _social;
            set => _social = value;
        }

        private float _damage;
        public virtual float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public virtual void SpecialAttack()
        {
            if (CurrentSpecial == MaxSpecial && Input.GetKey(KeyCode.F) && playerState == PlayerState.escape)
            {
                Debug.Log("Coup Special !");
            }
        }

        public virtual void SpecialRPGAttack(EnnemiesInstance[] ennemies)
        {
            if (CurrentSpecial == MaxSpecial && Input.GetKey(KeyCode.F) && playerState == PlayerState.actionRPG)
            {
                Debug.Log("Coup Special RPG !");
            }
        }

        public void TakeDamage(int number)
        {
            CurrentHp -= (number - Physic);
        }

        public void Heal(int number)
        {
            CurrentHp += number;
        }

        public void InflictDamage(EnnemiesInstance ennemy)
        {
            int damage = (int)Damage + (int)Physic;
            ennemy.TakeDamage(damage);
        }

        public void SwitchPlayerState()
        {
            if (Input.GetKey(KeyCode.Space)) playerState = PlayerState.actionRPG;
        }

        /*void Start()
        {
            playerState = PlayerState.escape;
            CurrentHp = MaxHp;
        }

        void Update()
        {
            if (CurrentSpecial != MaxSpecial)
            {
                CurrentSpecial++;
            }
            SwitchPlayerState();
        }*/
    }

}
