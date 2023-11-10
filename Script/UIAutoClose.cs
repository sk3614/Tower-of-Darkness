using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAutoClose : MonoBehaviour
{
    public float CloseTime;
    public float nowTime;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            nowTime += 1.0f * Time.deltaTime;
            CloseUI();
        }
    }

    public void OpenUI()
    {
        nowTime = 0f;
    }
    private void CloseUI()
    {
        if (nowTime>CloseTime)
        {
            nowTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
