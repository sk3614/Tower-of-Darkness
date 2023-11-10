using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveFloorUI : MonoBehaviour
{
    public static MoveFloorUI S;
    public GameObject uiBase;
    public Slider slider;
    public Text floorText;
    public Text towerName;
    public GameObject upButton;
    public GameObject downButton;
    public float keyDownTime;
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
    public void  Update()
    {
        if (uiBase.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeValueButton(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangeValueButton(-1);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ChangeFloor();
            }

            if (Input.GetKey(KeyCode.W))
            {
                keyDownTime += 1.0f * Time.deltaTime;
                if (keyDownTime>0.2f)
                {
                    ChangeValueButton(1);
                    keyDownTime = 0f;
                }

            }else if(Input.GetKey(KeyCode.S))
            {
                keyDownTime += 1.0f * Time.deltaTime;
                if (keyDownTime > 0.2f)
                {
                    ChangeValueButton(-1);
                    keyDownTime = 0f;
                }
            }
            else
            {
                keyDownTime = 0f;
            }

        }


    }
    public void MoveFloorUIOpen()
    {
        if (TowerMap.S.MoveLock && uiBase.activeInHierarchy)
        {
            MoveFloorUIClose();
        }
        if (TowerMap.S.MoveLock)
        {
            return;
        }
        if (Player.S.MoveFloorOn&&TowerMap.S.curFloorNum!=22)
        {

            if(!MonsterBookUI.S.uiBase.activeInHierarchy)
            {
                uiBase.SetActive(true);
                TowerMap.S.MoveLock = true;

                switch (TowerMap.S.curTowerNum)
                {
                    case 0:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                towerName.text = "성채";
                                break;
                            case Options.Language.Eng:
                                towerName.text = "A Fortress";
                                break;
                            default:
                                break;
                        }

                        break;
                    case 1:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                towerName.text = "뼈의 탑";
                                break;
                            case Options.Language.Eng:
                                towerName.text = "Tower\nof\nBone";
                                break;
                            default:
                                break;
                        }


                        break;
                    case 2:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                towerName.text = "피의 탑";
                                break;
                            case Options.Language.Eng:
                                towerName.text = "Tower\nof\nBlood";
                                break;
                            default:
                                break;
                        }

                        break;
                    case 3:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                towerName.text = "심연의 탑";
                                break;
                            case Options.Language.Eng:
                                towerName.text = "Tower\nof\nAbyss";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                towerName.text = "빛의 탑";
                                break;
                            case Options.Language.Eng:
                                towerName.text = "Tower\nof\nLight";
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }

                slider.maxValue = Player.S.MaxFloorNum;
                slider.minValue = Player.S.MinFloorNum;
                slider.value = TowerMap.S.curFloorNum;
                ChangeSliderValue();
            }
        }
       

       
    }
    public void MoveFloorUIClose()
    {
        uiBase.SetActive(false);
        TowerMap.S.MoveLock = false;
    }
    public void ChangeSliderValue()
    {
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                floorText.text = slider.value.ToString() + "층";
                break;
            case Options.Language.Eng:
                floorText.text = slider.value.ToString()+"F";
                break;
            default:
                break;
        }


        floorText.text = floorText.text.Replace("-", "B");
    }
    public void ChangeValueButton(int _num)
    {
        slider.value += _num;
    }
    public void ChangeFloor()
    {

        if (slider.value > TowerMap.S.curFloorNum)
        {
            TowerMap.S.ChangeFloor((int)slider.value, TowerMap.S.currentCell.x, TowerMap.S.currentCell.y);
            Vector2Int StairData = new Vector2Int();
            for (int i = 0; i < TowerMap.S.mapCells.Count; i++)
            {
                if (TowerMap.S.mapCells[i].GetComponent<MapCell>().towerObjectData.ObjectNum == 33)
                {
                    StairData = new Vector2Int(TowerMap.S.mapCells[i].GetComponent<MapCell>().X, TowerMap.S.mapCells[i].GetComponent<MapCell>().Y);
                }
            }
            TowerMap.S.SetPlayerLocation(TowerMap.S.towerCellArray[StairData.x, StairData.y]);


        }
        else if (slider.value == TowerMap.S.curFloorNum)
        {
            if (slider.value>=0)
            {
                if (slider.value==0)
                {
                    MoveFloorUIClose();
                    return;
                }
                bool isStair=false;
                TowerMap.S.ChangeFloor((int)slider.value, TowerMap.S.currentCell.x, TowerMap.S.currentCell.y);
                Vector2Int StairData = new Vector2Int();
                for (int i = 0; i < TowerMap.S.mapCells.Count; i++)
                {
                    if (TowerMap.S.mapCells[i].GetComponent<MapCell>().towerObjectData.ObjectNum == 33)
                    {
                        StairData = new Vector2Int(TowerMap.S.mapCells[i].GetComponent<MapCell>().X, TowerMap.S.mapCells[i].GetComponent<MapCell>().Y);
                        isStair = true;
                    }
                }
                if (isStair)
                {
                    TowerMap.S.SetPlayerLocation(TowerMap.S.towerCellArray[StairData.x, StairData.y]);
                    isStair = false;
                }
                else
                {

                }

            }
            else
            {
                TowerMap.S.ChangeFloor((int)slider.value, TowerMap.S.currentCell.x, TowerMap.S.currentCell.y);
                Vector2Int StairData = new Vector2Int();
                for (int i = 0; i < TowerMap.S.mapCells.Count; i++)
                {
                    if (TowerMap.S.mapCells[i].GetComponent<MapCell>().towerObjectData.ObjectNum == 31)
                    {
                        StairData = new Vector2Int(TowerMap.S.mapCells[i].GetComponent<MapCell>().X, TowerMap.S.mapCells[i].GetComponent<MapCell>().Y);
                    }
                }
                TowerMap.S.SetPlayerLocation(TowerMap.S.towerCellArray[StairData.x, StairData.y]);
            }
        }
        else if(slider.value < TowerMap.S.curFloorNum)
        {
            TowerMap.S.ChangeFloor((int)slider.value, TowerMap.S.currentCell.x, TowerMap.S.currentCell.y);
            Vector2Int StairData = new Vector2Int();
            for (int i = 0; i < TowerMap.S.mapCells.Count; i++)
            {
                if (TowerMap.S.mapCells[i].GetComponent<MapCell>().towerObjectData.ObjectNum == 31)
                {
                    StairData = new Vector2Int(TowerMap.S.mapCells[i].GetComponent<MapCell>().X, TowerMap.S.mapCells[i].GetComponent<MapCell>().Y);
                }
            }
            TowerMap.S.SetPlayerLocation(TowerMap.S.towerCellArray[StairData.x, StairData.y]);
        }
       MoveFloorUIClose();
    }
    
}
