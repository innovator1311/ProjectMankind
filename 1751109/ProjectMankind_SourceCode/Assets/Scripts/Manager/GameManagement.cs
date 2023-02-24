using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    public GameObject player;
    public AudioClip activeSOund;
    public float scanInterval = 1f;
    float time = 3;
    AstarPath astarPath;
    //public AstarPath ap;
    
    private void Awake() {
        astarPath = FindObjectOfType<AstarPath>().GetComponent<AstarPath>();
        instance = this;
        Time.timeScale = 1;  
        time = scanInterval;
    }

    public void Start()
    {
        player = FindObjectOfType<Ship>().gameObject;
        player.GetComponent<Ship>().enabled = false;   
        //astarPath = GameObject.FindObjectOfType<AstarPath>();
    }
    public void NewLevel() {
        if (SceneManager.GetActiveScene().buildIndex + 1 <= 10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene(0);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame() {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
        
    }

    private void Update() {

        if (time >= float.Epsilon) {
            
            time -= Time.deltaTime;
            if (time < float.Epsilon) astarPath.Scan();
        }

        //time = scanInterval;
        
    }

    public void StartGame() {

        // ChangeUI 
        UIManagement.instance.ClosePanel();
        UIManagement.instance.stopUI = true;

        // ChangePlayer
        player.GetComponent<Ship>().enabled = true;
        player.GetComponent<Ship>().skillNumber = UIManagement.instance.skillSet;
        player.GetComponent<Ship>().InitSkill();
        
        // ChangeCamera
        CameraManagement.instance.cineCamera.Follow = player.transform;
        CameraManagement.instance.ChangeBack(null);
        //CameraManagement.instance.gameObject.GetComponent<CameraDrag>().enabled = false;
    }

    public void ActiveExit(Exit exit) {

        StartCoroutine(RealSetActive(exit));

    }

    public IEnumerator RealSetActive(Exit exit) {
        
        Time.timeScale = 0;
        exit.cineCamera.Priority = 11;
        
        Debug.Log("RuninCourutuib1");
        yield return new WaitForSecondsRealtime(1.5f);

        exit.gameObject.SetActive(true);
        AudioManagement.instance.PlayOneShot(activeSOund);
        Debug.Log("RuninCourutuib2");
        yield return new WaitForSecondsRealtime(2f);

        exit.cineCamera.Priority = 9;
        Time.timeScale = 1f;
    }



}
