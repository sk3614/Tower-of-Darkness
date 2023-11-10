using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Monster", menuName = "New Monster")]
public class MonsterData : ScriptableObject
{
    public Sprite monsterImage;
    public int MonsterID;
    public Sprite[] aniSprites;
    public Skill NormalAttack;
    public Skill NormalShield;
    public List<Skill> skillList;
    
}
