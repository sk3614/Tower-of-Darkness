using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageEffectText : MonoBehaviour
{
    private float SetTime;
    public Text subText;
    public GameObject CriImage;
    public void Create(Color color,float _time=0.5f,bool isCri=false)
    {
        subText.text =GetComponent<Text>().text;
        subText.color = color;
        SetTime = _time;
        if (isCri == false)
        {
           CriImage.SetActive(false);
        }
        else
        {
            CriImage.SetActive(true);
        }
        gameObject.SetActive(true);
    }
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            SetTime -= 1 * Time.deltaTime;
            if (SetTime < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
