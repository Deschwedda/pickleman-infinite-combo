using UnityEngine;
using System.Collections.Generic;

public class AudioManagerProxyMine : MonoBehaviour
{
    public static AudioManagerProxyMine Instance; // Singleton pattern

    public GameObject soundPrefab; // Ses i�in prefab (AudioSource i�ermeli)
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

        // Ses havuzunu olu�tur
        for (int i = 0; i < poolSize; i++)
        {
            GameObject soundObject = Instantiate(soundPrefab, transform);
            AudioSource audioSource = soundObject.GetComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
    }

    // Ses �almak i�in public metod
    public void PlaySound(AudioClip clip)
    {
        // Bo� bir AudioSource bul
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

        // E�er t�m AudioSource'lar doluysa, en eski olan� durdur ve yeni sesi �al
        AudioSource oldestSource = audioSources[0];
        oldestSource.Stop();
        oldestSource.clip = clip;
        oldestSource.volume = Random.Range(0.8f, 1f); // Ses seviyesini rastgele ayarla
        oldestSource.pitch = Random.Range(0.9f, 1.1f); // Perdeyi rastgele ayarla
        oldestSource.Play();
    }
}