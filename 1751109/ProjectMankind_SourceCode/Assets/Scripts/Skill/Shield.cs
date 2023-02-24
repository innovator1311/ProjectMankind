using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Skill
{
    public int virtualHealth = 5;

    [HideInInspector]
    public bool isShield = false;
    [HideInInspector]
   
    public int presentVirtualHealth;

    public float invisibleTime = 1.5f;
    public AudioClip brokenShield;
    float time = -1, shieldColor = 1f;
    GameObject shield;

    private void Start() {
        presentVirtualHealth = virtualHealth;
        shield = gameObject.transform.Find("Shield").gameObject;
    }

    void Update()
    {
        if (time >= float.Epsilon) time -= Time.deltaTime;   
    }

    override public void TriggerSkill() {

        if (presentVirtualHealth <= 0) return;
        
        isShield = !isShield;
        shield.SetActive(isShield);
    }
    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,presentVirtualHealth,true);
    }

    public void UpdateVirtualHealth() {

        if (time >= float.Epsilon) return;
        time = invisibleTime;

        presentVirtualHealth--;
        shieldColor -= 0.2f;

        Color newColor = new Color(1,1,1,shieldColor);
        shield.GetComponent<SpriteRenderer>().color = newColor;

        UIManagement.instance.UpdateSkill(skillID,presentVirtualHealth,true);

        if (presentVirtualHealth <= 0) {
            isShield = false;
            gameObject.transform.Find("Shield").gameObject.SetActive(isShield);
            AudioManagement.instance.PlayOneShot(brokenShield);
        }
    }






}
