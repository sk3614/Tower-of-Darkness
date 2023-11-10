using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    static public  SoundManager S;
    #region singleton
    void Awake()
    {
        if(S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton;

    public AudioSource[] audioSourcesEffects;
    public AudioSource audioSourcesBGM;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSound;

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name==effectSounds[i].name)
            {
                for (int j = 0; j < audioSourcesEffects.Length; j++)
                {
                    if(!audioSourcesEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourcesEffects[j].clip = effectSounds[i].clip;
                        audioSourcesEffects[j].volume = Options.S.SESound;
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다");
        return;
    }

    public void PlayBG(string _name)
    {
        for (int i = 0; i < bgmSound.Length; i++)
        {
            if (_name == bgmSound[i].name)
            {
                if (bgmSound[i].clip==audioSourcesBGM.clip)
                {
                    return;
                }
                if (audioSourcesBGM.isPlaying)
                {
                    audioSourcesBGM.Stop();
                    audioSourcesBGM.loop = true;
                    audioSourcesBGM.clip = bgmSound[i].clip;
                    audioSourcesBGM.volume = Options.S.BgmSound;
                    audioSourcesBGM.Play();

                    return;
                }
                else
                {
                    audioSourcesBGM.clip = bgmSound[i].clip;
                    audioSourcesBGM.loop = true;
                    audioSourcesBGM.volume = Options.S.BgmSound;
                    audioSourcesBGM.Play();
                    return;
                }
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다");
        return;
    }
    public void StopBG()
    {
        audioSourcesBGM.Pause();
        return;
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if(playSoundName[i]==_name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }
            
        }
    }
    public void SetSEVolume()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].volume = Options.S.SESound;
        }
    }
    public void SetBGMVolume()
    {
        audioSourcesBGM.volume = Options.S.BgmSound;
    }
}
