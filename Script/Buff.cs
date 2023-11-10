using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ScriptableObject
{
    public string buffName;
    public int RegainNum;
    public int DotDamage;
    public int protectNum;
    public bool isCounter;
    public bool isPermanent;
    public bool CastThisTurn;
    public int EnduredamageNum;
    public int activeNum;//몇번발동되었나
    public int value;
    public bool isStack=false;//중첩가능한가
    public bool isUpgrade=false;
 
    public enum BigBuffType
    {
        //일반
        Buff,
        DeBuff,
        DotDamage,
        Regen
    }
    public enum BuffType
    {
       // 디버프
       CC,// 군중제어
       SS,// 지속피해
       DD,// 능력치 약화
       // 버프
       GG,
       RR,
        BB,// 능력치 강화
        AB,// 절대
       SR,// 지속회복

       // 복합
       SC,//전술적 제어
        Miss,//미스
        CleanHit,//필중
        Critical,//회심
        Pierce,//관통
        Guard,//막기
        Endure,

    }
    public enum BuffImageType
    {
        None,
        StatusUp,
        StatusDown,
        Bleed,
        Burn,
        Poison,
        Erosion,
        Bless,
        Curse,
        Fear,
        Infect,
        Regen,
        Stun,
        Sleep,
        Daze,
        Doom,
        Fortune,
        Haste,
        Invincible,
        Invisible,
        Mirror,
        MisFortune,
        Protection,
        Reveal,
        Wall,
        Paralyze,

    }
    public enum RemainType
    {
        Count,
        Turn,
        Active
    }

    public BuffType buffType;
    public BigBuffType bigBuffType;
    public RemainType remainType;
    public BuffImageType buffImageType;
    public BuffImageType buffImageType2;

    public int RemainTurn;
    public int RemainActive;
    public virtual void ActiveBuff(Character _user)
    {

    }
    public virtual void EndBuff(Character _user)
    {
        
    }
    public virtual void ActiveBuff(Character _user,int _num)
    {

    }
    public virtual void EndBuff(Character _user,int _num)
    {

    }

}
