using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Title : MonoBehaviour
{
    public Text[] texts;
    public Text[] Basics;
    public GameObject[] gameObjects;
    public void Start()
    {
        GetTexts();
    }

    public void GetTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = TextManager.S.GetTextsByName("Title", texts[i].name);
        }
        for (int i = 0; i < Basics.Length; i++)
        {
            Basics[i].text= TextManager.S.GetBasicText(Basics[i].name);
        }
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<TutoText>().ChangeText();
        }
    }

}
