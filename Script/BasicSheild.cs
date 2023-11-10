using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BasicSheild", menuName = "BasicSheild")]
public class BasicSheild : Skill
{
    private int atkperShield;
    public override void ActiveSkill(Character _user, Character _subject)
    {
        if (_user.skillList.Find(x=>x.skillName=="영웅심"))
        {
            atkperShield += (_user.ATK + _user.Buff_ATK) * 25 / 100;
        }

        if (ArtifactManager.S.OddPocket.able)
        {
            TakeSheild(_user, _subject, 110, atkperShield, false, true);
        }
        else
        {
            TakeSheild(_user, _subject, 100, atkperShield, false, true);
        }

        
    }
}
