using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;           // ������� ������ �����
    public AudioSource ambientSource;         // ������� ������� (�����, �����)
    public AudioSource uiSource;              // UI-�����
    public AudioSource eventSource;           // ������������, ������, �������������� � ���������
    public AudioSource playerSource;          // ����� ������ (������������, ������)

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


    // ������� ������� (��������, �����)
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    // ������� ������� (��������, �����)
    public void PlayAmbientEffect(AudioClip clip)
    {
        if (ambientSource.clip != clip)
        {
            ambientSource.clip = clip;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }
    // ����� ���������� (UI)
    public void PlayUISound(AudioClip clip)
    {
        uiSource.PlayOneShot(clip);
    }
    // ���������� ����� (������������, ������)
    public void PlayEventSound(AudioClip clip)
    {
        eventSource.PlayOneShot(clip);
    }
    // ����� ������ (������������, ������)
    public void PlayPlayerSound(AudioClip clip)
    {
        playerSource.PlayOneShot(clip);
    }
}