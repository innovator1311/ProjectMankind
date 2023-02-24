using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ExitCamera : MonoBehaviour
{
    public GameObject exit;
    CinemachineVirtualCamera cineCamera;
    bool isActive = false;
    private void Awake() {
        cineCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        
        if (isActive) {

             

            
            
            Debug.Log("StillRunINHERE");
           
            //Invoke("RealSetActive",1.5f);

        } 
    }

    public void PreActive() {
        isActive = true;
    }

     public void SetActive() {
        //GameManagement.instance.PauseGame();
       
    }

    
    void Return() {
        
    }
}
