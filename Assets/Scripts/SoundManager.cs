using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioMixer audioMixer;
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioClip[] MusicClips;

    [SerializeField] private AudioClip[] SFXClips;
    void Start()
    {
        if (MusicSource != null)
        {
            MusicSource.pitch = 1.0f;
        }
        if (MusicSource == null)
        {
            MusicSource = GetComponent<AudioSource>();
        }

        if (SFXSource == null)
        {
            SFXSource = GetComponent<AudioSource>();
        }

        PlayMusic(0);
    }

    // Continuously change the pitch of the music
    [SerializeField] private float pitchChangeSpeed = 0.1f;
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 2.0f;
    private float pitchDirection = 1f;

    void Update()
    {
        if (MusicSource != null && MusicSource.isPlaying)
        {
            MusicSource.pitch += pitchChangeSpeed * pitchDirection * Time.deltaTime;

            if (MusicSource.pitch > maxPitch)
            {
                float overflow = MusicSource.pitch - maxPitch;
                MusicSource.pitch = minPitch + overflow;
            }
            else if (MusicSource.pitch < minPitch)
            {
                float underflow = minPitch - MusicSource.pitch;
                MusicSource.pitch = maxPitch - underflow;
            }
        }

        if (MusicSource != null && MusicSource.clip != null && !MusicSource.isPlaying)
        {
            MusicSource.Play();
        }
    }

    public void PlayMusic(int index)
    {
        if (MusicClips != null && index >= 0 && index < MusicClips.Length && MusicSource != null)
        {
            MusicSource.clip = MusicClips[index];
            MusicSource.Play();
            if (MusicSource.pitch < minPitch || MusicSource.pitch > maxPitch)
            {
                float range = maxPitch - minPitch;
                MusicSource.pitch = minPitch + Mathf.Repeat(MusicSource.pitch - minPitch, range);
            }
        }
        else
        {
            Debug.LogWarning("Invalid music index or MusicSource/MusicClips not set.");
        }
    }
}
