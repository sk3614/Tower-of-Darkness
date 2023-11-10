using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutoText : MonoBehaviour
{
    [TextArea]
    public string Kor;
    [TextArea]
    public string Eng;
    // Start is called before the first frame update
    void Start()
    {
        if (Options.S.language==Options.Language.Kor)
        {
            GetComponent<Text>().text = Kor;
        }
        else
        {
            GetComponent<Text>().text = Eng;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText()
    {
        if (Options.S.language == Options.Language.Kor)
        {
            GetComponent<Text>().text = Kor;
        }
        else
        {
            GetComponent<Text>().text = Eng;
        }
    }
}
