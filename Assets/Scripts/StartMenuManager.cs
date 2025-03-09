using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void LoadGameMode(int modeIndex)
    {
        SceneManager.LoadScene(modeIndex); // Ensure your game mode scenes are added in Build Settings
    }
}
