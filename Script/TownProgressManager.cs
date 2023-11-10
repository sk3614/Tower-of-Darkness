using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownProgressManager : MonoBehaviour
{
    public static TownProgressManager S;
    public TownNPC[] Npcs;
    public MarketPlace GuildShop;



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
    }
    public void Start()
    {
        ChangeTownProgress();
    }

    public void ChangeTownProgress()
    {
        //NPC
        for (int i = 0; i < Npcs.Length; i++)
        {
            Npcs[i].gameObject.SetActive(false);
            for (int j = 0; j < Npcs[i].npcTownProgress.Length; j++)
            {
                if (Npcs[i].npcTownProgress[j].TownProgressNum==Player.S.mainProgress)
                {
                    Npcs[i].gameObject.SetActive(true);

                } 
            }
        }
        if (Player.S.mainProgress == 8)
        {
            TownUI.S.zundo.gameObject.SetActive(false);
        }
    }
}
