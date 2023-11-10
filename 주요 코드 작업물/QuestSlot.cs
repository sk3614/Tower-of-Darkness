using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlot : MonoBehaviour
{
    public QuestData questData;
    public Text questTitle;
    public Image CheckImage;
    public Sprite wait;
    public Sprite Accept;
    public Sprite Success;
    public Sprite Clear;
    public Sprite Fail;

    public void SetCheckImage()
    {
        switch (questData.questState)
        {
            case QuestData.QuestState.Wait:
                break;
            case QuestData.QuestState.Accept:
                break;
            case QuestData.QuestState.Success:
                break;
            case QuestData.QuestState.Clear:
                break;
            case QuestData.QuestState.Fail:
                break;
            default:
                break;
        }
    }
}
