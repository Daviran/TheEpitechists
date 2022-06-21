using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class DavidPlayer : PlayerInstance
    {
        public DavidPlayer(float hp = 15, float physic = 5, float techno = 5, float social = 3, string name = "David")
        {
            MaxHp = hp;
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
