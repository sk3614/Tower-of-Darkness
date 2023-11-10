using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageEffect : MonoBehaviour
{
    public static DamageEffect S;
    public GameObject damagePrefab;

    public int height;
    public int yNum;

    public int x;
    public Color damageColor;
    public Color shieldColor;
    public Color healColor;
    public List<int> yValue = new List<int>();

    [SerializeField]
    private GameObject S_playerEffect= null;
    [SerializeField]
    private GameObject D_playerEffect = null;
    [SerializeField]
    private GameObject H_playerEffect = null;
    [SerializeField]
    private GameObject S_monsterEffect=null;
    [SerializeField]
    private GameObject D_monsterEffect = null;
    [SerializeField]
    private GameObject H_monsterEffect = null;

    [SerializeField]
    private List<GameObject> effects = new List<GameObject>();

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
        x = 0;
        height = 0;
        yNum = -45;
    }

    public void PlayerDamaged(string _damageNum, bool isCri = false)
    {
        GameObject damageGo = Instantiate(damagePrefab, D_playerEffect.transform);
        effects.Add(damageGo);

        damageGo.GetComponent<Text>().text =_damageNum.ToString();
        damageGo.transform.localPosition = new Vector2(x, height - yValue[0]*yNum);
        yValue[0] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(damageColor,0.35f,isCri);

    }

    public void MonsterDamaged(string _damageNum,bool isCri=false)
    {
        GameObject damageGo = Instantiate(damagePrefab, D_monsterEffect.transform);
        effects.Add(damageGo);
        damageGo.GetComponent<Text>().text = _damageNum;
        damageGo.transform.localPosition = new Vector2(x, height - yValue[1] * yNum);
        yValue[1] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(damageColor, 0.35f, isCri);
    }
    public void PlayerShield(string _shieldNum)
    {
        GameObject damageGo = Instantiate(damagePrefab, S_playerEffect.transform);
        effects.Add(damageGo);
        damageGo.GetComponent<Text>().text =_shieldNum.ToString();
        damageGo.transform.localPosition = new Vector2(x, height - yValue[2] * yNum);
        yValue[2] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(shieldColor);

    }

    public void MonsterShield(string _shieldNum)
    {
        GameObject damageGo = Instantiate(damagePrefab, S_monsterEffect.transform);
        effects.Add(damageGo);
        damageGo.GetComponent<Text>().text =_shieldNum;
        damageGo.transform.localPosition = new Vector2(x, height - yValue[3] * yNum);
        yValue[3] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(shieldColor);
    }


    public void PlayerHealed(string _healNum)
    {
        int z = Random.Range(-15, 15);
        GameObject damageGo = Instantiate(damagePrefab, H_playerEffect.transform);
        effects.Add(damageGo);
        damageGo.GetComponent<Text>().text = _healNum;
        damageGo.transform.localPosition = new Vector2(x, height - yValue[4] * yNum);
        yValue[4] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(healColor);

    }

    public void MonsterHealed(string _healNum)
    {
        int z = Random.Range(-10, 10);
        GameObject damageGo = Instantiate(damagePrefab, H_monsterEffect.transform);
        effects.Add(damageGo);
        damageGo.GetComponent<Text>().text = _healNum;
        damageGo.transform.localPosition = new Vector2(x, height - yValue[5] * yNum);
        yValue[5] += 1;
        damageGo.GetComponent<DamageEffectText>().Create(healColor);
    }
    public void RemoveAllEfeect()
    {

        for (int i = 0; i < yValue.Count; i++)
        {
            yValue[i] = 0;
        }
        //for (int i = 0; i < effects.Count; i++)
        //{
        //    Destroy(effects[i]);
        //}
       
        //effects.Clear();
    }
}
