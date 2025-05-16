using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] musicSource;

    [SerializeField] private AudioSource[] SFXSource;

    [SerializeField] private AudioSource[] KeyInput;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] MusicClips;

    [SerializeField] private AudioClip[] SFXClips;

    [Header("Indexes")]
    [SerializeField] private int musicIndex = 0;
    [SerializeField] private int sfxIndex = 0;

    public int MusicIndex => musicIndex;
    public int SFXIndex => sfxIndex;
    
    void Start()
    {
        if (musicSource != null)
        {
            foreach (var source in musicSource)
            {
                if (source != null)
                {
                    source.pitch = 1.0f;
                }
            }
        }
        if (musicSource == null)
        {
            musicSource = GetComponents<AudioSource>();
        }

        if (SFXSource == null)
        {
            SFXSource = GetComponents<AudioSource>();
        }

        PlayMusic(musicIndex);

    }

    [Header("Pitch Settings")]
    [SerializeField] private float pitchChangeSpeed = 0.1f;
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 2.0f;
    private float pitchDirection = 1f;

    void Update()
    {
        if (musicSource != null && musicSource.Length > 0 && musicSource[musicIndex] != null && musicSource[musicIndex].isPlaying)
        {
            musicSource[musicIndex].pitch += pitchChangeSpeed * pitchDirection * Time.deltaTime;

            if (musicSource[musicIndex].pitch > maxPitch)
            {
                float overflow = musicSource[musicIndex].pitch - maxPitch;
                musicSource[musicIndex].pitch = minPitch + overflow;
            }
            else if (musicSource[musicIndex].pitch < minPitch)
            {
                float underflow = minPitch - musicSource[musicIndex].pitch;
                musicSource[musicIndex].pitch = maxPitch - underflow;
            }
        }

        if (musicSource != null && musicSource.Length > 0 && musicSource[musicIndex] != null && musicSource[musicIndex].clip != null && !musicSource[musicIndex].isPlaying)
        {
            musicSource[musicIndex].Play();
        }
        

    }

    public void SFXPlay(int sfxIndex)
    {
        if (SFXSource != null && SFXSource.Length > 0 && SFXSource[sfxIndex] != null)
        {
            SFXSource[sfxIndex].Play();
        }
        else
        {
            Debug.LogWarning("SFXSource is not set or index is out of range.");
        }
    }

    public void KeyInputPlay(int keyInputIndex)
    {
        if( KeyInput != null && KeyInput.Length > 0 && KeyInput[keyInputIndex] != null && Input.GetKeyDown(KeyCode.Space))
        {
            KeyInput[keyInputIndex].Play();
        }
        else
        {
            Debug.LogWarning("KeyInput is not set or index is out of range.");
        }
        
    }

    public void PlayMusic(int index)
    {
        if (MusicClips != null && index >= 0 && index < MusicClips.Length && musicSource != null)
        {
            if (musicSource != null && musicIndex >= 0 && musicIndex < musicSource.Length && musicSource[musicIndex] != null)
            {
                musicSource[musicIndex].clip = MusicClips[index];
                musicSource[musicIndex].Play();
                if (musicSource[musicIndex].pitch < minPitch || musicSource[musicIndex].pitch > maxPitch)
                {
                    float range = maxPitch - minPitch;
                    musicSource[musicIndex].pitch = minPitch + Mathf.Repeat(musicSource[musicIndex].pitch - minPitch, range);
                }
            }
        }
        else
        {
            Debug.LogWarning("Invalid music index or MusicSource/MusicClips not set.");
        }
    }
    public void PlaySFX(int index)
    {
        Debug.Log($"Playing SFX with index: {index}");
    }
    public bool IsMusicPlaying(int index)
    {
        return false;
    }
    public bool IsSFXPlaying(int index)
    {
        return SFXSource != null && index >= 0 && index < SFXSource.Length && SFXSource[index] != null && SFXSource[index].isPlaying && sfxIndex == index;
    }
}
