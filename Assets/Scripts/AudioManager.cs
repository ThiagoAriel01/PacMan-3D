using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum ChannelType
    {
        Sfx,
        BackgroundMusic,
        UIfx,
    }

    [SerializeField] private int _sfxChannels = 5;
    [SerializeField] private int _BackgroundMusicChannels = 2;
    [SerializeField] private int _UIfxChannels = 3;
    [SerializeField] private int _3DsfxChannels = 5;
    [SerializeField] private int unicChannel = 1;

    //variables de uso
    private AudioSource[] _sfx;
    private AudioSource[] _background;
    private AudioSource[] _UIfx;
    private AudioSource[] _3Dsfx;
    private AudioSource[] _unicChannel;

    static private AudioManager _instance;
    static public AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        _sfx = new AudioSource[_sfxChannels];
        Initialize2DChannels(_sfx);

        _background = new AudioSource[_BackgroundMusicChannels];
        Initialize2DChannels(_background);

        _UIfx = new AudioSource[_UIfxChannels];
        Initialize2DChannels(_UIfx);

        _3Dsfx = new AudioSource[_3DsfxChannels];
        Initialize3DChannels(_3Dsfx);

        _unicChannel = new AudioSource[1];
        Initialize3DChannels(_unicChannel);
    }

    private void Initialize2DChannels(AudioSource[] sources)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].spatialBlend = 0;
            sources[i].playOnAwake = false;
        }
    }

    private void Initialize3DChannels(AudioSource[] sources)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i] = new GameObject().AddComponent<AudioSource>();

            sources[i].spatialBlend = 1;
            sources[i].playOnAwake = false;
            sources[i].transform.SetParent(this.transform);
        }
    }

    private void PlayClip(AudioClip clip, float volume, bool loop, AudioSource[] sources)
    {
        AudioSource source = FindEmptySource(sources);
        if (source) // if(source != null)
        {
            source.clip = clip;
            source.volume = volume;
            source.loop = loop;

            source.Play();
        }
    }

    /// <summary>
    /// Devuelve un source libre. Si no encuentra devuelve null.
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    private AudioSource FindEmptySource(AudioSource[] sources)
    {
        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        Debug.Log("No empty sources");
        return null;
    }

    public void PlayClip(AudioClip clip, float volume, bool loop, ChannelType type)
    {
        switch (type)
        {
            case ChannelType.Sfx:
                PlayClip(clip, volume, loop, _sfx);
                break;
            case ChannelType.BackgroundMusic:
                PlayClip(clip, volume, loop, _background);
                break;
            case ChannelType.UIfx:
                PlayClip(clip, volume, loop, _UIfx);
                break;
            default:
                break;
        }
    }

    public void Play3DClip(AudioClip clip, float volume, bool loop, Vector3 position, float minDistance, float maxDistance)
    {
        AudioSource source = FindEmptySource(_3Dsfx);
        if (source) // if(source != null)
        {
            source.transform.position = position;

            source.clip = clip;
            source.volume = volume;
            source.loop = loop;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;

            source.Play();
        }
    }
    public void PlayUnicClip(AudioClip clip, float volume, bool loop, Vector3 position, float minDistance, float maxDistance)
    {
        AudioSource source = _unicChannel[0];
        if (source) // if(source != null)
        {
            source.transform.position = position;

            source.clip = clip;
            source.volume = volume;
            source.loop = loop;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;

            source.Play();
        }
    }
}
