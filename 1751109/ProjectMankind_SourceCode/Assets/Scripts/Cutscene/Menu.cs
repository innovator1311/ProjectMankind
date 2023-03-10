using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioClip clickButton;
    
    public void NewGame() {
        AudioManagement.instance.PlayOneShot(clickButton);
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
