using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapCell : MonoBehaviour
{
    public int X;
    public int Y;
    public Image ObjectImage;
    public TowerObjectData towerObjectData;

    public Sprite[] aniSprite;
    public bool spriteChange;
    public float aniTime;
    public float time;
    public bool isDel;
    public Sprite[] DelSprites;

    public TowerObjectData dummy;

    public void Start()
    {
        aniTime = 0.5f;
        time = .0f;
    }


    public void Update()
    {
        if (aniSprite!=null&&!isDel)
        {
            time += 1 * Time.deltaTime;

            if (time > aniTime && aniSprite.Length == 2)
            {
                if (spriteChange)
                {
                    spriteChange = false;
                    ObjectImage.sprite = aniSprite[0];
                }
                else
                {
                    spriteChange = true;
                    ObjectImage.sprite = aniSprite[1];
                }
                time = .0f;
            }
        }
       
    }

    public IEnumerator DelAni()
    {
        if (dummy!=null)
        {
            towerObjectData = dummy;
        }

        for (int i = 0; i < DelSprites.Length; i++)
        {
            ObjectImage.sprite = DelSprites[i];
            yield return new WaitForSeconds(0.05f);
        }
        isDel = false;
        TowerMap.S.ChangeMapCell(this, TowerMap.S.fieldDummies[TowerMap.S.curTowerNum]);
        yield break;
    }

}
