using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton pattern

    public GameObject soundPrefab; // Ses için prefab (AudioSource içermeli)
    public int poolSize = 10; // Ses havuzu boyutu

    private List<AudioSource> audioSources;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSources = new List<AudioSource>();

        // Ses havuzunu oluþtur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject soundObject = Instantiate(soundPrefab, transform);
            AudioSource audioSource = soundObject.GetComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
    }

    // Ses çalmak için public metod
    public void PlaySound(AudioClip clip)
    {
        // Boþ bir AudioSource bul
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.volume = Random.Range(0.8f, 1f); // Ses seviyesini rastgele ayarla
                audioSource.pitch = Random.Range(0.9f, 1.1f); // Perdeyi rastgele ayarla
                audioSource.Play();
                return;
            }
        }

        // Eðer tüm AudioSource'lar doluysa, en eski olaný durdur ve yeni sesi çal
        AudioSource oldestSource = audioSources[0];
        oldestSource.Stop();
        oldestSource.clip = clip;
        oldestSource.volume = Random.Range(0.8f, 1f); // Ses seviyesini rastgele ayarla
        oldestSource.pitch = Random.Range(0.9f, 1.1f); // Perdeyi rastgele ayarla
        oldestSource.Play();
    }
}