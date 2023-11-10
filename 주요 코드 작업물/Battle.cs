using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class BattleEffect
{
   //public Character actor;
    //public Character subject;

    public bool isPlayer=false;
    public int p_HP;
    public int p_tempHP;
    public int p_DP;
    public short p_ATK;
    public short p_DEF;
    public List<Buff> p_buff=new List<Buff>();
    public int koStack;


    public string BattleLog;
    public int m_HP;
    public int m_tempHP;
    public int m_DP;
    public short m_ATK;
    public short m_DEF;
    public List<Buff> m_buff = new List<Buff>();
    public int mSwordStack;

    public short turn;
    public bool isCri;
    public bool isAttack;
    public List<string> effectNames=new List<string>();
    public List<string> effectOnme = new List<string>();
    public List<string> effectOnEnemy = new List<string>();
    public List<string> effectOnEnemyTriple = new List<string>();
    public List<string> effectOnMiddle = new List<string>();

    public float effectOnEnemyTime;
    public float effectTime;
    public short damageNum;
    public int textEventNum = 0;
    public bool damageEffectOnme=false;

    public int shieldNum;
    public int healNum;

}

public class Battle : MonoBehaviour
{
    public static Battle S;

    public Character player;
    public Image playerImage;
    private int preKostack;

    public Character monster;
    public GameObject monsterEffect;
    public MonsterData nowMontertData;

    private Character F_Character;
    private Character S_Character;

    public List<BattleEffect> battleEffects = new List<BattleEffect>();
    public Coroutine temp;

    public int turn;
    public List<Dictionary<string, object>> monsterDatas;
    //UI
    public Text TurnUI;
    public Text MasterSwordStackUI;
    public GameObject battleUIBase;
    public TowerCell towercellLocation;

    public Image towerImage;
    public Sprite[] towerImageSource;
    public List<Sprite[]> towerAniSources = new List<Sprite[]>();
    public Sprite[] tower0Ani;
    public Sprite[] tower1Ani;
    public Sprite[] tower2Ani;
    public Sprite[] tower3Ani;
    public Sprite[] tower7Ani;
    public float aniTime;
    public int aniNum;

    public GameObject gameOverUI;
    //playerUI
    [SerializeField]
    private Text T_playerATK = null;
    [SerializeField]
    private Text T_playerDEF = null;
    [SerializeField]
    private Text T_playerHp = null;
    [SerializeField]
    private Text T_PlayerKoStack = null;

    public Text T_playerName;
    [SerializeField]
    private Text T_playerDp = null;
    [SerializeField]
    private BuffSlot player_BuffSlot = null;

    public Transform T_playerEffect;
    public Transform T_player2Effect;
    public Transform T_player3Effect;

    //monsterUI
    [SerializeField]
    private Text T_monsterHp = null;
    [SerializeField]
    private Text T_monsterDp = null;
    [SerializeField]
    private Text T_monsterATK = null;
    [SerializeField]
    private Text T_monsterDEF = null;
    [SerializeField]
    private BuffSlot monster_BuffSlot = null;
    public Text T_monsterName;

    public Transform T_monsterEffect;
    public Transform T_monster2Effect;
    public Transform T_monster3Effect;

    public Transform T_middleEffect;
    //effect
    public GameObject P_Effect;

    //BattleResultUI
    public bool isFlee;
    public int rewardGold;
    public int rewardExp;
    public GameObject resultUIbase;
    public Text monstername;
    public Text gold;
    public Text exp;

    public Skill KO;
    public int curEffectNum;

