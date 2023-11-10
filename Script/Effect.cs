using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : MonoBehaviour
{
    public GameObject character;
    public Animator ani;

    private void Start()
    {
        
    }

    public void PlayEffect(string EffectName)
    {
        ani = GetComponent<Animator>();
        if (EffectName!="")
        {
            ani.Play(EffectName);
        }

        
    }
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
    public void PlaySE(string _SEName)
    {
        if (_SEName=="deer1")
        {
            int value = Random.Range(0, 3);
            switch (value)
            {
                
                case 0:
                    _SEName = "deer1";
                    break;
                case 1:
                    _SEName = "deer2";
                    break;
                case 2:
                    _SEName = "deer3";
                    break;
                default:
                    break;
            }
        }
        SoundManager.S.PlaySE(_SEName);
    }
    public void Blur()
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            Battle.S.playerImage.GetComponent<BattleSceneCharacter>().Blur();
        }
        else
        {
            Battle.S.monster.GetComponent<BattleSceneCharacter>().Blur();
        }
    }
    public void Rush()
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            Battle.S.playerImage.GetComponent<BattleSceneCharacter>().Rush(-150,new Vector2(-326,317));
        }
        else
        {
            Battle.S.monster.GetComponent<BattleSceneCharacter>().Rush(150,new Vector2(322,317));
        }
    }
    public void Stamp()
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            Battle.S.playerImage.GetComponent<BattleSceneCharacter>().Stamp(75);
        }
        else
        {
            Battle.S.monster.GetComponent<BattleSceneCharacter>().Stamp(75);
        }
    }
}
