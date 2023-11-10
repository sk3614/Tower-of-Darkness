using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KO", menuName = "Skill/KO")]
public class KO : Skill
{
    int b_vam;
    int vam;
    public override void ActiveSkill(Character _user, Character _subject)
    {
        vam = _user.VAM;
        b_vam = _user.Buff_Vam;
        _user.Buff_Vam = 0;
        _user.VAM = 0;
        Attack(_user, _subject, 9000);
        _user.Buff_Vam = b_vam;
        _user.VAM = vam;
    }
}
