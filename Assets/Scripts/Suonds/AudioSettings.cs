using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Sliders for Volume Settings")]
    public Slider musicSlider;   
    public Slider otherSlider;   

    private void Start()
    {
        LoadVolumeSettings();

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        otherSlider.onValueChanged.AddListener(SetOtherVolumes);
    }

    private void LoadVolumeSettings()
    {
        float defaultVolume = 0.5f;
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        otherSlider.value = PlayerPrefs.GetFloat("OtherVolume", defaultVolume);

        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        // ��������� ��������� ������
        AudioManager.instance.musicSource.volume = musicSlider.value;

        // ��������� ��������� ��� ��������� ���������� �����
        float otherVolume = otherSlider.value;
        AudioManager.instance.ambientSource.volume = otherVolume;
        AudioManager.instance.uiSource.volume = otherVolume;
        AudioManager.instance.eventSource.volume = otherVolume;
        AudioManager.instance.playerSource.volume = otherVolume;
    }

    public void SetMusicVolume(float value)
    {

        AudioManager.instance.musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetOtherVolumes(float value)
    {
        
        AudioManager.instance.ambientSource.volume = value;
        AudioManager.instance.uiSource.volume = value;
        AudioManager.instance.eventSource.volume = value;
        AudioManager.instance.playerSource.volume = value;
        PlayerPrefs.SetFloat("OtherVolume", value);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
