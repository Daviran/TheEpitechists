using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class EdouPlayer : PlayerInstance
    {
        public EdouPlayer(float hp = 10, float physic = 3, float techno = 5, float social = 4, string name = "Edouard")
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

        public void LevelUp(string capacity)
        {
            switch(capacity)
            {
                case "physic":
                    Physic++;
                    break;
                case "techno":
                    Techno++;
                    break;
                case "social":
                    Social++;
                    break;
            }
        }

    }

}
