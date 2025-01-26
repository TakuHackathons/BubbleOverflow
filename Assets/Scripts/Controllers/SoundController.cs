using UnityEngine;
using System.Collections.Generic;

public enum BGM {
    Intro,
    Game,
}

public enum SE
{
    Ok,
    Cancel,
    BubblePop,
    BubbleCrush,
    BubblePickup,
    BubbleThrow,
    ShipLevelup,
}



public class SoundController : MonoBehaviour
{
    [System.Serializable]
    public struct BgmInfo
    {
        public BGM key;
        public AudioClip value;
    }

    [System.Serializable]
    public struct SeInfo
    {
        public SE key;
        public AudioClip value;
    }

    private static SoundController instance;

    public static SoundController Instance { get { return instance;  } }

    [SerializeField] public AudioSource bgmAudioSource;
    [SerializeField] public AudioSource seAudioSource;
    [SerializeField] public AudioSource voiceAudioSource;

    [SerializeField] public BgmInfo[] bgmList;
    [SerializeField] public SeInfo[] seList;


    private Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

 
    public void PlayBGM(BGM bgm) {
        foreach (var info in bgmList) {
            if (info.key == bgm) {
                bgmAudioSource.clip = info.value;
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
            }
        }
    }

    public void StopBGM() {
        bgmAudioSource.Stop();
    }

    public void PlaySE(SE se) {
        foreach (var info in seList)
        {
            if (info.key == se)
            {
                seAudioSource.clip = info.value;
                seAudioSource.loop = false;
                seAudioSource.Play();
            }
        }
    }

    public void PlayVoice(string voice) { 
        //TODO; Ç†Ç∆Ç≈é¿ëïÅH
    }
}
