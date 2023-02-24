using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManagement : MonoBehaviour
{
    public static UIManagement instance;
    public GameObject player;
    public GameObject healthUI;
    public float width;
    public Sprite[] skillSprite;
    public Button[] button;
    public GameObject inventory;
    public GameObject panel;
    public GameObject reward;
    public Sprite nullSprite;
    public TextMeshProUGUI textObj; 
    [TextArea]
    public string[] instructions;
    public Sprite[] numberSprite;
    int presentChoose = 0;
    List<GameObject> panelButton = new List<GameObject>();

    [HideInInspector]
    public int[] skillSet = {-1,-1,-1};

    [HideInInspector]
    public bool stopUI = false;

    void Awake()
    {
        instance = this;
        //skillSet = FindObjectOfType<Ship>().skillNumber;

        for (int i = 0; i < button.Length; i++) 
            //button[i].GetComponent<Image>().sprite = skillSprite[skillSet[i]];
            skillSet[i] = -1;

        int j = 0;
         foreach (Transform child in inventory.transform) {
            panelButton.Add(child.gameObject);
            child.gameObject.GetComponent<Image>().sprite = skillSprite[j++];
        }

        if (player != null)
            ChangeHealth(player.GetComponent<Ship>().presentHealth);
    }

   
    public void Start()
    {
        gameObject.SetActive(false);
        stopUI = false;
    }

   public void ChangeHealth(float health) {
       RectTransform rt = healthUI.GetComponent<RectTransform>();
       rt.sizeDelta = new Vector2(health * width, rt.sizeDelta.y);
   }

    public void StartUI() {
        gameObject.SetActive(true);
    }

   public void ClosePanel() {

       if (stopUI) return;
       //panel.SetActive(!panel.activeSelf);
       GetComponent<Animator>().SetTrigger("close");
   }

   public void ChooseImage(int id) {
       if (stopUI) return;
       if (presentChoose == -1) return;

        skillSet[presentChoose] = id;
        button[presentChoose].transform.Find("Icon").gameObject.GetComponent<Image>().sprite = skillSprite[id];
        
       for (int i = 0; i < button.Length; i++) {
           if (skillSet[i] == -1)  {presentChoose = i; break; }
           if (i == button.Length - 1) presentChoose = -1;
       }

       EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
   }

   public void Return(int id) {
       if (stopUI || skillSet[id] == -1) return;
        
        int j = 0;
        foreach (GameObject item in panelButton)
        {
            if (j == skillSet[id]) {
                item.GetComponent<Button>().interactable = true;
                break;
            }   
            j++;         
        }

        skillSet[id] = -1;
        EventSystem.current.currentSelectedGameObject.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = nullSprite;

       for (int i = 0; i < button.Length; i++) { if (skillSet[i] == -1)  {presentChoose = i; break; } }

   }
    public void Select(int id) {
        textObj.text = instructions[id];
    }
    public void UpdateSkill(int id, int number, bool active) {

        GameObject skillPoint1 = button[id].gameObject.transform.Find("SkillPoint1").gameObject;
        GameObject skillPoint2 = button[id].gameObject.transform.Find("SkillPoint2").gameObject;

        skillPoint1.SetActive(active);
        skillPoint2.SetActive(active);

        skillPoint1.GetComponent<Image>().sprite = numberSprite[number/10];
        skillPoint2.GetComponent<Image>().sprite = numberSprite[number % 10];
    }

    public void StartGame() {
        GameManagement.instance.StartGame();
    }

    public void OpenReward() {
        reward.SetActive(true);
    }

  

}
