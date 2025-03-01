using UnityEngine;

public class LeaderboardButtons : MonoBehaviour
{
    public GameObject leaderBoardScreen;
    public void ActivateLeaderBoardScreen()
    {
        leaderBoardScreen.SetActive(true);
    }

    public void DeactivateLeaderBoardScreen()
    {
        leaderBoardScreen.SetActive(false);
    }
}
