using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPrinter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private string fullText = "In one of the fantastic realms, unlike any known hero, there lived a pickle-man.\r\nThis pickle-man was a true pickle nationalist, ready to do anything for the sake of his people.\r\nOne day, he witnessed his fellow pickles being mercilessly chopped up and slaughtered for sandwich-making.\r\nHe declared war on this cruelty—every pickle-filled sandwich from now on would be crushed beneath his sacred hammer!";
    public float delayBetweenLetters = 0.1f;
    public AudioSource audioSource;
    public AudioClip typeSound;
    AudioSource clickSource;

    private void Start()
    {
        clickSource = GetComponent<AudioSource>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!");
            return;
        }

        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textComponent.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];

            if (audioSource != null && typeSound != null)
            {
                audioSource.PlayOneShot(typeSound);
            }

            yield return new WaitForSeconds(delayBetweenLetters);
        }

        Debug.Log("Text animation finished!");
    }

    public void ChangeScene()
    {
        
        SceneManager.LoadScene(1);
    }

    public void PlayClickSound()
    {
        clickSource.Play();
    }
}