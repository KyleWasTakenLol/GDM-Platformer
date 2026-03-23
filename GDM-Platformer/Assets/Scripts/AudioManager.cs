using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip damageSound;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameManager.Instance.onScoreChanged += OnScoreChanged;
        GameManager.Instance.onHealthChanged += OnHealthChanged;
        PlayMusic(backgroundMusic);
    }

    void OnDestroy()
    {
        GameManager.Instance.onScoreChanged -= OnScoreChanged;
        GameManager.Instance.onHealthChanged -= OnHealthChanged;
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    void OnScoreChanged(int newScore)
    {
        PlaySoundEffect(coinSound);
    }

    void OnHealthChanged(int newHealth)
    {
        PlaySoundEffect(damageSound);
    }
}