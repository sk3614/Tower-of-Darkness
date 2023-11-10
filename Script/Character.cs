using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    //스텟
    public int level;
    public int hp;
    public int tempHp;//임시체력
    public int bp;//디펜스포인트
    public int bpReset;//매턴BP초기화수치
    public int ATK;//공격수치
    public int DEF;//방어수치
    public int HIT;//적중수치
    public float AVD;//회피수치
    public int SPD;//스피드
    public float RES;//저항
    public float CRC;//치명타율
    public float CRD;//치명타피해량
    public float CDR;//치명타피해량감소
    public float CRR;//치명타저항
    public int POW;//마력
    public int PIE;//관통(bp를 무시하고 데미지)
    public int BLK;//저지(관통효과 저지)
    public int ICD;//주는피해 증가
    public int DCD;//받는피해 감소
    public int VAM;//흡혈
    public int HEL;//회복량
    public int ARC;//방어등급


    public int REG;//재생

    public int AP;//액션포인트
    public int DP;//디펜스포인트
    public int battleAP;
    public int battleAR;//리커버리포인트(있을경우 1당 1AP소모)
    public int battleDP;
    public int battleRD;//리커버리포인트(있을경우 1당 1BP소모)
    public int normalAttackBonusDamage;//평타 보너스 데미지 계수
    public int normalAttackBonusAttack;//평타공격횟수 추가
    //상태이상 수치
    public int Groggy;
    public int Daze;
    public int Doom;
    public int Misfortune;
    public int Infect;
    public int Fear;
    public int Invincible;
    public int Protect;
    public int Invisible;
    public int Bless;
    public int Curse;
    public int Wall;
    public int Fortune;
    public int Haste;
    public int Reveal;
    public int Endure;

    //전투 특수효과 계수
    public int RailgunStack;
    public bool LeapOn;
    public int MasterSwordStack;
    public int pistolStack;


    public MonsterClass monsterClass;
    public MonsterFace monsterFace;
    public int MonsterGrade;//몬스터 등급

    public int MIR;//반사
    public int trueDMG;//고정데미지

    public int Buff_ATK;//공격수치
    public int Buff_DEF;//방어수치
    public int Buff_HIT;//적중수치
    public float Buff_AVD;//회피수치
    public float Buff_CRC;//치명타율
    public float Buff_CRD;//치명타피해량
    public float Buff_CRR;//치명타저항
    public int Buff_SPD;//스피드
    public float Buff_CDR;//치명타피해량감소
    public int Buff_BLK;
    public int Buff_PIE;
    public int Buff_POW;
    public int Buff_ICD;
    public int Buff_DCD;
    public int Buff_Vam;
    public int Buff_REG;
    public int Buff_HEL;
    public int Buff_MIR;
    public int Buff_AP;
    public int Buff_DP;
    public int Buff_bpReset;
    public int Buff_ARC;
    public int MaxHp;
    public int LostHp;

    //상태이상 저항
    public int RG_stun;
    public int RG_Sleep;
    public int RG_Paralyze;
    public int RG_Poison;
    public int RG_Bleed;
    public int RG_Erosion;
    public int RG_Curse;
    public int RG_Daze;
    public int RG_Doom;
    public int RG_Misfortune;
    public int RG_Dispel;
    public int RG_Infect;
    public int RG_Fear;
    public int RG_Burn;
    public int BuffRG_CC;

    public bool isRevive;

    public Skill castSkill;
    public int remainCastTurn;

    public Skill normalAttack;
    public List<Skill> normalAttackBonusEffects;
    public Skill normalShield;
    public List<Skill> skillList;
    public List<Buff> buffList;
    public List<Skill> battleSkills;

    public List<int> firstSkillsCooltime = new List<int>();
    public List<int> firstSkillLimit = new List<int>();
    public List<int> ActiveSkillsCooltime = new List<int>();
    public List<int> ActiveSkillsLimit = new List<int>();
    public List<int> DefendSkillsCooltime = new List<int>();
    public List<int> DefendSkillsLimit = new List<int>();
    public List<int> TurnSkillsCooltime = new List<int>();
    public List<int> TurnSkillsLimit = new List<int>();
    public List<int> CounterSkillsCooltime = new List<int>();
    public List<int> CounterSkillsLimit = new List<int>();
    public List<int> EndSkillsCooltime = new List<int>();
    public enum MonsterClass
    {
        None,
        Guardian,//수호자
        Warrior,//전사
        Assasin,//암살자
        Shooter,//사수
        Magician,//마법사
        Fighter,//격투가
        etc//기타
    }
    public enum MonsterFace//몬스터 타입
    {
        None,
        Undead,//불사
        Demon,//악마
        BackSider,//타락자
        Creature,//피조물
        EvilSpirit,//마물
        Spirit,//정령
        Ain,//아인
        ETC,//기타
    }
    public void AddBuff(Buff _buff, int _turn)
    {
        if (_buff.bigBuffType==Buff.BigBuffType.Buff)
        {
            if (Curse>0)
            {
                return;
            }
        }
        if (_buff.bigBuffType == Buff.BigBuffType.DeBuff|| _buff.bigBuffType == Buff.BigBuffType.DotDamage)
        {
            if (Bless > 0)
            {
                return;
            }
        }

        if (!_buff.isStack)
        {
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i].buffName == _buff.buffName)
                {
                    buffList[i].RemainTurn = _turn;
                    if (_buff.CastThisTurn)
                    {
                        buffList[i].CastThisTurn = true;
                    }
                    return;
                }

            }
        }

        if (_buff.remainType==Buff.RemainType.Count)
        {
            _buff.RemainActive = _turn;
        }else _buff.RemainTurn = _turn;

        buffList.Add(_buff);
        _buff.ActiveBuff(this);
    }
    public void BuffTurnCountDown() //턴끝날시 버프 턴 감소 남은턴0일시 제거
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].bigBuffType==Buff.BigBuffType.Buff)
            {
                if (buffList[i].remainType == Buff.RemainType.Turn)
                {
                    if (buffList[i].CastThisTurn)
                    {
                        buffList[i].CastThisTurn = false;
                    }
                    else
                    {
                        buffList[i].RemainTurn -= 1;
                        if (buffList[i].RemainTurn <= 0)
                        {
                            buffList[i].EndBuff(this);

                        }
                    }
                   
                }
            }
           
        }
        for (int i = buffList.Count-1; i >= 0; i--)
        {
            if (buffList[i].bigBuffType == Buff.BigBuffType.Buff)
            {
                if (buffList[i].remainType == Buff.RemainType.Turn)
                {
                    if (buffList[i].RemainTurn <= 0)
                    {
                        buffList.Remove(buffList[i]);

                    }
                }
            }
        }
    }
    public void DeBuffTurnCountDown() //턴끝날시 디버프 턴 감소 남은턴0일시 제거
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].bigBuffType == Buff.BigBuffType.DeBuff)
            {
                if (buffList[i].remainType == Buff.RemainType.Turn)
                {
                    buffList[i].RemainTurn -= 1;
                    if (buffList[i].RemainTurn <= 0)
                    {
                        buffList[i].EndBuff(this);

                    }
                }
            }

        }
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            if (buffList[i].bigBuffType == Buff.BigBuffType.DeBuff)
            {
                if (buffList[i].remainType == Buff.RemainType.Turn)
                {
                    if (buffList[i].RemainTurn <= 0)
                    {
                        buffList.Remove(buffList[i]);

                    }
                }
            }
        }
    }
    public void DotTurnDown() //도트 턴 다운
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            if (buffList[i].bigBuffType == Buff.BigBuffType.DotDamage)
            {
                if (buffList[i].remainType == Buff.RemainType.Turn)
                {
                    buffList[i].RemainTurn -= 1;
                    if (buffList[i].RemainTurn <= 0)
                    {
                        buffList.Remove(buffList[i]);
                    }
                }
            }
        }
    }
    public void Damaged(int _damage, bool _cri,string _skillName = "undefine")
    {

        int damage = _damage;
        if (Protect > 0||Invincible>0)
        {
            damage = 0;
        }
        if (tempHp<damage)
        {
            damage -= tempHp;
            tempHp = 0;

        }
        else
        {
            tempHp -= damage;
            damage = 0;
        }


        if (hp<damage)
        {
            hp = 0;
        }
        else
        {
            hp -= damage;
        }

        
        LostHp += damage;
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffName=="수면")
            {
                buffList[i].EndBuff(this);
                buffList.RemoveAt(i);
                break;
            }
        }
        if (Battle.S.battleUIBase.activeInHierarchy&&( Protect > 0||Invincible>0))
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    Battle.S.AddBattleEffect(Battle.S.player, Battle.S.monster, null, Battle.HitType.Hit, characterName + "은  피해를 받지 않음", -1, true);
                    break;
                case Options.Language.Eng:
                    Battle.S.AddBattleEffect(Battle.S.monster, Battle.S.player, null, Battle.HitType.Hit, characterName + " not damaged ", -1, true);
                    break;
                default:
                    break;
            } 
        }
    }

    public void Heal(int _heal)
    {
        _heal=_heal*(HEL+Buff_HEL)/ 100;
        int maxHeal = MaxHp - hp;


        if (Infect>0)
        {
            _heal = 0;
        }
        if (Player.S.playerLocation==Player.PlayerLocation.Town)
        {
            hp += _heal;
            return ;
        }


        if (_heal>maxHeal)
        {
            hp += maxHeal;
            LostHp += maxHeal;
        }
        else
        {
            hp += _heal;
            LostHp += _heal;
        }
        if (this.tag=="Player"&&Battle.S.battleUIBase.activeInHierarchy)
        {
            Battle.S.AddBattleEffect(Battle.S.player, Battle.S.monster, null, Battle.HitType.Hit, "", -1, true, 0, false, _heal);
        }
        else
        {
            Battle.S.AddBattleEffect(Battle.S.monster, Battle.S.player, null, Battle.HitType.Hit, "", -1, true, 0, false, _heal);
        }
        

    }

    public void CountDownBuff(Buff.BuffType buffType)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffType == buffType)
            {
                buffList[i].RemainActive -= 1;
                if (buffList[i].RemainActive <= 0)
                {
                    buffList.Remove(buffList[i]);

                }
            }
        }
    }

    public void AllSkillCoolDown(int _num)
    {
        for (int i = 0; i < ActiveSkillsCooltime.Count; i++)
        {
            ActiveSkillsCooltime[i] -= _num;
            if (ActiveSkillsCooltime[i] < 0)
            {
                ActiveSkillsCooltime[i] = 0;
            }
        }
        for (int i = 0; i < CounterSkillsCooltime.Count; i++)
        {
            CounterSkillsCooltime[i] -= _num;
            if (CounterSkillsCooltime[i] < 0)
            {
                CounterSkillsCooltime[i] = 0;
            }
        }
        for (int i = 0; i < DefendSkillsCooltime.Count; i++)
        {
            DefendSkillsCooltime[i] -= _num;
            if (DefendSkillsCooltime[i] < 0)
            {
                DefendSkillsCooltime[i] = 0;
            }
        }
        for (int i = 0; i < firstSkillsCooltime.Count; i++)
        {
            firstSkillsCooltime[i] -= _num;
            if (firstSkillsCooltime[i] < 0)
            {
                firstSkillsCooltime[i] = 0;
            }
        }

        for (int i = 0; i < TurnSkillsCooltime.Count; i++)
        {
            TurnSkillsCooltime[i] -= _num;
            if (TurnSkillsCooltime[i] < 0)
            {
                TurnSkillsCooltime[i] = 0;
            }
        }

    }
}
