using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Artifact
{
    public int id;
    public Sprite image;
    public string artifactName;
    [TextArea]
    public string Info;
    [TextArea]
    public string E_Info;
    public string Ename;
    public bool getIs;
    public bool able;
    public int dropTable;
    public int tend;
    public int rare;
}
public class ArtifactManager : MonoBehaviour
{
    public static ArtifactManager S;

    public void Awake()
    {
        if (S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        AddNormalArtifact();
        AddRareArtifact();
        AddUniqueArtifact();
        AddEtcArtifact();
        AddPlusArtifact();
        AddArtifact();
    }

    public void Update()
    {
        
    }
    public List<Artifact> allArtifact=new List<Artifact>();
    //노말 아티팩트
    public List<Artifact> NormalArtifacts = new List<Artifact>();
    public List<Artifact> RareArtifacts = new List<Artifact>();
    public List<Artifact> UniqueArtifacts = new List<Artifact>();
    public List<Artifact> EpicArtifacts = new List<Artifact>();
    public List<Artifact> EtcArtifacts = new List<Artifact>();
    public List<Artifact> PlusArtifacts = new List<Artifact>();
    public Artifact RedOrb;
    public Artifact BlueOrb;
    public Artifact TeleScope;
    public Artifact Bible;
    public Artifact HellBible;
    public Artifact VampireCape;
    public Artifact UnicornStatus;
    public Artifact Sandal;
    public Artifact RidingGloves;
    public Artifact DriedWater;
    public Artifact MasterKey;
    public Artifact Coral;
    public Artifact Ginger;
    public Artifact SpikeBoots;
    public Artifact UndertakerLicense;
    public Artifact GreenLantern;
    public Artifact HiddenHourglass;
    public Artifact ProofOfDischarge;
    public Artifact CrossBow;
    public Artifact CapeOfRanger;
    public Artifact PirateFlag;
    public Artifact RegeneRing;
    public Artifact MaskOfChaos;
    public Artifact SpiderFriend;
    public Artifact AlchemyPot;
    public Artifact BlacksmithHammer;
    public Artifact Pickax;
    public Artifact RingOfSpirit;
    public Artifact PumpkinAndPumpkin;
    public Artifact FiveLeavesClover;
    public bool _____________________Rare_________________;
    public Artifact BlackOrb;
    public Artifact WhiteOrb;
    public Artifact StoneCoin;
    public Artifact SwordOfRevelation;
    public Artifact ScaleOfLife;
    public Artifact NONO;
    public Artifact RainbowSocks;
    public Artifact MedalOfPolitician;
    public Artifact HorseShoe;
    public Artifact DragonSword;
    public Artifact ElevenChainMail;
    public Artifact BraceletofUnbalance;
    public Artifact ListFromTheHades;
    public Artifact MoonBreaker;
    public Artifact TearstoneOftheFreak;
    public Artifact RoyalFlag;
    public Artifact NecklaceOfTheHypnosis;
    public Artifact GoldenEgg;
    public Artifact NonFantasticsAndWhereToFindThem;
    public Artifact MillenniumCodex;//아직못함

    public bool ______________________________Unique________________________;

    public Artifact UrnOfLife;
    public Artifact TreasureOfWitch;
    public Artifact FrozenFlowerCrystal;
    public Artifact SignalFromTheXienciel;
    public Artifact SwordOfTheJudgement;
    public Artifact EmeraldSword;
    public Artifact LegendaryScroll;
    public Artifact HornFromTheElysion;
    public Artifact AliceGrimoire;
    public Artifact Donguibogam;

    public bool __________________________ETC________________________;
    public Artifact ArkOfTheCovenant;
    public Artifact GoldneGoose;
    public Artifact FantsticsAndWhereToFindThem;


    //추가아티팩트
    public Artifact OddPocket;
    public Artifact TorturerBelt;
    public Artifact PangeaGlobe;
    public Artifact RawHam;
    public Artifact DirtSpoon;
    //레어
    public Artifact Thermometer;
    public Artifact Compass;
    public Artifact RawFish;
    public Artifact BigGold;
    //유니크
    public Artifact AngelFeather;
    public Artifact InvisibleCloak;
    public Artifact Seaweeds;
    //에픽
    public Artifact RingingBloom;
    public Artifact FictionReports;
    public Artifact HornofKirin;
    public Artifact CuriousCube;
    public Artifact Seer;
    public Artifact DarknessSympathizer;
    //기타
    public Artifact GoldenSpoon;

    //조합 아티팩트
    public Artifact BannerOfOrbCollector;
    public Artifact DiggerKit;
    public Artifact NecklaceOfVigor;
    public Artifact DivisamentDouMonde;
    public Artifact MealKit;
    public Artifact WitchesBroom;

    public void AddRareArtifact()
    {
        RareArtifacts.Add(BlackOrb);
        RareArtifacts.Add(WhiteOrb);
        RareArtifacts.Add(Coral);
        RareArtifacts.Add(Bible);
        RareArtifacts.Add(HellBible);
        RareArtifacts.Add(NONO);
        // RareArtifacts.Add(StoneCoin);
        //RareArtifacts.Add(SwordOfRevelation);
        RareArtifacts.Add(RainbowSocks);
        RareArtifacts.Add(MedalOfPolitician);
        RareArtifacts.Add(HorseShoe);
        RareArtifacts.Add(ElevenChainMail);
        RareArtifacts.Add(BraceletofUnbalance);
        RareArtifacts.Add(ListFromTheHades);
        RareArtifacts.Add(MoonBreaker);
        RareArtifacts.Add(TearstoneOftheFreak);
        RareArtifacts.Add(RoyalFlag);
        RareArtifacts.Add(NecklaceOfTheHypnosis);
        RareArtifacts.Add(GoldenEgg);
        RareArtifacts.Add(NonFantasticsAndWhereToFindThem);
        RareArtifacts.Add(MillenniumCodex);

        for (int i = 0; i < RareArtifacts.Count; i++)
        {
            RareArtifacts[i].rare = 1;
        }
    }
    public void AddNormalArtifact()
    {
        NormalArtifacts.Add(RedOrb);
        NormalArtifacts.Add(BlueOrb);
        NormalArtifacts.Add(MasterKey);
        NormalArtifacts.Add(Ginger);
        NormalArtifacts.Add(TeleScope);
        NormalArtifacts.Add(SpikeBoots);
        NormalArtifacts.Add(UndertakerLicense);
        NormalArtifacts.Add(GreenLantern);
        NormalArtifacts.Add(VampireCape);
        NormalArtifacts.Add(UnicornStatus);
        NormalArtifacts.Add(Sandal);
        NormalArtifacts.Add(RidingGloves);
        NormalArtifacts.Add(FiveLeavesClover);
        NormalArtifacts.Add(HiddenHourglass);
        NormalArtifacts.Add(ProofOfDischarge);
        NormalArtifacts.Add(CrossBow);
        NormalArtifacts.Add(CapeOfRanger);
        NormalArtifacts.Add(PirateFlag);
        NormalArtifacts.Add(RegeneRing);
        NormalArtifacts.Add(MaskOfChaos);
        NormalArtifacts.Add(DriedWater);
        NormalArtifacts.Add(SpiderFriend);
        NormalArtifacts.Add(PumpkinAndPumpkin);
        NormalArtifacts.Add(AlchemyPot);
        NormalArtifacts.Add(BlacksmithHammer);
        NormalArtifacts.Add(Pickax);
        NormalArtifacts.Add(RingOfSpirit);
        NormalArtifacts.Add(ScaleOfLife);

        for (int i = 0; i < NormalArtifacts.Count; i++)
        {
            NormalArtifacts[i].rare = 0;
        }
    }

    public void AddUniqueArtifact()
    {
        UniqueArtifacts.Add(DragonSword);
        UniqueArtifacts.Add(UrnOfLife);
        UniqueArtifacts.Add(TreasureOfWitch);
        UniqueArtifacts.Add(FrozenFlowerCrystal);
        UniqueArtifacts.Add(SignalFromTheXienciel);
        UniqueArtifacts.Add(SwordOfTheJudgement);
        UniqueArtifacts.Add(EmeraldSword);
        UniqueArtifacts.Add(LegendaryScroll);
        UniqueArtifacts.Add(HornFromTheElysion);
        UniqueArtifacts.Add(AliceGrimoire);
        UniqueArtifacts.Add(Donguibogam);
        for (int i = 0; i < UniqueArtifacts.Count; i++)
        {
            UniqueArtifacts[i].rare = 2;
        }
    }
    public void AddEtcArtifact()
    {
        EtcArtifacts.Add(ArkOfTheCovenant);
        EtcArtifacts.Add(GoldneGoose);
        EtcArtifacts.Add(FantsticsAndWhereToFindThem);
        for (int i = 0; i < EtcArtifacts.Count; i++)
        {
            EtcArtifacts[i].rare = 5;
        }
    }
    public void AddPlusArtifact()
    {
        PlusArtifacts.Add(OddPocket);
        PlusArtifacts.Add(TorturerBelt);
        PlusArtifacts.Add(PangeaGlobe);
        PlusArtifacts.Add(RawHam);
        PlusArtifacts.Add(DirtSpoon);

        PlusArtifacts.Add(Thermometer);
        PlusArtifacts.Add(Compass);
        PlusArtifacts.Add(RawFish);
        PlusArtifacts.Add(BigGold);

        PlusArtifacts.Add(AngelFeather);
        PlusArtifacts.Add(InvisibleCloak);
        PlusArtifacts.Add(Seaweeds);

        PlusArtifacts.Add(RingingBloom);
        PlusArtifacts.Add(FictionReports);
        PlusArtifacts.Add(HornofKirin);
        PlusArtifacts.Add(CuriousCube);
        PlusArtifacts.Add(Seer);
        PlusArtifacts.Add(DarknessSympathizer);

        PlusArtifacts.Add(GoldenSpoon);

        PlusArtifacts.Add(BannerOfOrbCollector);
        PlusArtifacts.Add(DiggerKit);
        PlusArtifacts.Add(NecklaceOfVigor);
        PlusArtifacts.Add(DivisamentDouMonde);
        PlusArtifacts.Add(MealKit);
        PlusArtifacts.Add(WitchesBroom);

    }
    public void AddArtifact()
    {
        for (int i = 0; i < NormalArtifacts.Count; i++)
        {
            allArtifact.Add(NormalArtifacts[i]);

        }
        for (int i = 0; i < RareArtifacts.Count; i++)
        {
            allArtifact.Add(RareArtifacts[i]);

        }

        for (int i = 0; i < UniqueArtifacts.Count; i++)
        {
            allArtifact.Add(UniqueArtifacts[i]);

        }
        for (int i = 0; i < EtcArtifacts.Count; i++)
        {
            allArtifact.Add(EtcArtifacts[i]);

        }
        for (int i = 0; i < PlusArtifacts.Count; i++)
        {
            if (i < 5)
            {
                PlusArtifacts[i].rare = 0;
                NormalArtifacts.Add(PlusArtifacts[i]);
            }
            else if (i < 9)
            {
                PlusArtifacts[i].rare = 1;
                RareArtifacts.Add(PlusArtifacts[i]);
            }
            else if (i < 12)
            {
                PlusArtifacts[i].rare = 2;
                UniqueArtifacts.Add(PlusArtifacts[i]);
            }
            else if (i < 18)
            {
                PlusArtifacts[i].rare = 3;
                EpicArtifacts.Add(PlusArtifacts[i]);
            }
            else if (i < 19)
            {
                PlusArtifacts[i].rare = 5;
                EtcArtifacts.Add(PlusArtifacts[i]);
            }

            allArtifact.Add(PlusArtifacts[i]);
        }
    }


    public List<Artifact> RandomArtifact(string _grade,int _num)
    {
        List<Artifact> returnArti = new List<Artifact>();
        List<Artifact> artifacts = new List<Artifact>();
        int value = 0;
        int normal = 0;
        int rare = 0;
        List<Artifact> dummy = new List<Artifact>();
        switch (_grade)
        {
            case "노말":
                artifacts =new List<Artifact>(NormalArtifacts);
                break;
            case "레어":
                artifacts = new List<Artifact>(RareArtifacts);
                break;
            case "유니크":
                artifacts = new List<Artifact>(UniqueArtifacts);
                break;
            case "에픽":
                artifacts = new List<Artifact>(EpicArtifacts);
                break;
            case "노말레어":
                for (int i = 0; i < _num; i++)
                {
                    value = Random.Range(0, 4);
                    switch (Player.S.mainProgress)
                    {
                        case 3:
                            if (value<3)
                            {
                                normal += 1;

                            }
                            else
                            {
                                rare += 1;

                            }
                            
                            break;
                        case 4:
                            if (value < 3)
                            {
                                normal += 1;

                            }
                            else
                            {
                                rare += 1;

                            }
                            break;
                        case 5:
                            if (value < 3)
                            {
                                normal += 1;

                            }
                            else
                            {
                                rare += 1;

                            }

                            break;
                        default:
                            break;
                    }
                }
                dummy = RandomArtifact("노말", normal);
                for (int i = 0; i < dummy.Count; i++)
                {
                    returnArti.Add(dummy[i]);
                }
                dummy = RandomArtifact("레어", rare);
                for (int i = 0; i < dummy.Count; i++)
                {
                    returnArti.Add(dummy[i]);
                }
                return returnArti;
            default:
                break;
        }

        for (int i = artifacts.Count-1; i >= 0; i--)
        {
            if (artifacts[i].able||artifacts[i].getIs)
            {
                artifacts.RemoveAt(i);
            }
        }
        if (artifacts.Count <= 0)
        {
            return returnArti;
        }
        if (_num > artifacts.Count)
        {
            _num = artifacts.Count;
        }

        if (_num == 1)//하나면 반환
        {

            while(returnArti.Count<1)
            {
                value = Random.Range(0, artifacts.Count);
                if (!artifacts[value].able)
                {
                    returnArti.Add(artifacts[value]);
                }
            }

        }
        else //하나 이상이면 중복안되게 반환
        {
            while (returnArti.Count < _num)
            {
                for (int i = 0; i < returnArti.Count; i++)
                {
                    for (int j = 0; j < artifacts.Count; j++)
                    {
                        if (returnArti[i] == artifacts[j])
                        {
                            artifacts.Remove(artifacts[j]);
                            break;
                        }
                    }
                }

                value = Random.Range(0, artifacts.Count);
                returnArti.Add(artifacts[value]);

            }
        }

        return returnArti;
    }

    public void GetArtifact(string _name)
    {
        for (int i = 0; i < allArtifact.Count; i++)
        {
            if (allArtifact[i].artifactName==_name)
            {
                allArtifact[i].getIs = true;
                allArtifact[i].able = true;
                Player.S.artiGetNum += 1;
            }
        }
        if (_name == RingingBloom.artifactName)
        {
            Player.S.reviveStack = 1;
        }
        if (_name== CapeOfRanger.artifactName)
        {
            Player.S.RG_Misfortune += 50;
        }
        if (_name == Coral.artifactName)
        {
            Player.S.RG_Poison += 50;
        }
        if (_name == BraceletofUnbalance.artifactName)
        {
            Player.S.RG_Curse += 50;
        }
        if (_name == NecklaceOfTheHypnosis.artifactName)
        {
            Player.S.RG_Sleep += 50;
        }
        if (_name == LegendaryScroll.artifactName)
        {
            Player.S.LevelUP();
            Player.S.LevelUP();
            Player.S.LevelUP();
        }
        if (_name==PumpkinAndPumpkin.artifactName)
        {
            Player.S.GetGold(100);
            Player.S.GetExp(50);
        }
        if (_name == MillenniumCodex.artifactName)
        {
            Player.S.LevelUP();
            Player.S.LevelUP();
        }
        if (_name == ProofOfDischarge.artifactName)
        {
            Player.S.LevelUP();
        }

        if (_name == AlchemyPot.artifactName)
        {
            Player.S.hp += 1000;
            Player.S.hp += Player.S.hp * 10 / 100;
        }
        if (_name == MasterKey.artifactName)
        {
            AddItem.S.SearchItem("돌 열쇠", 8);
            AddItem.S.SearchItem("쇠 열쇠", 4);
            AddItem.S.SearchItem("금 열쇠", 2);
            AddItem.S.SearchItem("보석 열쇠", 1);
        }
        if (_name==RingOfSpirit.artifactName)
        {
            AddItem.S.SearchItem("연막탄", 15);
        }
        if (_name == BigGold.artifactName)
        {
            Player.S.GetGold(300);
        }
        if (_name == Seer.artifactName)
        {
            AddItem.S.SearchItem("비밀방 두루마리", 50);
            AddItem.S.SearchItem("연막탄", 50);
        }
        if (_name== Pickax.artifactName)
        {
            AddItem.S.SearchItem("비밀방 두루마리", 10);
        }
        if (_name == BannerOfOrbCollector.artifactName)
        {
            Player.S.LevelUP();
            Player.S.LevelUP();
            Player.S.LevelUP();
            Player.S.LevelUP();
        }
        if (_name == DiggerKit.artifactName)
        {
            AddItem.S.SearchItem("보석 열쇠", 3);
        }
        if (_name == WitchesBroom.artifactName)
        {
            Player.S.KoStackMax -= 20;
        }
    }
    public Artifact SearchArti(string _name)
    {
        for (int i = 0; i < allArtifact.Count; i++)
        {
            if (allArtifact[i].artifactName==_name)
            {
                return allArtifact[i];
            }
        }
        if (_name=="구매완료")
        {
            return null;
        }
        Debug.Log("해당이름의 아티팩트가 없습니다");
        return null;
    }
    public int ReturnHaveArtiNum()
    {
        int num = 0;
        for (int i = 0; i < allArtifact.Count; i++)
        {
            if (allArtifact[i].able)
            {
                num += 1;
            }
        }
        return num;
    }
}
