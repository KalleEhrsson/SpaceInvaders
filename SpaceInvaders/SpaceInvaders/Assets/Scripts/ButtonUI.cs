using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel1 = "MainScene";

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel1);
    }
}
