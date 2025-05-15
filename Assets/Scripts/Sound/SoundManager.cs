using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] public AudioSource[] MusicSource;

    [SerializeField] public AudioSource[] SFXSource;

    [SerializeField] private AudioSource[] KeyInput;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] MusicClips;

    [SerializeField] private AudioClip[] SFXClips;

    [Header("Indexes")]
    [SerializeField] public int musicIndex = 0;
    [SerializeField] public int sfxIndex = 0;

    public int MusicIndex => musicIndex;
    public int SFXIndex => sfxIndex;
    
    void Start()
    {
        if (MusicSource != null)
        {
            foreach (var source in MusicSource)
            {
                if (source != null)
                {
                    source.pitch = 1.0f;
                }
            }
        }
        if (MusicSource == null)
        {
            MusicSource = GetComponents<AudioSource>();
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
        if (MusicSource != null && MusicSource.Length > 0 && MusicSource[musicIndex] != null && MusicSource[musicIndex].isPlaying)
        {
            MusicSource[musicIndex].pitch += pitchChangeSpeed * pitchDirection * Time.deltaTime;

            if (MusicSource[musicIndex].pitch > maxPitch)
            {
                float overflow = MusicSource[musicIndex].pitch - maxPitch;
                MusicSource[musicIndex].pitch = minPitch + overflow;
            }
            else if (MusicSource[musicIndex].pitch < minPitch)
            {
                float underflow = minPitch - MusicSource[musicIndex].pitch;
                MusicSource[musicIndex].pitch = maxPitch - underflow;
            }
        }

        if (MusicSource != null && MusicSource.Length > 0 && MusicSource[musicIndex] != null && MusicSource[musicIndex].clip != null && !MusicSource[musicIndex].isPlaying)
        {
            MusicSource[musicIndex].Play();
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
        if( KeyInput != null && KeyInput.Length > 0 && KeyInput[keyInputIndex] != null && Input.GetKeyDown(KeyCode.Q))
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
        if (MusicClips != null && index >= 0 && index < MusicClips.Length && MusicSource != null)
        {
            if (MusicSource != null && musicIndex >= 0 && musicIndex < MusicSource.Length && MusicSource[musicIndex] != null)
            {
                MusicSource[musicIndex].clip = MusicClips[index];
                MusicSource[musicIndex].Play();
                if (MusicSource[musicIndex].pitch < minPitch || MusicSource[musicIndex].pitch > maxPitch)
                {
                    float range = maxPitch - minPitch;
                    MusicSource[musicIndex].pitch = minPitch + Mathf.Repeat(MusicSource[musicIndex].pitch - minPitch, range);
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
