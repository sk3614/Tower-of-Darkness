using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScript : MonoBehaviour
{
    private Animator ani;
    public Image TitleMenu;
    public bool menuOn;
    public float a;
    public GameObject[] menus;
    public GameObject button;
    public void Start()
    {
        a = .0f;
        ani = gameObject.GetComponent<Animator>();
    }
    public void Setmenu()
    {
        menuOn = true;
        TitleMenu.gameObject.SetActive(true);
        TitleMenu.color = new Color(1, 1, 1, 0);
    }
    public void Update()
    {
        if (menuOn&&a<1)
        {
            a += 1.2f * Time.deltaTime / 2;
            TitleMenu.color = new Color(1, 1, 1, a);
        }
        if(menuOn&&a > 1)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                menus[i].SetActive(true);
            }
            menuOn = false;
            Destroy(button.gameObject);
        }
    }
    public void skip()
    {
        ani.speed = 3;
        Destroy(button.gameObject);
    }
}
