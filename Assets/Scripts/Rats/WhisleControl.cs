using UnityEngine;

public class WhisleCOntrol : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] private SoundManager soundManager;

    [Header("Whisle Settings")]
    [SerializeField] private float whisleRadius = 5f;

    [SerializeField] private float whisleDuration = 2f;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on this GameObject.");
            return;
        }

        // Assign SoundManager if not set in Inspector
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>();
            if (soundManager == null)
            {
                Debug.LogError("SoundManager not found in the scene.");
            }
        }

        var shape = particleSystem.shape;
        shape.radius = whisleRadius;

        var main = particleSystem.main;
        main.startLifetime = whisleDuration;
    }


    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundManager.KeyInputPlay(0);
            particleSystem.Play();
        }
    }
}