    public enum HitType
    {
        Hit,
        CriHit,
        Miss,
        Ward,
        Block
    }
    private void Awake()
    {
        if (S == null)
        {
            S = this;
            monsterDatas = CSVReader.Read("MonsterData");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        towerAniSources.Add(tower0Ani);
        towerAniSources.Add(tower1Ani);
        towerAniSources.Add(tower2Ani);
        towerAniSources.Add(tower3Ani);
        towerAniSources.Add(tower2Ani);
        towerAniSources.Add(tower2Ani);
        towerAniSources.Add(tower2Ani);
        towerAniSources.Add(tower2Ani);
    }
    private void Update()
    {
        CloseBattleUI();

        if (battleUIBase.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = 2;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            Time.timeScale = 1;
        }

        if (battleUIBase.activeInHierarchy)
        {
            aniTime += 1 * Time.deltaTime;
            if (aniTime > 0.5f)
            {
                aniTime = 0;
                if (aniNum < towerAniSources[TowerMap.S.curTowerNum].Length - 1)
                {
                    aniNum += 1;
                }
                else
                {
                    aniNum = 0;
                }
                towerImage.sprite = towerAniSources[TowerMap.S.curTowerNum][aniNum];
            }

            if (Input.GetKeyUp(KeyCode.Q) && !resultUIbase.activeInHierarchy)
            {
                if (Inventory.S.SearchItemCount("연막탄") > 0)
                {
                    isFlee = true;
                    EscapeBattle();
                    AddItem.S.SearchItem("연막탄", -1);
                    Player.S.useSmokebomb += 1;
                }

            }
        }


    }
    public void EternityPhase()//세팅페이즈
    {
        SetArtifact();

        SetNormalAttackBonusEffect(player);
        SetNormalAttackBonusEffect(monster);

        List<Skill> pSkills = PickSkills(player, Skill.ActiveTime.Passive);
        pSkills.AddRange(PickSkills(player, Skill.ActiveTime.ClassPasive));
        List<Skill> mSkills = PickSkills(monster, Skill.ActiveTime.Passive);
        for (int i = 0; i < pSkills.Count; i++)
        {
            int value = Random.Range(0, 100);
            if (pSkills[i].activePer > value)
            {
                pSkills[i].ActiveSkill(player, monster);
                BattleEndCheck();
            }

        }
        for (int i = 0; i < mSkills.Count; i++)
        {
            int value = Random.Range(0, 100);
            if (mSkills[i].activePer > value)
            {
                mSkills[i].ActiveSkill(monster, player);
                BattleEndCheck();
            }
        }
        AddBattleEffect(player, monster, null, HitType.Hit, turn + "Turn", -1, true);
        StartPhase();
    }
    public void StartPhase()//스타트 페이즈(시작 스킬 사용)
    {
        List<Skill> pSkills = PickSkills(player, Skill.ActiveTime.Start);
        List<Skill> mSkills = PickSkills(monster, Skill.ActiveTime.Start);
        for (int i = 0; i < pSkills.Count; i++)
        {
            int value = Random.Range(0, 101);
            if (pSkills[i].activePer > value)
            {
                pSkills[i].ActiveSkill(player, monster);
                BattleEndCheck();
            }

        }
        for (int i = 0; i < mSkills.Count; i++)
        {
            int value = Random.Range(0, 101);
            if (mSkills[i].activePer > value)
            {
                mSkills[i].ActiveSkill(monster, player);
                BattleEndCheck();
            }
        }
        player.bp = player.DEF + player.Buff_DEF;
        monster.bp = monster.DEF + monster.Buff_DEF + (int)monsterDatas[nowMontertData.MonsterID - 1]["BP"]; ;
        SetTurn();
    }
    public void StandbyPhase(Character actor, Character subject)//스탠바이 페이즈
    {
        if (actor.REG + actor.Buff_REG > 0)
        {
            actor.Heal(actor.REG + actor.Buff_REG);
            int _heal = (actor.REG + actor.Buff_REG) * (actor.HEL + actor.Buff_HEL) / 100;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    AddBattleEffect(actor, actor, null, HitType.Hit, actor.characterName + "의 HP " + _heal + " 회복");
                    break;
                case Options.Language.Eng:
                    AddBattleEffect(actor, actor, null, HitType.Hit, actor.characterName + " Healed " + _heal + "HP");
                    break;
                default:
                    break;
            }

        }
        if (actor.skillList.Find(x => x.skillName == "보호 결계"))
        {
            actor.bp += (actor.bpReset + actor.Buff_bpReset) * actor.ARC / 100;
            actor.bp+=50 * actor.ARC / 100;
        }
        else if (actor.skillList.Find(x => x.skillName == "철벽 자세"))
        {
            actor.bp = actor.bp / 2;
            actor.bp += (actor.bpReset + actor.Buff_bpReset) * actor.ARC / 100;
        }
        else 
        {
            actor.bp = (actor.bpReset + actor.Buff_bpReset) * actor.ARC / 100;
        }
        if (actor.LeapOn)
        {
            SkillDic.S.C_EmpireSkills2.Find(x => x.skillName == "도약강타").ActiveSkill(actor, subject);
        }


        CheckDotDamage(actor, subject);
        actor.DotTurnDown();
        if (BattleEndCheck())
        {
            temp = StartCoroutine(PlayBattle(battleEffects));
            ResetBuffStats();
            return;
        }
        StanbyArtiEffect(actor);


        List<Skill> pSkills = PickSkills(actor, Skill.ActiveTime.Standby);

        int value = 0;//확률체크용변수


        for (int i = 0; i < pSkills.Count; i++)
        {
            if (pSkills[i].waitTurn > turn) continue;
            if (pSkills[i].isLimit && actor.firstSkillLimit[i] == 0) continue;
            if (!pSkills[i].ActiveCheck(actor, subject)) continue;
            if (actor.firstSkillsCooltime[i] > 0) continue;

            value = Random.Range(0, 100);
            if (pSkills[i].activePer > value)//시전확률체크
            {
                pSkills[i].ActiveSkill(actor, subject);
                BattleEndCheck();
                actor.firstSkillsCooltime[i] = pSkills[i].coolTime;
                if (pSkills[i].isLimit) actor.firstSkillLimit[i] -= 1;
                if (actor.skillList.Find(x => x.skillName == "집중") && pSkills[i].skillType == Skill.SkillType.Attack)
                {
                    actor.firstSkillsCooltime[i] -= 2;
                    if (actor.firstSkillsCooltime[i] < 0) actor.firstSkillsCooltime[i] = 0;
                }
                ActivePhase(actor, subject);
                return;
            }
        }
        ActivePhase(actor, subject);
    }
    public void ActivePhase(Character actor, Character subject)
    {
        CheckGroggy(actor, subject);
        if (actor.Haste>0)
        {
            actor.battleAR = 0;
        }


        if (actor.battleAR > actor.battleAP)
        {
            actor.battleAR -= actor.battleAP;
            actor.battleAP = 0;
        }
        else
        {
            actor.battleAP -= actor.battleAR;
            actor.battleAR = 0;
        }

        while (actor.battleAP > 0 && actor.castSkill != null)
        {
            actor.remainCastTurn -= 1;
            if (actor.remainCastTurn <= 0)
            {
                actor.castSkill.ActiveSkill(actor, subject);
                actor.castSkill = null;
            }
            else
            {
                actor.castSkill.CastSkill(actor, subject);
            }
            actor.battleAP -= 1;
        }
        if (actor.battleAP <= 0)
        {
            actor.battleAP = 0;
            DefendPhase(actor, subject);
            return;
            
        }


        List<Skill> pSkills = PickSkills(actor, Skill.ActiveTime.Active,Skill.SpendType.AP);
        int value = 0;//확률체크용변수
        for (int i = 0; i < pSkills.Count; i++)
        {
            if (pSkills[i].isLimit&&actor.ActiveSkillsLimit[i] == 0) continue; //제한스킬 체크
            if (!pSkills[i].ActiveCheck(actor, subject)) continue;// 조건 체크
            if (pSkills[i].waitTurn > turn) continue;//시작쿨타임 체크
            if (actor.ActiveSkillsCooltime[i] > 0) continue;//쿨타임체크
            value = Random.Range(0, 100);
            if (pSkills[i].activePer > value)//시전확률체크
            {
                if (pSkills[i].isCastSkill)
                {
                    actor.castSkill = pSkills[i];
                    actor.remainCastTurn = pSkills[i].castTurn;
                    pSkills[i].CastSkill(actor, subject);
                    actor.ActiveSkillsCooltime[i] = pSkills[i].coolTime;
                    ActiveChase(actor, subject, Skill.ChaseType.ActiveCast, pSkills[i]);
                }
                else
                {
                    pSkills[i].ActiveSkill(actor, subject);
                    actor.ActiveSkillsCooltime[i] = pSkills[i].coolTime;
                    if (pSkills[i].isLimit) actor.ActiveSkillsLimit[i] -= 1;
                    if (actor.skillList.Find(x => x.skillName == "집중") && pSkills[i].skillType == Skill.SkillType.Attack)
                    {
                        actor.ActiveSkillsCooltime[i] = Mathf.Clamp(actor.ActiveSkillsCooltime[i], 0, pSkills[i].coolTime - 2);
                    }
                    if (pSkills[i].skillType == Skill.SkillType.Attack) ActiveChase(actor, subject, Skill.ChaseType.AfterAttack, pSkills[i]); //공격 체이스 발동

                    if (pSkills[i].skillType == Skill.SkillType.Attack) ActiveCounter(subject, actor, Skill.CounterType.TakeDamage); //적 카운터 발동
                }
                actor.battleAP -= 1;
                if (BattleEndCheck())
                {
                    temp = StartCoroutine(PlayBattle(battleEffects));
                    ResetBuffStats();
                    return;
                }
                if (actor.battleAP > 0)
                {
                    ActivePhase(actor, subject);
                    return;
                }
                else
                {
                    DefendPhase(actor, subject);
                    return;
                }
            }
        }

        actor.normalAttack.ActiveSkill(actor, subject); //스킬발동 실패시 일반공격 사용
        actor.battleAP -= 1;
        if (actor.normalAttack.skillType == Skill.SkillType.Attack) ActiveChase(actor, subject, Skill.ChaseType.AfterAttack, actor.normalAttack);
        if (actor.normalAttack.skillType == Skill.SkillType.Attack) ActiveCounter(subject, actor, Skill.CounterType.TakeDamage);

        if (BattleEndCheck())
        {
            temp = StartCoroutine(PlayBattle(battleEffects));
            ResetBuffStats();
            return;
        }
        if (actor.battleAP > 0)
        {
            ActivePhase(actor, subject);
        }
        else
        {
            DefendPhase(actor, subject);
        }
    }
    public void DefendPhase(Character actor, Character subject)
    {
        if (actor.Haste > 0)
        {
            actor.battleRD = 0;
        }

        if (actor.battleRD > actor.battleDP)
        {
            actor.battleRD -= actor.battleDP;
            actor.battleDP = 0;
        }
        else if(actor.battleRD < actor.battleDP)
        {
            actor.battleDP -= actor.battleRD;
            actor.battleRD = 0;
        }else
        {
            actor.battleRD = 0;
            actor.battleDP = 0;
        }

        if (actor.battleDP <= 0)
        {
            actor.battleDP = 0;
            EndPhase(actor, subject);
            return;
            
        }

        List<Skill> pSkills = PickSkills(actor, Skill.ActiveTime.Defend, Skill.SpendType.BP);
        int value = 0;//확률체크용변수
        for (int i = 0; i < pSkills.Count; i++)
        {
            if (!pSkills[i].ActiveCheck(actor, subject)) continue;// 조건 체크
            if (pSkills[i].isLimit && actor.DefendSkillsLimit[i] == 0) continue;
            if (pSkills[i].waitTurn > turn) continue;//시작쿨타임 체크
            if (actor.DefendSkillsCooltime[i] > 0) continue;//쿨타임체크
            value = Random.Range(0, 100);
            if (pSkills[i].activePer > value)//시전확률체크
            {
                pSkills[i].ActiveSkill(actor, subject);
                actor.DefendSkillsCooltime[i] = pSkills[i].coolTime;
                ActiveChase(actor, subject, Skill.ChaseType.AfterDefense, pSkills[i]);
                actor.battleDP -= 1;
                if (pSkills[i].isLimit) actor.DefendSkillsLimit[i] -= 1;
                if (BattleEndCheck())
                {
                    temp = StartCoroutine(PlayBattle(battleEffects));
                    ResetBuffStats();
                    return;
                }
                if (actor.battleDP > 0)
                {
                    DefendPhase(actor, subject);
                    return;
                }
                else
                {
                    EndPhase(actor, subject);
                    return;
                }

            }
                

        }

        actor.normalShield.ActiveSkill(actor, subject);
        ActiveChase(actor, subject, Skill.ChaseType.AfterDefense, actor.normalShield);
        actor.battleDP -= 1;
        if (BattleEndCheck())
        {
            temp = StartCoroutine(PlayBattle(battleEffects));
            ResetBuffStats();
            return;
        }
        if (actor.battleDP > 0)
        {
            DefendPhase(actor, subject);
        }
        else
        {
            EndPhase(actor, subject);
        }
    }
    public void EndPhase(Character actor, Character subject)
    {

        List<Skill> Skills = PickSkills(actor, Skill.ActiveTime.End);
        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills[i].ActiveCheck(actor,subject))
            {
                if (Skills[i].waitTurn <= turn)//시작쿨타임 체크
                {
                    if (actor.EndSkillsCooltime[i] == 0)//쿨타임 체크
                    {
                        int value = Random.Range(0, 100);
                        if (Skills[i].activePer > value)
                        {
                            Skills[i].ActiveSkill(actor, subject);
                            actor.EndSkillsCooltime[i] = Skills[i].coolTime;
                            BattleEndCheck();
                        }
                    }
                }
            }


        }
        if (actor.tag=="Player")
        {
            CheckKO(actor, subject);
        }


        AddBattleEffect(actor, subject, null, HitType.Hit, "RemoveD_Effect", -1, true);
        if (BattleEndCheck())
        {
            temp=StartCoroutine(PlayBattle(battleEffects));
            ResetBuffStats();
            return;
        }
        actor.BuffTurnCountDown();
        actor.DeBuffTurnCountDown();

        if (actor == S_Character)
        {

            EndTurn();

        }
        else
        {
            //턴넘어감
            StandbyPhase(S_Character, F_Character);
        }
    }
    public void DeathPhase(Character actor, Character subject)
    {
        int value = 0;//확률체크용변수

        if (Player.S.reviveStack > 0 && actor.tag == "Player")
        {
            Player.S.reviveStack = 0;
            actor.hp = player.level * 20;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    AddBattleEffect(actor, subject, null, HitType.Hit, "링잉 블룸의 효과로 부활",-1,true);
                    break;
                case Options.Language.Eng:
                    AddBattleEffect(actor, subject, null, HitType.Hit, "Revived by effect of Ringing Bloom",-1, true);
                    break;
                default:
                    break;
            }

        }

        List<Skill> Cskill = PickSkills(actor, Skill.ActiveTime.Death);
        for (int i = 0; i < Cskill.Count; i++)
        {
            value = Random.Range(0, 100);
            if (Cskill[i].ActiveCheck(actor,subject))
            {
                if (Cskill[i].activePer > value)
                {
                    Cskill[i].ActiveSkill(actor, subject);
                }
            } 

        }
    }
    public void FinalePhase(Character actor, Character subject)
    {
        int value = 0;//확률체크용변수
        List<Skill> Cskill = PickSkills(actor, Skill.ActiveTime.Finale);
        for (int i = 0; i < Cskill.Count; i++)
        {
            value = Random.Range(0, 100);
            if (Cskill[i].ActiveCheck(actor,subject))
            {
                if (Cskill[i].activePer > value)
                {
                    Cskill[i].ActiveSkill(actor, subject);
                }
            }
           
        }
        AddBattleEffect(actor, subject, null, HitType.Hit, "RemoveD_Effect", -1, true);
    }

    public void SetTurn()//턴페이즈
    {

        player.battleAP = player.AP;

        monster.battleAP = monster.AP;
        player.battleDP = player.DP;
        monster.battleDP = monster.DP;
        if (ArtifactManager.S.DragonSword.able&&turn==1)
        {
            player.battleAP += 1;

        }
        if (player.SPD + player.Buff_SPD == monster.SPD + monster.Buff_SPD)
        {
            F_Character = player;
            S_Character = monster;
        }
        else if (player.SPD + player.Buff_SPD > monster.SPD + monster.Buff_SPD)
        {
            F_Character = player;
            S_Character = monster;
        }
        else
        {
            S_Character = player;
            F_Character = monster;
        }

        if (Player.S.ClassPasiveSkills.Find(x=>x.skillName=="긴 사정거리")||
            Player.S.ClassPasiveSkills.Find(x => x.skillName == "긴 사정거리+")||
            Player.S.ClassPasiveSkills.Find(x => x.skillName == "긴 사정거리++"))
        {
            F_Character = player;
            S_Character = monster;
        }

        if (ArtifactManager.S.SignalFromTheXienciel.able)
        {
            F_Character = player;
            S_Character = monster;
        }
        StandbyPhase(F_Character, S_Character);
    }

    public void SetMonster(MonsterData _monsterData)
    {

        monster.GetComponent<Image>().sprite = _monsterData.monsterImage;



        if (_monsterData.MonsterID==323|| _monsterData.MonsterID == 378 || _monsterData.MonsterID == 379)
        {
            monster.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 400);
        }else if(_monsterData.MonsterID == 349)
        {
            monster.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 400);
        }
        else if (_monsterData.MonsterID == 376|| _monsterData.MonsterID == 377)
        {
            monster.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 400);
        }
        else
        {
            monster.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
        }

        if (_monsterData.MonsterID == 386 || _monsterData.MonsterID == 395 || _monsterData.MonsterID == 404)
        {
            monster.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 400);
        }

        if (_monsterData.MonsterID==501|| _monsterData.MonsterID ==502|| _monsterData.MonsterID == 503)
        {
            monsterEffect.GetComponent<RectTransform>().anchoredPosition =new Vector2(0, 20);
        }
        else
        {
            monsterEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 75);
        }

        monster.GetComponent<BattleSceneCharacter>().sprites=_monsterData.aniSprites;
        playerImage.gameObject.GetComponent<BattleSceneCharacter>().sprites = Player.S.PlayerBattleAni;
        monster.isRevive = false;

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                monster.characterName = (string)monsterDatas[_monsterData.MonsterID - 1]["name"];
                break;
            case Options.Language.Eng:
                monster.characterName = (string)monsterDatas[_monsterData.MonsterID - 1]["Ename"];
                break;
            default:
                break;
        }
        
        monster.monsterFace = (Character.MonsterFace)(int)monsterDatas[_monsterData.MonsterID - 1]["Face"];
        monster.monsterClass = (Character.MonsterClass)(int)monsterDatas[_monsterData.MonsterID - 1]["Class"];
        monster.MonsterGrade= (int)monsterDatas[_monsterData.MonsterID - 1]["Grade"];
        monster.level = (int)monsterDatas[_monsterData.MonsterID-1]["LV"];
        monster.hp= (int)monsterDatas[_monsterData.MonsterID-1]["HP"];
        monster.MaxHp = (int)monsterDatas[_monsterData.MonsterID - 1]["HP"];
        monster.bp= (int)monsterDatas[_monsterData.MonsterID-1]["BP"];
        monster.bpReset= (int)monsterDatas[_monsterData.MonsterID-1]["BP_reset"];
        monster.ATK= (int)monsterDatas[_monsterData.MonsterID-1]["ATK"];
        monster.DEF= (int)monsterDatas[_monsterData.MonsterID-1]["DEF"];
        monster.HIT= (int)monsterDatas[_monsterData.MonsterID-1]["HIT"];
        monster.AVD= (int)monsterDatas[_monsterData.MonsterID-1]["AVD"];
        monster.CRC= (int)monsterDatas[_monsterData.MonsterID-1]["CRC"];
        monster.CRD= (int)monsterDatas[_monsterData.MonsterID-1]["CRD"];
        monster.CRR= (int)monsterDatas[_monsterData.MonsterID-1]["CRR"];
        monster.POW= (int)monsterDatas[_monsterData.MonsterID-1]["POW"];
        monster.SPD= (int)monsterDatas[_monsterData.MonsterID-1]["SPD"];
        monster.PIE= (int)monsterDatas[_monsterData.MonsterID-1]["PIE"];
        monster.BLK= (int)monsterDatas[_monsterData.MonsterID-1]["BLK"];
        monster.ICD= (int)monsterDatas[_monsterData.MonsterID-1]["ICD"];
        monster.DCD= (int)monsterDatas[_monsterData.MonsterID-1]["DCD"];
        monster.VAM= (int)monsterDatas[_monsterData.MonsterID-1]["VAM"];
        monster.REG= (int)monsterDatas[_monsterData.MonsterID-1]["REG"];
        monster.HEL= (int)monsterDatas[_monsterData.MonsterID-1]["HEL"];
        monster.ARC= (int)monsterDatas[_monsterData.MonsterID-1]["ARC"];
        monster.AP= (int)monsterDatas[_monsterData.MonsterID-1]["A.P"];
        monster.DP= (int)monsterDatas[_monsterData.MonsterID-1]["D.P"];
        rewardGold= (int)monsterDatas[_monsterData.MonsterID-1]["GOL"];
        rewardExp= (int)monsterDatas[_monsterData.MonsterID-1]["EXP"];
        monster.RG_stun= (int)monsterDatas[_monsterData.MonsterID-1]["stun"];
        monster.RG_Paralyze= (int)monsterDatas[_monsterData.MonsterID-1]["paralyze"];
        monster.RG_Poison= (int)monsterDatas[_monsterData.MonsterID-1]["poison"];
        monster.RG_Burn= (int)monsterDatas[_monsterData.MonsterID-1]["burn"];
        monster.RG_Bleed= (int)monsterDatas[_monsterData.MonsterID-1]["bleed"];
        monster.RG_Erosion= (int)monsterDatas[_monsterData.MonsterID-1]["erosion"];
        monster.RG_Curse= (int)monsterDatas[_monsterData.MonsterID-1]["curse"];
        monster.RG_Daze= (int)monsterDatas[_monsterData.MonsterID-1]["daze"];
        monster.RG_Doom= (int)monsterDatas[_monsterData.MonsterID-1]["doom"];
        monster.RG_Misfortune= (int)monsterDatas[_monsterData.MonsterID-1]["misfortune"];
        monster.RG_Infect= (int)monsterDatas[_monsterData.MonsterID-1]["infect"];
        monster.RG_Fear= (int)monsterDatas[_monsterData.MonsterID-1]["fear"];


       

        monster.skillList = new List<Skill>(_monsterData.skillList);
        monster.normalAttack = _monsterData.NormalAttack;
        monster.normalShield = _monsterData.NormalShield;
        monster.Buff_ATK = 0;
    }

    public List<Skill> PickSkills(Character _character, Skill.ActiveTime _activeTime,Skill.SpendType spendType=Skill.SpendType.ETC)
    {
        List<Skill> skills = new List<Skill>();
        for (int i = 0; i < _character.battleSkills.Count; i++)
        {
            if (_character.battleSkills[i].activeTime == _activeTime
                &&_character.battleSkills[i].skillSpendType==spendType)
            {
                skills.Add(_character.battleSkills[i]);
            }
        }
        skills.Sort(delegate (Skill A, Skill B)
        {
            if (A.P < B.P) return 1;
            else if (A.P > B.P) return -1;
            return 0;
        });
        return skills;
    }

    public void SetBattle(Character _player, Character _monster)
    {
        player = Player.S;
        player.bp = 0;
        monster = _monster;
        player.MaxHp = player.hp;
        monster.MaxHp = _monster.hp;
        player.LostHp = 0;
        monster.LostHp = 0;
        preKostack = Player.S.KoStack;
        F_Character = null;
        S_Character = null;

        _player.battleSkills.Clear();
        _monster.battleSkills.Clear();

        List<Skill> publicSkills = new List<Skill>(Player.S.publicSkillinven);
        _player.battleSkills = new List<Skill>(Player.S.skillList);
        for (int i = 0; i < Player.S.ClassPasiveSkills.Count; i++)
        {
            _player.battleSkills.Add(Player.S.ClassPasiveSkills[i]);
        }
        for (int i = 0; i < publicSkills.Count; i++)
        {
            _player.battleSkills.Add(publicSkills[i]);
        }
        _monster.battleSkills = new List<Skill>(_monster.skillList);


        SetSkillCoolTime(player);
        SetSkillCoolTime(monster);

        EternityPhase();

        towerImage.sprite = towerImageSource[TowerMap.S.curTowerNum];

    }
    public void SetSkillCoolTime(Character character)
    {
        character.ActiveSkillsCooltime.Clear();
        character.ActiveSkillsLimit.Clear();
        character.firstSkillLimit.Clear();
        character.firstSkillsCooltime.Clear();
        character.DefendSkillsCooltime.Clear();
        character.DefendSkillsLimit.Clear();
        character.TurnSkillsCooltime.Clear();
        character.TurnSkillsLimit.Clear();
        character.CounterSkillsCooltime.Clear();
        character.CounterSkillsLimit.Clear();
        character.EndSkillsCooltime.Clear();
        List<Skill> skills = PickSkills(character, Skill.ActiveTime.Standby,Skill.SpendType.ETC);
        for (int i = 0; i < skills.Count; i++)
        {
            character.firstSkillsCooltime.Add(0);
            character.firstSkillLimit.Add(skills[i].LimitNum);
        }
        skills = PickSkills(character, Skill.ActiveTime.Counter, Skill.SpendType.ETC);
        for (int i = 0; i < skills.Count; i++)
        {
            character.CounterSkillsCooltime.Add(0);
            character.CounterSkillsLimit.Add(skills[i].LimitNum);
        }
        skills = PickSkills(character, Skill.ActiveTime.Active,Skill.SpendType.AP);
        for (int i = 0; i < skills.Count; i++)
        {
            character.ActiveSkillsCooltime.Add(0);
            character.ActiveSkillsLimit.Add(skills[i].LimitNum);
        }
        skills = PickSkills(character, Skill.ActiveTime.Defend,Skill.SpendType.BP);
        for (int i = 0; i < skills.Count; i++)
        {
            character.DefendSkillsCooltime.Add(0);
            character.DefendSkillsLimit.Add(skills[i].LimitNum);
        }
        skills = PickSkills(character, Skill.ActiveTime.Turn, Skill.SpendType.ETC);
        for (int i = 0; i < skills.Count; i++)
        {
            character.TurnSkillsCooltime.Add(0);
            character.TurnSkillsLimit.Add(skills[i].LimitNum);
        }
        skills = PickSkills(character, Skill.ActiveTime.End, Skill.SpendType.ETC);
        for (int i = 0; i < skills.Count; i++)
        {
            character.EndSkillsCooltime.Add(0);
        }
    }
    public void CoolTimeDecrease(Character character)
    {
        for (int i = 0; i < character.ActiveSkillsCooltime.Count; i++)
        {
            if (character.ActiveSkillsCooltime[i] > 0)
            {
                character.ActiveSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.CounterSkillsCooltime.Count; i++)
        {
            if (character.CounterSkillsCooltime[i] > 0)
            {
                character.CounterSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.firstSkillsCooltime.Count; i++)
        {
            if (character.firstSkillsCooltime[i] > 0)
            {
                character.firstSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.DefendSkillsCooltime.Count; i++)
        {
            if (character.DefendSkillsCooltime[i] > 0)
            {
                character.DefendSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.TurnSkillsCooltime.Count; i++)
        {
            if (character.TurnSkillsCooltime[i] > 0)
            {
                character.TurnSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.EndSkillsCooltime.Count; i++)
        {
            if (character.EndSkillsCooltime[i] > 0)
            {
                character.EndSkillsCooltime[i] -= 1;
            }

        }
    }
    public void ClassCoolTimeDecrease(Character character)
    {
        for (int i = 0; i < character.ActiveSkillsCooltime.Count; i++)
        {
            if (character.ActiveSkillsCooltime[i] > 0)
            {
                if (PickSkills(character,Skill.ActiveTime.Active,Skill.SpendType.AP)[i].isPublicSkill)
                {
                    continue;
                }
                character.ActiveSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.CounterSkillsCooltime.Count; i++)
        {
            if (character.CounterSkillsCooltime[i] > 0)
            {
                if (PickSkills(character, Skill.ActiveTime.Counter, Skill.SpendType.ETC)[i].isPublicSkill)
                {
                    continue;
                }
                character.CounterSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.firstSkillsCooltime.Count; i++)
        {
            if (character.firstSkillsCooltime[i] > 0)
            {
                if (PickSkills(character, Skill.ActiveTime.Standby, Skill.SpendType.ETC)[i].isPublicSkill)
                {
                    continue;
                }
                character.firstSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.DefendSkillsCooltime.Count; i++)
        {
            if (character.DefendSkillsCooltime[i] > 0)
            {
                if (PickSkills(character, Skill.ActiveTime.Defend, Skill.SpendType.BP)[i].isPublicSkill)
                {
                    continue;
                }
                character.DefendSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.TurnSkillsCooltime.Count; i++)
        {
            if (character.TurnSkillsCooltime[i] > 0)
            {
                if (PickSkills(character, Skill.ActiveTime.Turn, Skill.SpendType.ETC)[i].isPublicSkill)
                {
                    continue;
                }
                character.TurnSkillsCooltime[i] -= 1;
            }

        }
        for (int i = 0; i < character.EndSkillsCooltime.Count; i++)
        {
            if (character.EndSkillsCooltime[i] > 0)
            {
                if (PickSkills(character, Skill.ActiveTime.End, Skill.SpendType.ETC)[i].isPublicSkill)
                {
                    continue;
                }
                character.EndSkillsCooltime[i] -= 1;
            }

        }
    }
    public void EndTurn()
    {
        turn += 1;
        if (ArtifactManager.S.TreasureOfWitch.able)
        {
            Player.S.KoStackUp(1);
        }

        if (turn/5>=2)
        {
            Player.S.KoStackUp(3);
        }else if(turn/5>=1)
        {
            Player.S.KoStackUp(2);
        }
        else
        {
            Player.S.KoStackUp(1);
        }
        Player.S.KoCri = false;
        Player.S.KoMiss = false;
        AddBattleEffect(player, monster, null, HitType.Hit, turn + "Turn", -1, true);

        TurnSkillActive(player, monster);
        TurnSkillActive(player, monster);

        CoolTimeDecrease(F_Character);
        CoolTimeDecrease(S_Character);
        player.battleAP = player.AP+player.Buff_AP;
        monster.battleAP = monster.AP + monster.Buff_AP;
        player.battleDP = player.DP + player.Buff_DP;
        monster.battleDP = monster.DP + monster.Buff_DP;
        StandbyPhase(F_Character, S_Character);

    }
    public bool BattleEndCheck()
    {
        if (player.hp <= 0)
        {
            DeathPhase(player, monster);
        }
        if (monster.hp <= 0)
        {
            DeathPhase(monster, player);
        }

        if (player.hp <= 0 || monster.hp <= 0)
        {
            FinalePhase(player, monster);
            FinalePhase(monster, player);
            EndBattleRemoveBuffs(player);
            EndBattleRemoveBuffs(monster);
            return true;
        }

        return false;
    }
    public void AddBattleEffect(Character _actor, Character _subject, Skill _skill,Battle.HitType hitType,string log,int _damageNum=-1,bool noEffect=false,int _textEventNum=0,bool _isCri=false,int _healNum=0,int _shieldNum=0,float _effectTime=0,string _effectName="")
    {
        BattleEffect battleEffect=new BattleEffect();
        battleEffect.koStack = Player.S.KoStack;
        battleEffect.mSwordStack = monster.MasterSwordStack;
        battleEffect.textEventNum = _textEventNum;
        if (player.ATK + player.Buff_ATK<0)
        {
            battleEffect.p_ATK = 0;
        }
        else battleEffect.p_ATK = (short)(player.ATK + player.Buff_ATK);

        if (player.DEF + player.Buff_DEF < 0)
        {
            battleEffect.p_DEF = 0;
        }
        else battleEffect.p_DEF = (short)(player.DEF + player.Buff_DEF);
        battleEffect.p_HP = player.hp;
        battleEffect.p_tempHP = player.tempHp;
        battleEffect.p_DP = player.bp;

        for (int i = 0; i < player.buffList.Count; i++)
        {
            battleEffect.p_buff.Add(player.buffList[i]);
        }



        if (monster.ATK + monster.Buff_ATK < 0)
        {
            battleEffect.m_ATK = 0;
        }
        else battleEffect.m_ATK = (short)(monster.ATK + monster.Buff_ATK);

        if (monster.DEF + monster.Buff_DEF < 0)
        {
            battleEffect.m_DEF = 0;
        }
        else battleEffect.m_DEF = (short)(monster.DEF + monster.Buff_DEF);
        battleEffect.m_DP = monster.bp;
        battleEffect.m_HP = monster.hp;
        battleEffect.m_tempHP = monster.tempHp;
        battleEffect.isCri = _isCri;
        battleEffect.healNum = _healNum;
        battleEffect.shieldNum = _shieldNum;
        for (int i = 0; i < monster.buffList.Count; i++)
        {
            battleEffect.m_buff.Add(monster.buffList[i]);
        }

        if (_actor.gameObject.tag=="Player")
        {
            battleEffect.isPlayer = true;
        }
        else
        {
            battleEffect.isPlayer = false;
        }
        
        battleEffect.turn = (short)turn;

        if (_effectName!="")
        {
            battleEffect.effectNames.Add(_effectName);
            battleEffect.effectTime = _effectTime;
        }
        if (_skill!=null)
        {
            battleEffect.damageEffectOnme = _skill.damageEffectOnme;
            if (_skill.isEffectEnemy)
            {
                battleEffect.isAttack = true;
            }
            else
            {
                battleEffect.isAttack = false;
            }

            for (int i = 0; i < _skill.OtherEffectsOnme.Length; i++)
            {
                if (_skill.OtherEffectsOnme[i] =="NA")
                {
                    battleEffect.effectOnme.Add(_actor.normalAttack.effectName);
                }
                else if(_skill.OtherEffectsOnme[i] == "ND")
                {
                    battleEffect.effectOnme.Add(_actor.normalShield.effectName);
                }
                else
                {
                    battleEffect.effectOnme.Add(_skill.OtherEffectsOnme[i]);
                }


            }
            for (int i = 0; i < _skill.OtherEffectsOnEnemy.Length; i++)
            {
                if (_skill.OtherEffectsOnEnemy[i] == "NA")
                {
                    battleEffect.effectOnEnemy.Add(_actor.normalAttack.effectName);
                }
                else if (_skill.OtherEffectsOnEnemy[i] == "ND")
                {
                    battleEffect.effectOnEnemy.Add(_actor.normalShield.effectName);
                }
                else
                {
                    battleEffect.effectOnEnemy.Add(_skill.OtherEffectsOnEnemy[i]);
                    battleEffect.effectOnEnemyTime = _skill.otherEffectOnEnemyTime;
                
                }

            }
            for (int i = 0; i < _skill.OtherEffectsOnEnemyTriple.Length; i++)
            {
                if (_skill.OtherEffectsOnEnemyTriple[i] == "NA")
                {
                    battleEffect.effectOnEnemyTriple.Add(_actor.normalAttack.effectName);
                }
                else if (_skill.OtherEffectsOnEnemyTriple[i] == "ND")
                {
                    battleEffect.effectOnEnemyTriple.Add(_actor.normalShield.effectName);
                }
                else
                {
                    battleEffect.effectOnEnemyTriple.Add(_skill.OtherEffectsOnEnemyTriple[i]);
                    battleEffect.effectOnEnemyTime = _skill.otherEffectOnEnemyTime;

                }

            }

            for (int i = 0; i < _skill.OtherEffectsOnMiddle.Length; i++)
            {
                if (_skill.OtherEffectsOnMiddle[i] == "NA")
                {
                    battleEffect.effectOnMiddle.Add(_actor.normalAttack.effectName);
                }
                else if (_skill.OtherEffectsOnMiddle[i] == "ND")
                {
                    battleEffect.effectOnMiddle.Add(_actor.normalShield.effectName);
                }
                else
                {
                    battleEffect.effectOnMiddle.Add(_skill.OtherEffectsOnMiddle[i]);
                }

            }



            switch (hitType)
            {
                case HitType.Hit:
                    battleEffect.effectNames.Add(_skill.effectName);
                    break;
                case HitType.CriHit:
                    battleEffect.effectNames.Add(_skill.effectName);
                    break;
                case HitType.Miss:
                    battleEffect.effectNames.Add("Miss");
                    break;
                case HitType.Ward:
                    battleEffect.effectNames.Add("UnholyBarrier");
                    break;
                case HitType.Block:
                    battleEffect.effectNames.Add("Block");
                    break;
                default:
                    break;
            }
            if (noEffect)
            {
                battleEffect.effectNames.Clear();
                battleEffect.effectOnme.Clear();
                battleEffect.effectOnEnemy.Clear();
                battleEffect.effectOnEnemyTriple.Clear();
            }
            else
            {
                if (hitType==HitType.Miss)
                {
                    battleEffect.effectTime = 0.3f;
                }
                else
                {
                    if (_effectTime>0)
                    {
                        battleEffect.effectTime = _effectTime;
                    }
                    else
                    {
                        battleEffect.effectTime = _skill.skillEffectTime;
                    }

                }

            }


        }

        battleEffect.damageNum = (short)_damageNum;
        battleEffect.BattleLog = log;

        battleEffects.Add(battleEffect);
    }
    public void UpdateUI(BattleEffect battleEffect)
    {
        if (battleEffect.p_tempHP>0)
        {
            T_playerHp.text = "H  P : " + battleEffect.p_HP.ToString() + "+" + battleEffect.p_tempHP.ToString();
        }
        else
        {
            T_playerHp.text = "H  P : " + battleEffect.p_HP.ToString();
        }
        T_playerDp.text = "B  P : " + battleEffect.p_DP.ToString();
        T_playerATK.text = "ATK : " + battleEffect.p_ATK.ToString();
        T_playerDEF.text = "DEF : " + battleEffect.p_DEF.ToString();
        T_playerName.text=player.characterName;
        T_PlayerKoStack.text = "KO : " + battleEffect.koStack;
        if (battleEffect.m_tempHP > 0)
        {
            T_monsterHp.text = "H  P : " + battleEffect.m_HP.ToString() + "+" + battleEffect.m_tempHP.ToString();
        }
        else
        {
            T_monsterHp.text = "H  P : " + battleEffect.m_HP.ToString();
        }
        T_monsterDp.text = "B  P : " + battleEffect.m_DP.ToString();
        T_monsterATK.text = "ATK : " + battleEffect.m_ATK.ToString();
        T_monsterDEF.text = "DEF : " + battleEffect.m_DEF.ToString();
        T_monsterName.text = monster.characterName;
        if (battleEffect.turn==1)
        {
            TurnUI.text = battleEffect.turn + " Turn";
        }
        else
        {
            TurnUI.text = battleEffect.turn + " Turns";
        }
        if (battleEffect.mSwordStack>0)
        {
            MasterSwordStackUI.gameObject.SetActive(true);
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    MasterSwordStackUI.text = "검성 스택 : " + battleEffect.mSwordStack;
                    break;
                case Options.Language.Eng:
                    MasterSwordStackUI.text = "Master Sword Stack : "+ battleEffect.mSwordStack;
                    break;
                default:
                    break;
            }
        }
        else
        {
            MasterSwordStackUI.gameObject.SetActive(false);
        }
        
        player_BuffSlot.BuffSlotCreate(battleEffect.p_buff);
        monster_BuffSlot.BuffSlotCreate(battleEffect.m_buff);
    }
    public IEnumerator PlayBattle(List<BattleEffect> battleEffects, int _num = 0)
    {
        int value = 0;
        for (int i = _num; i < battleEffects.Count; i++)
        {
            curEffectNum = i;
            if (battleEffects[i].BattleLog == "RemoveD_Effect")
            {
                DamageEffect.S.RemoveAllEfeect();
                continue;
            }

            for (int j = 0; j < battleEffects[i].effectNames.Count; j++)
            {
                SpawnEffect(battleEffects[i].isPlayer, battleEffects[i].isAttack, battleEffects[i].effectNames[j]);
            }
            for (int j = 0; j < battleEffects[i].effectOnme.Count; j++)
            {
                SpawnEffect(battleEffects[i].isPlayer, false, battleEffects[i].effectOnme[j]);
            }
            for (int j = 0; j < battleEffects[i].effectOnEnemy.Count; j++)
            {
                if (battleEffects[i].effectOnEnemyTime>0)
                {
                    yield return new WaitForSeconds(battleEffects[i].effectOnEnemyTime);
                }
                SpawnEffect(battleEffects[i].isPlayer, true, battleEffects[i].effectOnEnemy[j]);
            }
            for (int j = 0; j < battleEffects[i].effectOnEnemyTriple.Count; j++)
            {
                if (battleEffects[i].effectOnEnemyTime > 0)
                {
                    yield return new WaitForSeconds(battleEffects[i].effectOnEnemyTime);
                }
                SpawnEffect(battleEffects[i].isPlayer, true, battleEffects[i].effectOnEnemyTriple[j],1);
            }
            for (int j = 0; j < battleEffects[i].effectOnMiddle.Count; j++)
            {
                if (battleEffects[i].effectOnEnemyTime > 0)
                {
                    yield return new WaitForSeconds(battleEffects[i].effectOnEnemyTime);
                }
                SpawnEffect(battleEffects[i].isPlayer, true, battleEffects[i].effectOnMiddle[j], 0,true);
            }

            if (battleEffects[i].damageNum != -1)
            {
                if (!battleEffects[i].isPlayer)
                {
                    if (battleEffects[i].damageEffectOnme)
                    {
                        DamageEffect.S.MonsterDamaged(battleEffects[i].damageNum.ToString(), battleEffects[i].isCri);
                    }
                    else
                    {
                        DamageEffect.S.PlayerDamaged(battleEffects[i].damageNum.ToString(), battleEffects[i].isCri);
                    }

                }
                else
                {
                    if (battleEffects[i].damageEffectOnme)
                    {
                        DamageEffect.S.PlayerDamaged(battleEffects[i].damageNum.ToString(), battleEffects[i].isCri);
                    }
                    else
                    {
                        DamageEffect.S.MonsterDamaged(battleEffects[i].damageNum.ToString(), battleEffects[i].isCri);
                    }

                }
            }
            if (battleEffects[i].shieldNum >0)
            {
                if (!battleEffects[i].isPlayer)
                {
                    DamageEffect.S.MonsterShield(battleEffects[i].shieldNum.ToString());
                }
                else
                {
                    DamageEffect.S.PlayerShield(battleEffects[i].shieldNum.ToString());
                }
            }
            if (battleEffects[i].healNum > 0)
            {
                if (!battleEffects[i].isPlayer)
                {
                    DamageEffect.S.MonsterHealed(battleEffects[i].healNum.ToString());
                }
                else
                {
                    DamageEffect.S.PlayerHealed(battleEffects[i].healNum.ToString());
                }
            }

            if (battleEffects[i].BattleLog=="")
            {

            }
            else
            {
                BattleLog.S.CreateLog(battleEffects[i].BattleLog);
            }
            UpdateUI(battleEffects[i]);

            yield return new WaitForSeconds(battleEffects[i].effectTime);
            if (battleEffects[i].textEventNum>0)
            {
                value = i;
                break;
            }
        }
        switch (battleEffects[value].textEventNum)
        {
            case 0:
                break;
            case 1:
                if (TowerMap.S.curTowerNum==1)
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(101, 102), true, 4, value + 1);
                }
                yield break;
            case 2:
                if (TowerMap.S.curTowerNum == 3)
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(265, 267), true, 4, value + 1);
                }

                yield break;
            default:
                break;
        }
        battleEffects.Clear();

        if (player.hp>0)
        {
            TowerMap.S.WinBattle(towercellLocation);

            if (ArtifactManager.S.ListFromTheHades.able)
            {
                Player.S.KoStackUp(4);
            }
            else
            {
                Player.S.KoStackUp(2);
            }

            Player.S.KoCri = false;
            Player.S.KoMiss = false;
            OpenBattleResultUI(nowMontertData);
        }
        else
        {
            gameOverUI.SetActive(true);
        }
        yield break;

    }
    public void AddLog(string _texts)
    {
        BattleLog.S.CreateLog(_texts);
    }
    public void SpawnEffect(bool isPlayer, bool isEffectEnemy, string effectName, int eventNum = 0,bool isMiddle=false)
    {
        GameObject go;

        if (eventNum==1&& effectName != "Miss")
        {
            List<Transform> transforms = new List<Transform>();
            if (isPlayer)
            {
                if (isEffectEnemy)
                {
                    go = Instantiate(P_Effect, T_monsterEffect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                    go = Instantiate(P_Effect, T_monster2Effect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                    go = Instantiate(P_Effect, T_monster3Effect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                }
                else
                {
                    go = Instantiate(P_Effect, T_playerEffect);
                    if (effectName != "Miss")
                    {
                        go.transform.localScale = new Vector3(-1, 1, 1);
                    }

                }

            }
            else
            {
                if (isEffectEnemy)
                {
                    go = Instantiate(P_Effect, T_playerEffect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                    go = Instantiate(P_Effect, T_player2Effect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                    go = Instantiate(P_Effect, T_player3Effect);
                    go.GetComponent<Effect>().PlayEffect(effectName);
                    if (effectName != "Miss")
                    {
                        go.transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else
                {
                    go = Instantiate(P_Effect, T_monsterEffect);
                }
            }
            go.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            go.GetComponent<Effect>().PlayEffect(effectName);
           
            return;
        }//특수이펙트
        
        if (isPlayer)
        {
            if (isEffectEnemy)
            {
                if (!isMiddle)
                {
                    go = Instantiate(P_Effect, T_monsterEffect);
                }
                else
                {
                    go = Instantiate(P_Effect, T_middleEffect);
                }

            }
            else
            {
                go = Instantiate(P_Effect, T_playerEffect);
                if (effectName != "Miss")
                {
                    go.transform.localScale = new Vector3(-1, 1, 1);
                }

            }

        }
        else
        {
            if (isEffectEnemy)
            {
                if (!isMiddle)
                {
                    go = Instantiate(P_Effect, T_playerEffect);
                }
                else
                {
                    go = Instantiate(P_Effect, T_middleEffect);
                }
                if (effectName != "Miss")
                {
                    go.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                go = Instantiate(P_Effect, T_monsterEffect);
            }
        }
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        go.GetComponent<Effect>().PlayEffect(effectName);
    }
    public void GoBattleScene(MonsterData monsterData, TowerCell _towercell)
    {
        turn = 1;
        towercellLocation = _towercell;
        SetMonster(monsterData);
        playerImage.sprite = Player.S.PlayerTowerImage;
        nowMontertData = monsterData;
        battleUIBase.SetActive(true);
        SetBattle(player, monster);
    }
    public void OpenBattleResultUI(MonsterData monsterData)
    {
        resultUIbase.SetActive(true);
        if (!isFlee)
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    monstername.text = monster.characterName + " 처치";
                    break;
                case Options.Language.Eng:
                    monstername.text = "Defeat a(an) " + monster.characterName;
                    break;
                default:
                    break;
            }


            float curgold = rewardGold;
            float curexp = rewardExp;
            if (ArtifactManager.S.DirtSpoon.able)
            {
                Player.S.SpoonStack += 1;
            }
            if (ArtifactManager.S.GoldenEgg.able)
            {
                Player.S.GoldenEggStack += 1;
            }
            if (ArtifactManager.S.NonFantasticsAndWhereToFindThem.able)
            {
                Player.S.BookStack += 1 ;
            }
            if (Player.S.GoldenEggStack>=50)
            {
                ArtifactManager.S.GetArtifact("황금 거위");
                ArtifactManager.S.GoldenEgg.able = false;
                Player.S.GoldenEggStack = 0;
            }
            if (Player.S.BookStack >= 50)
            {
                ArtifactManager.S.GetArtifact("신비한 사전");
                ArtifactManager.S.NonFantasticsAndWhereToFindThem.able = false;
                Player.S.BookStack = 0;
            }
            if (Player.S.SpoonStack >= 50)
            {
                ArtifactManager.S.GetArtifact("금 숟가락");
                ArtifactManager.S.DirtSpoon.able = false;
                Player.S.SpoonStack = 0;
            }
            if (ArtifactManager.S.GoldneGoose.able)
            {
                curgold += curgold * 10 / 100;
            }
            curgold = Mathf.CeilToInt(curgold);
            gold.text = "Gold : " + curgold;
            if (ArtifactManager.S.FantsticsAndWhereToFindThem.able)
            {
                curexp += curexp * 10 / 100;
            }
            curexp = Mathf.CeilToInt(curexp);
            exp.text = "EXP : " + curexp;

            Player.S.GetExp((int)curexp);
            Player.S.GetGold((int)curgold);

        }
        else
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    monstername.text ="전투에서 도주 했습니다.";
                    break;
                case Options.Language.Eng:
                    monstername.text = "Run Away From Battle";
                    break;
                default:
                    break;
            }
            gold.text = "Gold : " + 0;
            exp.text = "EXP : " + 0;
        }

    }
    public void CloseBattleUI()
    {
        if (resultUIbase.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TowerMap.S.MoveLock = false;
                resultUIbase.SetActive(false);
                battleUIBase.SetActive(false);
                BattleLog.S.ClearLog();

                if (isFlee)
                {
                    isFlee = false;
                    nowMontertData = null;
                    rewardExp = 0;
                    rewardGold = 0;
                    aniNum = 0;
                    aniTime = 0f;
                    return;
                }
                if (ArtifactManager.S.NecklaceOfVigor.able)
                {
                    Player.S.hp += Player.S.LostHp / 2;
                }

                if (TowerMap.S.curTowerNum==4&&TowerMap.S.curFloorNum==6)
                {
                    TowerVariable.S.KillMonsterInFloor6 += 1;
                }
                if (TowerMap.S.curTowerNum == 4 && TowerMap.S.curFloorNum == 24)
                {
                    TowerVariable.S.KillMonsterInFloor24 += 1;
                }

                QuestManager.S.MonsterKillCountUp(nowMontertData.MonsterID);
                if (TowerMap.S.curTowerNum==0&&TowerMap.S.curFloorNum==1)
                {
                    if (!TowerVariable.S.isFirstBattle)
                    {
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(138, 141),false);
                        TowerVariable.S.isFirstBattle = true;
                    }
                }

                switch (nowMontertData.MonsterID)//특정 몬스터 처치 후
                {
                    case 322:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(57, 59), false,6);
                        break;
                    case 323:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(76, 77), false,7);
                        break;
                    case 324:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(103, 106), false,11);
                        break;
                    case 348:
                        TowerVariable.S.killVamLord += 1;
                        break;
                    case 349:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(133, 134), false);
                        for (int i = 3; i < 6; i++)
                        {
                            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, 6].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        }
                        for (int i = 2; i < 7; i++)
                        {
                            for (int j = 7; j < 9; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }

                        }
                        break;
                    case 350:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(172, 173), false);

                        break;
                    case 351:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(179, 180), false,20);
                        break;
                    case 375:
                        TowerVariable.S.golemKillCount += 1;
                        break;
                    case 376:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(201, 202), false, 22);
                        for (int i = 3; i < 6; i++)
                        {
                            for (int j = 3; j < 6; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }
                           
                        }
                        break;
                    case 377:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(217, 219), false, 24);
                        for (int i = 3; i < 6; i++)
                        {
                            for (int j = 4; j < 7; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }

                        }
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[2, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[6, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        break;
                    case 378:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(243, 245), false, 26);
                        for (int i =1; i < 4; i++)
                        {
                            for (int j = 3; j < 7; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }

                        }
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[2, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        break;
                    case 379:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(268, 272), false, 29);
                        for (int i = 3; i < 6; i++)
                        {
                            for (int j = 5; j < 9; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }

                        }
                        break;
                    case 408:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(311, 312), false);
                        break;
                    case 409:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(315, 316), false);
                        TowerVariable.S.KillLady =true;
                        break;
                    case 410:
                        //TextBox.S.BetaendUI.SetActive(true);
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(321, 324), false,34);
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[5, 0].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        break;
                    case 411:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(327, 327), false);
                        break;
                    case 412:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(332, 335), false);
                        break;
                    case 413:
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(343, 346), false,38);
                       
                        break;
                    case 414:
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[2, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[6, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                        for (int i = 3; i < 6; i++)
                        {
                            for (int j = 4; j < 7; j++)
                            {
                                TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[i, j].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
                            }

                        }
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(356, 357), false);
                        break;

                    case 509:
                        CsvTest.S.SaveMap(TowerMap.S.curTowerNum, TowerMap.S.curFloorNum, TowerMap.S.mapCells);
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(145, 147), false, 14);
                        break;
                    default:
                        break;
                }

                nowMontertData = null;
                rewardExp = 0;
                rewardGold = 0;
                aniNum = 0;
                aniTime = 0f;


            }

        }
    }
    public void EndBattleRemoveBuffs(Character _actor)
    {
        for (int i = _actor.buffList.Count - 1; i >= 0; i--)
        {
            _actor.buffList[i].EndBuff(_actor);
            _actor.buffList.Remove(_actor.buffList[i]);
        }
        ResetBuffStats();
    }
    public void ResetBuffStats()
    {
        player.Buff_ATK = 0;
        player.Buff_DEF = 0;
        player.Buff_AVD = 0;
        player.Buff_CDR = 0;
        player.Buff_CRC = 0;
        player.Buff_CRD = 0;
        player.Buff_CRR = 0;
        player.Buff_HIT = 0;
        player.Buff_POW = 0;
        player.Buff_SPD = 0;
        player.Buff_DCD = 0;
        player.Buff_ICD = 0;
        player.Buff_Vam = 0;
        player.Buff_REG = 0;
        player.Buff_BLK = 0;
        player.Buff_PIE = 0;
        player.Buff_HEL = 0;
        player.Buff_MIR = 0;
        player.Buff_AP = 0;
        player.Buff_DP = 0;
        player.Buff_bpReset = 0;
        player.Buff_ARC = 0;
        player.battleAP = 0;
        player.battleAR = 0;
        player.battleDP = 0;
        player.battleRD = 0;
        player.BuffRG_CC = 0;
        player.tempHp = 0;
        player.Groggy = 0;
        player.Daze = 0;
        player.Doom = 0;
        player.Misfortune = 0;
        player.Infect = 0;
        player.Fear = 0;
        player.Invincible = 0;
        player.Protect = 0;
        player.Invisible = 0;
        player.Bless = 0;
        player.Curse = 0;
        player.Wall = 0;
        player.Fortune = 0;
        player.Haste = 0;
        player.Reveal = 0;
        player.Endure = 0;
        player.remainCastTurn = 0;
        player.MasterSwordStack = 0;
        player.castSkill = null;
        player.pistolStack = 0;

        monster.RailgunStack = 0;
        monster.Buff_ATK = 0;
        monster.Buff_DEF = 0;
        monster.Buff_AVD = 0;
        monster.Buff_CDR = 0;
        monster.Buff_CRC = 0;
        monster.Buff_CRD = 0;
        monster.Buff_CRR = 0;
        monster.Buff_HIT = 0;
        monster.Buff_POW = 0;
        monster.Buff_SPD = 0;
        monster.Buff_DCD = 0;
        monster.Buff_ICD = 0;
        monster.Buff_Vam = 0;
        monster.Buff_REG = 0;
        monster.Buff_BLK = 0;
        monster.Buff_PIE = 0;
        monster.Buff_HEL = 0;
        monster.Buff_MIR = 0;
        monster.Buff_AP = 0;
        monster.Buff_DP = 0;
        monster.Buff_bpReset = 0;
        monster.Buff_ARC = 0;
        monster.battleAP = 0;
        monster.battleAR = 0;
        monster.battleDP = 0;
        monster.battleRD = 0;
        monster.tempHp = 0;
        monster.RailgunStack = 0;
        monster.Groggy = 0;
        monster.Daze = 0;
        monster.Doom = 0;
        monster.Misfortune = 0;
        monster.Infect = 0;
        monster.Fear = 0;
        monster.Invincible = 0;
        monster.Protect = 0;
        monster.Invisible = 0;
        monster.Bless = 0;
        monster.Curse = 0;
        monster.Wall = 0;
        monster.Fortune = 0;
        monster.Haste = 0;
        monster.Reveal = 0;
        monster.Endure = 0;
        monster.remainCastTurn = 0;
        monster.castSkill = null;
        monster.MasterSwordStack = 0;
        monster.pistolStack = 0;

        player.battleSkills.Clear();
        monster.battleSkills.Clear();
    }
    public void SetArtifact()
    {
        if (ArtifactManager.S.RegeneRing.able)
        {
            player.Buff_REG += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.Thermometer.able)
        {
            player.Buff_REG += 2 * TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.RedOrb.able)
        {
            player.Buff_ICD += 5;
        }
        if (ArtifactManager.S.BlueOrb.able)
        {
            player.Buff_DCD += 5;
        }
        if (ArtifactManager.S.TeleScope.able)
        {
            player.Buff_HIT += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.Bible.able)
        {
            Bless buff=ScriptableObject.CreateInstance<Bless>();
            buff.buffName = "축복";
            buff.bigBuffType = Buff.BigBuffType.Buff;
            buff.buffType = Buff.BuffType.BB;
            buff.remainType = Buff.RemainType.Turn;
            buff.buffImageType = Buff.BuffImageType.Bless;
            buff.CastThisTurn = true;
            player.AddBuff(buff, 1);
        }
        if (ArtifactManager.S.HellBible.able)
        {
            Curse buff = ScriptableObject.CreateInstance<Curse>();
            buff.buffName = "저주";
            buff.bigBuffType = Buff.BigBuffType.DeBuff;
            buff.buffType = Buff.BuffType.DD;
            buff.remainType = Buff.RemainType.Turn;
            buff.buffImageType = Buff.BuffImageType.Curse;
            buff.CastThisTurn = true;
            monster.AddBuff(buff, 1);
        }
        if (ArtifactManager.S.SpiderFriend.able)
        {
            player.Buff_AVD += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.VampireCape.able)
        {
            player.Buff_Vam += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.UnicornStatus.able)
        {
            player.Buff_SPD += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.Sandal.able)
        {
            player.Buff_POW += 1 + TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.RidingGloves.able)
        {
            player.Buff_ATK += 2 + TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.BlacksmithHammer.able)
        {
            player.Buff_DEF += 2 + TowerMap.S.curTowerNum;
        }

        if (ArtifactManager.S.BlackOrb.able)
        {
            player.Buff_ICD += 10;
        }
        if (ArtifactManager.S.WhiteOrb.able)
        {
            player.Buff_DCD += 10;
        }
        if (ArtifactManager.S.ScaleOfLife.able)
        {
            monster.Buff_HEL -= 20;
        }
        if (ArtifactManager.S.RainbowSocks.able)
        {
            player.Buff_AVD += Player.S.CurTowerNum;
            player.Buff_CRC += Player.S.CurTowerNum;
        }
        if (ArtifactManager.S.HorseShoe.able)
        {
            player.Buff_HIT += TowerMap.S.curTowerNum;
            player.Buff_CRR += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.ElevenChainMail.able)
        {
            player.Buff_ATK += 2 + TowerMap.S.curTowerNum;
            player.Buff_DEF += 2 + TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.AliceGrimoire.able)
        {
            player.Buff_POW += 16;
        }
        if (ArtifactManager.S.Donguibogam.able)
        {
            player.Buff_HEL += 25;
        }
        if (ArtifactManager.S.FiveLeavesClover.able)
        {
            player.Buff_CRR += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.Ginger.able)
        {
            player.BuffRG_CC += 25;
        }
        if (ArtifactManager.S.CrossBow.able)
        {
            player.Buff_CRC += TowerMap.S.curTowerNum;
        }
        if (ArtifactManager.S.RawFish.able)
        {
            int value = Player.S.level*2;
            player.tempHp += value;
        }
        if (ArtifactManager.S.RawHam.able)
        {
            int value = Player.S.level;
            player.tempHp += value;
        }
        if (ArtifactManager.S.Seaweeds.able)
        {
            int value = Player.S.level*3;
            player.tempHp += value;
        }
        if (ArtifactManager.S.MealKit.able)
        {
            int value = player.hp*25/ 1000;
            value = Mathf.Clamp(value, 25, player.hp);
            player.tempHp += value;
        }

        if (ArtifactManager.S.HiddenHourglass.able)
        {
            monster.Buff_SPD -= 2;
        }
        if (ArtifactManager.S.DriedWater.able)
        {
            player.Buff_HEL += 10;
        }
    }
    public void CheckKO(Character actor,Character subject)
    {
        if (Player.S.KoStack>=Player.S.KoStackMax)
        {
            Player.S.koSkill.ActiveSkill(actor, subject);
            Player.S.KoStack=0;
        }
    }
    public void ActiveCounter(Character actor, Character subject, Skill.CounterType counterType)
    {
        if (actor.hp<=0) return;
        int value = 0;//확률체크용변수
        List<Skill> Cskill = PickSkills(actor, Skill.ActiveTime.Counter);
        for (int i = 0; i < Cskill.Count; i++)
        {
            if (Cskill[i].isLimit&&actor.CounterSkillsLimit[i]==0) continue;

            if (Cskill[i].counterType != counterType) continue;
            if (actor.CounterSkillsCooltime[i] > 0) continue;
            value = Random.Range(0, 100);
            if (Cskill[i].activePer > value)
            {
                Cskill[i].ActiveSkill(actor, subject);
                actor.CounterSkillsCooltime[i] = Cskill[i].coolTime;
            }
        }
    }
    public void ActiveChase(Character actor, Character subject, Skill.ChaseType chaseType,Skill _skill)
    {
        if (_skill.activeTime==Skill.ActiveTime.Counter|| _skill.activeTime == Skill.ActiveTime.Chase) return;
        int value = 0;//확률체크용변수
        List<Skill> Cskill = PickSkills(actor, Skill.ActiveTime.Chase);
        for (int i = 0; i < Cskill.Count; i++)
        {
            if (Cskill[i].chaseType != chaseType) continue;
            value = Random.Range(0, 100);
            if (Cskill[i].activePer > value)
            {
                Cskill[i].ActiveSkill(actor, subject);
            }
        }
    }
    public void CheckDotDamage(Character _actor,Character _subject)
    {
        int allDamage = 0;
        for (int i = 0; i < _actor.buffList.Count; i++)
        {
            
            switch (_actor.buffList[i].buffName)
            {
                case "출혈":
                    int bleedDmg= (_actor.ATK+_actor.Buff_ATK) * _actor.buffList[i].DotDamage / 100;
                    if (bleedDmg < 1) bleedDmg = 1;
                    _actor.Damaged(bleedDmg, false, "출혈");
                    allDamage += bleedDmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 출혈로 인해 HP -" + bleedDmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Bleed HP -" + bleedDmg);
                            break;
                        default:
                            break;
                    }
                    break;
                case "턴출혈":
                    int TbleedDmg = (_actor.ATK + _actor.Buff_ATK) * _actor.buffList[i].DotDamage / 100;
                    if (TbleedDmg < 1) TbleedDmg = 1;
                    _actor.Damaged(TbleedDmg, false, "출혈");
                    allDamage += TbleedDmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 출혈로 인해 HP -" + TbleedDmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Bleed HP -" + TbleedDmg);
                            break;
                        default:
                            break;
                    }


                    break;
                case "화상":
                    int burndmg = (_actor.DEF + _actor.Buff_DEF) * _actor.buffList[i].DotDamage / 100;
                    if (burndmg < 1) burndmg = 1;
                    _actor.Damaged(burndmg, false, "화상");
                    allDamage += burndmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 화상으로 인해 HP -" + burndmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Burn HP -" + burndmg);
                            break;
                        default:
                            break;
                    }

                    break;
                case "턴화상":
                    int Tburndmg = (_actor.DEF + _actor.Buff_DEF) * _actor.buffList[i].DotDamage / 100;
                    if (Tburndmg < 1) Tburndmg = 1;
                    _actor.Damaged(Tburndmg, false, "화상");
                    allDamage += Tburndmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 화상으로 인해 HP -" + Tburndmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Burn HP -" + Tburndmg);
                            break;
                        default:
                            break;
                    }

                    break;
                case "중독":
                    int dmg = _actor.hp * _actor.buffList[i].DotDamage / 100;
                    if (dmg < 1) dmg = 1;
                    _actor.Damaged(dmg, false, "중독");
                    allDamage += dmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 중독으로 인해 HP -" + dmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Poison HP -" + dmg);
                            break;
                        default:
                            break;
                    }
                    break;
                case "턴중독":
                    int tdmg = _actor.hp * _actor.buffList[i].DotDamage / 100;
                    if (tdmg < 1) tdmg = 1;
                    _actor.Damaged(tdmg, false, "중독");
                    allDamage += tdmg;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 중독으로 인해 HP -" + tdmg);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Poison HP -" + tdmg);
                            break;
                        default:
                            break;
                    }
                    break;
                case "침식":
                    _actor.Damaged(_actor.buffList[i].DotDamage, false, "침식");
                    allDamage += _actor.buffList[i].DotDamage;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + " 침식으로 인해 HP -" + _actor.buffList[i].DotDamage);
                            break;
                        case Options.Language.Eng:
                            AddBattleEffect(_actor, _subject, null, HitType.Hit, _actor.characterName + "Damaged by Erosion HP -" + _actor.buffList[i].DotDamage);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            

        }
        if (allDamage > 0)
        {
            AddBattleEffect(_subject, _actor, null, HitType.Hit, "", allDamage);
        }
    }
    public void EscapeBattle()
    {
        StopCoroutine(temp);
        Player.S.hp = player.MaxHp;
        TowerMap.S.MoveLock = false;
        OpenBattleResultUI(null);
        battleEffects.Clear();
        Player.S.KoStack = preKostack;
        EndBattleRemoveBuffs(player);
        EndBattleRemoveBuffs(monster);
        nowMontertData = null;
        aniNum = 0;
        aniTime = 0f;
    }
    public void CheckGroggy(Character actor,Character subject)
    {
        if (actor.Groggy>0)
        {
            actor.battleAP = 0;
            actor.battleDP = 0;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    AddBattleEffect(actor, subject, null, HitType.Hit, actor.characterName + "는 그로기 상태입니다.", -1, true,0,false,0,0,0.5f);
                    break;
                case Options.Language.Eng:
                    AddBattleEffect(actor, subject, null, HitType.Hit, actor.characterName +"is Groggy", -1, true, 0, false, 0, 0, 0.5f);
                    break;
                default:
                    break;
            }

        }
        //스턴
    }

    public void CheckCounter(Character _actor, Character _subject, Skill _pSkill)
    {
         if(_actor.Groggy>0)
        {
            return;

        }
        switch (_pSkill.skillType)
        {
            case Skill.SkillType.Attack:
                ActiveCounter(_subject, _actor, Skill.CounterType.TakeDamage);
                break;
            case Skill.SkillType.Defence:
                break;
            case Skill.SkillType.Obstruction:
                break;
            case Skill.SkillType.Assistance:
                ActiveCounter(_subject, _actor, Skill.CounterType.ActiveBuff);
                break;
            case Skill.SkillType.Counter:
                break;
            case Skill.SkillType.Chase:
                break;
            case Skill.SkillType.ETC:
                break;
            default:
                break;
        }
    }
    public void SetNormalAttackBonusEffect(Character _character)
    {
        _character.normalAttackBonusEffects.Clear();
        for (int i = 0; i < _character.skillList.Count; i++)
        {
            if (_character.skillList[i].activeTime==Skill.ActiveTime.NormalAttack)
            {
                _character.normalAttackBonusEffects.Add(_character.skillList[i]);
            }
        }
    }

    public void ActiveSwordOfJudgement(Character _character)
    {
        SwordOfJudgement buff = ScriptableObject.CreateInstance<SwordOfJudgement>();
        buff.buffName = "심판의 검";
        buff.bigBuffType = Buff.BigBuffType.Buff;
        buff.buffType = Buff.BuffType.BB;
        buff.remainType = Buff.RemainType.Turn;
        buff.buffImageType = Buff.BuffImageType.None;
        buff.CastThisTurn = true;
        _character.AddBuff(buff, 1);
    }
    public void ActiveHornfromtheElysion(Character _character)
    {
        if (_character.ATK<100)
        {
            _character.Buff_ATK += 1;
        }
        else
        {
            _character.Buff_ATK += _character.ATK/100;
        }
        if (_character.DEF < 100)
        {
            _character.Buff_DEF += 1;
        }
        else
        {
            _character.Buff_DEF += _character.DEF / 100;
        }


    }
    public void StanbyArtiEffect(Character actor)
    {
        if (ArtifactManager.S.DarknessSympathizer.able && actor.tag == "Player")
        {
            int value = Random.Range(0, 100);
            if (value<10)
            {
                CoolTimeDecrease(actor);
            }

        }
        if (ArtifactManager.S.DivisamentDouMonde.able && actor.tag == "Player" && turn == 3)
        {
            player.Reveal += 1;
            player.Fortune += 1;
        }
        if (ArtifactManager.S.DivisamentDouMonde.able && actor.tag == "Player" && turn == 4)
        {
            player.Reveal -= 1;
            player.Fortune -= 1;
        }



        if (ArtifactManager.S.PangeaGlobe.able && actor.tag == "Player" && turn == 6)
        {
            player.Reveal += 1;
        }
        if (ArtifactManager.S.PangeaGlobe.able && actor.tag == "Player" && turn == 9)
        {
            player.Reveal -= 1;
        }
        if (ArtifactManager.S.Compass.able && actor.tag == "Player" && turn == 6)
        {
            player.Fortune += 1;
        }
        if (ArtifactManager.S.Compass.able && actor.tag == "Player" && turn == 9)
        {
            player.Fortune -= 1;
        }
        if (ArtifactManager.S.AngelFeather.able && actor.tag == "Player" && turn == 1)
        {
            int value = Random.Range(0, 100);
            if (value<14)
            {
                Invincible invincible = ScriptableObject.CreateInstance<Invincible>();
                for (int i = 0; i < player.buffList.Count; i++)
                {
                    if ((player.buffList[i].bigBuffType == Buff.BigBuffType.DeBuff || player.buffList[i].bigBuffType == Buff.BigBuffType.DotDamage) && !player.buffList[i].isPermanent)
                    {
                        player.buffList[i].EndBuff(player);
                    }
                }

                for (int i = player.buffList.Count - 1; i >= 0; i--)
                {
                    if ((player.buffList[i].bigBuffType == Buff.BigBuffType.DeBuff || player.buffList[i].bigBuffType == Buff.BigBuffType.DotDamage) && !player.buffList[i].isPermanent)
                    {
                        player.buffList.Remove(player.buffList[i]);
                    }
                }
                invincible.buffName = "무적";
                invincible.bigBuffType = Buff.BigBuffType.Buff;
                invincible.buffType = Buff.BuffType.BB;
                invincible.remainType = Buff.RemainType.Turn;
                invincible.buffImageType = Buff.BuffImageType.Invincible;
                invincible.CastThisTurn = true;
                invincible.isPermanent = true;
                player.AddBuff(invincible, 1);
            }

        }
        if (ArtifactManager.S.InvisibleCloak.able && actor.tag == "Player" && (turn == 3))
        {
            Invisible Invisible = ScriptableObject.CreateInstance<Invisible>();
            Invisible.buffName = "은신";
            Invisible.bigBuffType = Buff.BigBuffType.Buff;
            Invisible.buffType = Buff.BuffType.BB;
            Invisible.remainType = Buff.RemainType.Turn;
            Invisible.buffImageType = Buff.BuffImageType.Invisible;
            Invisible.CastThisTurn = true;
            actor.AddBuff(Invisible, 1);
        }
        //if (ArtifactManager.S.InvisibleCloak.able && actor.tag == "Player" && turn == 4)
        //{
        //    player.Invisible -= 1;
        //}
        if (ArtifactManager.S.SwordOfTheJudgement.able && actor.tag == "Player" && turn == 1)
        {
            ActiveSwordOfJudgement(actor);
        }
        if (ArtifactManager.S.HornFromTheElysion.able && actor.tag == "Player")
        {
            ActiveHornfromtheElysion(actor);
        }
        if (ArtifactManager.S.HornofKirin.able && actor.tag == "Player" && turn%4==0&&turn>1)
        {
            for (int i = 0; i < actor.buffList.Count; i++)
            {
                if (actor.buffList[i].bigBuffType == Buff.BigBuffType.DeBuff && !actor.buffList[i].isPermanent)
                {
                    actor.buffList[i].EndBuff(actor);
                }
            }

            for (int i = actor.buffList.Count - 1; i >= 0; i--)
            {
                if (actor.buffList[i].bigBuffType == Buff.BigBuffType.DeBuff && !actor.buffList[i].isPermanent)
                {
                    actor.buffList.Remove(actor.buffList[i]);
                }
            }
        }


    }
    public void TurnSkillActive(Character actor, Character subject)
    {
        int value;
        List<Skill> pSkills = PickSkills(actor, Skill.ActiveTime.Turn, Skill.SpendType.ETC);

        for (int i = 0; i < pSkills.Count; i++)
        {
            if (pSkills[i].isLimit && pSkills[i].LimitNum <= 0) continue;

            if (!pSkills[i].ActiveCheck(actor, subject)) continue;// 조건 체크
            if (pSkills[i].waitTurn > turn) continue;//시작쿨타임 체크
            if (actor.TurnSkillsCooltime[i] > 0) continue;//쿨타임체크
            value = Random.Range(0, 100);
            if (pSkills[i].activePer > value)//시전확률체크
            {
                pSkills[i].ActiveSkill(actor, subject);
                actor.TurnSkillsCooltime[i] = pSkills[i].coolTime;
                actor.TurnSkillsLimit[i] -= 1;
                if (actor.skillList.Find(x => x.skillName == "집중") && pSkills[i].skillType == Skill.SkillType.Attack)
                {
                    actor.TurnSkillsCooltime[i] -= 2;
                    if (actor.TurnSkillsCooltime[i] < 0) actor.TurnSkillsCooltime[i] = 0;
                }
            }
        }
    }
}
