using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeFont : MonoBehaviour
{
    public Font font;
    private Text[] texts;
    public void Start()
    {
        texts = GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].font = font;
            texts[i].fontStyle = FontStyle.Normal;
        }
        
    }
}
