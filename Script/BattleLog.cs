using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleLog : MonoBehaviour
{
    public static BattleLog S;

    public Text text;
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    private List<GameObject> textList=new List<GameObject>();

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
    }
    
    public void CreateLog(string _message)
    {
        text.text += "\n" + _message;
    }

    public void ChangeScrollbar()
    {
        scrollRect.verticalNormalizedPosition = 0;
        Mathf.Clamp(scrollRect.verticalNormalizedPosition, 0f, 0f);
    }
    public void ClearLog()
    {
        text.text = "";
    }
}
