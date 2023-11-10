using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerVariable : MonoBehaviour
{
    public static TowerVariable S;

    public bool ___________Tower0______________;
    public bool isFrontStair;
    public bool isFrontMonster;
    public bool isFirstBattle;
    public bool isIronWallTile;
    

    public bool ___________Tower1______________;
    public bool npc13;
    public bool npc24;
    public bool npc27;
    public bool on21Floor;
    public bool floor12Shop;
    public int Shop1AtkGold=10;
    public int Shop1DefGold=10;
    public int Shop2AtkGold = 25;
    public int Shop2DefGold = 25;
    public int KillLich = 0;

    public bool ___________Tower2______________;

    public bool isFloorB1;
    public int Shop3AtkGold = 40;
    public int Shop3DefGold = 40;
    public bool on10Floor;
    public bool isMeetFairy;
    public bool isClassUp;
    public bool npc47;
    public bool npc48;
    public int killVamLord;
    public bool killVamEvent;
    public bool isSecretStair;

    public int Shop4AtkGold = 100;
    public int Shop4DefGold = 100;

    public bool ___________Tower3______________;

    public int Shop5AtkGold = 80;
    public int Shop5DefGold = 80;
    public int Shop6AtkGold = 200;
    public int Shop6DefGold = 200;

    public bool market11=false;
    public string Arti1;
    public string Arti2;
    public string Arti3;
    public bool isfloorB20;
    public bool isgetallitemB21;
    public bool changeStair;
    public bool floor9Shop;
    public bool floor9Shop2;
    public bool opengoldGate;
    public int golemKillCount;
    public bool T7F4;
    public bool npc72;


    public bool ___________Tower4______________;

    //저장필요
    public int Shop7AtkGold = 120;
    public int Shop7DefGold = 120;
    public int Shop8AtkGold = 350;
    public int Shop8DefGold = 350;
    public bool GetStats;
    public string T4Arti1;
    public string T4Arti2;
    public string T4Arti3;
    public bool market20;
    public bool floor5shop;
    public bool KillLady;
    public bool npc87;
    //
    public bool KillScolpion;
    public int KillMonsterInFloor6;
    public int KillMonsterInFloor24;
    public bool floor24;


    public void Awake()
    {
        if (S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
