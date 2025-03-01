using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

public class AuthenticationManager : MonoBehaviour
{
    private async void Awake()
    {
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Logged in! Player ID: " + AuthenticationService.Instance.PlayerId);
        }
    }
}
