using UnityEngine;


public class ExitButton : MonoBehaviour
{
    public GameObject areYouSureScreen;
    public AudioSource clickSource;

    private void Start()
    {
        clickSource = GetComponent<AudioSource>();
    }
    public void AreYouSure()
    {
        clickSource.Play();
        areYouSureScreen.SetActive(true);
    }

    public void NotSure()
    {
        clickSource.Play();
        areYouSureScreen.SetActive(false);
    }

    public void ExitGame() 
    {
        clickSource.Play();
        Application.Quit();
    }
}
