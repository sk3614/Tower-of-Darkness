using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Inven
{
    public Transform T_inven;
    public List<Slot> slots=new List<Slot>();
}
public class Inventory : MonoBehaviour
{
    public static Inventory S;

   

    public Inven allItemInven;

    public Inven ItemInven;

    public Inven eventItemInven;

    public Inven artifactInven;

    public GameObject[] Inventories;//0=Ues, 1=Key, 2=event, 3=arti
    
    //Slot Prefab
    public GameObject invenSlotPrefab;

    //IteminfoUI
    public GameObject ItemInfoUI;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemCount;
    public Text ItemLongInfo;



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
        InventoryData.S.LoadKey();
    }
    public void SlotListRenewal()
    {
    }

    public void SortInven(Inven _inven)
    {
        _inven.slots.Sort(delegate (Slot A, Slot B)
        {
            if (A.sortnum < B.sortnum) return 1;
            else if (A.sortnum > B.sortnum) return -1;
            return 0;
        });
        for (int i = 0; i < _inven.slots.Count; i++)
        {
            _inven.slots[i].transform.SetSiblingIndex(i);
        }
    }
    public void ShowAllItem()
    {
        Debug.Log("전체");
        for (int i = allItemInven.slots.Count-1; i >=0; i--)
        {
            Destroy(allItemInven.slots[i].gameObject);
        }
        allItemInven.slots.Clear();

        GameObject go;
        for (int i = 0; i < ItemInven.slots.Count; i++)
        {
            go = Instantiate(invenSlotPrefab, allItemInven.T_inven);
            go.GetComponent<Slot>().AddItem(ItemInven.slots[i].item, ItemInven.slots[i].itemCount);
            allItemInven.slots.Add(go.GetComponent<Slot>());
        }
        for (int i = 0; i < eventItemInven.slots.Count; i++)
        {
            go = Instantiate(invenSlotPrefab, allItemInven.T_inven);
            go.GetComponent<Slot>().AddItem(eventItemInven.slots[i].item, eventItemInven.slots[i].itemCount);
            allItemInven.slots.Add(go.GetComponent<Slot>());
        }

        List<Artifact> allArtifact = ArtifactManager.S.allArtifact;
        for (int i = 0; i < allArtifact.Count; i++)
        {
            if (allArtifact[i].able)
            {
                go = Instantiate(invenSlotPrefab, allItemInven.T_inven);
                go.GetComponent<Slot>().AddArtifact(allArtifact[i]);
                allItemInven.slots.Add(go.GetComponent<Slot>());
            }
        }
    }
    public void GetItem(Item _item, int _num = 1)
    {
        if (_num==0)
        {
            return;
        }
        GameObject go;
        switch (_item.itemType)
        {
            case Item.ItemType.UseItem:
                for (int i = 0; i < ItemInven.slots.Count; i++)
                {
                    if (_item.itemName== ItemInven.slots[i].item.itemName)
                    {
                        ItemInven.slots[i].AddItem(_item, _num);
                        if (ItemInven.slots[i].itemCount<=0)
                        {
                            ItemInven.slots[i].ClearSlot();
                            ItemInven.slots.RemoveAt(i);
                            return;
                        }
                        return;
                    }
                }
                go = Instantiate(invenSlotPrefab, ItemInven.T_inven);
                go.GetComponent<Slot>().AddItem(_item, _num);
                ItemInven.slots.Add(go.GetComponent<Slot>());
                break;
            case Item.ItemType.KeyItem:
                for (int i = 0; i < ItemInven.slots.Count; i++)
                {
                    if (_item.itemName == ItemInven.slots[i].item.itemName)
                    {
                        ItemInven.slots[i].AddItem(_item, _num);
                        if (ItemInven.slots[i].itemCount <= 0) 
                        {
                            ItemInven.slots[i].ClearSlot();
                            ItemInven.slots.RemoveAt(i);
                            return;
                        }
                        return;
                    }
                }
                go = Instantiate(invenSlotPrefab, ItemInven.T_inven);
                go.GetComponent<Slot>().AddItem(_item, _num);
                ItemInven.slots.Add(go.GetComponent<Slot>());
                break;
            case Item.ItemType.EventItem:
                for (int i = 0; i < eventItemInven.slots.Count; i++)
                {
                    if (_item.itemName == eventItemInven.slots[i].item.itemName)
                    {
                        eventItemInven.slots[i].AddItem(_item, _num);
                        if (eventItemInven.slots[i].itemCount <= 0) 
                        {
                            eventItemInven.slots[i].ClearSlot();
                            eventItemInven.slots.RemoveAt(i);
                            return;
                        }
                        return;
                    }
                }
                go = Instantiate(invenSlotPrefab, eventItemInven.T_inven);
                go.GetComponent<Slot>().AddItem(_item, _num);
                eventItemInven.slots.Add(go.GetComponent<Slot>());
                break;
            case Item.ItemType.Artifact:
                for (int i = 0; i < artifactInven.slots.Count; i++)
                {
                    if (_item.itemName == artifactInven.slots[i].item.itemName)
                    {
                        artifactInven.slots[i].AddItem(_item, _num);
                        if (artifactInven.slots[i].itemCount <= 0)
                        {
                            artifactInven.slots[i].ClearSlot();
                            artifactInven.slots.RemoveAt(i);
                            return;
                        }
                        return;
                    }
                }
                go = Instantiate(invenSlotPrefab, artifactInven.T_inven);
                go.GetComponent<Slot>().AddItem(_item, _num);
                artifactInven.slots.Add(go.GetComponent<Slot>());
                break;
            case Item.ItemType.ETC:
                break;
            default:
                break;
        }
    }

    public void OpenItemInfoUI(Slot _slot)
    {
        if (!ItemInfoUI.activeInHierarchy)
        {
            ItemInfoUI.SetActive(true);
        }
        ItemImage.sprite = _slot.itemImage.sprite;
        ItemName.text = _slot.itemName;
        ItemCount.text = _slot.text_Count.text;
        ItemLongInfo.text = _slot.itemLongInfo;

    }
    public void CloseItemInfoUI()
    {
        ItemInfoUI.SetActive(false);
    }

    public void InventoryOn(int _num=0)
    {
        CloseItemInfoUI();


        if (_num==0)
        {
            ShowAllItem();
        }
        for (int i = 0; i < Inventories.Length; i++)
        {
            Inventories[i].SetActive(false);
        }
        Inventories[_num].SetActive(true);

        SortInven(artifactInven);
        SortInven(allItemInven);
        SortInven(eventItemInven);
        SortInven(ItemInven);
    }

    public int SearchItemCount(string _itemName)
    {
        for (int i = 0; i < ItemInven.slots.Count; i++)
        {
            if (ItemInven.slots[i].item.itemName == _itemName)
            {
                return ItemInven.slots[i].itemCount;
            }
        }
        for (int i = 0; i < eventItemInven.slots.Count; i++)
        {
            if (eventItemInven.slots[i].item.itemName == _itemName)
            {
                return eventItemInven.slots[i].itemCount;
            }
        }
        for (int i = 0; i < artifactInven.slots.Count; i++)
        {
            if (artifactInven.slots[i].itemName == _itemName)
            {
                return artifactInven.slots[i].itemCount;
            }
        }

        return 0;
    }

    public List<Item> ReturnAllItem()
    {
        List<Item> itemList = new List<Item>();
        for (int i = 0; i < allItemInven.slots.Count; i++)
        {
            itemList.Add(allItemInven.slots[i].item);
        }

        for (int i = 0; i < ItemInven.slots.Count; i++)
        {
            itemList.Add(ItemInven.slots[i].item);
        }

        for (int i = 0; i < eventItemInven.slots.Count; i++)
        {
            itemList.Add(eventItemInven.slots[i].item);
        }

        for (int i = 0; i < artifactInven.slots.Count; i++)
        {
            itemList.Add(artifactInven.slots[i].item);
        }

        return itemList;
    }

    public Item ReturnItem(string _itemName)
    {
        for (int i = 0; i < ItemInven.slots.Count; i++)
        {
            if (ItemInven.slots[i].item.itemName==_itemName)
            {

                return ItemInven.slots[i].item;
            }
        }
        return null;

    }

    public void ArtifactInvenOn()
    {
        for (int i = 0; i < artifactInven.slots.Count; i++)
        {
            Destroy(artifactInven.slots[i].gameObject);
        }
        artifactInven.slots.Clear();
        List<Artifact> allArtifact = ArtifactManager.S.allArtifact;
        GameObject go;
        for (int i = 0; i < allArtifact.Count; i++)
        {
            if (allArtifact[i].able)
            {
                go=Instantiate(invenSlotPrefab, artifactInven.T_inven);
                go.GetComponent<Slot>().AddArtifact(allArtifact[i]);
                artifactInven.slots.Add(go.GetComponent<Slot>());
            }
        }
    }
}
