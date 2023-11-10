using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    public static Dictionaries S;


    public TowerObjectData[] objects;
    public TowerObjectData[] monsterObjects;

    private Dictionary<int,TowerObjectData> D_TowerObject = new Dictionary<int,TowerObjectData>();//타워오브젝트 데이터
   // private Dictionary<Vector4, Vector4> D_floorDic= new Dictionary<Vector4, Vector4>();
    private Dictionary<Vector4, int> D_NPCDic = new Dictionary<Vector4, int>();

   // private Dictionary<Vector2, Vector2Int> D_UpStair = new Dictionary<Vector2, Vector2Int>();
   // private Dictionary<Vector2, Vector2Int> D_DownStair = new Dictionary<Vector2, Vector2Int>();

    private Dictionary<Vector4, Vector4> D_SecretWall = new Dictionary<Vector4, Vector4>(); //앞에꺼 찾는위치 뒤에꺼 사라질 벽 위치
    private Dictionary<string, Equipment> D_Equipment = new Dictionary<string, Equipment>();
    public Equipment[] equipments;

    
    private void Awake()
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

        for (int i = 0; i < objects.Length; i++)
        {
            if (D_TowerObject.ContainsKey(objects[i].ObjectNum))
            {
                continue;
            }

            D_TowerObject.Add(objects[i].ObjectNum, objects[i]);
        }
        for (int i = 0; i < monsterObjects.Length; i++)
        {
            D_TowerObject.Add(monsterObjects[i].ObjectNum, monsterObjects[i]);
        }
        //AddFloorDic();
        AddNPCDic();
       // AddStairDic();
        AddSecretWall();
        AddEquipment();
    }

    public void Start()
    {
        
    }

    public TowerObjectData GetTowerObjectDic(int objectNum,int x=0, int y=0)
    {
        if (D_TowerObject.ContainsKey(objectNum))
        {
            return D_TowerObject[objectNum];
        }
        Debug.Log("존재하지않는 오브젝트번호" + objectNum.ToString());
        return D_TowerObject[100];
    }

    //public Vector4 GetFloorData(int _tower,int floor,int x, int y)
    //{
    //    Vector4 floorData = new Vector4(_tower, floor, x, y);

    //    return (D_floorDic[floorData]);
    //}

    public int GetNPCData(int _tower, int floor, int x, int y)
    {
        Vector4 npcData = new Vector4(_tower, floor, x, y);
        return D_NPCDic[npcData];
    }

    //public Vector2Int GetStair(int TowerNum, int Floor, bool Up)
    //{
        
    //    if (Up)
    //    {
    //        return D_UpStair[new Vector2(TowerNum, Floor)];
    //    }
    //    else
    //    {
    //        return D_DownStair[new Vector2(TowerNum, Floor)];
    //    }
    //}

    public Vector4 GetSecretWallData(int _tower, int floor, int x, int y)
    {
        Vector4 floorData = new Vector4(_tower, floor, x, y);

        return (D_SecretWall[floorData]);
    }
    //public void AddFloorDic()
    //{
    //    //0층
    //    D_floorDic.Add(new Vector4(1, 0, 4, 8), new Vector4(1, 1, 4, 8));
    //    //1층
    //    D_floorDic.Add(new Vector4(1, 1, 4, 8), new Vector4(1, 0, 4, 8));
    //    D_floorDic.Add(new Vector4(1, 1, 6, 8), new Vector4(1, 2, 6, 8));
    //    //2층
    //    D_floorDic.Add(new Vector4(1, 2, 6, 8), new Vector4(1, 1, 6, 8));
    //    D_floorDic.Add(new Vector4(1, 2, 6, 0), new Vector4(1, 3, 6, 0));
    //    D_floorDic.Add(new Vector4(1, 2, 8, 0), new Vector4(1, 3, 8, 0));
    //    //3층
    //    D_floorDic.Add(new Vector4(1, 3, 1, 6), new Vector4(1, 4, 1, 6));
    //    D_floorDic.Add(new Vector4(1, 3, 6, 0), new Vector4(1, 2, 6, 0));
    //    D_floorDic.Add(new Vector4(1, 3, 8, 0), new Vector4(1, 2, 8, 0));
    //    //4층
    //    D_floorDic.Add(new Vector4(1, 4, 1, 6), new Vector4(1, 3, 1, 6));
    //    D_floorDic.Add(new Vector4(1, 4, 8, 4), new Vector4(1, 5, 8, 4));
    //    //5층
    //    D_floorDic.Add(new Vector4(1, 5, 8, 4), new Vector4(1, 4, 8, 4));
    //    D_floorDic.Add(new Vector4(1, 5, 0, 4), new Vector4(1, 6, 0, 4));
    //    //6층
    //    D_floorDic.Add(new Vector4(1, 6, 0, 4), new Vector4(1, 5, 0, 4));
    //    D_floorDic.Add(new Vector4(1, 6, 8, 1), new Vector4(1, 7, 8, 1));
    //    //7층
    //    D_floorDic.Add(new Vector4(1, 7, 8, 1), new Vector4(1, 6, 8, 1));
    //    D_floorDic.Add(new Vector4(1, 7, 0, 4), new Vector4(1, 8, 0, 5));
    //    D_floorDic.Add(new Vector4(1, 7, 0, 2), new Vector4(1, 8, 0, 3));
    //    //8층
    //    D_floorDic.Add(new Vector4(1, 8, 0, 5), new Vector4(1, 7, 0, 4));
    //    D_floorDic.Add(new Vector4(1, 8, 0, 3), new Vector4(1, 7, 0, 2));
    //    D_floorDic.Add(new Vector4(1, 8, 8, 6), new Vector4(1, 9, 8, 6));
    //    //9층
    //    D_floorDic.Add(new Vector4(1, 9, 8, 6), new Vector4(1, 8, 8, 6));
    //    D_floorDic.Add(new Vector4(1, 9, 0, 6), new Vector4(1, 10, 0, 6));
    //    //10층
    //    D_floorDic.Add(new Vector4(1, 10, 0, 6), new Vector4(1, 9, 0, 6));
    //    D_floorDic.Add(new Vector4(1, 10, 8, 6), new Vector4(1, 11, 8, 6));
    //    //11층
    //    D_floorDic.Add(new Vector4(1, 11, 0, 6), new Vector4(1, 12, 0, 6));
    //    D_floorDic.Add(new Vector4(1, 11, 0, 0), new Vector4(1, 12, 0, 0));
    //    D_floorDic.Add(new Vector4(1, 11, 8, 6), new Vector4(1, 10, 8, 6));
    //    //12층
    //    D_floorDic.Add(new Vector4(1, 12, 0, 0), new Vector4(1, 11, 0, 0));
    //    D_floorDic.Add(new Vector4(1, 12, 0, 6), new Vector4(1, 11, 0, 6));
    //    D_floorDic.Add(new Vector4(1, 12, 8, 4), new Vector4(1, 13, 8, 4));
    //    //13층
    //    D_floorDic.Add(new Vector4(1, 13, 8, 2), new Vector4(1, 14, 8, 2));
    //    D_floorDic.Add(new Vector4(1, 13, 8, 4), new Vector4(1, 12, 8, 4));
    //    //14층
    //    D_floorDic.Add(new Vector4(1, 14, 8, 2), new Vector4(1, 13, 8, 2));
    //    D_floorDic.Add(new Vector4(1, 14, 0, 8), new Vector4(1, 15, 0, 8));
    //    //15층
    //    D_floorDic.Add(new Vector4(1, 15, 0, 8), new Vector4(1, 14, 0, 8));
    //    D_floorDic.Add(new Vector4(1, 15, 8, 0), new Vector4(1, 16, 10, 0));
    //    //16층
    //    D_floorDic.Add(new Vector4(1, 16, 10, 0), new Vector4(1, 15, 8, 0));
    //    D_floorDic.Add(new Vector4(1, 16, 5, 10), new Vector4(1, 17, 5, 10));
    //    //17층
    //    D_floorDic.Add(new Vector4(1, 17, 5, 0), new Vector4(1, 18, 5, 0));
    //    D_floorDic.Add(new Vector4(1, 17, 5, 10), new Vector4(1, 16, 5, 10));
    //    //18층
    //    D_floorDic.Add(new Vector4(1, 18, 5, 0), new Vector4(1, 17, 5, 0));
    //    D_floorDic.Add(new Vector4(1, 18, 8, 10), new Vector4(1, 19, 8, 10));
    //    //19층
    //    D_floorDic.Add(new Vector4(1, 19, 9, 2), new Vector4(1, 20, 9, 2));
    //    D_floorDic.Add(new Vector4(1, 19, 8, 10), new Vector4(1, 18, 8, 10));
    //    //20층
    //    D_floorDic.Add(new Vector4(1, 20, 9, 2), new Vector4(1, 19, 9, 2));
    //    D_floorDic.Add(new Vector4(1, 20, 1, 2), new Vector4(1, 21, 5, 1));


    //}

    public void AddNPCDic()
    {
        //0탑
        D_NPCDic.Add(new Vector4(0, 0, 3, 4), 1);
        D_NPCDic.Add(new Vector4(0, 0, 5, 4), 2);
        D_NPCDic.Add(new Vector4(0, 1, 4, 6), 3);
        D_NPCDic.Add(new Vector4(0, 1, 4, 1), 4);
        D_NPCDic.Add(new Vector4(0, 2, 5, 2), 5);
        D_NPCDic.Add(new Vector4(0, 2, 3, 6), 6);
        D_NPCDic.Add(new Vector4(0, 3, 8, 1), 7);
        D_NPCDic.Add(new Vector4(0, 3, 5, 7), 8);
        D_NPCDic.Add(new Vector4(0, 6, 4, 4), 9);





        //1탑
        D_NPCDic.Add(new Vector4(1, 0, 2, 6), 10);
        D_NPCDic.Add(new Vector4(1, 0, 6, 6), 11);

        D_NPCDic.Add(new Vector4(1, 1, 4, 4), 12);

        D_NPCDic.Add(new Vector4(1, 2, 5, 8), 13);

        D_NPCDic.Add(new Vector4(1, 2, 5, 6), 14);

        D_NPCDic.Add(new Vector4(1, 3, 7, 0), 15);

        D_NPCDic.Add(new Vector4(1, 4, 5, 0), 16);

        D_NPCDic.Add(new Vector4(1, 5, 8, 1), 17);

        D_NPCDic.Add(new Vector4(1, 7, 1, 8), 18);

        D_NPCDic.Add(new Vector4(1, 8, 2, 4), 19);

        D_NPCDic.Add(new Vector4(1, 9, 5, 4), 20);

        D_NPCDic.Add(new Vector4(1, 11, 0, 3), 21);

        D_NPCDic.Add(new Vector4(1, 11, 0, 1), 22);

        D_NPCDic.Add(new Vector4(1, 12, 7, 5), 23);

        D_NPCDic.Add(new Vector4(1, 13, 1, 1), 24);

        D_NPCDic.Add(new Vector4(1, 15, 4, 1), 25);

        D_NPCDic.Add(new Vector4(1, 16, 5, 8), 26);

        D_NPCDic.Add(new Vector4(1, 19, 0, 0), 27);

        D_NPCDic.Add(new Vector4(1, 19, 8, 1), 28);

        D_NPCDic.Add(new Vector4(1, 20, 6, 6), 29);

        D_NPCDic.Add(new Vector4(1, 20, 6, 2), 30);


        //2탑
        D_NPCDic.Add(new Vector4(2, 0, 3, 2), 31);

        D_NPCDic.Add(new Vector4(2, 1, 4, 0), 32);

        D_NPCDic.Add(new Vector4(2, 2, 2, 6), 33);

        D_NPCDic.Add(new Vector4(2, 2, 6, 2), 34);

        D_NPCDic.Add(new Vector4(2, 5, 0, 0), 35);

        D_NPCDic.Add(new Vector4(2, 6, 4, 0), 36);

        D_NPCDic.Add(new Vector4(2, 7, 1, 8), 37);

        D_NPCDic.Add(new Vector4(2, 7, 7, 8), 38);

        D_NPCDic.Add(new Vector4(2, -4, 6, 6), 39);

        D_NPCDic.Add(new Vector4(2, -4, 6, 4), 40);

        D_NPCDic.Add(new Vector4(2, -6, 8, 1), 41);

        D_NPCDic.Add(new Vector4(2, -7, 0, 1), 42);

        D_NPCDic.Add(new Vector4(2, -7, 1, 4), 251);

        D_NPCDic.Add(new Vector4(2, -12, 8, 8), 43);

        D_NPCDic.Add(new Vector4(2, -13, 4, 8), 44);

        D_NPCDic.Add(new Vector4(2, -13, 6, 8), 45);

        D_NPCDic.Add(new Vector4(2, -14, 8, 0), 46);

        D_NPCDic.Add(new Vector4(2, -18, 0, 4), 47);

        D_NPCDic.Add(new Vector4(2, -18, 8, 4), 48);

        //3탑
        D_NPCDic.Add(new Vector4(3, 0, 4, 1), 49);

        D_NPCDic.Add(new Vector4(3, -2, 0, 4), 50);

        D_NPCDic.Add(new Vector4(3, -4, 0, 7), 51);

        D_NPCDic.Add(new Vector4(3, -4, 0, 6), 52);

        D_NPCDic.Add(new Vector4(3, -4, 8, 7), 53);

        D_NPCDic.Add(new Vector4(3, -4, 8, 6), 54);

        D_NPCDic.Add(new Vector4(3, -8, 0, 7), 55);

        D_NPCDic.Add(new Vector4(3, -8, 0, 1), 56);

        D_NPCDic.Add(new Vector4(3, -11, 0, 7), 57);

        D_NPCDic.Add(new Vector4(3, -11, 0, 1), 58);

        D_NPCDic.Add(new Vector4(3, -16, 1, 4), 59);

        D_NPCDic.Add(new Vector4(3, -16, 7, 4), 60);

        D_NPCDic.Add(new Vector4(3, -18, 0, 0), 61);

        D_NPCDic.Add(new Vector4(3, 1, 0, 0), 62);

        D_NPCDic.Add(new Vector4(3, 1, 8, 0), 63);

        D_NPCDic.Add(new Vector4(3, 3, 3, 6), 64);

        D_NPCDic.Add(new Vector4(3, 3, 5, 6), 65);

        D_NPCDic.Add(new Vector4(3, 7, 1, 1), 66);

        D_NPCDic.Add(new Vector4(3, 7, 7, 1), 67);

        D_NPCDic.Add(new Vector4(3, 11, 7, 8), 68);
        D_NPCDic.Add(new Vector4(3, 13, 4, 7), 69);
        D_NPCDic.Add(new Vector4(3, 13, 4, 1), 70);
        D_NPCDic.Add(new Vector4(3, 15, 0, 4), 71);
        D_NPCDic.Add(new Vector4(3, 18, 7, 0), 72);
        D_NPCDic.Add(new Vector4(3, 19, 7, 0), 73);

        //4탑
        D_NPCDic.Add(new Vector4(4, 1, 0, 4), 74);
        D_NPCDic.Add(new Vector4(4, 1, 8, 4), 75);
        D_NPCDic.Add(new Vector4(4, 4, 2, 7), 76);
        D_NPCDic.Add(new Vector4(4, 5, 1, 7), 77);
        D_NPCDic.Add(new Vector4(4, 5, 7, 7), 78);
        D_NPCDic.Add(new Vector4(4, 5, 1, 5), 79);
        D_NPCDic.Add(new Vector4(4, 5, 7, 5), 80);
        D_NPCDic.Add(new Vector4(4, 5, 1, 3), 81);
        D_NPCDic.Add(new Vector4(4, 5, 7, 3), 82);
        D_NPCDic.Add(new Vector4(4, 6, 0, 0), 83);
        D_NPCDic.Add(new Vector4(4, 10, 1, 1), 84);
        D_NPCDic.Add(new Vector4(4, 11, 7, 4), 85);
        D_NPCDic.Add(new Vector4(4, 15, 1, 7), 86);
        D_NPCDic.Add(new Vector4(4, -4, 1, 0), 87);
        D_NPCDic.Add(new Vector4(4, -4, 7, 0), 88);

        D_NPCDic.Add(new Vector4(4, 16, 8, 4), 89);
        D_NPCDic.Add(new Vector4(4, 17, 0, 1), 90);
        D_NPCDic.Add(new Vector4(4, 17, 8, 1), 91);
        D_NPCDic.Add(new Vector4(4, 19, 8, 0), 92);
        D_NPCDic.Add(new Vector4(4, 23, 7, 3), 93);
        D_NPCDic.Add(new Vector4(4, 21, 7, 6), 200);



        //7탑
        D_NPCDic.Add(new Vector4(7, 2, 4, 3), 250);
    }
    public void AddSecretWall()
    {
        D_SecretWall.Add(new Vector4(1, 0, 1, 2), new Vector4(1, 1, 3, 5));
    }
    public void AddEquipment()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            D_Equipment.Add(equipments[i].equipName, equipments[i]);
        }
    }
    public Equipment GetEquipment(string name)
    {
        return D_Equipment[name];
    }
    
}
