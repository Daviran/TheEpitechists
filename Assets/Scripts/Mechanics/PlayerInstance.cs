using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class PlayerInstance : MonoBehaviour
    {
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

        private float _maxSpecial = 5f;
        public virtual float MaxSpecial
        {
            get => _maxSpecial;
            set => _maxSpecial = value;
        }

        private float _currentSpecial;
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

        public virtual void SpecialAttack()
        {
            if (CurrentSpecial == MaxSpecial && Input.GetKey(KeyCode.F))
            {

            }
        }

        public virtual void SpecialRPGAttack()
        {
            if (CurrentSpecial == MaxSpecial && Input.GetKey(KeyCode.F))
            {

            }
        }

        void Start()
        {
            CurrentHp = MaxHp;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
