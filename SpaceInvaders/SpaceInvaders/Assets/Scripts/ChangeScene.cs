using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
  public void PlayGame()
    {
        print("play");
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }
}
