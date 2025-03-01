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

public enum Voice
{
    Greeting,
    PickUp,
    Throw,
    Win,
    Lose,
}

public class SoundController : SingletonBehaviour<SoundController>
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

    [System.Serializable]
    public struct VoiceInfo
    {
        public Voice key;
        public AudioClip value;
    }

    [SerializeField] public AudioSource bgmAudioSource;
    [SerializeField] public AudioSource seAudioSource;
    [SerializeField] public AudioSource voiceAudioSource;

    [SerializeField] public BgmInfo[] bgmList;
    [SerializeField] public SeInfo[] seList;
    [SerializeField] public VoiceInfo[] dogVoiceList;
    [SerializeField] public VoiceInfo[] catVoiceList;
    [SerializeField] public VoiceInfo[] rabbitVoiceList;
    [SerializeField] public VoiceInfo[] horseVoiceList;

    private Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();

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

    public void PlayVoice(int playerNum, Voice voice)
    {
        switch (playerNum)
        {
            case 0:
                PlayDogVoice(voice);
                break;
            case 1:
                PlayCatVoice(voice);
                break;
            case 2:
                PlayRabbitVoice(voice);
                break;
            case 3:
                PlayHorseVoice(voice);
                break;
        }
    }

    public void PlayDogVoice(Voice voice) {
        foreach (var info in dogVoiceList)
        {
            if (info.key == voice)
            {
                voiceAudioSource.clip = info.value;
                voiceAudioSource.loop = false;
                voiceAudioSource.Play();
            }
        }
    }

    public void PlayCatVoice(Voice voice)
    {
        foreach (var info in catVoiceList)
        {
            if (info.key == voice)
            {
                voiceAudioSource.clip = info.value;
                voiceAudioSource.loop = false;
                voiceAudioSource.Play();
            }
        }
    }

    public void PlayRabbitVoice(Voice voice)
    {
        foreach (var info in rabbitVoiceList)
        {
            if (info.key == voice)
            {
                voiceAudioSource.clip = info.value;
                voiceAudioSource.loop = false;
                voiceAudioSource.Play();
            }
        }
    }

    public void PlayHorseVoice(Voice voice)
    {
        foreach (var info in horseVoiceList)
        {
            if (info.key == voice)
            {
                voiceAudioSource.clip = info.value;
                voiceAudioSource.loop = false;
                voiceAudioSource.Play();
            }
        }
    }
}
