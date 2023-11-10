using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player S;

    public int mainProgress;//타워 진행도
    public int CurTowerNum;

    public Sprite PlayerImage;
    public Sprite PlayerTowerImage;
    public Sprite[] PlayerBattleAni;
    public int gold;
    public int exp;
    public int BonusGoldPer;
    public int BonusExpPer;

    public int TP;
    public int SP;
    public int UseSP;
    public int PP;
    public int CP;//공용스킬 최대 보유갯수
    public int useCP;// 공용스킬 보유 갯수
    public int jobClass;// 1차전직번호
    public bool classUp;
    public bool classUp1;//2차전직
    public bool classUp2;//3차전직
    public int MaxFloorNum;
    public int MinFloorNum;
    public int GambleNum;//겜블번호1:일반,2:고급3:올인
    public int GambleValue;

    public bool MonsterBookOn;
    public bool MoveFloorOn;
    public bool SecretWallOn;
    //마녀사냥꾼 스킬 스텟
    //아티팩트 스택
    public int MasterKeyNum;
    public int GoldenEggStack;
    public int BookStack;
    public int SpoonStack;
    public int reviveStack;


    public List<Skill> ClassPasiveSkills;
    public List<Skill> classSkillinven;
    public List<Skill> publicSkillinven;
    public List<List<Skill>> PreSetList = new List<List<Skill>> { };
    public int NowPresetNum;

    public List<string> EquipmentStrings;

    public Skill SwordNormalAttack;
    public Skill FistNormalAttack;
    public Skill ShootNormalAttack;
    public Skill PierceNormalAttack;

    //KO스킬
    public int KoStack;
    public int KoStackMax;
    public Skill koSkill;
    public bool KoCri;//크리시 스택 한번만
    public bool KoMiss;//회피시 스택 한번만
    //스토리용 변수
    public bool isMeetKing;
    public bool isMeetSaint;
    //스토리용 스트링 변수
    public string HomeTown;//"(출신지)"

    //보부상 템정보
    public List<int> Pedller;

    //로드변수
    public bool isDemo;
    public bool isLoad;
    public bool endTuto;
    public MarketPlace tutomarket;
    public int loadNum;
    public bool noSave;

    //퀘스트용 카운트
    public int artiGetNum;
    public int useSmokebomb;
    public int getSetArtifact;
    public int useGodstone;

    public enum PlayerJob
    {
        Inquisitor,//이단심문관
        Paladin,//성기사
        WitchHunter,//마녀사냥꾼
        NightShadow,//밤그림자
        Alchemist,//연금술사
        none

    }
    public PlayerJob job;
    public enum PlayerLocation
    {
        Town,
        Tower
    }
    public PlayerLocation playerLocation;
    private void Awake()
    {
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Slash))
        //{
        //    Player.S.ATK += 100;
        //    MaxFloorNum = 14;
        //}
        ChangePreset();

    }
    private void Start()
    {
       SetStatus();

        Pedller = new List<int> { 1, 1, 1, 1, 1,2,1,1,1,1,1,1,1 };
        //ClassUp();
        //ClassUp2();
        //ClassUp3();

        //QuestManager.S.AcceptQuest(5);
        //QuestManager.S.AcceptQuest(6);
        //QuestManager.S.AcceptQuest(7);
        //QuestManager.S.QuestFail(30);
        //AddItem.S.SearchItem("금 열쇠", 999);
        //AddItem.S.SearchItem("보석 열쇠", 999);
        //AddItem.S.SearchItem("돌 열쇠", 999);
        //AddItem.S.SearchItem("쇠 열쇠", 999);
        //AddItem.S.SearchItem("연막탄", 999);
        //AddItem.S.SearchItem("신석", 2);
        //AddItem.S.SearchItem("신비한 가루", 4);

        //AddItem.S.SearchItem("비밀방 두루마리", 999);

        //EquipmentUI.S.GetEquipment("최신예 검+3");
        //EquipmentUI.S.GetEquipment("날카로운 검+3");
        //ArtifactManager.S.GetArtifact("만능 열쇠");
        //ArtifactManager.S.GetArtifact("곡괭이");
        //ArtifactManager.S.GetArtifact("정령의 반지");


    }
    public void SetStatus()
    {
        switch (job)
        {
            case PlayerJob.Inquisitor:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        characterName = "이단심문관";
                        HomeTown = "고향";
                        break;
                    case Options.Language.Eng:
                        characterName = "Inquisitor";
                        HomeTown = "Home Town";
                        break;
                    default:
                        break;
                }

                hp = 800;
                ATK = 12;
                DEF = 12;
                HIT = 0;
                AVD = 4;
                CRC = 4;
                CRD = 50;
                POW = 3;
                SPD = 2;
                PP = 3;
                AP = 1;
                DP = 1;
                ARC = 100;
                HEL = 100;
                CP = 5;
                KoStackMax = 100;
                ResetTP();
                PlusTP();
                PlusTP();
                koSkill = SkillDic.S.D_AllSkillDic["말살"];
                break;
            case PlayerJob.Paladin:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        characterName = "십자군 기사";
                        HomeTown = "던 힐";
                        break;
                    case Options.Language.Eng:
                        characterName = "Crusader";
                        HomeTown = "Dawn Hill";
                        break;
                    default:
                        break;
                }

                hp = 1000;
                ATK = 12;
                DEF = 12;
                HIT = 0;
                AVD = 2;
                CRC = 2;
                CRD = 50;
                POW = 1;
                SPD = 1;
                PP = 3;
                AP = 1;
                DP = 1;
                ARC = 100;
                HEL = 100;
                CP = 5;
                KoStackMax = 100;
                normalAttack = FistNormalAttack;
                ResetTP();
                PlusTP();
                PlusTP();
                koSkill = SkillDic.S.D_AllSkillDic["충격"];

                break;
            case PlayerJob.WitchHunter:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        characterName = "마녀 사냥꾼";
                        HomeTown = "웨스트 랜드";
                        break;
                    case Options.Language.Eng:
                        characterName = "Witch Hunter";
                        HomeTown = "West Land";
                        break;
                    default:
                        break;
                }
                hp = 500;
                ATK = 15;
                DEF = 10;
                HIT = 0;
                AVD = 4;
                CRC = 4;
                CRD = 50;
                POW = 2;
                SPD = 3;
                PP = 3;
                AP = 1;
                DP = 1;
                ARC = 100;
                HEL = 100;
                CP = 5;
                KoStackMax = 100;
                ResetTP();
                PlusTP();
                PlusTP();
                koSkill = SkillDic.S.D_AllSkillDic["난도질"];
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
    }
    public void LevelUP()
    {
        switch (job)
        {
            case PlayerJob.Inquisitor:
                level += 1;
                hp += 300;
                ATK += 2;
                DEF += 2;
                break;
            case PlayerJob.Paladin:
                level += 1;
                hp += 400;
                ATK += 1;
                DEF += 3;
                break;
            case PlayerJob.WitchHunter:
                level += 1;
                hp += 250;
                ATK += 3;
                DEF += 1;
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
        if (ArtifactManager.S.GoldenSpoon.able)
        {
            hp += 100;
        }
        QuestManager.S.CountUp(QuestData.CountType.LevelUp, 1);
    }
    public void StatusUP(int _statusNum)
    {
        switch (job)
        {
           
            case PlayerJob.none:
                break;
            default:
                break;
        }

    }
    public void StatusDown(int _statusNum)
    {
        switch (job)
        {
           
            case PlayerJob.none:
                break;
            default:
                break;
        }

    }
    public void SetClass(int _class)
    {
        switch (_class)
        {
            case 0:
                job = PlayerJob.Inquisitor;
                SetStatus();
                break;
            case 1:
                job = PlayerJob.Paladin;
                SetStatus();
                break;
            case 2:
                job = PlayerJob.WitchHunter;
                SetStatus();
                break;
            case 3:
                job = PlayerJob.NightShadow;
                SetStatus();
                break;
            case 4:
                job = PlayerJob.Alchemist;
                SetStatus();
                break;
            default:
                break;
        }
    }
    public void GetGold(int _num)
    {
        float curgold = _num;
        curgold += (curgold * BonusGoldPer) *0.01f;
        gold+=Mathf.CeilToInt(curgold);
    }

    public void GetExp(int _num)
    {
        float curexp = _num;
        curexp += (curexp * BonusExpPer) *0.01f;
        exp += Mathf.CeilToInt(curexp);
    }
    public void SpendGold(int _num)
    {
        gold -= _num;
        QuestManager.S.GoldSpend(_num);
    }
    public void SpendExp(int _num)
    {
        exp -= _num;
    }
    public void PlusMainProgress()
    {
        mainProgress += 1;
        QuestManager.S.AcceptQuest(mainProgress);
        Debug.Log("메인프로그레스 증가");
    }
    public void PlusTP()
    {
        TP += 1;
        PreSetList.Add(new List<Skill>());
        Debug.Log("TP+1");
    }
    public void ResetTP()
    {
        TP = 0;
        PreSetList.Clear();
    }
    public void ClassUp()
    {
        List<string> skills = new List<string>();
        switch (job)
        {
            case PlayerJob.Inquisitor:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "재판관";
                                break;
                            case Options.Language.Eng:
                                characterName = "Judge";
                                break;
                            default:
                                break;
                        }
                        normalAttack = SwordNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["종교재판"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["형벌의 채찍"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["낙인"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["냉혹"]);
                        skills.Add("종교재판");
                        skills.Add("형벌의 채찍");
                        skills.Add("낙인");
                        skills.Add("냉혹");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("교차 베기+");
                        skills.Add("교차 막기+");
                        skills.Add("관찰+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "선교사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Missonary";
                                break;
                            default:
                                break;
                        }
                        normalAttack = FistNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["포교"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["슬랫지해머"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["힘의 기적"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["신앙 치료"]);
                        skills.Add("포교");
                        skills.Add("슬랫지해머");
                        skills.Add("힘의 기적");
                        skills.Add("신앙 치료");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("신념+");
                        skills.Add("부정+");
                        skills.Add("회복 가스+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "요원";
                                break;
                            case Options.Language.Eng:
                                characterName = "Agent";
                                break;
                            default:
                                break;
                        }
                        normalAttack = SwordNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["해결사"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["폭탄"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["각성제"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["동체 시력"]);
                        skills.Add("해결사");
                        skills.Add("폭탄");
                        skills.Add("각성제");
                        skills.Add("동체 시력");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("권총+");
                        skills.Add("1회용 방패+");
                        skills.Add("직관+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.Paladin:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "검투사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Gladiator";
                                break;
                            default:
                                break;
                        }
                        normalAttack = PierceNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["창의 벽"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["치명적인 찌르기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["성급함"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["역습"]);
                        skills.Add("창의 벽");
                        skills.Add("치명적인 찌르기");
                        skills.Add("성급함");
                        skills.Add("역습");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("휩쓸기+");
                        skills.Add("방패 막기+");
                        skills.Add("심호흡+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "왕국 기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Empire Knight";
                                break;
                            default:
                                break;
                        }
                        normalAttack = SwordNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["방패 숙련"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["대검"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["정신력"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["위치 사수"]);
                        skills.Add("방패 숙련");
                        skills.Add("대검");
                        skills.Add("정신력");
                        skills.Add("위치 사수");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("무기 정비+");
                        skills.Add("강철 무장+");
                        skills.Add("방패 타격+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "성기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Pladin";
                                break;
                            default:
                                break;
                        }
                        normalAttack = FistNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["성자"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["천벌"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["보복의 고리"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["보호 기적"]);
                        skills.Add("성자");
                        skills.Add("천벌");
                        skills.Add("보복의 고리");
                        skills.Add("보호 기적");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("치유 기적+");
                        skills.Add("기도+");
                        skills.Add("신성한 오라+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.WitchHunter:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "저격수";
                                break;
                            case Options.Language.Eng:
                                characterName = "Sniper";
                                break;
                            default:
                                break;
                        }
                        normalAttack = ShootNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["긴 사정거리"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["연사"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["명사수"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["독화살"]);//독화살로 변경
                        skills.Add("긴 사정거리");
                        skills.Add("연사");
                        skills.Add("명사수");
                        skills.Add("독화살");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("추적자+");
                        skills.Add("흔적 찾기+");
                        skills.Add("저격+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "레인저";
                                break;
                            case Options.Language.Eng:
                                characterName = "Ranger";
                                break;
                            default:
                                break;
                        }
                        normalAttack = ShootNormalAttack;

                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["길잡이"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["선제 사격"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["기동 사격"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["마름쇠 함정"]);
                        skills.Add("길잡이");
                        skills.Add("선제 사격");
                        skills.Add("기동 사격");
                        skills.Add("마름쇠 함정");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("캠핑 전문가+");
                        skills.Add("엄폐+");
                        skills.Add("응급 처치+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "악마 사냥꾼";
                                break;
                            case Options.Language.Eng:
                                characterName = "Demon Hunter";
                                break;
                            default:
                                break;
                        }
                        normalAttack = SwordNormalAttack;
                        ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic["복수자"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["데몬슬레이어"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["풍요 의식"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["성수 폭탄"]);
                        skills.Add("복수자");
                        skills.Add("데몬슬레이어");
                        skills.Add("풍요 의식");
                        skills.Add("성수 폭탄");
                        GetSkillsUI.S.AddStack(skills, false);
                        skills.Clear();
                        skills.Add("제사용 단검+");
                        skills.Add("사냥 의식+");
                        skills.Add("부적+");
                        GetSkillsUI.S.AddStack(skills, true);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i] = SkillDic.S.ChangePlusSkill(skillList[i].skillName);
        }
        for (int i = 0; i < classSkillinven.Count; i++)
        {
            classSkillinven[i] = SkillDic.S.ChangePlusSkill(classSkillinven[i].skillName);
        }
        for (int i = 0; i < PreSetList.Count; i++)
        {
            for (int j = 0; j < PreSetList[i].Count; j++)
            {
                PreSetList[i][j] = SkillDic.S.ChangePlusSkill(PreSetList[i][j].skillName);
            }
        }
        classUp = true;
        QuestManager.S.CountUpQuestByID(18);
    }
    public void ClassUp3()
    {
        classUp2 = true;
        List<string> skills = new List<string>();

        switch (job)
        {
            case PlayerJob.Inquisitor:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "재판관";
                                break;
                            case Options.Language.Eng:
                                characterName = "Judge";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["종교재판++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["사형 선고"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["심문"]);
                        skills.Add("종교재판++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("사형 선고");
                        skills.Add("심문");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "선교사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Missonary";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["포교++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["구원의 일격"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["영광의 기적"]);
                        skills.Add("포교++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("구원의 일격");
                        skills.Add("영광의 기적");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "요원";
                                break;
                            case Options.Language.Eng:
                                characterName = "Agent";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["해결사++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["마탄의 사수"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["결투가"]);
                        skills.Add("해결사++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("마탄의 사수");
                        skills.Add("결투가");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살+++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.Paladin:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "검투사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Gladiator";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["창의 벽++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["할버드 장인"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["투창 연계"]);
                        skills.Add("창의 벽++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("할버드 장인");
                        skills.Add("투창 연계");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "왕국 기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Empire Knight";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["방패 숙련++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["캡틴 엠파이어"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["영웅심"]);
                        skills.Add("방패 숙련++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("캡틴 엠파이어");
                        skills.Add("영웅심");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "성기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Pladin";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["성자++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["별의 기적"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["수호천사"]);
                        skills.Add("성자++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("별의 기적");
                        skills.Add("수호천사");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격+++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.WitchHunter:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "저격수";
                                break;
                            case Options.Language.Eng:
                                characterName = "Sniper";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["긴 사정거리++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["지뢰"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["헤드샷"]);
                        skills.Add("긴 사정거리++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("지뢰");
                        skills.Add("헤드샷");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "레인저";
                                break;
                            case Options.Language.Eng:
                                characterName = "Ranger";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["길잡이++"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["난사"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["소멸"]);
                        skills.Add("길잡이++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("난사");
                        skills.Add("소멸");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "악마 사냥꾼";
                                break;
                            case Options.Language.Eng:
                                characterName = "Demon Hunter";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = SkillDic.S.D_AllSkillDic["복수자++"];
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["희생 의식"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["데몬베인"]);
                        skills.Add("복수자++");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("희생 의식");
                        skills.Add("데몬베인");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질+++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i] = SkillDic.S.ChangePlusSkill(skillList[i].skillName);
        }
        for (int i = 0; i < classSkillinven.Count; i++)
        {
            classSkillinven[i] = SkillDic.S.ChangePlusSkill(classSkillinven[i].skillName);
        }
        for (int i = 0; i < PreSetList.Count; i++)
        {
            for (int j = 0; j < PreSetList[i].Count; j++)
            {
                PreSetList[i][j] = SkillDic.S.ChangePlusSkill(PreSetList[i][j].skillName);
            }
        }
    }

    public void ClassUp2()
    {
        classUp1 = true;
        List<string> skills = new List<string>();

        switch (job)
        {
            case PlayerJob.Inquisitor:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "재판관";
                                break;
                            case Options.Language.Eng:
                                characterName = "Judge";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["종교재판+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["비탄"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["원죄"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["파문"]);
                        skills.Add("종교재판+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("비탄");
                        skills.Add("원죄");
                        skills.Add("파문");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "선교사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Missonary";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["포교+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["홀인원"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["선행 기적"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["구호기사단"]);
                        skills.Add("포교+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("홀인원");
                        skills.Add("선행 기적");
                        skills.Add("구호기사단");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "요원";
                                break;
                            case Options.Language.Eng:
                                characterName = "Agent";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["해결사+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["쌍권총"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["작전"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["알약"]);
                        skills.Add("해결사+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("쌍권총");
                        skills.Add("작전");
                        skills.Add("알약");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["말살++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.Paladin:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "검투사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Gladiator";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["창의 벽+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["베고 찌르기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["회오리 창"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["관통함"]);
                        skills.Add("창의 벽+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("베고 찌르기");
                        skills.Add("회오리 창");
                        skills.Add("관통함");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "왕국 기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Empire Knight";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["방패 숙련+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["도약 강타"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["철벽 자세"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["중갑 숙련"]);
                        skills.Add("방패 숙련+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("도약 강타");
                        skills.Add("철벽 자세");
                        skills.Add("중갑 숙련");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "성기사";
                                break;
                            case Options.Language.Eng:
                                characterName = "Pladin";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0] = (SkillDic.S.D_AllSkillDic["성자+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["무쇠 기적"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["갑옷 기적"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["순례자"]);
                        skills.Add("성자+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("무쇠 기적");
                        skills.Add("갑옷 기적");
                        skills.Add("순례자");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["충격++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.WitchHunter:
                switch (jobClass)
                {
                    case 1://저격수
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "저격수";
                                break;
                            case Options.Language.Eng:
                                characterName = "Sniper";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0]=(SkillDic.S.D_AllSkillDic["긴 사정거리+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["실명 화살"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["폭발 화살"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["집중"]);
                        skills.Add("긴 사정거리+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("실명 화살");
                        skills.Add("폭발 화살");
                        skills.Add("집중");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질++"];
                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "레인저";
                                break;
                            case Options.Language.Eng:
                                characterName = "Ranger";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0]=(SkillDic.S.D_AllSkillDic["길잡이+"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["삼연발 화살"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["다리 걸기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["곡예 숙련"]);
                        skills.Add("길잡이+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("삼연발 화살");
                        skills.Add("다리 걸기");
                        skills.Add("곡예 숙련");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질++"];
                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                characterName = "악마 사냥꾼";
                                break;
                            case Options.Language.Eng:
                                characterName = "Demon Hunter";
                                break;
                            default:
                                break;
                        }
                        ClassPasiveSkills[0]=SkillDic.S.D_AllSkillDic["복수자+"];
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["은빛 다트"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["행운 의식"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["주술 보호"]);
                        skills.Add("복수자+");
                        GetSkillsUI.S.AddStack(skills, true);
                        skills.Clear();
                        skills.Add("은빛 다트");
                        skills.Add("행운 의식");
                        skills.Add("주술 보호");
                        GetSkillsUI.S.AddStack(skills, false);
                        koSkill = SkillDic.S.D_AllSkillDic["난도질++"];
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i] = SkillDic.S.ChangePlusSkill(skillList[i].skillName);
        }
        for (int i = 0; i < classSkillinven.Count; i++)
        {
            classSkillinven[i] = SkillDic.S.ChangePlusSkill(classSkillinven[i].skillName);
        }
        for (int i = 0; i < PreSetList.Count; i++)
        {
            for (int j = 0; j < PreSetList[i].Count; j++)
            {
                PreSetList[i][j] = SkillDic.S.ChangePlusSkill(PreSetList[i][j].skillName);
            }
        }
        QuestManager.S.CountUpQuestByID(18);
    }

    public void SelectPreset(int _num)
    {
        if (_num>TP)
        {
            return;
        }
       skillList.Clear();
       skillList = new List<Skill>(PreSetList[_num]);
       NowPresetNum = _num;
        if (playerLocation==PlayerLocation.Tower)
        {
            TowerMap.S.nowPreset.text = "Skill Preset : " + (Player.S.NowPresetNum + 1).ToString();
        }
    }

    public void ChangePreset()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectPreset(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectPreset(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectPreset(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectPreset(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectPreset(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectPreset(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectPreset(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectPreset(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectPreset(8);
        }
    }

    public void TutoSet()
    {
        GetBasicClassSkill(0);
        PreSetList[0].Add(classSkillinven[0]);
        PreSetList[1].Add(classSkillinven[0]);
        SelectPreset(0);
        hp += 150;
        AddItem.S.SearchItem("연막탄", 10);
        tutomarket = GameObject.Find("일용품 상인").GetComponent<MarketPlace>();
        tutomarket.slots[0].GetComponent<TownShopSlot>().itemNum = 0;
        tutomarket.slots[0].GetComponent<TownShopSlot>().T_itemNum.text = "0";


    }
    public void KoStackUp(int _num)
    {
        KoStack += _num;
        if (KoStack>KoStackMax)
        {
            KoStack = KoStackMax;
        }
    }

    public void GetBasicClassSkill(int _num)
    {
        List<string> skills = new List<string>();
        switch (job)
        {

            case PlayerJob.Inquisitor:
                switch (_num)
                {
                    case 0:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["교차 베기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["신념"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["권총"]);
                        skills.Add("교차 베기");
                        skills.Add("신념");
                        skills.Add("권총");
                        GetSkillsUI.S.AddStack(skills, false);

                        break;
                    case 1:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["교차 막기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["부정"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["1회용 방패"]);
                        skills.Add("교차 막기");
                        skills.Add("부정");
                        skills.Add("1회용 방패");
                        GetSkillsUI.S.AddStack(skills, false);

                        break;
                    case 2:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["관찰"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["회복 가스"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["직관"]);
                        skills.Add("관찰");
                        skills.Add("회복 가스");
                        skills.Add("직관");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;

                    default:
                        break;
                }
                break;
            case PlayerJob.Paladin:
                switch (_num)
                {
                    case 0:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["휩쓸기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["방패 막기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["기도"]);
                        skills.Add("휩쓸기");
                        skills.Add("방패 막기");
                        skills.Add("기도");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;
                    case 1:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["무기 정비"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["방패 타격"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["치유 기적"]);
                        skills.Add("무기 정비");
                        skills.Add("방패 타격");
                        skills.Add("치유 기적");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;

                    case 2:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["심호흡"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["강철 무장"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["신성한 오라"]);
                        skills.Add("심호흡");
                        skills.Add("강철 무장");
                        skills.Add("신성한 오라");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;

                    default:
                        break;
                }
                break;
            case PlayerJob.WitchHunter:
                switch (_num)
                {
                    case 0:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["저격"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["캠핑 전문가"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["부적"]);
                        skills.Add("저격");
                        skills.Add("캠핑 전문가");
                        skills.Add("부적");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;
                    case 1:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["흔적 찾기"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["엄폐"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["제사용 단검"]);
                        skills.Add("흔적 찾기");
                        skills.Add("엄폐");
                        skills.Add("제사용 단검");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;
                    case 2:
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["추적자"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["응급 처치"]);
                        classSkillinven.Add(SkillDic.S.D_AllSkillDic["사냥 의식"]);
                        skills.Add("추적자");
                        skills.Add("응급 처치");
                        skills.Add("사냥 의식");
                        GetSkillsUI.S.AddStack(skills, false);
                        break;

                    default:
                        break;
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
    }
    public void SetNormalAttackKO()
    {
        switch (job)
        {
            case PlayerJob.Inquisitor:
                switch (jobClass)
                {
                    case 0:
                        normalAttack = SwordNormalAttack;
                        break;
                    case 1:
                        normalAttack = SwordNormalAttack;
                        break;
                    case 2:
                        normalAttack = FistNormalAttack;
                        break;
                    case 3:
                        normalAttack = SwordNormalAttack;
                        break;

                    default:
                        break;
                }
                if (classUp2)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["말살+++"];
                }
                else if(classUp1)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["말살++"];
                }
                else if(classUp)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["말살+"];
                }
                else
                {
                    koSkill = SkillDic.S.D_AllSkillDic["말살"];
                }
                break;
            case PlayerJob.Paladin:
                switch (jobClass)
                {
                    case 0:
                        normalAttack = FistNormalAttack;
                        break;
                    case 1:
                        normalAttack = PierceNormalAttack;
                        break;
                    case 2:
                        normalAttack = SwordNormalAttack;
                        break;
                    case 3:
                        normalAttack = FistNormalAttack;
                        break;

                    default:
                        break;
                }
                if (classUp2)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["충격+++"];
                }
                else if (classUp1)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["충격++"];
                }
                else if (classUp)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["충격+"];
                }
                else
                {
                    koSkill = SkillDic.S.D_AllSkillDic["충격"];
                }
                break;
            case PlayerJob.WitchHunter:
                switch (jobClass)
                {
                    case 0:
                        normalAttack = SwordNormalAttack;
                        break;
                    case 1:
                        normalAttack = ShootNormalAttack;
                        break;
                    case 2:
                        normalAttack = ShootNormalAttack;
                        break;
                    case 3:
                        normalAttack = SwordNormalAttack;
                        break;

                    default:
                        break;
                }
                if (classUp2)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["난도질+++"];
                }
                else if (classUp1)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["난도질++"];
                }
                else if (classUp)
                {
                    koSkill = SkillDic.S.D_AllSkillDic["난도질+"];
                }
                else
                {
                    koSkill = SkillDic.S.D_AllSkillDic["난도질"];
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                break;
        }
    }

    public string ReturnCharacterName(PlayerJob _job,int _jobClass)
    {
        switch (_job)
        {
            case PlayerJob.Inquisitor:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        switch (_jobClass)
                        {
                            case 0:
                                return "이단심문관";
                            case 1:
                                return "재판관";
                            case 2:
                                return "선교사";
                            case 3:
                                return "요원";
                            default:
                                break;
                        }
                        break;
                    case Options.Language.Eng:
                        switch (_jobClass)
                        {
                            case 0:
                                return "Inquisitor";
                            case 1:
                                return "Judge";
                            case 2:
                                return "Missonary";
                            case 3:
                                return "Agent";
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.Paladin:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        switch (_jobClass)
                        {
                            case 0:
                                return "십자군 기사";
                            case 1:
                                return "검투사";
                            case 2:
                                return "왕국 기사";
                            case 3:
                                return "성기사";
                            default:
                                break;
                        }
                        break;
                    case Options.Language.Eng:
                        switch (_jobClass)
                        {
                            case 0:
                                return "Crusader";
                            case 1:
                                return "Gladiator";
                            case 2:
                                return "Empire Knight";
                            case 3:
                                return "Paladin";
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.WitchHunter:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        switch (_jobClass)
                        {
                            case 0:
                                return "마녀사냥꾼";
                            case 1:
                                return "저격수";
                            case 2:
                                return "레인저";
                            case 3:
                                return "악마사냥꾼";
                            default:
                                break;
                        }
                        break;
                    case Options.Language.Eng:
                        switch (_jobClass)
                        {
                            case 0:
                                return "Witch Hunter";
                            case 1:
                                return "Sniper";
                            case 2:
                                return "Ranger";
                            case 3:
                                return "Demon Hunter";
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case PlayerJob.NightShadow:
                break;
            case PlayerJob.Alchemist:
                break;
            case PlayerJob.none:
                break;
            default:
                return "";
                
        }

        return "";
    }

}
