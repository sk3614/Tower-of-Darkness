using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Logo : MonoBehaviour
{
    public Image LogoImage;
    public float a;
    public bool over2;
    public bool noFadeIn;
    public GameObject Menu;
    public void Start()
    {
        if (!noFadeIn)
        {
            a = 0.0f;
        }

      
    }
    public void Update()
    {
        if (!gameObject.activeInHierarchy||noFadeIn)
        {
            return;
        }
        if (a<2&& !over2)
        {
            a += 1 * Time.deltaTime;
            LogoImage.color = new Color(1, 1, 1, a);
        }
        if (a>2f)
        {
            over2 = true;
        }

        if (over2)
        {
            a -= 1 * Time.deltaTime;
            LogoImage.color = new Color(1, 1, 1, a);
        }
        if(a<-1f)
        {
            Menu.SetActive(true);
            Destroy(gameObject);
        }
    }
    public void SkipLogo()
    {
        Menu.SetActive(true);
        Destroy(gameObject);
    }
}
