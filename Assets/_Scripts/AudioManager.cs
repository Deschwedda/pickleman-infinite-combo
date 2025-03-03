using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton pattern

    public GameObject soundPrefab; 
    public int poolSize = 10; 

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

    
    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.volume = Random.Range(0.8f, 1f); 
                audioSource.pitch = Random.Range(0.9f, 1.1f); 
                audioSource.Play();
                return;
            }
        }

        AudioSource oldestSource = audioSources[0];
        oldestSource.Stop();
        oldestSource.clip = clip;
        oldestSource.volume = Random.Range(0.8f, 1f); 
        oldestSource.pitch = Random.Range(0.9f, 1.1f); 
        oldestSource.Play();
    }
}