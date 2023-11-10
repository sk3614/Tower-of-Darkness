using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill : ScriptableObject
{
    public string skillName;
    public string skillEName;
    public Sprite skillImage;

    public bool isUpgrade=false;
    public bool isUpgrade2 = false;
    public bool isUpgrade3 = false;
    public bool isCastSkill = false;
    public int castTurn;
    public bool isPublicSkill;
    public bool isKO;
    public bool isLimit;
    public int LimitNum;
    public SkillType skillType;
    public SpendType skillSpendType;
    public ActiveTime activeTime;
    public DamageType damageType;
    public SkillClass skillClass;
    public ChaseType chaseType;
    public CounterType counterType;
    public Special[] specials;

   
    public string effectName;
    public bool damageEffectOnme;
    public string[] OtherEffectsOnme;
    public string[] OtherEffectsOnEnemy;
    public string[] OtherEffectsOnEnemyTriple;
    public string[] OtherEffectsOnMiddle;
    public float otherEffectOnEnemyTime;

    public bool isEffectEnemy;
    public bool isEffectMiddle;
    public float skillEffectTime;

    public int pp;//소모값
    public int P;//우선도
    public int CP;//공용스킬일경우 소모값
    public int activePer;//발동확률
    public int waitTurn;//첫 사용 대기 턴
    public int coolTime;//재사용 대기 턴
    public int Hit;
    [TextArea]
    public string skillLongInfo;
    [TextArea]
    public string flavorText;
    [TextArea]
    public string skillE_LongInfo;
    [TextArea]
    public string flavorEText;

    public enum SpendType
    {
        AP,
        BP,
        ETC
    }

    public enum SkillType
    {
        Attack,//공격
        Defence,//방어
        Obstruction,//방해
        Assistance,//보조
        Counter,//카운터
        Chase,
        ETC,//기타
        NormalAttackBonusEffect,

    }
    public enum ActiveTime
    {
        Start,//시작
        Passive,//영구
        Standby,//선제
        Active,//행동
        Counter,//카운터
        Special,//특수
        BattleEnd,
        Turn,//턴
        Death,//죽음
        Defend,//방어
        Chase,
        End,
        Finale,
        NormalAttack,
        ClassPasive
            
    }
    public enum CounterType
    {
        None,
        TakeDamage,
        ActiveBuff,
    }
    public enum ChaseType
    {
        None,
        AfterAttack,
        AfterDefense,
        AttackSkillHit,
        ActiveCast,
    }

    public enum DamageType
    {
        Basic,
        True,
        ETC
    }
    public enum SkillClass
    {
        Normal,
        Rare
    }
    public enum Special
    {
        Cri,
        NoMiss,
        NoCri,
        Pierce,
        IgnoreDCD
    }
    public virtual void CastSkill(Character _user, Character _subject)
    {
        string log = "";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                log = _user.characterName + "는 "+_user.castSkill.skillName+ "을 시전중입니다. "+_user.remainCastTurn+ "턴 남음";
                break;
            case Options.Language.Eng:
                log = _user.characterName + " is casting "+ _user.castSkill.skillEName + "remain "+ _user.remainCastTurn +"turns";
                break;
            default:
                break;
        }

        Battle.S.AddBattleEffect(_user, _subject, null, Battle.HitType.Hit, log,-1,false,0,false,0,0,0.4f, "Cast");

    }
    public virtual void ActiveSkill(Character _user, Character _subject)
    {
        
    }

    public virtual void ActiveSkill(Character _user, Character _subject,bool noEffect)
    {

    }
    public virtual bool ActiveCheck(Character _user, Character _subject)
    {
        return true;
    }
    public bool HitCheck(Character user, Character subject, bool NoMissEffect=false)
    {
        string log="";

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                log = user.characterName + "의 " + this.skillName + " 빗나감!";
                break;
            case Options.Language.Eng:
                log = user.characterName + " Active " + this.skillEName + " MISS!";
                break;
            default:
                break;
        }
        if (user.Reveal>0)
        {
            return true;
        }
        int SPDValue = CalSPDValue(user, subject);
        if (subject.Invisible > 0)
        {
            if (NoMissEffect)
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
            }
            else
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
            }
            if (!Player.S.KoMiss && subject.tag == "Player")
            {
                Player.S.KoStackUp(1);
                Player.S.KoMiss = true;
            }
            if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
            {
                subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
            }

            return false;
        }
        int value = 0;
        int userHit = user.HIT + user.Buff_HIT;
        int subAVD = (int)(subject.AVD + subject.Buff_AVD);
        if (user.Daze > 0) userHit-=50;
        if (subject.Daze > 0) subAVD /= 2;
        int hitPer = (Hit+ userHit) -(subAVD)+SPDValue;

        if (BuffCheck(subject, Buff.BuffType.Miss) && BuffCheck(user, Buff.BuffType.CleanHit))
        {
            user.CountDownBuff(Buff.BuffType.CleanHit);
            subject.CountDownBuff(Buff.BuffType.Miss);
            if (hitPer <= 0)
            {

                if (NoMissEffect)
                {
                    Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
                }
                else
                {
                    Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
                }
                if (!Player.S.KoMiss && subject.tag == "Player")
                {
                    Player.S.KoStackUp(1);
                    Player.S.KoMiss = true;
                }
                if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
                {
                    subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                    subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
                }
                return false;
            }
            else
            {
                value = Random.Range(0, 101);
                if (hitPer < value)
                {
                    if (NoMissEffect)
                    {
                        Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
                    }
                    else
                    {
                        Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
                    }
                    if (!Player.S.KoMiss && subject.tag == "Player")
                    {
                        Player.S.KoStackUp(1);
                        Player.S.KoMiss = true;
                    }
                    if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
                    {
                        subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                        subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
                    }
                    return false;
                }
            }
        }
        else if (BuffCheck(subject, Buff.BuffType.Miss))
        {
            subject.CountDownBuff(Buff.BuffType.Miss);
            if (NoMissEffect)
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
            }
            else
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
            }
            if (!Player.S.KoMiss && subject.tag == "Player")
            {
                Player.S.KoStackUp(1);
                Player.S.KoMiss = true;
            }
            if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
            {
                subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
            }
            return false;
        }
        if (hitPer <= 0)
        {
            if (NoMissEffect)
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
            }
            else
            {
                Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
            }
            if (!Player.S.KoMiss && subject.tag == "Player")
            {
                Player.S.KoStackUp(1);
                Player.S.KoMiss = true;
            }
            if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
            {
                subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
            }
            return false;
        }
        else
        {
            value = Random.Range(0, 101);
            if (hitPer < value)
            {
                if (NoMissEffect)
                {
                    Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log);
                }
                else
                {
                    Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Miss, log);
                }
                if (!Player.S.KoMiss && subject.tag == "Player")
                {
                    Player.S.KoStackUp(1);
                    Player.S.KoMiss = true;
                }
                if (subject.buffList.Exists(x => x.buffName == "곡예 숙련"))
                {
                    subject.buffList.Find(x => x.buffName == "곡예 숙련").EndBuff(subject);
                    subject.buffList.RemoveAll(x => x.buffName == "곡예 숙련");
                }
                return false;
            }
        }

        return true;
    }
    public void TakeSheild(Character user,Character subject,int ShieldPer,int ShieldNum=0,bool _noEffect=false,bool isBasic=false)
    {
        string log="";
        int value= (user.DEF+user.Buff_DEF) * ShieldPer / 100*(user.ARC+user.Buff_ARC)/ 100;
        value += ShieldNum * (user.ARC + user.Buff_ARC) / 100;
        if (value<0)
        {
            value = 0;
        }
        if (user.Fear>0)
        {
            value = 0;
        }
        user.bp += value;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                log = user.characterName + "의 " + this.skillName + " 사용 +" + value + "BP";
                break;
            case Options.Language.Eng:
                log = user.characterName + " Active" + this.skillEName + "+"+ value+"BP";
                break;
            default:
                break;
        }

        if (isBasic)
        {
           // Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.CriHit, log, -1, _noEffect, 0, false, 0);
        }
        else
        {
            Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.CriHit, log, -1, _noEffect, 0, false, 0, value);
        }

    }

    public int Attack(Character user, Character subject,int _damagePer,int _defDamagePer=0,bool noEffect=false,int _bonusDamage=0,bool _noLog=false,float _time=0,bool ispierce=false)
    {
        Battle.S.ActiveChase(user, subject, Skill.ChaseType.AttackSkillHit, this);
        string log="";
        int value = 0;
        bool ActiveCri=false;
        int criValue=0;
        int SPDValue = CalSPDValue(user, subject);

        if (damageType != DamageType.True)
        {
            //저항값계산
            criValue = (int)(user.CRC + user.Buff_CRC) - (int)(subject.CRR + subject.Buff_CRR)+ SPDValue;
            if (criValue > 0)
            {
                value = Random.Range(0, 101);
                if (criValue > value)
                {
                    ActiveCri = true;
                }
            }
            if (BuffCheck(user, Buff.BuffType.Critical))
            {
                user.CountDownBuff(Buff.BuffType.Critical);
                ActiveCri = true;
            }
        }

        if(user.Misfortune>0)
        {
            ActiveCri = false;
        }

        if (user.Fortune>0)
        {
            ActiveCri = true;
        }
        if (subject.Doom>0)
        {
            ActiveCri = true;
        }

        if (CheckSpecial(Special.NoCri))
        {
            ActiveCri = false;
        }
        if (CheckSpecial(Special.Cri))
        {
            ActiveCri = true;
        }

        int dmg;
        int userAtk;
        int userDef=0;
        userAtk = (user.ATK + user.Buff_ATK);
        if (_defDamagePer>0)
        {
            userDef = (user.DEF + user.Buff_DEF);
            userDef = (userDef * _defDamagePer)/100;
        }

        dmg = ((userAtk+_bonusDamage) * _damagePer)/100+userDef;

        if (dmg < 0) dmg = 0;
        dmg += user.trueDMG;//고정데미지 추가

        int BonusDmg = 0;
        if (ArtifactManager.S.FictionReports.able &&( subject.monsterFace == Character.MonsterFace.Ain || subject.monsterFace == Character.MonsterFace.Spirit || subject.monsterFace == Character.MonsterFace.EvilSpirit))
        {
            BonusDmg += dmg * 15 / 100;
        }
        if (ArtifactManager.S.UndertakerLicense.able&&subject.monsterFace==Character.MonsterFace.Undead)
        {
            BonusDmg += dmg * 5 / 100;
        }
        if (ArtifactManager.S.GreenLantern.able && subject.monsterFace == Character.MonsterFace.Demon)
        {
            BonusDmg += dmg * 5 / 100;
        }
        if (ArtifactManager.S.TearstoneOftheFreak.able && subject.monsterFace == Character.MonsterFace.Creature)
        {
            BonusDmg += dmg * 10 / 100;
        }

        if (ArtifactManager.S.TorturerBelt.able && subject.buffList.Find(x=>x.bigBuffType==Buff.BigBuffType.DotDamage) && user.tag == "Player")
        {
            BonusDmg += dmg * 10 / 100;
        }
        if (ArtifactManager.S.RoyalFlag.able && subject.monsterFace == Character.MonsterFace.BackSider) 
        {
            BonusDmg += dmg * 10/ 100;
        }
        if (ArtifactManager.S.MoonBreaker.able && subject.tempHp>0 &&user.tag=="Player")
        {
            BonusDmg += dmg * 20 / 100;
        }
        if (ArtifactManager.S.EmeraldSword.able && subject.MonsterGrade>=4 && user.tag == "Player")
        {

            BonusDmg += dmg * 10 / 100;
        }
        if (user.skillList.Find(x=>x.skillName=="종교재판")&&subject.buffList.Find(x=>x.buffType==Buff.BuffType.SS))
        {

            BonusDmg += dmg * 10 / 100;
        }
        if (user.skillList.Find(x => x.skillName == "종교재판+") && subject.buffList.Find(x => x.buffType == Buff.BuffType.SS))
        {

            BonusDmg += dmg * 15 / 100;
        }
        if (user.skillList.Find(x => x.skillName == "종교재판++") && subject.buffList.Find(x => x.buffType == Buff.BuffType.SS))
        {

            BonusDmg += dmg * 20 / 100;
        }
        dmg += BonusDmg;




        

        
        if (ActiveCri)
        {
            dmg += dmg * (int)((user.CRD + user.Buff_CRD)-(subject.CDR+subject.Buff_CDR)) / 100;
        }
        //흡혈 계산
        if (user.VAM+user.Buff_Vam>0&&!isKO)
        {
            user.Heal((dmg * (user.VAM+user.Buff_Vam)) / 100);
            Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, "", -1, true, 0, false); 
        }

        //관통계산
        bool isPierce=false;

        int BLKvalue = (user.PIE + user.Buff_PIE) - (subject.BLK + subject.Buff_BLK);
        if (BLKvalue<0)
        {
            BLKvalue = 0;
        }
        if (dmg <= 0) dmg = 1;



        if (user.monsterClass==Character.MonsterClass.Magician||CheckSpecial(Special.Pierce)||Random.Range(0,100)<BLKvalue||ispierce)
        {
            isPierce = true;
        }
        if (subject.Wall>0&&user.monsterClass != Character.MonsterClass.Magician)
        {
            isPierce = false;
        }

        if (subject.skillList.Find(x => x.skillName == "보호 결계"))
        {
            isPierce = false;
        }

        //반사 처리
        int MIR_dmg=0;
        if (subject.MIR + subject.Buff_MIR > 0)
        {
            MIR_dmg = dmg * (subject.MIR + subject.Buff_MIR) / 100;
        }

        if (ArtifactManager.S.NONO.able && subject.tag == "Player")
        {
            dmg -= MIR_dmg;
        }
        if (subject.skillList.Find(x=>x.skillName=="기회주의자"))
        {
            dmg -= MIR_dmg;
        }
        if (isPierce)
        {

        }
        else if (subject.bp>=dmg)
        {
            subject.bp -= dmg;
            dmg = 0;
        }
        else
        {
            dmg -= subject.bp;
            subject.bp = 0;
        }

        int usericd = user.ICD + user.Buff_ICD;
        usericd = Mathf.Clamp(usericd, -100, 200);
        int subdcd = subject.DCD + subject.Buff_DCD;
        subdcd = Mathf.Clamp(subdcd, -100, 90);

        if (user.skillList.Find(x => x.skillName == "거상") || subject.skillList.Find(y => y.skillName == "거상"))
        {
            usericd = 0;
            subdcd = 0;
        }
        if (dmg>0)
        {
            if (CheckSpecial(Special.IgnoreDCD))
            {
                dmg += dmg * usericd / 100;
            }
            else
            {
                dmg += (dmg * (usericd - subdcd)) / 100;//보너스피해 계산

            }
        }


        if (dmg < 0) dmg = 0;


        if (dmg>0&&!isKO)
        {
            for (int i = 0; i < subject.skillList.Count; i++)
            {
                if (subject.skillList[i].skillName == "가고일 구조물")
                {
                    dmg = 1;
                }
            }
        }
        if (dmg > 0 &&(subject.characterName=="정오"|| subject.characterName == "Noon"))
        {
            for (int i = 0; i < subject.skillList.Count; i++)
            {
                if (subject.skillList[i].skillName == "가고일 구조물")
                {
                    dmg = 1;
                }
            }
        }
        if (subject.Endure > 0)//인듀어처리
        {
            Buff buff = subject.buffList.Find(x => x.buffType == Buff.BuffType.Endure);
            buff.EnduredamageNum += dmg;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    log = subject.characterName + "는 " + "피해 인내 상태입니다.";
                    break;
                case Options.Language.Eng:
                    log = subject.characterName + " is in a state of Endure";
                    break;
                default:
                    break;
            }
            Battle.S.AddBattleEffect(user, subject, null, Battle.HitType.Hit, log);
        }else
        {
            subject.Damaged(dmg, ActiveCri, skillName);//데미지 처리
        }



        if (ActiveCri)
        {
            if (!Player.S.KoCri&&user.tag=="Player")
            {
                Player.S.KoStackUp(1);
                Player.S.KoCri = true;
            }

            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    log = user.characterName + "의 " + this.skillName + " 회심! " + subject.characterName + "의 HP -" + dmg;
                    if (isPierce)
                    {
                        log = log.Replace("!", ", 관통! ");
                    }
                    break;
                case Options.Language.Eng:
                    log = user.characterName + " Active " + this.skillEName + " CRT! " + subject.characterName + "'s Hp -" + dmg;
                    if (isPierce)
                    {
                        log = log.Replace("!", ", PIE! ");
                    }
                    break;
                default:
                    break;
            }
            if (_noLog)
            {
                log = "";
            }
            Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.CriHit, log, dmg,noEffect,0,true,0,0,_time);
        }
        else
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    log = user.characterName + "의 " + this.skillName + " " + subject.characterName + "의 HP -" + dmg;
                    if (isPierce)
                    {
                        log = user.characterName + "의 " + this.skillName + " 관통! " + subject.characterName + "의 HP -" + dmg;
                    }
                    break;
                case Options.Language.Eng:
                    log = user.characterName + " Active " + this.skillEName + " " + subject.characterName + "'s Hp -" + dmg;
                    if (isPierce)
                    {
                        log = user.characterName + " Active " + this.skillEName + " PIE! " + subject.characterName + "'s Hp -" + dmg;
                    }
                    break;
                default:
                    break;
            }
            if (_noLog)
            {
                log = "";
            }
            Battle.S.AddBattleEffect(user, subject, this, Battle.HitType.Hit, log, dmg,noEffect,0,false,0,0,_time);
        }
        //반사데미지 처리
        if (MIR_dmg>0)
        {
            if (user.bp >= dmg)
            {
                user.bp -= MIR_dmg;
                dmg = 0;
            }
            else
            {
                MIR_dmg -= user.bp;
                user.bp = 0;
            }
            user.Damaged(MIR_dmg, false);
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    log = user.characterName + "는 " + MIR_dmg + "의 반사데미지를 받음"; 
                    break;
                case Options.Language.Eng:
                    log = user.characterName + " take " + MIR_dmg + " Reflect Damage";
                    break;
                default:
                    break;
            }
            Battle.S.AddBattleEffect(subject, user, this, Battle.HitType.Hit, log, MIR_dmg, true);


        }

        //패시브처리
        if (user.skillList.Find(x => x.skillName == "할버드 장인"))
        {
            user.skillList.Find(x => x.skillName == "할버드 장인").ActiveSkill(user, subject);
        }


        return dmg;
    }
   public bool BuffCheck(Character _character,Buff.BigBuffType _bigBuffType)
    {
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].bigBuffType==_bigBuffType)
            {
                return true;
            }
        }
        return false;
    }
    public bool BuffCheck(Character _character, Buff.BuffType _buffType)
    {
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffType == _buffType)
            {
                return true;
            }
        }
        return false;
    }
    public void Purify(Character _character)
    {
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if ((_character.buffList[i].bigBuffType==Buff.BigBuffType.DeBuff|| _character.buffList[i].bigBuffType == Buff.BigBuffType.DotDamage) && !_character.buffList[i].isPermanent)
            {
                _character.buffList[i].EndBuff(_character);
            }
        }

        for (int i = _character.buffList.Count-1; i >=0 ; i--)
        {
            if ((_character.buffList[i].bigBuffType == Buff.BigBuffType.DeBuff || _character.buffList[i].bigBuffType == Buff.BigBuffType.DotDamage) && !_character.buffList[i].isPermanent)
            {
                _character.buffList.Remove(_character.buffList[i]);
            }
        }
    }
    public void Cure(Character _character,string _name)
    {
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == _name)
            {
                _character.buffList[i].EndBuff(_character);
            }
        }

        for (int i = _character.buffList.Count - 1; i >= 0; i--)
        {
            if (_character.buffList[i].buffName == _name)
            {
                _character.buffList.Remove(_character.buffList[i]);
            }
        }
    }
    public void Dispel(Character _character)
    {

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].bigBuffType == Buff.BigBuffType.Buff&& !_character.buffList[i].isPermanent)
            {
                _character.buffList[i].EndBuff(_character);
            }
        }

        for (int i = _character.buffList.Count - 1; i >= 0; i--)
        {
            if (_character.buffList[i].bigBuffType == Buff.BigBuffType.Buff&& !_character.buffList[i].isPermanent)
            {
                _character.buffList.Remove(_character.buffList[i]);
            }
        }
    }
    public void AddCurse(Character _character, int _num)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Curse)
        {
            return;
        }
        Curse curse = CreateInstance<Curse>();
        curse.buffName = "저주";
        curse.bigBuffType = Buff.BigBuffType.DeBuff;
        curse.buffImageType = Buff.BuffImageType.StatusDown;
        curse.buffType = Buff.BuffType.SC;
        curse.remainType = Buff.RemainType.Turn;
        curse.buffImageType = Buff.BuffImageType.Curse;
        _character.AddBuff(curse, _num);
    }
    public void AddBleed(Character _character, int per)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Bleed)
        {
            return;
        }

        int _dmg =per;
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName=="출혈")
            {
                _character.buffList[i].DotDamage += _dmg;
                return;
            }
        }
        
        Bleed bleed = CreateInstance<Bleed>();
        bleed.buffName = "출혈";
        bleed.bigBuffType = Buff.BigBuffType.DotDamage;
        bleed.buffType = Buff.BuffType.SS;
        bleed.remainType = Buff.RemainType.Turn;
        bleed.buffImageType = Buff.BuffImageType.Bleed;
        bleed.DotDamage = _dmg;
        _character.AddBuff(bleed, 100);
    }
    public void AddBleed(Character _character, int per,int turn)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Bleed)
        {
            return;
        }
        if (ArtifactManager.S.FrozenFlowerCrystal.able && _character.tag == "Player")
        {
            return;
        }

        int _dmg = per;
        Bleed bleed = CreateInstance<Bleed>();
        bleed.buffName = "턴출혈";
        bleed.bigBuffType = Buff.BigBuffType.DotDamage;
        bleed.buffType = Buff.BuffType.SS;
        bleed.remainType = Buff.RemainType.Turn;
        bleed.buffImageType = Buff.BuffImageType.Bleed;
        bleed.DotDamage = _dmg;
        _character.AddBuff(bleed, turn);
    }
    public void AddErosion(Character _character, int dmg,int Turn)
    {
        if (ArtifactManager.S.CuriousCube.able && _character.tag == "Player")
        {
            return;
        }

        int value = Random.Range(0, 100);
        if (value < _character.RG_Erosion)
        {
            return;
        }
        if (ArtifactManager.S.FrozenFlowerCrystal.able && _character.tag == "Player")
        {
            return;
        }

        int _dmg = dmg;

        Erosion erosion = CreateInstance<Erosion>();
        erosion.buffName = "침식";
        erosion.bigBuffType = Buff.BigBuffType.DotDamage;
        erosion.buffType = Buff.BuffType.SS;
        erosion.remainType = Buff.RemainType.Turn;
        erosion.buffImageType = Buff.BuffImageType.Erosion;
        erosion.DotDamage = _dmg;
        _character.AddBuff(erosion, Turn);
    }

    public void AddBurn(Character _character, int dmg)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Burn)
        {
            return;
        }
        if (ArtifactManager.S.FrozenFlowerCrystal.able && _character.tag == "Player")
        {
            return;
        }
        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == "화상")
            {
                _character.buffList[i].DotDamage += dmg;
                return;
            }
        }

        Burn burn = CreateInstance<Burn>();
        burn.buffName = "화상";
        burn.bigBuffType = Buff.BigBuffType.DotDamage;
        burn.buffType = Buff.BuffType.SS;
        burn.remainType = Buff.RemainType.Turn;
        burn.DotDamage = dmg;
        burn.buffImageType = Buff.BuffImageType.Burn;
        _character.AddBuff(burn, 100);
    }
    public void AddBurn(Character _character, int dmg,int turn)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Burn)
        {
            return;
        }
        if (ArtifactManager.S.FrozenFlowerCrystal.able && _character.tag == "Player")
        {
            return;
        }

        Burn burn = CreateInstance<Burn>();
        burn.buffName = "턴화상";
        burn.bigBuffType = Buff.BigBuffType.DotDamage;
        burn.buffType = Buff.BuffType.SS;
        burn.remainType = Buff.RemainType.Turn;
        burn.DotDamage = dmg;
        burn.buffImageType = Buff.BuffImageType.Burn;
        _character.AddBuff(burn, turn);
    }
    public void AddPoison(Character _character, int percent)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Poison)
        {
            return;
        }

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == "중독")
            {
                _character.buffList[i].DotDamage += percent;
                return;
            }
        }
        Poison poison = CreateInstance<Poison>();
        poison.buffName = "중독";
        poison.bigBuffType = Buff.BigBuffType.DotDamage;
        poison.buffType = Buff.BuffType.SS;
        poison.remainType = Buff.RemainType.Turn;
        poison.DotDamage = percent;
        poison.buffImageType = Buff.BuffImageType.Poison;
        _character.AddBuff(poison, 10000);
    }
    public void AddPoison(Character _character, int percent ,int turn)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Poison)
        {
            return;
        }
        if (_character.tag == "Player" && ArtifactManager.S.Coral.able)//산호 아티팩트
        {
            return;
        }

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == "턴중독")
            {
                _character.buffList[i].DotDamage += percent;
                _character.buffList[i].RemainTurn += turn;
                return;
            }
        }
        Poison poison = CreateInstance<Poison>();
        poison.buffName = "턴중독";
        poison.bigBuffType = Buff.BigBuffType.DotDamage;
        poison.buffType = Buff.BuffType.SS;
        poison.remainType = Buff.RemainType.Turn;
        poison.DotDamage = percent;
        poison.buffImageType = Buff.BuffImageType.Poison;
        _character.AddBuff(poison, turn);
    }
    public void AddFear(Character _user, Character _subject,int Turn,int _hitPer=100)
    {
        int value2 = Random.Range(0, 100);
        if (value2 < _subject.RG_Fear)
        {
            return;
        }

        int value = Random.Range(0, 100);
        if (value<_hitPer)
        {
            Fear fear = CreateInstance<Fear>();
            fear.buffName = "공포";
            fear.bigBuffType = Buff.BigBuffType.DeBuff;
            fear.buffType = Buff.BuffType.SC;
            fear.remainType = Buff.RemainType.Turn;
            fear.buffImageType = Buff.BuffImageType.Fear;
            _subject.AddBuff(fear, Turn);
            string log = "";
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    log = _subject.characterName + "에게 공포 효과";
                    break;
                case Options.Language.Eng:
                    log ="Fear effect on "+ _subject.characterName;
                    break;
                default:
                    break;
            }
            Battle.S.AddBattleEffect(_user, _subject, this, Battle.HitType.Hit, log,-1,true);
        }


    }
   

     public void AddStun(Character _character)
    {
        int value = Random.Range(0, 100);
        if (value<_character.BuffRG_CC+_character.RG_stun)
        {
            return;
        }

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName=="스턴")
            {
                return;
            }
        }
        Stun stun = CreateInstance<Stun>();
        stun.buffName = "스턴";
        stun.bigBuffType = Buff.BigBuffType.DeBuff;
        stun.buffType = Buff.BuffType.CC;
        stun.remainType = Buff.RemainType.Turn;
        stun.buffImageType = Buff.BuffImageType.Stun;
        
        _character.AddBuff(stun, 1);
    }
    public void AddSleep(Character _character, int _turn)
    {
        int value = Random.Range(0, 100);
        if (value < _character.BuffRG_CC + _character.RG_Sleep)
        {
            return;
        }

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == "수면")
            {
                return;
            }
        }
        Sleep sleep = CreateInstance<Sleep>();
        sleep.buffName = "수면";
        sleep.bigBuffType = Buff.BigBuffType.DeBuff;
        sleep.buffType = Buff.BuffType.CC;
        sleep.remainType = Buff.RemainType.Turn;
        sleep.buffImageType = Buff.BuffImageType.Sleep;
        _character.AddBuff(sleep, _turn);
    }
    public void AddParalyze(Character _character, int _turn)
    {
        int value = Random.Range(0, 100);
        if (value < _character.BuffRG_CC + _character.RG_Paralyze)
        {
            return;
        }

        for (int i = 0; i < _character.buffList.Count; i++)
        {
            if (_character.buffList[i].buffName == "마비")
            {
                return;
            }
        }
        Paralyze paralyze = CreateInstance<Paralyze>();
        paralyze.buffName = "마비";
        paralyze.bigBuffType = Buff.BigBuffType.DeBuff;
        paralyze.buffType = Buff.BuffType.CC;
        paralyze.remainType = Buff.RemainType.Turn;
        paralyze.buffImageType = Buff.BuffImageType.Stun;
        _character.AddBuff(paralyze, _turn);
    }
    public void AddInFect(Character _character, int _num)
    {
        int value = Random.Range(0, 100);
        if (value <_character.RG_Infect)
        {
            return;
        }
        InFect infect = CreateInstance<InFect>();
        infect.buffName = "감염";
        infect.bigBuffType = Buff.BigBuffType.DeBuff;
        infect.buffType = Buff.BuffType.SC;
        infect.remainType = Buff.RemainType.Turn;
        infect.buffImageType = Buff.BuffImageType.Infect;
        _character.AddBuff(infect, _num);
    }
    public void AddDaze(Character _character, int _num)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Daze)
        {
            return;
        }
        Daze daze = CreateInstance<Daze>();
        daze.buffName = "눈부심";
        daze.bigBuffType = Buff.BigBuffType.DeBuff;
        daze.buffType = Buff.BuffType.SC;
        daze.remainType = Buff.RemainType.Turn;
        daze.buffImageType = Buff.BuffImageType.Daze;
        _character.AddBuff(daze, _num);
    }
    public void AddDoom(Character _character, int _num)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Doom)
        {
            return;
        }
        Doom doom = CreateInstance<Doom>();
        doom.buffName = "파멸";
        doom.bigBuffType = Buff.BigBuffType.DeBuff;
        doom.buffType = Buff.BuffType.SC;
        doom.remainType = Buff.RemainType.Turn;
        doom.buffImageType = Buff.BuffImageType.Doom;
        _character.AddBuff(doom, _num);
    }
    public void AddMisfortune(Character _character, int _num)
    {
        int value = Random.Range(0, 100);
        if (value < _character.RG_Misfortune)
        {
            return;
        }
        Misfortune misfortune = CreateInstance<Misfortune>();
        misfortune.buffName = "불운";
        misfortune.bigBuffType = Buff.BigBuffType.DeBuff;
        misfortune.buffType = Buff.BuffType.SC;
        misfortune.remainType = Buff.RemainType.Turn;
        misfortune.buffImageType = Buff.BuffImageType.MisFortune;
        _character.AddBuff(misfortune, _num);
    }
    public void AddBless(Character _character, int _num,bool _isPermanent=false)
    {
        Bless bless = CreateInstance<Bless>();
        bless.buffName = "축복";
        bless.bigBuffType = Buff.BigBuffType.Buff;
        bless.buffType = Buff.BuffType.BB;
        bless.remainType = Buff.RemainType.Turn;
        bless.buffImageType = Buff.BuffImageType.Bless;
        bless.CastThisTurn = true;
        bless.isPermanent = _isPermanent;
        _character.AddBuff(bless, _num);
    }
    public void AddWall(Character _character, int _num)
    {
        Wall wall = CreateInstance<Wall>();
        wall.buffName = "철벽";
        wall.bigBuffType = Buff.BigBuffType.Buff;
        wall.buffType = Buff.BuffType.BB;
        wall.remainType = Buff.RemainType.Turn;
        wall.buffImageType = Buff.BuffImageType.Wall;
        wall.CastThisTurn = true;
        _character.AddBuff(wall, _num);
        string log = "";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                log = _character.characterName + "에게 철벽 효과 " + _num + "턴";
                break;
            case Options.Language.Eng:
                log = _character.characterName + "Gain Wall Effect "+ _num + "Turn";
                break;
            default:
                break;
        }

        Battle.S.AddBattleEffect(Battle.S.player, Battle.S.monster, null, Battle.HitType.Hit,log,-1,true);
    }
    public void AddInvisible(Character _character, int _num)
    {
        Invisible Invisible = CreateInstance<Invisible>();
        Invisible.buffName = "은신";
        Invisible.bigBuffType = Buff.BigBuffType.Buff;
        Invisible.buffType = Buff.BuffType.BB;
        Invisible.remainType = Buff.RemainType.Turn;
        Invisible.buffImageType = Buff.BuffImageType.Invisible;
        Invisible.CastThisTurn = true;
        _character.AddBuff(Invisible, _num);
    }
    public void AddHaste(Character _character, int _num)
    {
        Haste Invisible = CreateInstance<Haste>();
        Invisible.buffName = "신속";
        Invisible.bigBuffType = Buff.BigBuffType.Buff;
        Invisible.buffType = Buff.BuffType.BB;
        Invisible.remainType = Buff.RemainType.Turn;
        Invisible.buffImageType = Buff.BuffImageType.Haste;
        Invisible.CastThisTurn = true;
        _character.AddBuff(Invisible, _num);
    }
    public void AddFortune(Character _character, int _num)
    {
        Fortune fortune = CreateInstance<Fortune>();
        fortune.buffName = "행운";
        fortune.bigBuffType = Buff.BigBuffType.Buff;
        fortune.buffType = Buff.BuffType.BB;
        fortune.remainType = Buff.RemainType.Turn;
        fortune.buffImageType = Buff.BuffImageType.Fortune;
        fortune.CastThisTurn = true;
        _character.AddBuff(fortune, _num);
    }
    public void AddProtect(Character _character, int _num)
    {
        ProtectBuff protect = CreateInstance<ProtectBuff>();
        protect.buffName = "보호";
        protect.bigBuffType = Buff.BigBuffType.Buff;
        protect.buffType = Buff.BuffType.BB;
        protect.remainType = Buff.RemainType.Turn;
        protect.buffImageType = Buff.BuffImageType.Protection;
        protect.CastThisTurn = true;
        _character.AddBuff(protect, _num);
    }
    public void AddReveal(Character _character, int _num,bool _isPermanent=false)
    {
        Reveal reveal = CreateInstance<Reveal>();
        reveal.buffName = "간파";
        reveal.bigBuffType = Buff.BigBuffType.Buff;
        reveal.buffType = Buff.BuffType.BB;
        reveal.remainType = Buff.RemainType.Turn;
        reveal.buffImageType = Buff.BuffImageType.Reveal;
        reveal.CastThisTurn = true;
        reveal.isPermanent = _isPermanent;
        _character.AddBuff(reveal, _num);
    }
    public void AddRegain(Character _character, int _num,int _turn)
    {

        Regen regen = CreateInstance<Regen>();
        regen.buffName = "재생";
        regen.bigBuffType = Buff.BigBuffType.Buff;
        regen.buffType = Buff.BuffType.SC;
        regen.remainType = Buff.RemainType.Turn;
        regen.buffImageType = Buff.BuffImageType.Regen;
        regen.CastThisTurn = true;
        regen.value = _num;
        _character.AddBuff(regen, _turn);

    }
    public void AddInvincible(Character _character, int _turn)
    {

        Invincible invincible = CreateInstance<Invincible>();
        invincible.buffName = "무적";
        invincible.bigBuffType = Buff.BigBuffType.Buff;
        invincible.buffType = Buff.BuffType.SC;
        invincible.remainType = Buff.RemainType.Turn;
        invincible.buffImageType = Buff.BuffImageType.Invincible;
        invincible.CastThisTurn = true;
        invincible.isPermanent = true;
        Purify(_character);
        _character.AddBuff(invincible, _turn);

    }

    public bool CheckSpecial(Special special)
    {
        for (int i = 0; i < specials.Length; i++)
        {
            if (specials[i]==special)
            {
                return true;
            }
        }
        return false;
    }

    public string GetSkillTypeString()
    {
        switch (activeTime)//MarketPlace에서도 사용중임
        {
            case ActiveTime.Start:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "전투시작";
                    case Options.Language.Eng:
                        return "Start";
                    default:
                        break;
                }
                break;
            case ActiveTime.Passive:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "영구";
                    case Options.Language.Eng:
                        return "Passive";
                    default:
                        break;
                }
                break;
            case ActiveTime.Standby:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "준비";
                    case Options.Language.Eng:
                        return "Standby";
                    default:
                        break;
                }
                break;
            case ActiveTime.Active:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "행동";
                    case Options.Language.Eng:
                        return "Active";
                    default:
                        break;
                }
                break;
            case ActiveTime.Counter:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "반격";
                    case Options.Language.Eng:
                        return "Counter";
                    default:
                        break;
                }
                break;
            case ActiveTime.Special:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "특수";
                    case Options.Language.Eng:
                        return "Special";
                    default:
                        break;
                }
                break;
            case ActiveTime.BattleEnd:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "전투종료";
                    case Options.Language.Eng:
                        return "BattleEnd";
                    default:
                        break;
                }
                break;
            case ActiveTime.Turn:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "턴";
                    case Options.Language.Eng:
                        return "Turn";
                    default:
                        break;
                }
                break;
            case ActiveTime.Death:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "죽음";
                    case Options.Language.Eng:
                        return "Death";
                    default:
                        break;
                }
                break;
            case ActiveTime.Defend:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "방어";
                    case Options.Language.Eng:
                        return "Defend";
                    default:
                        break;
                }
                break;
            case ActiveTime.Chase:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "연계";
                    case Options.Language.Eng:
                        return "Chase";
                    default:
                        break;
                }
                break;
            case ActiveTime.End:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "종료";
                    case Options.Language.Eng:
                        return "End";
                    default:
                        break;
                }
                break;
            case ActiveTime.Finale:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "종료";
                    case Options.Language.Eng:
                        return "Finale";
                    default:
                        break;
                }
                break;
            case ActiveTime.NormalAttack:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "평타 강화";
                    case Options.Language.Eng:
                        return "Normal Attack";
                    default:
                        break;
                }
                break;
            case ActiveTime.ClassPasive:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "직업 패시브";
                    case Options.Language.Eng:
                        return "Class Pasive";
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return "";
    }
    public int CalPowValue(Character _user,Character _subject, int _multiple=1, bool isSS=false)
    {



        int SPDValue = CalSPDValue(_user, _subject);
        int powvalue = _user.POW + _user.Buff_POW - _subject.POW - _subject.Buff_POW+SPDValue;
        //if (isSS && _user.tag == "Player" && ArtifactManager.S.RingOfSpirit.able)
        //{
        //    powvalue += 6;
        //}
        if (powvalue<0)
        {
            return 0;
        }
        powvalue *= _multiple;
        return powvalue;
    }
    public int CalPowValue(Character _user, Character _subject, float _multiple = 1.0f, bool isSS = false)
    {

        int SPDValue = CalSPDValue(_user, _subject);

        float powvalue = _user.POW + _user.Buff_POW - _subject.POW - _subject.Buff_POW + SPDValue;
        //if (isSS && _user.tag == "Player" && ArtifactManager.S.RingOfSpirit.able)
        //{
        //    powvalue += 6;
        //}
        if (powvalue < 0)
        {
            return 0;
        }
        powvalue *= _multiple;
        return (int)powvalue;
    }
    public void AddTempHP(Character character, int _num)
    {
        character.tempHp += _num;
    }

    public int CalSPDValue(Character user, Character subject)
    {
        int SPDValue= (user.SPD + user.Buff_SPD) - (subject.SPD + subject.Buff_SPD);

        if (Player.S.ClassPasiveSkills.Count>0)
        {
            if (user.tag == "Player" && Player.S.ClassPasiveSkills[0].skillName == "긴 사정거리+")
            {
                SPDValue = Mathf.Clamp(SPDValue, 0, 100);
            }
            if (user.tag == "Player" && Player.S.ClassPasiveSkills[0].skillName == "긴 사정거리++")
            {
                SPDValue = (user.SPD + user.Buff_SPD);
            }
            if (user.tag == "Player" && Player.S.ClassPasiveSkills[0].skillName == "길잡이")
            {
                if (SPDValue>0)
                {
                    SPDValue += SPDValue / 2;
                }
                else
                {
                    SPDValue +=Mathf.Abs(SPDValue)/ 2;
                }
            }
            if (user.tag == "Player" && Player.S.ClassPasiveSkills[0].skillName == "길잡이+")
            {
                if (SPDValue > 0)
                {
                    SPDValue += SPDValue / 2;
                }
                else
                {
                    SPDValue += Mathf.Abs(SPDValue) / 2;
                }
            }
            if (user.tag == "Player" && Player.S.ClassPasiveSkills[0].skillName == "길잡이++")
            {
                if (SPDValue > 0)
                {
                    SPDValue *=2;
                }
                else
                {
                    SPDValue =0;
                }
            }
        }

        return SPDValue;
    }
}
