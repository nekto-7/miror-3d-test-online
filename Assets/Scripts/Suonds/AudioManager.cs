using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;           // Фоновая музыка сцены
    public AudioSource ambientSource;         // Фоновые эффекты (дождь, ветер)
    public AudioSource uiSource;              // UI-звуки
    public AudioSource eventSource;           // Телепортация, победа, взаимодействие с объектами
    public AudioSource playerSource;          // Звуки игрока (передвижение, прыжок)

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Фоновые эффекты (например, дождь)
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    // Фоновые эффекты (например, дождь)
    public void PlayAmbientEffect(AudioClip clip)
    {
        if (ambientSource.clip != clip)
        {
            ambientSource.clip = clip;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }
    // Звуки интерфейса (UI)
    public void PlayUISound(AudioClip clip)
    {
        uiSource.PlayOneShot(clip);
    }
    // Событийные звуки (телепортация, победа)
    public void PlayEventSound(AudioClip clip)
    {
        eventSource.PlayOneShot(clip);
    }
    // Звуки игрока (передвижение, прыжок)
    public void PlayPlayerSound(AudioClip clip)
    {
        playerSource.PlayOneShot(clip);
    }
}