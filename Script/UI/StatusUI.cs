using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusUI : MonoBehaviour
{
    public static StatusUI S;

    public Image playerImage;

    //MainStat
    public Text JobName;
    public Text LV;
    public Text HP;
    public Text ATK;
    public Text DEF;
    public Text HIT;
    public Text AVD;
    public Text SPD;
    public Text MAG;

    public Text CRC;
    public Text CRD;
    public Text HEL;
    public Text VAM;
    public Text ARC;

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

    public void StatusUIOn()
    {
        playerImage.sprite = Player.S.PlayerImage;
        JobName.text = Player.S.characterName;
        LV.text = Player.S.level.ToString();
        HP.text = Player.S.hp.ToString();
        ATK.text = Player.S.ATK.ToString();
        DEF.text = Player.S.DEF.ToString();
        HIT.text = "+"+Player.S.HIT.ToString();
        AVD.text = Mathf.CeilToInt(Player.S.AVD).ToString();
        SPD.text = Player.S.SPD.ToString();
        MAG.text = Player.S.POW.ToString();

        CRC.text = Mathf.CeilToInt(Player.S.CRC).ToString() + "%";
        CRD.text = Mathf.CeilToInt(Player.S.CRD).ToString() + "%";
        HEL.text = Player.S.HEL.ToString() + "%";
        ARC.text = Player.S.ARC.ToString() + "%";
        VAM.text = Player.S.VAM.ToString()+"%";

    }
}
