using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class CarmeloPlayer : PlayerInstance
    {
        public CarmeloPlayer(float hp = 5, float physic = 1, float techno = 4, float social = 2, string name = "Melo")
        {
            MaxHp = hp;
            CurrentHp = MaxHp;
            Physic = physic;
            Techno = techno;
            Social = social;
            Name = name;
        }

        public override void SpecialAttack()
        {

        }
        public override void SpecialRPGAttack(EnnemiesInstance[] ennemies)
        {
            if (CurrentSpecial == MaxSpecial && Input.GetKey(KeyCode.F) && playerState == PlayerState.actionRPG)
            {
                for (int i = 0; i < ennemies.Length; i++)
                {
                    InflictDamage(ennemies[i]);
                    ennemies[i].CanAttack = false;
                }
            }
        }

    }

}
