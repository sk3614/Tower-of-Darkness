using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveData
{
    //Player
    public string SaveDate;

    public string characterName;
    public int mainProgress;
    public int curTowerNum;
    public int level;
    public int hp;
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
    public bool MonsterBookOn;
    public bool MoveFloorOn;
    public bool SecretWallOn;
    public int MasterKeyNum;
    public int GoldenEggStack;
    public int BookStack;
    public int SpoonStack;
    public int reviveStack;
    public bool classup;
    public bool classup1;
    public bool classup2;
    public int caltroptoken;
    public Player.PlayerJob job;
    public Player.PlayerLocation playerLocation;
    //
    public int gold;
    public int exp;
    public int BonusGoldPer;
    public int BonusExpPer;
    public int SP;
    public int UseSP;
    public int PP;
    public int CP;//공용스킬 최대 보유갯수
    public int useCP;// 공용스킬 보유 갯수
    public int jobClass;// 직업차수
    public int secretWallChance;
    public int MaxFloorNum;
    public int RemainMaidHealNum;
    public int KoStack;
    public int KostackMax;
    public List<string> EquipmentStrings=new List<string>();
    public bool endTuto;
    public int GambleNum;//겜블 종류
    public int GambleValue;//겜블 계수
    public int artiGetNum;
    public int useSmokebomb;
    public int getSetArtifact;
    public int useGodstone;



    //스킬
    public List<string> ClassPasiveSkills=new List<string>();
    public List<string> C_skills=new List<string>();
    public List<string> P_skills= new List<string>();

    //프리셋
    public int presetNum;
    public int nowPresetNum;
    public List<string> PresetSkills1=new List<string>();
    public List<string> PresetSkills2 = new List<string>();
    public List<string> PresetSkills3 = new List<string>();
    public List<string> PresetSkills4 = new List<string>();
    public List<string> PresetSkills5 = new List<string>();
    public List<string> PresetSkills6= new List<string>();
    public List<string> PresetSkills7 = new List<string>();
    public List<string> PresetSkills8 = new List<string>();
    public List<string> PresetSkills9 = new List<string>();

    //타운 상점
    //길드상점
    public List<string> GuildName = new List<string>();
    public List<int> GuildNum = new List<int>();
    public List<int> GuildPrice = new List<int>();
    //도구상점
    public List<int> Peddler = new List<int>();
    public List<string> ToolName = new List<string>();
    public List<int> ToolNum = new List<int>();
    public List<int> ToolPrice = new List<int>();
    //항구상인
    public List<string> PortName = new List<string>();
    public List<int> PortNum = new List<int>();
    public List<int> PortPrice = new List<int>();
    //보부상
    public List<string> ShopName = new List<string>();
    public List<int> ShopNum = new List<int>();
    public List<int> ShopPrice = new List<int>();
    //암시장
    public List<string> BlackName = new List<string>();
    public List<int> BlackKind = new List<int>();//0아이템,1스킬,2아티,3장비
    public List<int> BlackNum = new List<int>();
    public List<int> BlackPrice = new List<int>();

    //전도사
    public List<string> SkillCenterSkills = new List<string>();
    public List<string> SkillCenterSkills2 = new List<string>();
    public int buyPrice;
    public int plusPrice;
    public int buyPrice2;
    public int plusPrice2;
    //인벤
    public List<string> UseItemName = new List<string>();
    public List<int> UseItemNum = new List<int>();

    public List<string> importantName = new List<string>();
    public List<int> importantNum = new List<int>();

    public List<string> KeyName = new List<string>();
    public List<int> KeyNum = new List<int>();

    //아티팩트
    public List<bool> getIs = new List<bool>();
    public List<bool> ArtiAble = new List<bool>();

    //퀘스트
    public List<QuestData.QuestState> questStates = new List<QuestData.QuestState>();
    public List<int> objectNum = new List<int>();
    public List<int> objectCurNum = new List<int>();

    //타워변수
    public bool npc13;
    public bool npc24;
    public bool npc27;
    public bool on21Floor;
    public bool floor12Shop;
    public int Shop1AtkGold = 10;
    public int Shop1DefGold = 10;
    public int Shop1HpGold = 10;
    public int Shop2AtkGold = 25;
    public int Shop2DefGold = 25;
    public int Shop3AtkGold = 40;
    public int Shop3DefGold = 40;
    public int Shop4AtkGold = 40;
    public int Shop4DefGold = 100;
    public int Shop5AtkGold = 70;
    public int Shop5DefGold = 70;
    public int Shop6AtkGold = 180;
    public int Shop6DefGold = 180;
    public bool floor9Shop;
    public bool floor9Shop2;
    public bool isMeetFairy=false;
    public bool isClassUp=false;
    public bool market11;
    public string arti1;
    public string arti2;
    public string arti3;
    public bool isfloorB20;
    public bool isgetallitemB21;
    public bool changeStair;
    public int golemKillCount;
    public bool T7F4;
    public bool npc72;
    public int Shop7AtkGold = 100;
    public int Shop7DefGold = 100;
    public string T4Arti1;
    public string T4Arti2;
    public string T4Arti3;
    public bool market20;
    public bool floor5shop;
    public bool KillLady;
    public bool npc87;
    public int Shop8AtkGold = 270;
    public int Shop8DefGold = 270;
    public bool GetStats;

    public int MinFloorNum;
    public List<int> TownTMI = new List<int>();
}
public class SaveNLoad : MonoBehaviour
{
    public static SaveNLoad S;
    public SaveData saveData=new SaveData();
    public List<Sprite> witchHunterImages;
    public List<Sprite> crusaderImages;
    public List<Sprite> InquisitorImages;
    public PeddlerTable portshop;
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }
    private void Start()
    {

        if (Player.S.isLoad)
        {
            Load(Player.S.loadNum);
            Player.S.isLoad = false;
        }
        else if(!Player.S.noSave && Player.S.playerLocation == Player.PlayerLocation.Town)
        {
            Save(1);
        }

    }
    public void Save(int _num)
    {
        saveData = new SaveData();

        saveData.SaveDate = System.DateTime.Now.ToString();

        saveData.characterName = Player.S.characterName;
        saveData.mainProgress = Player.S.mainProgress;
        saveData.curTowerNum = Player.S.CurTowerNum;
        saveData.level= Player.S.level;
        saveData.hp = Player.S.hp;
        saveData.bp = Player.S.bp;//디펜스포인트
        saveData.bpReset = Player.S.bpReset;//매턴BP초기화수치
        saveData.ATK = Player.S.ATK;//공격수치
        saveData.DEF = Player.S.DEF;//방어수치
        saveData.HIT = Player.S.HIT;//적중수치
        saveData.AVD = Player.S.AVD;//회피수치
        saveData.SPD = Player.S.SPD;//스피드
        saveData.RES = Player.S.RES;//저항
        saveData.CRC = Player.S.CRC;//치명타율
        saveData.CRD = Player.S.CRD;//치명타피해량
        saveData.CDR = Player.S.CDR;//치명타피해량감소
        saveData.CRR = Player.S.CRR;//치명타저항
        saveData.POW = Player.S.POW;//마력
        saveData.PIE = Player.S.PIE;//관통(bp를 무시하고 데미지)
        saveData.BLK = Player.S.BLK;//저지(관통효과 저지)
        saveData.ICD = Player.S.ICD;//주는피해 증가
        saveData.DCD = Player.S.DCD;//받는피해 감소
        saveData.VAM = Player.S.VAM;//흡혈
        saveData.HEL = Player.S.HEL;//회복량
        saveData.ARC = Player.S.ARC;//방어등급
        saveData.REG = Player.S.REG;//재생
        saveData.AP = Player.S.AP;//액션포인트
        saveData.DP = Player.S.DP;//디펜스포인트
        saveData.KoStack = Player.S.KoStack;
        saveData.KostackMax = Player.S.KoStackMax;
        saveData.RG_stun = Player.S.RG_stun;
        saveData.RG_Sleep = Player.S.RG_Sleep;
        saveData.RG_Paralyze = Player.S.RG_Paralyze;
        saveData.RG_Poison = Player.S.RG_Poison;
        saveData.RG_Bleed = Player.S.RG_Bleed;
        saveData.RG_Erosion = Player.S.RG_Erosion;
        saveData.RG_Curse = Player.S.RG_Curse;
        saveData.RG_Daze = Player.S.RG_Daze;
        saveData.RG_Doom = Player.S.RG_Doom;
        saveData.RG_Misfortune = Player.S.RG_Misfortune;
        saveData.RG_Dispel = Player.S.RG_Dispel;
        saveData.RG_Infect = Player.S.RG_Infect;
        saveData.RG_Fear = Player.S.RG_Fear;
        saveData.RG_Burn = Player.S.RG_Burn;
        saveData.MonsterBookOn = Player.S.MonsterBookOn;
        saveData.MoveFloorOn = Player.S.MoveFloorOn;
        saveData.SecretWallOn = Player.S.SecretWallOn;
        saveData. MasterKeyNum = Player.S.MasterKeyNum;
        saveData.GoldenEggStack = Player.S.GoldenEggStack;
        saveData.BookStack = Player.S.BookStack;
        saveData.SpoonStack = Player.S.SpoonStack;
        saveData.reviveStack = Player.S.reviveStack;
        saveData.job = Player.S.job;
        saveData.playerLocation = Player.S.playerLocation;
        saveData.gold = Player.S.gold;
        saveData.exp = Player.S.exp;
        saveData.BonusGoldPer = Player.S.BonusGoldPer;
        saveData.BonusExpPer = Player.S.BonusExpPer;
        saveData.SP = Player.S.SP;
        saveData.UseSP = Player.S.UseSP;
        saveData.PP = Player.S.PP;
        saveData.CP = Player.S.CP;//공용스킬 최대 보유갯수
        saveData.useCP = Player.S.useCP;// 공용스킬 보유 갯수
        saveData.jobClass = Player.S.jobClass;// 직업차수
        saveData.MaxFloorNum = Player.S.MaxFloorNum;
        saveData.MinFloorNum = Player.S.MinFloorNum;
        saveData.classup = Player.S.classUp;
        saveData.classup1 = Player.S.classUp1;
        saveData.classup2 = Player.S.classUp2;
        saveData.endTuto = Player.S.endTuto;
        saveData.EquipmentStrings = Player.S.EquipmentStrings;
        saveData.GambleNum = Player.S.GambleNum;
        saveData.GambleValue = Player.S.GambleValue;
        saveData.artiGetNum = Player.S.artiGetNum;
        saveData.useSmokebomb = Player.S.useSmokebomb;
        saveData.getSetArtifact = Player.S.getSetArtifact;
        saveData.useGodstone = Player.S.useGodstone;

        for (int i = 0; i < Player.S.Pedller.Count; i++)
        {
            saveData.Peddler.Add(Player.S.Pedller[i]);
        }

        //스킬 세이브
        for (int i = 0; i < Player.S.ClassPasiveSkills.Count; i++)
        {
            saveData.ClassPasiveSkills.Add(Player.S.ClassPasiveSkills[i].skillName);
        }
        for (int i = 0; i < Player.S.classSkillinven.Count; i++)
        {
            saveData.C_skills.Add(Player.S.classSkillinven[i].skillName);
        }
        for (int i = 0; i < Player.S.publicSkillinven.Count; i++)
        {
            saveData.P_skills.Add(Player.S.publicSkillinven[i].skillName);
        }

        //프리셋 세이브
        saveData.presetNum = Player.S.TP;
        saveData.nowPresetNum = Player.S.NowPresetNum;
  

        //for (int i = 0; i <SkillUI.S.PreSets[0].skills.Length; i++)
        //{
        //    saveData.PresetSkills1.Add(SkillUI.S.PreSets[0].skills[i].skillName);
        //}
        //for (int i = 0; i < SkillUI.S.PreSets[1].skills.Length; i++)
        //{
        //    saveData.PresetSkills2.Add(SkillUI.S.PreSets[1].skills[i].skillName);
        //}
        for (int i = 0; i < Player.S.PreSetList.Count; i++)
        {
            for (int j = 0; j < Player.S.PreSetList[i].Count; j++)
            {
                switch (i)
                {
                    case 0:
                        saveData.PresetSkills1.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 1:
                        saveData.PresetSkills2.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 2:
                        saveData.PresetSkills3.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 3:
                        saveData.PresetSkills4.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 4:
                        saveData.PresetSkills5.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 5:
                        saveData.PresetSkills6.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 6:
                        saveData.PresetSkills7.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 7:
                        saveData.PresetSkills8.Add(Player.S.PreSetList[i][j].skillName);
                        break;
                    case 8:
                        saveData.PresetSkills9.Add(Player.S.PreSetList[i][j].skillName);
                        break;

                    default:
                        break;
                }
            }
        }


        //광장 세이브
        saveData.TownTMI = Plaza.S.npcNums;

        //인벤 세이브


        //타운 상점 세이브
        List<MarketPlace> markets = GameObject.Find("Town").GetComponent<TownUI>().markets;
        for (int i = 0; i < markets.Count; i++)
        {
            for (int j = 0; j < markets[i].slots.Count; j++)
            {
                switch (i)
                {
                    case 0:
                        saveData.GuildName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().item.itemName);
                        saveData.GuildNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                        saveData.GuildPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                        break;
                    case 1:
                        //saveData.ToolName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().skill.skillName);
                        //saveData.ToolNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                        //saveData.ToolPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                        break;
                    case 2:
                        saveData.PortName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().artifact.artifactName);
                        saveData.PortNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                        saveData.PortPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                        break;
                    case 3:
                        saveData.ShopName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().equipment.equipName);
                        saveData.ShopNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                        saveData.ShopPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                        break;
                    case 4:
                        switch (markets[i].slots[j].GetComponent<TownShopSlot>().kind)
                        {
                            case TownShopSlot.Kind.Item:
                                saveData.BlackName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().item.itemName);
                                saveData.BlackKind.Add(1);
                                saveData.BlackNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                                saveData.BlackPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                                break;
                            case TownShopSlot.Kind.Skill:
                                saveData.BlackName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().skill.skillName);
                                saveData.BlackKind.Add(2);
                                saveData.BlackNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                                saveData.BlackPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                                break;
                            case TownShopSlot.Kind.Arti:
                                saveData.BlackName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().artifact.artifactName);
                                saveData.BlackKind.Add(3);
                                saveData.BlackNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                                saveData.BlackPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                                break;
                            case TownShopSlot.Kind.Equip:
                                saveData.BlackName.Add(markets[i].slots[j].GetComponent<TownShopSlot>().equipment.equipName);
                                saveData.BlackKind.Add(4);
                                saveData.BlackNum.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemNum);
                                saveData.BlackPrice.Add(markets[i].slots[j].GetComponent<TownShopSlot>().itemPrice);
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        //전도사 세이브
        for (int i = 0; i < TrainingRoom.S.skills.Count; i++)
        {
            saveData.SkillCenterSkills.Add(TrainingRoom.S.skills[i].skillName);
        }
        for (int i = 0; i < TrainingRoom.S.skills2.Count; i++)
        {
            saveData.SkillCenterSkills2.Add(TrainingRoom.S.skills2[i].skillName);
        }
        saveData.buyPrice = TrainingRoom.S.buyPrice;
        saveData.plusPrice = TrainingRoom.S.plusPrice;
        saveData.buyPrice2 = TrainingRoom.S.buyPrice2;
        saveData.plusPrice2 = TrainingRoom.S.plusPrice2;
        //인벤 세이브
        List<Inven> invens = new List<Inven>();
        invens.Add(Inventory.S.allItemInven);
        invens.Add(Inventory.S.ItemInven);
        invens.Add(Inventory.S.eventItemInven);
        for (int i = 0; i < invens.Count; i++)
        {
            for (int j = 0; j < invens[i].slots.Count; j++)
            {
                switch (i)// = Use, 1 = Key, 2 =event
                {
                    case 0:
                        saveData.UseItemName.Add(invens[i].slots[j].item.itemName);
                        saveData.UseItemNum.Add(invens[i].slots[j].itemCount);
                        break;
                    case 1:
                        saveData.KeyName.Add(invens[i].slots[j].item.itemName);
                        saveData.KeyNum.Add(invens[i].slots[j].itemCount);
                        break;
                    case 2:
                        saveData.importantName.Add(invens[i].slots[j].item.itemName);
                        saveData.importantNum.Add(invens[i].slots[j].itemCount);
                        break;
                    default:
                        break;
                }
            }
        }
        //아티팩트세이브
        
        for (int i = 0; i < ArtifactManager.S.allArtifact.Count; i++)
        {
            saveData.getIs.Add(ArtifactManager.S.allArtifact[i].getIs);
            saveData.ArtiAble.Add(ArtifactManager.S.allArtifact[i].able);
        }
        //퀘스트 세이브
        List<int> CurNums = new List<int>();
        for (int i = 0; i < QuestManager.S.AllQuestData.Length; i++)
        {
            saveData.questStates.Add(QuestManager.S.AllQuestData[i].questState);
            saveData.objectNum.Add(QuestManager.S.AllQuestData[i].questObjects.Length);
            for (int j = 0; j < QuestManager.S.AllQuestData[i].questObjects.Length; j++)
            {
                saveData.objectCurNum.Add(QuestManager.S.AllQuestData[i].questObjects[j].curNum);
            }
        }
        //타워 변수 세이브
        saveData.npc13 = TowerVariable.S.npc13;
        saveData.npc24 = TowerVariable.S.npc24;
        saveData.npc27 = TowerVariable.S.npc27;
        saveData.on21Floor = TowerVariable.S.on21Floor;
        saveData.floor9Shop = TowerVariable.S.floor9Shop;
        saveData.floor9Shop2 = TowerVariable.S.floor9Shop2;
        saveData.floor12Shop = TowerVariable.S.floor12Shop;
        saveData.Shop1AtkGold = TowerVariable.S.Shop1AtkGold;
        saveData.Shop1DefGold = TowerVariable.S.Shop1DefGold;
        saveData.Shop2AtkGold = TowerVariable.S.Shop2AtkGold;
        saveData.Shop2DefGold = TowerVariable.S.Shop2DefGold;
        saveData.Shop3AtkGold = TowerVariable.S.Shop3AtkGold;
        saveData.Shop3DefGold = TowerVariable.S.Shop3DefGold;
        saveData.Shop4AtkGold = TowerVariable.S.Shop4AtkGold;
        saveData.Shop4DefGold = TowerVariable.S.Shop4DefGold;
        saveData.Shop5AtkGold = TowerVariable.S.Shop5AtkGold;
        saveData.Shop5DefGold = TowerVariable.S.Shop5DefGold;
        saveData.Shop6AtkGold = TowerVariable.S.Shop6AtkGold;
        saveData.Shop6DefGold = TowerVariable.S.Shop6DefGold;
        saveData.isMeetFairy=TowerVariable.S.isMeetFairy;
        saveData.isClassUp=TowerVariable.S.isClassUp;
        saveData.market11 = TowerVariable.S.market11;
        saveData.arti1 = TowerVariable.S.Arti1;
        saveData.arti2 = TowerVariable.S.Arti2;
        saveData.arti3 = TowerVariable.S.Arti3;
        saveData.isfloorB20 = TowerVariable.S.isfloorB20;
        saveData.isgetallitemB21 = TowerVariable.S.isgetallitemB21;
        saveData.changeStair = TowerVariable.S.changeStair;
        saveData.golemKillCount = TowerVariable.S.golemKillCount;
        saveData.T7F4 = TowerVariable.S.T7F4;
        saveData.npc72 = TowerVariable.S.npc72;
        saveData.Shop7AtkGold = TowerVariable.S.Shop7AtkGold;
        saveData.Shop7DefGold = TowerVariable.S.Shop7DefGold;
        saveData.T4Arti1 = TowerVariable.S.T4Arti1;
        saveData.T4Arti2 = TowerVariable.S.T4Arti2;
        saveData.T4Arti3= TowerVariable.S.T4Arti3;
        saveData.market20= TowerVariable.S.market20;
        saveData.floor5shop= TowerVariable.S.floor5shop;
        saveData.KillLady= TowerVariable.S.killVamEvent;
        saveData.npc87= TowerVariable.S.npc87;
        saveData.Shop8AtkGold = TowerVariable.S.Shop8AtkGold;
        saveData.Shop8DefGold = TowerVariable.S.Shop8DefGold;
        saveData.GetStats = TowerVariable.S.GetStats;



        //맨밑에 두기
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData/Save"+_num+"/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/Save" + _num+"/");
        }
        string data = JsonUtility.ToJson(saveData, true);
        string path = Application.persistentDataPath + "/SaveData/Save"+_num+"/"+"Save.json";
        File.WriteAllText(path, data);

        //맵 세이브
        if (Directory.Exists(Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/"))
        {
            string[] allMaps = Directory.GetFiles(Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/");

            for (int i = 0; i < allMaps.Length; i++)
            {
                File.Delete(allMaps[i]);
            }
        }


        if (!Directory.Exists(Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/");
        }

        string[] allfiles = Directory.GetFiles(Application.persistentDataPath + "/MapSave/");
        string savePath = Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/";
        for (int i = 0; i < allfiles.Length; i++)
        {
            string fileName = System.IO.Path.GetFileName(allfiles[i]);
            string filePath = System.IO.Path.Combine(savePath, fileName);
            File.Copy(allfiles[i],filePath,true);
        }
    }
    public void Load(int _num)
    {
        Debug.Log("Start Load");
        string path = Application.persistentDataPath + "/SaveData/Save" + _num + "/" + "Save.json";
        string data = File.ReadAllText(path);
        saveData = JsonUtility.FromJson<SaveData>(data);


        Player.S.mainProgress = saveData.mainProgress;
        Player.S.CurTowerNum = saveData.curTowerNum;
        Player.S.level= saveData.level;
        Player.S.hp=saveData.hp;
        Player.S.bp = saveData.bp;//디펜스포인트
        Player.S.bpReset = saveData.bpReset;//매턴BP초기화수치
        Player.S.ATK = saveData.ATK;//공격수치
        Player.S.DEF=saveData.DEF;//방어수치
        Player.S.HIT= saveData.HIT;//적중수치
        Player.S.AVD = saveData.AVD;//회피수치
        Player.S.SPD= saveData.SPD;//스피드
        Player.S.RES = saveData.RES;//저항
        Player.S.CRC= saveData.CRC;//치명타율
        Player.S.CRD= saveData.CRD;//치명타피해량
        Player.S.CDR = saveData.CDR;//치명타피해량감소
        Player.S.CRR = saveData.CRR;//치명타저항
        Player.S.POW = saveData.POW;//마력
        Player.S.PIE= saveData.PIE;//관통(bp를 무시하고 데미지)
        Player.S.BLK= saveData.BLK;//저지(관통효과 저지)
        Player.S.ICD= saveData.ICD;//주는피해 증가
        Player.S.DCD= saveData.DCD;//받는피해 감소
        Player.S.VAM= saveData.VAM;//흡혈
        Player.S.HEL= saveData.HEL;//회복량
        Player.S.ARC= saveData.ARC;//방어등급
        Player.S.REG= saveData.REG;//재생
        Player.S.AP= saveData.AP;//액션포인트
        Player.S.DP= saveData.DP;//디펜스포인트
        Player.S.RG_stun= saveData.RG_stun;
        Player.S.RG_Sleep= saveData.RG_Sleep;
        Player.S.RG_Paralyze= saveData.RG_Paralyze;
        Player.S.RG_Poison= saveData.RG_Poison;
        Player.S.RG_Bleed= saveData.RG_Bleed;
        Player.S.RG_Erosion= saveData.RG_Erosion;
        Player.S.RG_Curse= saveData.RG_Curse;
        Player.S.RG_Daze= saveData.RG_Daze;
        Player.S.RG_Doom= saveData.RG_Doom;
        Player.S.RG_Misfortune= saveData.RG_Misfortune;
        Player.S.RG_Dispel = saveData.RG_Dispel;
        Player.S.RG_Infect= saveData.RG_Infect;
        Player.S.RG_Fear= saveData.RG_Fear;
        Player.S.RG_Burn = saveData.RG_Burn;
        Player.S.MonsterBookOn= saveData.MonsterBookOn;
        Player.S.MoveFloorOn= saveData.MoveFloorOn;
        Player.S.SecretWallOn= saveData.SecretWallOn;
        Player.S.MasterKeyNum= saveData.MasterKeyNum;
        Player.S.GoldenEggStack = saveData.GoldenEggStack;
        Player.S.SpoonStack = saveData.SpoonStack;
        Player.S.BookStack=saveData.BookStack;
        Player.S.reviveStack = saveData.reviveStack;
        Player.S.job= saveData.job ;
        Player.S.playerLocation=saveData.playerLocation;
        Player.S.gold = saveData.gold ;
        Player.S.exp= saveData.exp;
        Player.S.BonusGoldPer= saveData.BonusGoldPer;
        Player.S.BonusExpPer= saveData.BonusExpPer ;
        Player.S.SP= saveData.SP;
        Player.S.UseSP= saveData.UseSP;
        Player.S.PP= saveData.PP;
        Player.S.CP= saveData.CP;//공용스킬 최대 보유갯수
        Player.S.useCP= saveData.useCP;// 공용스킬 보유 갯수
        Player.S.jobClass= saveData.jobClass;// 직업차수
        Player.S.MaxFloorNum= saveData.MaxFloorNum;
        Player.S.MinFloorNum = saveData.MinFloorNum;
        Player.S.classUp = saveData.classup;
        Player.S.classUp1 = saveData.classup1;
        Player.S.classUp2 = saveData.classup2;
        Player.S.EquipmentStrings = saveData.EquipmentStrings;
        Player.S.endTuto = saveData.endTuto;
        Player.S.GambleValue = saveData.GambleValue;
        Player.S.GambleNum = saveData.GambleNum;
        Player.S.KoStack=saveData.KoStack;
        Player.S.KoStackMax=saveData.KostackMax;
        Player.S.SetNormalAttackKO();
        Player.S.characterName = Player.S.ReturnCharacterName(saveData.job,saveData.jobClass);
        Player.S.Pedller.Clear();
        Player.S.artiGetNum=saveData.artiGetNum;
        Player.S.useSmokebomb=saveData.useSmokebomb;
        Player.S.getSetArtifact=saveData.getSetArtifact;
        Player.S.useGodstone=saveData.useGodstone;

        for (int i = 0; i < saveData.Peddler.Count; i++)
        {
            Player.S.Pedller.Add(saveData.Peddler[i]);
        }
        for (int i = 0; i < Player.S.EquipmentStrings.Count; i++)
        {
            EquipmentUI.S.AddEquipment(Dictionaries.S.GetEquipment(Player.S.EquipmentStrings[i]));
        }
        portshop.SetShop();

        //플레이어 이미지 로드
        switch (Player.S.job)
        {
            case Player.PlayerJob.Inquisitor:
                Player.S.PlayerImage = InquisitorImages[0];
                Player.S.PlayerTowerImage = InquisitorImages[0];
                Player.S.PlayerBattleAni[0] = InquisitorImages[1];
                Player.S.PlayerBattleAni[1] = InquisitorImages[2];
                break;
            case Player.PlayerJob.Paladin:
                Player.S.PlayerImage = crusaderImages[0];
                Player.S.PlayerTowerImage = crusaderImages[0];
                Player.S.PlayerBattleAni[0] = crusaderImages[1];
                Player.S.PlayerBattleAni[1] = crusaderImages[2];
                break;
            case Player.PlayerJob.WitchHunter:
                Player.S.PlayerImage = witchHunterImages[0];
                Player.S.PlayerTowerImage = witchHunterImages[0];
                Player.S.PlayerBattleAni[0] = witchHunterImages[1];
                Player.S.PlayerBattleAni[1] = witchHunterImages[2];
                break;
            case Player.PlayerJob.NightShadow:
                break;
            case Player.PlayerJob.Alchemist:
                break;
            case Player.PlayerJob.none:
                break;
            default:
                break;
        }


        //스킬 로드
        Player.S.ClassPasiveSkills.Clear();
        Player.S.classSkillinven.Clear();
        Player.S.publicSkillinven.Clear();
        for (int i = 0; i < saveData.ClassPasiveSkills.Count; i++)
        {
            Player.S.ClassPasiveSkills.Add(SkillDic.S.D_AllSkillDic[saveData.ClassPasiveSkills[i]]);
        }
        for (int i = 0; i < saveData.C_skills.Count; i++)
        {
            Player.S.classSkillinven.Add(SkillDic.S.D_AllSkillDic[saveData.C_skills[i]]);
        }
        for (int i = 0; i < saveData.P_skills.Count; i++)
        {
            Player.S.publicSkillinven.Add(SkillDic.S.D_AllSkillDic[saveData.P_skills[i]]);
        }

        //프리셋 로드
        Player.S.ResetTP();
        for (int i = 0; i < saveData.presetNum; i++)
        {
            Player.S.PlusTP();
        }
        List<List<string>> savePresets = new List<List<string>>();
        savePresets.Add(saveData.PresetSkills1);
        savePresets.Add(saveData.PresetSkills2);
        savePresets.Add(saveData.PresetSkills3);
        savePresets.Add(saveData.PresetSkills4);
        savePresets.Add(saveData.PresetSkills5);
        savePresets.Add(saveData.PresetSkills6);
        savePresets.Add(saveData.PresetSkills7);
        savePresets.Add(saveData.PresetSkills8);
        savePresets.Add(saveData.PresetSkills9);
        Player.S.PreSetList.Clear();
        for (int i = 0; i < saveData.presetNum; i++)
        {
            List<Skill> presetDummy = new List<Skill>();
            for (int j = 0; j < savePresets[i].Count; j++)
            {
                presetDummy.Add(SkillDic.S.D_AllSkillDic[savePresets[i][j]]);
            }
            Player.S.PreSetList.Add(presetDummy);
        }

        //List<Skill> preset1=new List<Skill>();
        //List<Skill> preset2 = new List<Skill>(); ;
        //for (int i = 0; i < saveData.PresetSkills1.Count; i++)
        //{
        //    preset1.Add(SkillDic.S.D_AllSkillDic[saveData.PresetSkills1[i]]);
            
        //}
        //for (int i = 0; i < saveData.PresetSkills2.Count; i++)
        //{
        //    preset2.Add(SkillDic.S.D_AllSkillDic[saveData.PresetSkills2[i]]);
        //}



        //Player.S.PreSetList.Add(preset1);
        //Player.S.PreSetList.Add(preset2);
        Debug.Log(Player.S.PreSetList.Count);
        Player.S.NowPresetNum = saveData.nowPresetNum;
        Player.S.skillList = new List<Skill>(Player.S.PreSetList[Player.S.NowPresetNum]);

        //타운상점 로드
        TownProgressManager.S.ChangeTownProgress();
        List<MarketPlace> markets = GameObject.Find("Town").GetComponent<TownUI>().markets;
        for (int i = 0; i < markets.Count; i++)
        {
            markets[i].isload = true;
            markets[i].slots.Clear();

            switch (i)
            {
                case 0:
                    for (int j = 0; j < saveData.GuildName.Count; j++)
                    {
                        GameObject go = Instantiate(markets[i].P_slot, markets[i].T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.item = AddItem.S.itemDictionary[saveData.GuildName[j]];
                        slot.itemPrice = saveData.GuildPrice[j];
                        slot.itemNum = saveData.GuildNum[j];
                        slot.market = markets[i];
                        slot.SetShopSlot();
                        markets[i].slots.Add(go);
                    }
                    break;
                case 1://스킬관련으로 수정하기
                    //for (int j = 0; j < saveData.ToolName.Count; j++)
                    //{
                    //    GameObject go = Instantiate(markets[i].P_slot, markets[i].T_slots);
                    //    TownShopSlot slot = go.GetComponent<TownShopSlot>();
                    //    slot.item = AddItem.S.itemDictionary["노말직업스킬"];
                    //    slot.skill = SkillDic.S.D_AllSkillDic[saveData.ToolName[j]];
                    //    slot.itemPrice = saveData.ToolPrice[j];
                    //    slot.itemNum = saveData.ToolNum[j];
                    //    slot.market = markets[i];
                    //    slot.SetShopSlot();
                    //    markets[i].slots.Add(go);
                    //}
                    break;
                case 2:
                    for (int j = 0; j < saveData.PortName.Count; j++)
                    {
                        GameObject go = Instantiate(markets[i].P_slot, markets[i].T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.item = AddItem.S.itemDictionary["아티팩트더미"];
                        slot.artifact = ArtifactManager.S.SearchArti(saveData.PortName[j]);
                        slot.itemPrice = saveData.PortPrice[j];
                        slot.itemNum = saveData.PortNum[j];
                        slot.market = markets[i];
                        slot.SetShopSlot();
                        markets[i].slots.Add(go);
                    }
                    break;
                case 3:
                    if (saveData.mainProgress>= 3)
                    {
                        markets[i].sellListByProgresses[0].Progress = saveData.mainProgress;
                        markets[i].SetMarketByProgress();
                    }
                    break;
                case 4:
                    for (int j = 0; j < saveData.BlackName.Count; j++)
                    {

                        switch (saveData.BlackKind[j])
                        {

                            case 1:
                                GameObject go1 = Instantiate(markets[i].P_slot, markets[i].T_slots);
                                TownShopSlot slot1 = go1.GetComponent<TownShopSlot>();
                                slot1.item = AddItem.S.itemDictionary[saveData.BlackName[j]];
                                slot1.itemPrice = saveData.BlackPrice[j];
                                slot1.itemNum = saveData.BlackNum[j];
                                slot1.market = markets[i];
                                slot1.SetShopSlot();
                                markets[i].slots.Add(go1);
                                break;
                            case 2:
                                GameObject go2 = Instantiate(markets[i].P_slot, markets[i].T_slots);
                                TownShopSlot slot2 = go2.GetComponent<TownShopSlot>();
                                slot2.item = AddItem.S.itemDictionary["노말직업스킬"];
                                slot2.skill = SkillDic.S.D_AllSkillDic[saveData.BlackName[j]];
                                slot2.itemPrice = saveData.BlackPrice[j];
                                slot2.itemNum = saveData.BlackNum[j];
                                slot2.market = markets[i];
                                slot2.SetShopSlot();
                                markets[i].slots.Add(go2);
                                break;
                            case 3:
                                GameObject go3 = Instantiate(markets[i].P_slot, markets[i].T_slots);
                                TownShopSlot slot3 = go3.GetComponent<TownShopSlot>();
                                slot3.item = AddItem.S.itemDictionary["아티팩트더미"];
                                slot3.artifact = ArtifactManager.S.SearchArti(saveData.BlackName[j]);
                                slot3.itemPrice = saveData.BlackPrice[j];
                                slot3.itemNum = saveData.BlackNum[j];
                                slot3.market = markets[i];
                                slot3.SetShopSlot();
                                markets[i].slots.Add(go3);
                                break;
                            case 4:
                                GameObject go = Instantiate(markets[i].P_slot, markets[i].T_slots);
                                TownShopSlot slot = go.GetComponent<TownShopSlot>();
                                slot.item = AddItem.S.itemDictionary["장비더미"];
                                slot.equipment = Dictionaries.S.GetEquipment(saveData.BlackName[j]);
                                slot.itemPrice = saveData.BlackPrice[j];
                                slot.itemNum = saveData.BlackNum[j];
                                slot.market = markets[i];
                                slot.SetShopSlot();
                                markets[i].slots.Add(go);
                                break;
                            default:
                                break;
                        }
                    }


                    break;
                default:
                    break;
            }


        }
        //전도사 로드
        TrainingRoom trainingRoom = GameObject.Find("훈련소").GetComponent<TrainingRoom>();
        TrainingRoom.S.buyPrice=saveData.buyPrice ;
        TrainingRoom.S.plusPrice=saveData.plusPrice;
        TrainingRoom.S.buyPrice2 = saveData.buyPrice2;
        TrainingRoom.S.plusPrice2 = saveData.plusPrice2;
        trainingRoom.LoadSkillShop(saveData.SkillCenterSkills, saveData.SkillCenterSkills2);

        //인벤 로드
        for (int i = 0; i < saveData.UseItemName.Count; i++)
        {
            Inventory.S.GetItem(AddItem.S.itemDictionary[saveData.UseItemName[i]], saveData.UseItemNum[i]);
        }
        for (int i = 0; i < saveData.KeyName.Count; i++)
        {
            Inventory.S.GetItem(AddItem.S.itemDictionary[saveData.KeyName[i]], saveData.KeyNum[i]);
        }
        for (int i = 0; i < saveData.importantName.Count; i++)
        {
            Inventory.S.GetItem(AddItem.S.itemDictionary[saveData.importantName[i]], saveData.importantNum[i]);
        }
       

        //아티 로드
        for (int i = 0; i < ArtifactManager.S.allArtifact.Count; i++)
        {
            if (saveData.getIs.Count<=i)
            {
                break;
            }
            ArtifactManager.S.allArtifact[i].getIs = saveData.getIs[i];
            ArtifactManager.S.allArtifact[i].able = saveData.ArtiAble[i];
        }
        //광장 로드
        for (int i = 0; i < Plaza.S.Npcs.Count; i++)
        {
            Plaza.S.Npcs[i].SetActive(false);
        }
        for (int i = 0; i < saveData.TownTMI.Count; i++)
        {
            
            Plaza.S.Npcs[i].SetActive(true);
        }
        //퀘스트 로드
        for (int i = 0; i < QuestManager.S.AllQuestData.Length; i++)
        {
            if (i>=saveData.questStates.Count)
            {
                break;
            }
            QuestManager.S.AllQuestData[i].questState = saveData.questStates[i];
        }
        int plusValue = 0;
        for (int i = 0; i < QuestManager.S.AllQuestData.Length;i++)
        {
            if (i >= saveData.questStates.Count)
            {
                break;
            }
            for (int j = 0; j <QuestManager.S.AllQuestData[i].questObjects.Length; j++ )
            {

                QuestManager.S.AllQuestData[i].questObjects[j].curNum = saveData.objectCurNum[i+j+plusValue];
                
            }
            plusValue += saveData.objectNum[i] - 1;
        }
        for (int i = 0; i < QuestManager.S.AllQuestData.Length; i++)
        {
            if (i >= saveData.questStates.Count)
            {
                break;
            }
            if (QuestManager.S.AllQuestData[i].questState == QuestData.QuestState.Accept)
            {
                QuestManager.S.curQuestData.Add(QuestManager.S.AllQuestData[i]);
            }
            
        }
        //타워 변수 로드
        TowerVariable.S.npc13=saveData.npc13;
        TowerVariable.S.npc24=saveData.npc24 ;
        TowerVariable.S.npc27=saveData.npc27;
        TowerVariable.S.on21Floor=saveData.on21Floor ;
        TowerVariable.S.floor12Shop=saveData.floor12Shop ;
        TowerVariable.S.floor9Shop = saveData.floor9Shop;
        TowerVariable.S.floor9Shop2 = saveData.floor9Shop2;
        TowerVariable.S.Shop1AtkGold=saveData.Shop1AtkGold ;
        TowerVariable.S.Shop1DefGold=saveData.Shop1DefGold ;
        TowerVariable.S.Shop2AtkGold=saveData.Shop2AtkGold;
        TowerVariable.S.Shop2DefGold=saveData.Shop2DefGold ;
        TowerVariable.S.Shop3AtkGold=saveData.Shop3AtkGold;
        TowerVariable.S.Shop3DefGold=saveData.Shop3DefGold ;
        TowerVariable.S.Shop4AtkGold = saveData.Shop4AtkGold;
        TowerVariable.S.Shop4DefGold = saveData.Shop4DefGold;
        TowerVariable.S.Shop5AtkGold = saveData.Shop5AtkGold;
        TowerVariable.S.Shop5DefGold = saveData.Shop5DefGold;
        TowerVariable.S.Shop6AtkGold = saveData.Shop6AtkGold;
        TowerVariable.S.Shop6DefGold = saveData.Shop6DefGold;
        TowerVariable.S.isMeetFairy = saveData.isMeetFairy;
        TowerVariable.S.isClassUp = saveData.isClassUp;
        TowerVariable.S.market11 = saveData.market11;
        TowerVariable.S.Arti1 = saveData.arti1;
        TowerVariable.S.Arti2 = saveData.arti2;
        TowerVariable.S.Arti3 = saveData.arti3;
        TowerVariable.S.isfloorB20=saveData.isfloorB20;
        TowerVariable.S.isgetallitemB21=saveData.isgetallitemB21;
        TowerVariable.S.changeStair=saveData.changeStair;
        TowerVariable.S.golemKillCount=saveData.golemKillCount ;
        TowerVariable.S.T7F4=saveData.T7F4;
        TowerVariable.S.npc72=saveData.npc24;
        TowerVariable.S.Shop7AtkGold=saveData.Shop7AtkGold;
        TowerVariable.S.Shop7DefGold=saveData.Shop7DefGold;
        TowerVariable.S.T4Arti1= saveData.T4Arti1;
        TowerVariable.S.T4Arti2= saveData.T4Arti2;
        TowerVariable.S.T4Arti3= saveData.T4Arti3;
        TowerVariable.S.market20=saveData.market20;
        TowerVariable.S.floor5shop=saveData.floor5shop;
        TowerVariable.S.killVamEvent=saveData.KillLady;
        TowerVariable.S.npc87=saveData.npc87;
        TowerVariable.S.Shop8AtkGold=saveData.Shop8AtkGold;
        TowerVariable.S.Shop8DefGold = saveData.Shop8DefGold;
        TowerVariable.S.GetStats = saveData.GetStats;

        //맵로드

        string[] allfiles = Directory.GetFiles(Application.persistentDataPath + "/SaveData/Save" + _num + "/Maps/");
        string savePath = Application.persistentDataPath + "/MapSave/";
        for (int i = 0; i < allfiles.Length; i++)
        {
            string fileName = System.IO.Path.GetFileName(allfiles[i]);
            string filePath = System.IO.Path.Combine(savePath, fileName);
            File.Copy(allfiles[i], filePath, true);
        }
    }
}
