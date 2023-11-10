using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    public float setTimeScale;
    public float a;
    private bool over1;

    public void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (over1)
            {
                a -= 1 * Time.deltaTime * setTimeScale;
            }
            else
            {
                a += 1 * Time.deltaTime * setTimeScale;
            }
            if (a>1)
            {
                over1 = true;
            }
            if (a<0)
            {
                over1 = false;
            }
            gameObject.GetComponent<Text>().color = new Vector4(1, 1, 1, a);
        }

    }
}
