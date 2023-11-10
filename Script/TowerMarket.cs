using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerMarket : MonoBehaviour
{
    public static TowerMarket S;

    public GameObject[] Markets;

    public Text NowGold;
    public Text NeedGold_atk;
    public Text NeedGold_def;
    public Text NowExp;

    public Text NowGold2;
    public Text NeedGold_atk2;
    public Text NeedGold_def2;

    public Text NowGold3;
    public Text NeedGold_atk3;
    public Text NeedGold_def3;

    public Text NowGold4;
    public Text NeedGold_atk4;
    public Text NeedGold_def4;

    public Text NowGold5;
    public Text NeedGold_atk5;
    public Text NeedGold_def5;

    public Text NowGold6;
    public Text NeedGold_atk6;
    public Text NeedGold_def6;

    public Text NowGold7;
    public Text NeedGold_atk7;
    public Text NeedGold_def7;

    public Text NowGold8;
    public Text NeedGold_atk8;
    public Text NeedGold_def8;

    public TowerArtiMarket artiMarket;
    public TowerArtiMarket artiMarket2;

    public TowerArtiMarket artiMarket3;
    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void TowerMarketOn(int _num)
    {
        Markets[_num].SetActive(true);
        TowerMap.S.MoveLock = true;
        RefreshUI();
    }
    public void TowerMarketClose()
    {
        TowerMap.S.MoveLock = false;
    }
    public void RefreshUI()
    {
        NowGold.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk.text = "Gold : " + TowerVariable.S.Shop1AtkGold;
        NeedGold_def.text = "Gold : " + TowerVariable.S.Shop1DefGold;
        NowExp.text = "Now EXP : " + Player.S.exp;

        NowGold2.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk2.text = "Gold : " + TowerVariable.S.Shop2AtkGold;
        NeedGold_def2.text = "Gold : " + TowerVariable.S.Shop2DefGold;

        NowGold3.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk3.text = "Gold : " + TowerVariable.S.Shop3AtkGold;
        NeedGold_def3.text = "Gold : " + TowerVariable.S.Shop3DefGold;

        NowGold4.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk4.text = "Gold : " + TowerVariable.S.Shop4AtkGold;
        NeedGold_def4.text = "Gold : " + TowerVariable.S.Shop4DefGold;

        NowGold5.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk5.text = "Gold : " + TowerVariable.S.Shop5AtkGold;
        NeedGold_def5.text = "Gold : " + TowerVariable.S.Shop5DefGold;

        NowGold6.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk6.text = "Gold : " + TowerVariable.S.Shop6AtkGold;
        NeedGold_def6.text = "Gold : " + TowerVariable.S.Shop6DefGold;

        NowGold7.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk7.text = "Gold : " + TowerVariable.S.Shop7AtkGold;
        NeedGold_def7.text = "Gold : " + TowerVariable.S.Shop7DefGold;

        NowGold8.text = "Now Gold : " + Player.S.gold;
        NeedGold_atk8.text = "Gold : " + TowerVariable.S.Shop8AtkGold;
        NeedGold_def8.text = "Gold : " + TowerVariable.S.Shop8DefGold;
    }
    public void FirstMarket(int _num)
    {

            switch (_num)
            {
                case 0:
                    if (Player.S.gold>= TowerVariable.S.Shop1AtkGold)
                    {
                        Player.S.ATK += 1;
                        Player.S.SpendGold(TowerVariable.S.Shop1AtkGold);
                        TowerVariable.S.Shop1AtkGold += 1;
                    SoundManager.S.PlaySE("income");
                    }
                    break;
                case 1:
                if (Player.S.gold >= TowerVariable.S.Shop1DefGold)
                {
                    Player.S.DEF += 1;
                    Player.S.SpendGold(TowerVariable.S.Shop1DefGold);
                    TowerVariable.S.Shop1DefGold += 1;
                    SoundManager.S.PlaySE("income");
                }
                    break;
                default:
                    break;
            }
        RefreshUI();
    }
    public void SecondMarket(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp>=50)
                {
                    Player.S.LevelUP();
                    Player.S.exp -= 50;
                    SoundManager.S.PlaySE("upgrade1");
                }

                break;
            case 1:
                if (Player.S.exp >= 10)
                {
                    Player.S.ATK += 1;
                    Player.S.exp -= 10;
                }

                break;
            case 2:
                if (Player.S.exp >= 15)
                {
                    Player.S.DEF += 2;
                    Player.S.exp -= 15;
                }

                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void KeyMarket(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.gold >= 20)
                {
                    AddItem.S.SearchItem("돌 열쇠", 1);
                    Player.S.SpendGold(20);
                    SoundManager.S.PlaySE("income");
                }

                break;
            case 1:
                if (Player.S.gold >= 50)
                {
                    AddItem.S.SearchItem("쇠 열쇠", 1);
                    Player.S.SpendGold(50);
                    SoundManager.S.PlaySE("income");
                }

                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market3(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop2AtkGold)
                {
                    Player.S.ATK += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop2AtkGold);
                    TowerVariable.S.Shop2AtkGold += 2;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop2DefGold)
                {
                    Player.S.DEF += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop2DefGold);
                    TowerVariable.S.Shop2DefGold += 2;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market5(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop3AtkGold)
                {
                    Player.S.ATK += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop3AtkGold);
                    TowerVariable.S.Shop3AtkGold += 3;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop3DefGold)
                {
                    Player.S.DEF += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop3DefGold);
                    TowerVariable.S.Shop3DefGold += 3;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market6(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 100)
                {
                    Player.S.LevelUP();
                    Player.S.exp -= 100;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market7(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop4AtkGold)
                {
                    Player.S.ATK += 3;
                    Player.S.SpendGold(TowerVariable.S.Shop4AtkGold);
                    TowerVariable.S.Shop4AtkGold += 4;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop4DefGold)
                {
                    Player.S.DEF += 3;
                    Player.S.SpendGold(TowerVariable.S.Shop4DefGold);
                    TowerVariable.S.Shop4DefGold += 4;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market8(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 175)
                {
                    Player.S.LevelUP();
                    Player.S.LevelUP();
                    Player.S.exp -= 175;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market9(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 150)
                {
                    Player.S.LevelUP();
                    Player.S.exp -= 150;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market11(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop5AtkGold)
                {
                    Player.S.ATK += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop5AtkGold);
                    TowerVariable.S.Shop5AtkGold += 5;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop5DefGold)
                {
                    Player.S.DEF += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop5DefGold);
                    TowerVariable.S.Shop5DefGold += 5;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market12(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop6AtkGold)
                {
                    Player.S.ATK += 5;
                    Player.S.SpendGold(TowerVariable.S.Shop6AtkGold);
                    TowerVariable.S.Shop6AtkGold += 10;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop6DefGold)
                {
                    Player.S.DEF += 5;
                    Player.S.SpendGold(TowerVariable.S.Shop6DefGold);
                    TowerVariable.S.Shop6DefGold += 10;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market13(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 390)
                {
                    Player.S.LevelUP();
                    Player.S.LevelUP();
                    Player.S.LevelUP();
                    Player.S.exp -= 390;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market18(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop7AtkGold)
                {
                    Player.S.ATK += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop7AtkGold);
                    TowerVariable.S.Shop7AtkGold += 6;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop7DefGold)
                {
                    Player.S.DEF += 2;
                    Player.S.SpendGold(TowerVariable.S.Shop7DefGold);
                    TowerVariable.S.Shop7DefGold += 6;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market19(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 200)
                {
                    Player.S.LevelUP();
                    Player.S.exp -= 200;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void Market22(int _num)
    {

        switch (_num)
        {
            case 0:
                if (Player.S.gold >= TowerVariable.S.Shop8AtkGold)
                {
                    Player.S.ATK += 6;
                    Player.S.SpendGold(TowerVariable.S.Shop8AtkGold);
                    TowerVariable.S.Shop8AtkGold += 12;
                    SoundManager.S.PlaySE("income");
                }
                break;
            case 1:
                if (Player.S.gold >= TowerVariable.S.Shop8DefGold)
                {
                    Player.S.DEF += 6;
                    Player.S.SpendGold(TowerVariable.S.Shop8DefGold);
                    TowerVariable.S.Shop8DefGold += 12;
                    SoundManager.S.PlaySE("income");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }

    public void Market23(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.exp >= 500)
                {
                    Player.S.LevelUP();
                    Player.S.LevelUP();
                    Player.S.LevelUP();
                    Player.S.exp -= 500;
                    SoundManager.S.PlaySE("upgrade1");
                }
                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void KeyMarket2(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.gold >= 20)
                {
                    AddItem.S.SearchItem("돌 열쇠", 1);
                    Player.S.SpendGold(20);
                    SoundManager.S.PlaySE("income");
                }

                break;
            case 1:
                if (Player.S.gold >= 50)
                {
                    AddItem.S.SearchItem("쇠 열쇠", 1);
                    Player.S.SpendGold(50);
                    SoundManager.S.PlaySE("income");
                }

                break;
            default:
                break;
        }
        RefreshUI();
    }
    public void KeyMarket3(int _num)
    {
        switch (_num)
        {
            case 0:
                if (Player.S.gold >= 50)
                {
                    AddItem.S.SearchItem("돌 열쇠", 1);
                    Player.S.SpendGold(50);
                    SoundManager.S.PlaySE("income");
                }

                break;
            case 1:
                if (Player.S.gold >= 100)
                {
                    AddItem.S.SearchItem("쇠 열쇠", 1);
                    Player.S.SpendGold(100);
                    SoundManager.S.PlaySE("income");
                }

                break;
            default:
                break;
        }
        RefreshUI();
    }
}
