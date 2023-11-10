using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    public GameObject uibase;
    public Text ShopName;
    public bool isblacksmith;
    public int needGodstone;
    public int needPowder;

    public Text curGodstone;
    public Text curPowder;
    public Text Noupgrade;


    //EquipSlot
    public Transform T_slots;
    public GameObject P_EquipSlot;
    public List<GameObject> slots = new List<GameObject>();


    //InfoUI
    public Equipment selectedEquip;
    public GameObject InfoUIbase;
    public Image InfoImage;
    public Text InfoName;
    public Text InfoStats;
    public Text needMaterials;
    public GameObject noMaterials;
    public GameObject UpgradeButton;

    //AfterUpgradeUI
    public GameObject UpUIbase;
    public Image UpImage;
    public Text UpName;
    public Text UpStats;

    //upgradeUI
    public GameObject upgradeAnibase;
    public RectTransform gagebar;
    public Image upgradeImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UIOn(bool isBlackSmith)
    {
        uibase.SetActive(true);
        InfoUIbase.SetActive(false);
        UpUIbase.SetActive(false);
        curGodstone.text = Inventory.S.SearchItemCount("신석").ToString();
        curPowder.text = Inventory.S.SearchItemCount("신비한 가루").ToString();
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();


        List<Equipment> equips = new List<Equipment>();
        for (int i = 0; i < Player.S.EquipmentStrings.Count; i++)
        {
            equips.Add(Dictionaries.S.GetEquipment(Player.S.EquipmentStrings[i]));
        }


        if (isBlackSmith)
        {
            isblacksmith = true;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ShopName.text = "대장간";
                    break;
                case Options.Language.Eng:
                    ShopName.text = "Forge";
                    break;
                default:
                    break;
            }
            for (int i = 0; i < equips.Count; i++)
            {
                if (equips[i].equipKind == Equipment.EquipKind.Weapon)
                {
                    GameObject go = Instantiate(P_EquipSlot, T_slots);
                    EquipSlot slot = go.GetComponent<EquipSlot>();
                    slot.equipment = equips[i];
                    slot.equipImage.sprite = slot.equipment.equipSprite;
                    slots.Add(go);
                    go.GetComponent<Button>().onClick.AddListener(() => InfoUIOn(slot.equipment));
                }

            }
        }
        else
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ShopName.text = "세공사";
                    break;
                case Options.Language.Eng:
                    ShopName.text = "Jeweler";
                    break;
                default:
                    break;
            }
            isblacksmith = false;
            for (int i = 0; i < equips.Count; i++)
            {
                if (equips[i].equipKind == Equipment.EquipKind.Accecaary)
                {
                    GameObject go = Instantiate(P_EquipSlot, T_slots);
                    EquipSlot slot = go.GetComponent<EquipSlot>();
                    slot.equipment = equips[i];
                    slot.equipImage.sprite = slot.equipment.equipSprite;
                    slots.Add(go);
                    go.GetComponent<Button>().onClick.AddListener(() => InfoUIOn(slot.equipment));
                }

            }
        }
    }

    public void InfoUIOn(Equipment _equipment)
    {
        InfoUIbase.gameObject.SetActive(true);
        selectedEquip = _equipment;
        InfoImage.sprite = _equipment.equipSprite;

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                InfoName.text = _equipment.equipName;
                InfoStats.text = _equipment.stats;
                needMaterials.text = UpgradeMaterials(_equipment.UpgradeNum);
                break;
            case Options.Language.Eng:
                InfoName.text = _equipment.equipEName;
                InfoStats.text = _equipment.E_stats;
                needMaterials.text = UpgradeMaterials(_equipment.UpgradeNum);
                break;
            default:
                break;
        }
        if (needGodstone <= Inventory.S.SearchItemCount("신석")&& needPowder <= Inventory.S.SearchItemCount("신비한 가루"))
        {
            UpgradeButton.SetActive(true);
            noMaterials.SetActive(false);
        }
        else
        {
            UpgradeButton.SetActive(false);
            noMaterials.SetActive(true);
        }
        if (_equipment.UpgradeNum<3)
        {
            UpUIbase.gameObject.SetActive(true);
            UpImage.sprite = _equipment.upgradeEquip.equipSprite;

            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    UpName.text = _equipment.upgradeEquip.equipName;
                    UpStats.text = _equipment.upgradeEquip.stats;
                    break;
                case Options.Language.Eng:
                    UpName.text = _equipment.upgradeEquip.equipEName;
                    UpStats.text = _equipment.upgradeEquip.E_stats;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    needMaterials.text = "최대 강화 상태";
                    break;
                case Options.Language.Eng:
                    needMaterials.text = " Maximum upgrade.";
                    break;
                default:
                    break;
            }
            UpgradeButton.SetActive(false);
        }

    }

    public string UpgradeMaterials(int i)
    {
        switch (i)
        {
            case 0:
                needGodstone = 1;
                needPowder = 2;
                switch (Options.S.language)
                {
                    case Options.Language.Kor:

                        return "신석 : 1\n신비한 가루: : 2";
                    case Options.Language.Eng:
                        return "God stone : 1\nMystic powder: : 2";
                    default:
                        break;
                }
                break;
            case 1:
                needGodstone = 2;
                needPowder = 4;
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "신석 : 2\n신비한 가루: : 4";
                    case Options.Language.Eng:
                        return "God stone : 2\nMystic powder: : 4";
                    default:
                        break;
                }
                break;
            case 2:
                needGodstone = 3;
                needPowder = 6;
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "신석 : 3\n신비한 가루: : 6";
                    case Options.Language.Eng:
                        return "God stone : 3\nMystic powder: : 6";
                    default:
                        break;
                }
                break;
            default:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        return "신석 : 0\n신비한 가루: : 0";
                    case Options.Language.Eng:
                        return "God stone : 0\nMystic powder: : 0";
                    default:
                        break;
                }
                break;
        }
        return "";

    }

    public void Upgrade()
    {
        AddItem.S.SearchItem("신석", -needGodstone);
        Player.S.useGodstone += needGodstone;
        AddItem.S.SearchItem("신비한 가루", -needPowder);
        EquipmentUI.S.GetEquipment(selectedEquip.upgradeEquip.equipName);
        EquipmentUI.S.RemoveEquipment(selectedEquip);
        upgradeImage.sprite = selectedEquip.equipSprite;
        selectedEquip = null;
        UIOn(isblacksmith);
        SoundManager.S.PlaySE("blacksmith2");
        StartCoroutine("UpgradeUIon");
    }
    public IEnumerator UpgradeUIon()
    {
        upgradeAnibase.SetActive(true);

        for (int i = 0; i < 430; i+=10)
        {
            gagebar.sizeDelta =new Vector2(i, gagebar.sizeDelta.y);
            yield return new WaitForSeconds(0.05f);
        }
        gagebar.sizeDelta = new Vector2(0, gagebar.sizeDelta.y);
        upgradeAnibase.SetActive(false);
        StopCoroutine("UpgradeUIon");
    }
}
