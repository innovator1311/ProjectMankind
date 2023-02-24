using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagement : MonoBehaviour
{
    public static CameraManagement instance;
    public CinemachineVirtualCamera cineCamera;
    public CinemachineVirtualCamera dragCamera;
    public GameObject player;

    private void Awake() {

        instance = this;
    }

    void Start()
    {
        player = FindObjectOfType<Ship>().gameObject;
        //cineCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
       // if (player == null) GameManagement.instance.RestartLevel();
    }

    public void ChangeCamera(Transform smallShip) {
        cineCamera.Follow = smallShip;
        cineCamera.m_Lens.OrthographicSize = 3;
        player.GetComponent<Ship>().isCurrentControl = false;
    }

    public void ChangeBack(GameObject SmallShip) {
        
        if (SmallShip == player) GameManagement.instance.RestartLevel();
        else Destroy(SmallShip);

        cineCamera.Follow = player.transform;
        cineCamera.m_Lens.OrthographicSize = 5;
        player.gameObject.GetComponent<Ship>().isCurrentControl = true;   
        player.gameObject.GetComponent<Ship>().UpdateHealth(0);
        player.gameObject.GetComponent<Ship>().InitSkill();

        dragCamera.Priority = 0;
    }
    
    
}
