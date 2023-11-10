using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Object", menuName = "NewObject")]
public class TowerObjectData : ScriptableObject
{
    public int ObjectNum;
    public Sprite ObjectImage;
    public Sprite[] aniImages;
    public Sprite[] delAniImages;
    public string objectName;
    public string e_objectName;
    public ObjectType objectType;
    [TextArea]
    public string objectInfo;
    public MonsterData monsterData;
    public enum ObjectType
    {
        none,
        Wall,
        Monster,
        Door,
        Iron_Bar,
        Barrier,
        Switch,
        Trap,
        Up_Stair,
        Down_Stair,
        Chest,
        Potion,
        Key,
        UpgradeStone,
        SecretWall,
        SecretWallTriger
    }


}