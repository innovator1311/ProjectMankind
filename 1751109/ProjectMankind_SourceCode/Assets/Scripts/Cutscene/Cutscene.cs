using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cutscene : MonoBehaviour
{
    public GameObject player;
    public Dialogue[] diologues;
    public TextMeshProUGUI textMeshProUGUI;
    public float mouseSentitive = 0.5f;
    float time = -1;
    bool isWork = false;
    bool isDisplay = false;
    string displaySentence = "";
    IEnumerator coroutine;
    int idx = 0;

    private void Awake() {
       // Invoke("StartDislay",2f);
       StartDislay();
    }

    void Update()
    {
        if (time >= float.Epsilon) {
            time -= Time.deltaTime;
            return;
        }


        if (Input.anyKeyDown && !isWork) {

            isWork = true;
            time = mouseSentitive;

            if (isDisplay) {
                Debug.Log("IS DISPLAY");
                StopCoroutine(coroutine);
                textMeshProUGUI.text = displaySentence;
                isDisplay = false;
                isWork = false;
                return;
            }

            Debug.Log("Check end " + idx);
            if (diologues[idx].EndOfDiologue()) {
                
                ++idx;
                Debug.Log("This is dialouge " + idx);

                if (idx == diologues.Length) {
                    Debug.Log(UIManagement.instance);
                    Skip();
                    return;
                }

            }

            StartDislay();
            isWork = false;
            
        }
        
    }

    void StartDislay() {
        displaySentence = diologues[idx].GetSentences();
        coroutine = Display(displaySentence);
        StartCoroutine(coroutine);
    }

    IEnumerator Display(string sentence) {
       
        isDisplay = true;
        string displaySen = "";

        foreach (char word in sentence) {
            displaySen += word;
            textMeshProUGUI.text = displaySen;
            yield return null;
        }

        textMeshProUGUI.text = displaySen;
        isDisplay = false;
    }

    public void Skip() {
        player.SetActive(true);
        Destroy(textMeshProUGUI.gameObject);
        this.enabled = false;
    }

}
