using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Dialogue : MonoBehaviour
{
    public CinemachineVirtualCamera followCamera;
    public string[] sentences;

    int i = 0;

    public string GetSentences() {
        Debug.Log("This is sentence " +i);

        return sentences[i++];
    }

    public bool EndOfDiologue() {

        Debug.Log("hadEnter");
        if (i == sentences.Length) {
            followCamera.Priority = 0;
            return true;
        }
        return false;
    }





}
