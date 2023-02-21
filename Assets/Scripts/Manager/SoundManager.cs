using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioMixer audioMixer;
    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;

    const string MusicPath = "Music/";
    const string SoundPath = "Sound/";

    #region 音量的开与关
    private bool musicOn;
    public bool MusicOn
    {
        get { return musicOn; }
        set
        {
            musicOn = value;
            this.MusicMute(!musicOn);
        }
    }
    private bool soundOn;
    public bool SoundOn
    {
        get { return soundOn; }
        set
        {
            soundOn = value;
            this.SoundMute(!soundOn);
        }
    }
    #endregion


    #region 音量大小
    private int musicVolume;
    public int MusicVolume
    {
        get { return musicVolume; }
        set
        {
            if (musicVolume != value)    // 如果值没动过就啥也不干
            {
                musicVolume = value;
                if (musicOn) this.SetVolume("MusicVolume", musicVolume);    // 静音模式下不会保存音量值
            }
        }
    }

    private int soundVolume;
    public int SoundVolume
    {
        get { return soundVolume; }
        set
        {
            if (soundVolume != value)
            {
                soundVolume = value;
                if (soundOn) this.SetVolume("SoundVolume", soundVolume);
            }
        }
    }
    #endregion

    public void Init()
    {

    }
    void Start()
    {
        this.MusicVolume = SoundConfig.MusicVolume;
        this.SoundVolume = SoundConfig.SoundVolume;
        this.MusicOn = SoundConfig.MusicOn;
        this.SoundOn = SoundConfig.SoundOn;
    }

    public void MusicMute(bool mute) => this.SetVolume("MusicVolume", mute ? 0 : musicVolume);
    public void SoundMute(bool mute) => this.SetVolume("SoundVolume", mute ? 0 : soundVolume);

    /// <summary>
    /// 修改音量，从分贝映射为音量值
    /// </summary>
    private void SetVolume(string name, int value)
    {
        float volume = value * 0.5f - 50f;
        this.audioMixer.SetFloat(name, volume);     // 调用混音器方法，修改指定变量的值
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    public void PlayMusic(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(MusicPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic:{0}not existed.", name);
            return;
        }
        if (musicAudioSource.isPlaying)     // 把当前播放的给停了
            musicAudioSource.Stop();

        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(SoundPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlaySound:{0} not existed.", name);
            return;
        }

        soundAudioSource.PlayOneShot(clip);     // 音效只播放一次
    }

    protected void PlayClipOnAudioSource(AudioSource source, AudioClip clip, bool isLoop)
    {

    }




}
