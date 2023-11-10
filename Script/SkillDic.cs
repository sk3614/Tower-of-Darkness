using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDic : MonoBehaviour
{
    public static SkillDic S;

    public Dictionary<string, Skill> D_AllSkillDic = new Dictionary<string, Skill>();
    public Dictionary<string, List<Skill>> D_PublicSkillsByGrade=new Dictionary<string, List<Skill>>();
    public Dictionary<string, List<Skill>> D_WitchHunterSkillsByGrade=new Dictionary<string, List<Skill>>();
    public Dictionary<string, List<Skill>> D_CrusaderSkillsByGrade = new Dictionary<string, List<Skill>>();
    public Dictionary<string, List<Skill>> D_InquisitorSkillsByGrade = new Dictionary<string, List<Skill>>();
    public List<Skill> Allskills=new List<Skill>();//플레이어가 사용가능한 모든 스킬


    public bool ________________PublicSkill_______________;
    public List<Skill> P_normalSkills=new List<Skill>(); //공용 노말
    public List<Skill> P_RareSkills = new List<Skill>(); //공용 레어
    public List<Skill> P_UniqueSkills = new List<Skill>(); //공용 유니크
    public List<Skill> P_EpicSkills = new List<Skill>(); //공용 에픽

    public bool ________________ClassSkill________________;
    public List<Skill> WC_normalSkills = new List<Skill>();//마녀 노말
    public List<Skill> WC_normalPlusSkills = new List<Skill>();//마녀 노말+
    public List<Skill> WC_SniperSkills = new List<Skill>();//마녀 저격수스킬
    public List<Skill> WC_RangerSkills = new List<Skill>();//마녀 순찰자스킬
    public List<Skill> WC_DemonSkills = new List<Skill>();//마녀 저격수스킬

    public List<Skill> WC_SniperSkills2 = new List<Skill>();//마녀 저격수2스킬
    public List<Skill> WC_RangerSkills2 = new List<Skill>();//마녀 순찰자2스킬
    public List<Skill> WC_DemonSkills2 = new List<Skill>();//마녀 저격수2스킬

    public List<Skill> WC_SniperSkills3 = new List<Skill>();//마녀 저격수2스킬
    public List<Skill> WC_RangerSkills3 = new List<Skill>();//마녀 순찰자2스킬
    public List<Skill> WC_DemonSkills3 = new List<Skill>();//마녀 저격수2스킬

    public List<Skill> WC_RareSkills = new List<Skill>(); //마녀 레어

    public List<Skill> C_normalSkills = new List<Skill>();//크루세이더 노말
    public List<Skill> C_normalPlusSkills = new List<Skill>();//크루세이더 노말+
    public List<Skill> C_GladiSkills = new List<Skill>();//크루세이더 검투사
    public List<Skill> C_EmpireSkills = new List<Skill>();//크루세이더 왕국기사
    public List<Skill> C_PaladinSkills = new List<Skill>();//크루세이더 성기사
    public List<Skill> C_GladiSkills2 = new List<Skill>();//크루세이더 검투사
    public List<Skill> C_EmpireSkills2 = new List<Skill>();//크루세이더 왕국기사
    public List<Skill> C_PaladinSkills2 = new List<Skill>();//크루세이더 성기사
    public List<Skill> C_GladiSkills3 = new List<Skill>();//크루세이더 검투사
    public List<Skill> C_EmpireSkills3 = new List<Skill>();//크루세이더 왕국기사
    public List<Skill> C_PaladinSkills3 = new List<Skill>();//크루세이더 성기사


    public List<Skill> I_normalSkills = new List<Skill>();//이단심문관 노말
    public List<Skill> I_normalPlusSkills = new List<Skill>();//이단심문관 노말+
    public List<Skill> I_JudgeSkills = new List<Skill>();//이단심문관 재판관
    public List<Skill> I_MissonarySkills = new List<Skill>();//이단심문관 선교사
    public List<Skill> I_AgentSkills = new List<Skill>();//이단심문관 요원
    public List<Skill> I_JudgeSkills2 = new List<Skill>();//이단심문관 재판관
    public List<Skill> I_MissonarySkills2 = new List<Skill>();//이단심문관 선교사
    public List<Skill> I_AgentSkills2= new List<Skill>();//이단심문관 요원
    public List<Skill> I_JudgeSkills3 = new List<Skill>();//이단심문관 재판관
    public List<Skill> I_MissonarySkills3 = new List<Skill>();//이단심문관 선교사
    public List<Skill> I_AgentSkills3 = new List<Skill>();//이단심문관 요원


    public void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Allskills.AddRange(P_normalSkills);
        Allskills.AddRange(P_RareSkills);
        Allskills.AddRange(P_UniqueSkills);
        Allskills.AddRange(P_EpicSkills);


        Allskills.AddRange(WC_normalSkills);
        Allskills.AddRange(WC_normalPlusSkills);
        Allskills.AddRange(WC_SniperSkills);
        Allskills.AddRange(WC_RangerSkills);
        Allskills.AddRange(WC_DemonSkills);
        Allskills.AddRange(WC_SniperSkills2);
        Allskills.AddRange(WC_RangerSkills2);
        Allskills.AddRange(WC_DemonSkills2);
        Allskills.AddRange(WC_SniperSkills3);
        Allskills.AddRange(WC_RangerSkills3);
        Allskills.AddRange(WC_DemonSkills3);
        Allskills.AddRange(WC_RareSkills);

        Allskills.AddRange(C_normalSkills);
        Allskills.AddRange(C_normalPlusSkills);
        Allskills.AddRange(C_GladiSkills);
        Allskills.AddRange(C_EmpireSkills);
        Allskills.AddRange(C_PaladinSkills);
        Allskills.AddRange(C_GladiSkills2);
        Allskills.AddRange(C_EmpireSkills2);
        Allskills.AddRange(C_PaladinSkills2);
        Allskills.AddRange(C_GladiSkills3);
        Allskills.AddRange(C_EmpireSkills3);
        Allskills.AddRange(C_PaladinSkills3);

        Allskills.AddRange(I_normalSkills);
        Allskills.AddRange(I_normalPlusSkills);
        Allskills.AddRange(I_JudgeSkills);
        Allskills.AddRange(I_MissonarySkills);
        Allskills.AddRange(I_AgentSkills);
        Allskills.AddRange(I_JudgeSkills2);
        Allskills.AddRange(I_MissonarySkills2);
        Allskills.AddRange(I_AgentSkills2);
        Allskills.AddRange(I_JudgeSkills3);
        Allskills.AddRange(I_MissonarySkills3);
        Allskills.AddRange(I_AgentSkills3);
        for (int i = 0; i < Allskills.Count; i++)
        {
            D_AllSkillDic.Add(Allskills[i].skillName, Allskills[i]);
        }
        
        D_PublicSkillsByGrade.Add("노말", P_normalSkills);
        D_PublicSkillsByGrade.Add("레어", P_RareSkills);
        D_PublicSkillsByGrade.Add("유니크", P_UniqueSkills);
        D_PublicSkillsByGrade.Add("에픽", P_EpicSkills);
        D_WitchHunterSkillsByGrade.Add("노말", WC_normalSkills);
        D_WitchHunterSkillsByGrade.Add("저격수", WC_SniperSkills);
        D_WitchHunterSkillsByGrade.Add("레인저", WC_RangerSkills);
        D_WitchHunterSkillsByGrade.Add("데몬", WC_DemonSkills);
        D_WitchHunterSkillsByGrade.Add("저격수2", WC_SniperSkills2);
        D_WitchHunterSkillsByGrade.Add("레인저2", WC_RangerSkills2);
        D_WitchHunterSkillsByGrade.Add("데몬2", WC_DemonSkills2);
        D_WitchHunterSkillsByGrade.Add("저격수3", WC_SniperSkills3);
        D_WitchHunterSkillsByGrade.Add("레인저3", WC_RangerSkills3);
        D_WitchHunterSkillsByGrade.Add("데몬3", WC_DemonSkills3);

        D_CrusaderSkillsByGrade.Add("노말", C_normalSkills);
        D_CrusaderSkillsByGrade.Add("검투사", C_GladiSkills);
        D_CrusaderSkillsByGrade.Add("왕국 기사", C_EmpireSkills);
        D_CrusaderSkillsByGrade.Add("성기사", C_PaladinSkills);
        D_CrusaderSkillsByGrade.Add("검투사2", C_GladiSkills2);
        D_CrusaderSkillsByGrade.Add("왕국 기사2", C_EmpireSkills2);
        D_CrusaderSkillsByGrade.Add("성기사2", C_PaladinSkills2);
        D_CrusaderSkillsByGrade.Add("검투사3", C_GladiSkills3);
        D_CrusaderSkillsByGrade.Add("왕국 기사3", C_EmpireSkills3);
        D_CrusaderSkillsByGrade.Add("성기사3", C_PaladinSkills3);

        D_InquisitorSkillsByGrade.Add("노말", I_normalSkills);
        D_InquisitorSkillsByGrade.Add("재판관", I_JudgeSkills);
        D_InquisitorSkillsByGrade.Add("선교사", I_MissonarySkills);
        D_InquisitorSkillsByGrade.Add("요원", I_AgentSkills);
        D_InquisitorSkillsByGrade.Add("재판관2", I_JudgeSkills2);
        D_InquisitorSkillsByGrade.Add("선교사2", I_MissonarySkills2);
        D_InquisitorSkillsByGrade.Add("요원2", I_AgentSkills2);
        D_InquisitorSkillsByGrade.Add("재판관3", I_JudgeSkills3);
        D_InquisitorSkillsByGrade.Add("선교사3", I_MissonarySkills3);
        D_InquisitorSkillsByGrade.Add("요원3", I_AgentSkills3);
    }


    public List<Skill> NormalSkillByRandom(string _grade, int _num = 1)
    {
        List<Skill> _retunrskills = new List<Skill>();
        List<Skill> skills = new List<Skill>();
        int value;
        skills = new List<Skill>( D_PublicSkillsByGrade[_grade]);

        for (int i = skills.Count - 1; i >= 0; i--)//플레이어 인벤에 있으면 제거
        {
            for (int j = 0; j < Player.S.publicSkillinven.Count; j++)
            {
                if (skills[i] == Player.S.publicSkillinven[j])
                {
                    skills.Remove(skills[i]);
                    break;
                }
            }
        }
        if (skills.Count <= 0)
        {
            return _retunrskills;
        }
        if (_num > skills.Count)
        {
            _num = skills.Count;
        }

        if (_num == 1)//하나면 반환
        {
            value = Random.Range(0, skills.Count);
            _retunrskills.Add(skills[value]);
        }

        else //하나 이상이면 중복안되게 반환
        {
            while (_retunrskills.Count < _num)
            {
                for (int i = 0; i < _retunrskills.Count; i++)
                {
                    for (int j = 0; j < skills.Count; j++)
                    {
                        if (_retunrskills[i] == skills[j])
                        {
                            skills.Remove(skills[j]);
                            break;
                        }
                    }
                }

                value = Random.Range(0, skills.Count);
                _retunrskills.Add(skills[value]);

            }
        }
        return _retunrskills;


    }
    public List<Skill> ClassSkillByRandom(string _grade,int _num=1)
    {
        if (_grade=="전직")
        {
            switch (Player.S.job)
            {
                case Player.PlayerJob.Inquisitor:
                    switch (Player.S.jobClass)
                    {
                        case 1:
                            _grade = "재판관";
                            break;
                        case 2:
                            _grade = "선교사";
                            break;
                        case 3:
                            _grade = "요원";
                            break;
                        default:
                            break;
                    }
                    break;
                case Player.PlayerJob.Paladin:
                    switch (Player.S.jobClass)
                    {
                        case 1:
                            _grade = "검투사";
                            break;
                        case 2:
                            _grade = "왕국 기사";
                            break;
                        case 3:
                            _grade = "성기사";
                            break;
                        default:
                            break;
                    }
                    break;
                case Player.PlayerJob.WitchHunter:
                    switch (Player.S.jobClass)
                    {
                        case 1:
                            _grade = "저격수";
                            break;
                        case 2:
                            _grade = "레인저";
                            break;
                        case 3:
                            _grade = "데몬";
                            break;
                        default:
                            break;
                    }
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
        }

        List<Skill> _retunrskills=new List<Skill>();
        List<Skill> skills = new List<Skill>();
        int value;
        switch (Player.S.job)
        {
            case Player.PlayerJob.Inquisitor:
                skills = new List<Skill>(D_InquisitorSkillsByGrade[_grade]);
                if (Player.S.classUp1)
                {
                    skills.AddRange(new List<Skill>(D_InquisitorSkillsByGrade[(_grade+"2")]));
                }
                break;
            case Player.PlayerJob.Paladin:
                skills = new List<Skill>(D_CrusaderSkillsByGrade[_grade]);
                if (Player.S.classUp1)
                {
                    skills.AddRange(new List<Skill>(D_CrusaderSkillsByGrade[(_grade + "2")]));
                }
                break;
            case Player.PlayerJob.WitchHunter:
                skills =new List<Skill>(D_WitchHunterSkillsByGrade[_grade]);
                if (Player.S.classUp1)
                {
                    skills.AddRange(new List<Skill>(D_WitchHunterSkillsByGrade[(_grade + "2")]));
                }
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
        for (int i = skills.Count-1; i >=0; i--)//플레이어 인벤에 있으면 제거
        {
            for (int j = 0; j < Player.S.classSkillinven.Count; j++)
            {
                if (skills[i] == Player.S.classSkillinven[j])
                {
                    skills.Remove(skills[i]);
                    break;
                }
                if (skills[i].skillName+"+" == Player.S.classSkillinven[j].skillName)//+스킬 걸러내기
                {
                    skills.Remove(skills[i]);
                    break;
                }
            }
        }
        if (skills.Count <= 0)
        {
            return _retunrskills;
        }
        if (_num > skills.Count)
        {
            _num = skills.Count;
        }

        if (_num==1)//하나면 반환
        {
            value = Random.Range(0, skills.Count);
            _retunrskills.Add(skills[value]);
        }
        else //하나 이상이면 중복안되게 반환
        {
            while (_retunrskills.Count<_num)
            {
                for (int i = 0; i < _retunrskills.Count; i++)
                {
                    for (int j = 0; j < skills.Count; j++)
                    {
                        if (_retunrskills[i] == skills[j])
                        {
                            skills.Remove(skills[j]);
                            break;
                        }
                    }
                }

                value = Random.Range(0, skills.Count);
                _retunrskills.Add(skills[value]);

            }
        }
        if (Player.S.jobClass!=0)
        {
            for (int i = 0; i < _retunrskills.Count; i++)
            {
                _retunrskills[i] = ChangePlusSkill(_retunrskills[i].skillName);
            }
        }

        return _retunrskills;
        

    }

    public Skill ChangePlusSkill(string _skillname)
    {
        Skill _skill=D_AllSkillDic[_skillname];
        
        switch (Player.S.jobClass)
        {
            case 1:
                if (_skillname == "추적자") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "흔적 찾기") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "저격") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "휩쓸기") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "방패 막기") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "심호흡") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "교차 베기") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "교차 막기") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "관찰") _skill = D_AllSkillDic[_skillname + "+"];
                break;
            case 2:
                if (_skillname == "캠핑 전문가") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "엄폐") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "응급 처치") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "무기 정비") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "강철 무장") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "방패 타격") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "신념") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "부정") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "회복 가스") _skill = D_AllSkillDic[_skillname + "+"];
                break;
            case 3:
                if (_skillname == "제사용 단검") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "사냥 의식") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "부적") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "치유 기적") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "기도") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "신성한 오라") _skill = D_AllSkillDic[_skillname + "+"];

                if (_skillname == "권총") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "1회용 방패") _skill = D_AllSkillDic[_skillname + "+"];
                if (_skillname == "직관") _skill = D_AllSkillDic[_skillname + "+"];
                break;
            default:
                break;
        }
        return _skill;
    }

}
