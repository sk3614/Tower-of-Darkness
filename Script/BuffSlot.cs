using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffSlot : MonoBehaviour
{
    public GameObject buffslotPrefab;

    public List<GameObject> buffslots;
    public Sprite[] buffImages;
    public void BuffSlotCreate(List<Buff> buffs)
    {
        BuffSlotClear();

        if (true)
        {

        }
        for (int i = 0; i < buffs.Count; i++)
        {
            bool isSame=false;
            for (int j = 0; j < buffslots.Count; j++)
            {
                if (buffslots[j].GetComponent<BuffSlots>().imageType==buffs[i].buffImageType)
                {
                    isSame = true;
                }
            }
            if (isSame)
            {
                continue;
            }
            GameObject buffslot =  Instantiate(buffslotPrefab,transform);
            buffslot.GetComponent<BuffSlots>().imageType = buffs[i].buffImageType;
            buffslot.GetComponent<BuffSlots>().BuffImage.sprite = buffImages[(int)buffs[i].buffImageType];
            buffslots.Add(buffslot);




        }


        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].buffImageType2 ==Buff.BuffImageType.None)
            {
                break;
            }
            bool isSame = false;
            for (int j = 0; j < buffslots.Count; j++)
            {
                if (buffslots[j].GetComponent<BuffSlots>().imageType == buffs[i].buffImageType2)
                {
                    isSame = true;
                }
            }
            if (isSame)
            {
                continue;
            }
            GameObject buffslot = Instantiate(buffslotPrefab, transform);
            buffslot.GetComponent<BuffSlots>().imageType = buffs[i].buffImageType2;
            buffslot.GetComponent<BuffSlots>().BuffImage.sprite = buffImages[(int)buffs[i].buffImageType2];
            buffslots.Add(buffslot);


        }

    }
    private void BuffSlotClear()
    {
        for (int i = buffslots.Count-1; i >= 0; i--)
        {
            Destroy(buffslots[i]);
        }
        buffslots.Clear();
    }
    
}
