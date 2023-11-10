using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OneTimeShop : MonoBehaviour
{
    public static OneTimeShop S;

    public GameObject UI;
    public GameObject[] NPCs;
    public GameObject[] ClassUpUIs;
    public Text shop16Text;
    public int shop16Count;

    private void Awake()
    {
        if (S==null)
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
        shop16Count = 20;
    }

    public void ShopUIOn(int _num)
    {
        if (_num == 5)
        {
            NPCs[_num].GetComponent<TowerSkillMarket>().MarketOn(SkillDic.S.NormalSkillByRandom("노말", 3));
        }
        if (_num == 16)
        {
            NPCs[_num].GetComponent<TowerSkillMarket>().MarketOn(SkillDic.S.NormalSkillByRandom("레어", 3));
        }
        if (_num == 25)
        {
            NPCs[_num].GetComponent<TowerSkillMarket>().MarketOn(SkillDic.S.NormalSkillByRandom("유니크", 3));
        }
        UI.SetActive(true);
        NPCs[_num].SetActive(true);
        TowerMap.S.MoveLock = true;

        if (_num==4)
        {
            NPCs[_num].GetComponent<TowerSkillMarket>().MarketOn(SkillDic.S.NormalSkillByRandom("노말",3));
        }
        if (_num==21)
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    shop16Text.text = "35 Gold : 신비한 가루 \n" + shop16Count + "개 남음";
                    break;
                case Options.Language.Eng:
                    shop16Text.text = "35 Gold : Mystic powder\nRemain " + shop16Count;
                    break;
                default:
                    break;
            }
        }
    }


    public void ShopUIClose(int _num)
    {
        UI.SetActive(false);
        NPCs[_num].SetActive(false);
        TowerMap.S.MoveLock = false;
    }

    public void NPC4()
    {
        if (Player.S.gold>=25)
        {
            Player.S.ATK += 3;
            Player.S.SpendGold(25);
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[5, 0].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            SoundManager.S.PlaySE("income");
            ShopUIClose(0);
        }
       
    }
    public void NPC5()
    {
        if (Player.S.exp >= 50)
        {
            Player.S.DEF += 20;
            Player.S.exp -= 50;
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[1, 0].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            SoundManager.S.PlaySE("income");
            ShopUIClose(1);
        }

    }
    public void NPC28()
    {
        if (Player.S.gold >= 40)
        {
            Player.S.hp += 2000;
            Player.S.SpendGold(40);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[8, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(2);
        }

    }
    public void NPC29(int _num)
    {
        if (_num==0)
        {
            Player.S.ATK += 3;
            ShopUIClose(3);
        }
        else
        {
            Player.S.DEF += 3;
            ShopUIClose(3);
        }

    }
    public void NPC30(int _num)
    {
        if (_num == 0)
        {
            Player.S.POW += 3;
            ShopUIClose(4);
        }
        else
        {
            Player.S.SPD += 1;
            ShopUIClose(4);
        }

    }
    public void npc41()
    {
        if (Player.S.gold >= 200)
        {
            Player.S.DEF += 6;
            Player.S.SpendGold(200);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[8, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(6);
        }
    }
    public void npc44()
    {
        if (Player.S.gold >= 50)
        {
            Player.S.hp += 750;
            Player.S.SpendGold(50);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[4, 8].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(7);
        }
    }
    public void npc45()
    {
        if (Player.S.gold >= 50)
        {
            Player.S.hp += 750;
            Player.S.SpendGold(50);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[6, 8].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(8);
        }
    }
    public void npc47(int _num)
    {
        if (_num == 0)
        {
            Player.S.PP += 1;
            ShopUIClose(9);
        }
        else
        {
            Player.S.CP += 2;
            ShopUIClose(9);
        }
        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[0, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
        TowerVariable.S.npc47 = true;
    }
    public void npc48(int _num)
    {
        if (_num == 0)
        {
            Player.S.ATK += 6;
            ShopUIClose(10);
        }
        else
        {
            Player.S.DEF += 6;
            ShopUIClose(10);
        }
        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[8, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
        TowerVariable.S.npc48 = true;
    }
    public void NPC53()
    {
        if (Player.S.gold >= 100)
        {
            Player.S.hp += 2500;
            Player.S.SpendGold(100);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[8, 7].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(14);
            if (Player.S.mainProgress==6)
            {
                QuestManager.S.QuestFail(30);
            }
            
        }

    }
    public void NPC61()
    {
        if (Player.S.gold >= 360)
        {
            Player.S.POW += 3;
            Player.S.SpendGold(360);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[0, 0].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(15);
        }

    }
    public void NPC64()
    {
        if (Player.S.gold >= 100)
        {
            AddItem.S.SearchItem("돌 열쇠", 6);
            Player.S.SpendGold(100);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[3, 6].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(17);
        }

    }
    public void NPC65()
    {
        if (Player.S.gold >= 50)
        {
            Player.S.hp += 1000;
            Player.S.SpendGold(50);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[5, 6].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(18);
        }

    }
    public void NPC69()
    {
        if (Player.S.gold >= 500)
        {
            Player.S.ATK += 5;
            Player.S.SpendGold(500);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[4, 7].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(19);
        }

    }

    public void NPC70()
    {
        if (Player.S.exp >= 200)
        {
            Player.S.DEF += 5;
            Player.S.SpendExp(200);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[4, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(20);
        }

    }
    public void NPC81()
    {
    if (Player.S.gold >= 666)
    {
        Player.S.ATK += 6;
        Player.S.SpendGold(666);
        SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[1, 3].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(23);
        }

    }
    public void NPC74()
    {
        if (Player.S.exp >= 666)
        {
            Player.S.PP += 1;
            Player.S.SpendExp(666);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[0, 4].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(26);
        }

    }
    public void NPC82()
    {
        if (Player.S.gold >= 300)
        {
            Player.S.hp += 3000;
            Player.S.SpendGold(300);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[7, 3].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(24);
        }

    }
    public void NPC90()
    {
        if (Player.S.gold >= 999)
        {
            Player.S.POW += 6;
            Player.S.SpendGold(999);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[0, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(27);
        }

    }
    public void NPC91()
    {
        if (Player.S.exp >= 369)
        {
            Player.S.SpendExp(369);
            AddItem.S.SearchItem("돌 열쇠", 18);
            SoundManager.S.PlaySE("income");
            TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[8, 1].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
            ShopUIClose(28);
        }

    }
    public void SkillSet()
    {
        UIBar.S.OpenCharacterUI();
        UIBar.S.GoSkillSetUI();
        TowerMap.S.ChangeMapCell(TowerMap.S.towerCellArray[7, 6].Cell.GetComponent<MapCell>(), TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
        ShopUIClose(29);
    }
    public void Shop16()
    {
        if (Player.S.gold >= 35&&shop16Count>0)
        {
            AddItem.S.SearchItem("신비한 가루");
            Player.S.SpendGold(35);
            SoundManager.S.PlaySE("income");
            shop16Count -= 1;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    shop16Text.text = "35 Gold : 신비한 가루 \n"+ shop16Count + "개 남음";
                    break;
                case Options.Language.Eng:
                    shop16Text.text = "35 Gold : Mystic powder\nRemain "+shop16Count;
                    break;
                default:
                    break;
            }
            if (Player.S.mainProgress == 9 && (QuestManager.S.curQuestData.Find(x=>x.questID==42).questState==QuestData.QuestState.Accept))
            {
                QuestManager.S.QuestFail(42);
            }
        }

    }
    public void Shop17()
    {
        if (Player.S.exp >= 10)
        {
            Player.S.hp += 100;
            Player.S.SpendExp(10);
            SoundManager.S.PlaySE("income");
            if (Player.S.mainProgress == 9 && (QuestManager.S.curQuestData.Find(x => x.questID == 42).questState == QuestData.QuestState.Accept))
            {
                QuestManager.S.QuestFail(42);
            }
        }
    }
    public void GetClassUp(int _num)
    {
        TextBox.S.TextBoxOn(TextBox.S.GetTexts(182, 183), true);
        TowerMap.S.GetClassUpItem(_num);

        ShopUIClose(11);
        ShopUIClose(12);
        ShopUIClose(13);

    }


}
