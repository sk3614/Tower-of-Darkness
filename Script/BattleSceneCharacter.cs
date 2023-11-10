using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSceneCharacter : MonoBehaviour
{
    public Image image;
    public float time;
    public float aniTime;
    public Sprite[] sprites;
    public bool spriteChange;

    public float a = 1f;
    public bool isplay;
    public IEnumerator enumerator1;
    public IEnumerator enumerator2;

    public Vector2 curPosition;
    public Vector2 goalPosition;
    public float PosX;

    public int GoalSize;
    public int CurSize;

    public void Start()
    {
        time = .0f;
        aniTime = 0.5f;
        image = gameObject.GetComponent<Image>();

    }
    public void Update()
    {
        time += 1 * Time.deltaTime;

        if (time > aniTime && sprites.Length == 2)
        {
            if (spriteChange)
            {
                spriteChange = false;
                image.sprite = sprites[0];
            }
            else
            {
                spriteChange = true;
                image.sprite = sprites[1];
            }
            time = .0f;
        }
    }
    public void Blur()
    {
        enumerator1 = FadeOut();
        enumerator2 = FadeIn();
        if (isplay==false)
        {
            isplay = true;
            a = 1.0f;
            
            StartCoroutine(enumerator1);
        }
    }
    public IEnumerator FadeOut()
    {
        while(a>0.3f)
        {
            a -= 1.0f * Time.deltaTime*3;
            image.color = new Vector4(1, 1, 1, a);
            yield return null;
        }
        StartCoroutine(enumerator2);
        yield break;

    }
    public IEnumerator FadeIn()
    {
        while (a <= 1.0f)
        {
            a += 1.0f * Time.deltaTime * 3;
            image.color = new Vector4(1, 1, 1, a);
            yield return null;

        }
        a = 1.0f;
        isplay = false;
        yield break;
    }
    public void Rush(int x,Vector2 vector2)
    {
        time = .0f;
        aniTime = 0.5f;
        curPosition = vector2;
        goalPosition = curPosition + new Vector2(x, 0);
        PosX = curPosition.x;
        StartCoroutine("RushIn");
    }
    public void Stamp(int x)
    {
        time = .0f;
        aniTime = 0.3f;
        CurSize = (int)image.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        GoalSize = x;
        PosX= (int)image.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        StartCoroutine("StampIn");
    }
    public IEnumerator StampIn()
    {
        if (PosX > GoalSize)
        {
            while (PosX > GoalSize)
            {
                PosX -=1000 * Time.deltaTime * 10;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x,PosX);
                yield return null;
            }

        }
        else
        {
            while (PosX < GoalSize)
            {
                PosX += 1000 * Time.deltaTime * 10;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x, PosX);
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("StampOut");
        yield break;
    }
    public IEnumerator StampOut()
    {
        if (PosX > GoalSize)
        {
            while (PosX > CurSize)
            {
                PosX -= 200 * Time.deltaTime *10;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x, PosX);
                yield return null;
            }

        }
        else
        {
            while (PosX < CurSize)
            {
                PosX += 200 * Time.deltaTime * 10;
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x, PosX);
                yield return null;
            }
        }
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(image.gameObject.GetComponent<RectTransform>().sizeDelta.x, CurSize);
        yield break;
    }


    public IEnumerator RushIn()
    {
        if (PosX < goalPosition.x)
        {
            while (PosX < goalPosition.x)
            {
                PosX += 600 * Time.deltaTime*2;
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosX, curPosition.y);
                yield return null;
            }

        }
        else
        {
            while (PosX > goalPosition.x)
            {
                PosX -= 600 * Time.deltaTime*2;
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosX, curPosition.y);
                yield return null;
            }
        }
        StartCoroutine("RushOut");
        yield break;
    }

    public IEnumerator RushOut()
    {
        if (PosX < curPosition.x)
        {
            while (PosX < curPosition.x)
            {
                PosX += 600 * Time.deltaTime*2;
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosX, curPosition.y);
                yield return null;
            }

        }
        else
        {
            while (PosX > curPosition.x)
            {
                PosX -= 600 * Time.deltaTime*2;
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(PosX, curPosition.y);
                yield return null;
            }
        }
        image.GetComponent<RectTransform>().anchoredPosition = curPosition;
        yield break;
    }
}
