using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Exit : MonoBehaviour
{
    [HideInInspector]
    public int numberOfTrigger = 0;
    public CinemachineVirtualCamera cineCamera;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameManagement.instance.PauseGame();
            UIManagement.instance.OpenReward();
        }
    }

    public void SolveTrigger() {
        numberOfTrigger--;
        if (numberOfTrigger == 0) GameManagement.instance.ActiveExit(this);
    }

    

}
