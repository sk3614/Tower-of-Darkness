using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class NPCTownProgress
{
    public int TownProgressNum;
    public Vector2Int TextNum;
    public bool isOneTime;
    public bool noPlay;
    public int Event;
}
public class TownNPC : MonoBehaviour
{
    public Sprite NpcImage;
    public NPCTownProgress[] npcTownProgress;
    public int EventNum = 0;



    public void PlayDialogue()
    {
        SoundManager.S.PlaySE("node");
        for (int i = 0; i < npcTownProgress.Length; i++)
        {
            if (npcTownProgress[i].TownProgressNum==Player.S.mainProgress&&!npcTownProgress[i].noPlay)
            {
                EventNum = npcTownProgress[i].Event;
                DialogueManager.S.TextSet(npcTownProgress[i].TextNum.x, npcTownProgress[i].TextNum.y,this,npcTownProgress[i].Event);
                if (npcTownProgress[i].isOneTime)
                {
                    npcTownProgress[i].noPlay = true;
                }
                return;
            }
        }

    }

    public void PlayTmi()
    {
        SoundManager.S.PlaySE("node");
        for (int i = 0; i < npcTownProgress.Length; i++)
        {
            if (npcTownProgress[i].TownProgressNum == Player.S.mainProgress && !npcTownProgress[i].noPlay)
            {
                DialogueManager.S.TextSetPlaza(npcTownProgress[i].TextNum.x-1, npcTownProgress[i].TextNum.y-1);
                if (npcTownProgress[i].isOneTime)
                {
                    npcTownProgress[i].noPlay = true;
                }
                EndEvent(npcTownProgress[i].Event);
                return;
            }
        }

    }

    public void EndEvent(int _num)
    {
        switch (_num)
        {
            case 1:
                Player.S.isMeetKing = true;
                break;
            case 2:
                Player.S.isMeetSaint = true;
                break;
            case 3:
                Player.S.GetExp(25);
                break;
            case 4:

                switch (Player.S.mainProgress)
                {
                    case 3:
                        Player.S.AVD += 1f;
                        break;
                    case 4:
                        Player.S.POW += 1;
                        break;
                    case 5:
                        Player.S.hp += 1000;
                        break;
                    case 6:
                        Player.S.DEF += 3;
                        break;
                    case 7:
                        Player.S.CP += 1;
                        break;
                    case 8:
                        Player.S.CRC += 1;
                        break;
                    case 9:
                        Player.S.ATK += 5;
                        break;
                    case 10:
                        Player.S.hp += 2500;
                        break;
                    case 11:
                        Player.S.DEF += 5;
                        break;
                    case 12:
                        Player.S.LevelUP();
                        break;
                    case 13:
                        AddItem.S.SearchItem("금 열쇠");
                        break;
                    case 14:
                        Player.S.LevelUP();
                        Player.S.GetGold(200);
                        break;
                    default:
                        break;
                }

                if (ArtifactManager.S.MaskOfChaos.able)
                {
                    DialogueManager.S.TextSet(178,179);
                    ArtifactManager.S.MaskOfChaos.able = false;
                    ArtifactManager.S.GetArtifact(ArtifactManager.S.RandomArtifact("레어", 1)[0].artifactName);

                }
                if (ArtifactManager.S.MedalOfPolitician.able&&Player.S.mainProgress>=6)
                {
                    DialogueManager.S.TextSet(251, 252);
                    ArtifactManager.S.MedalOfPolitician.able = false;
                    ArtifactManager.S.GetArtifact(ArtifactManager.S.RandomArtifact("유니크", 1)[0].artifactName);
                }
                break;
            case 5:
                if (ArtifactManager.S.MaskOfChaos.able)
                {
                    DialogueManager.S.TextSet(178, 179);
                    ArtifactManager.S.MaskOfChaos.able = false;
                    ArtifactManager.S.GetArtifact(ArtifactManager.S.RandomArtifact("레어", 1)[0].artifactName);

                }
                if (ArtifactManager.S.MedalOfPolitician.able && Player.S.mainProgress >= 6)
                {
                    DialogueManager.S.TextSet(251, 252);
                    ArtifactManager.S.MedalOfPolitician.able = false;
                    ArtifactManager.S.GetArtifact(ArtifactManager.S.RandomArtifact("유니크", 1)[0].artifactName);
                }
                break;
            case 6:
                AddItem.S.SearchItem("고대의 석판", -1);
                TrainingRoom.S.adeptui.SetActive(true);
                break;
            case 7:
                TownUI.S.zundo.SetActive(true);
                break;
            case 8:
                Gambler.S.SetGambleUI[0].SetActive(true);
                break;
            case 9:
                Gambler.S.SetGambleUI[1].SetActive(true);
                break;
            case 10:
                Gambler.S.SetGambleUI[2].SetActive(true);
                break;
            case 11:
                Player.S.ClassUp2();
                break;
            case 12:
                Player.S.GetGold(500);
                break;
            case 13:
                Player.S.LevelUP();
                break;
            case 14:
                AddItem.S.SearchItem("고대의 석판2", -1);
                TrainingRoom.S.adeptui.SetActive(true);
                break;
            case 15:
                Player.S.ClassUp3();
                TownUI.S.zundo.SetActive(false);
                break;
            default:
                break;
        }
        EventNum = 0;
    }
}

