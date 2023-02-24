using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Skill
{
    bool isLight = false;
    public float lightTime = 20f;

    override public void TriggerSkill() {

        //if (lightTime < 0) return;

        isLight = !isLight;
        gameObject.transform.Find("Light").gameObject.SetActive(isLight);
    }       

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
       /*  if (isLight && lightTime > 0) {
            lightTime -= Time.deltaTime;
            UIManagement.instance.UpdateSkill(skillID,(int)lightTime);
        }*/


        //if (lightTime <= 0) gameObject.transform.Find("Light").gameObject.SetActive(false);
    } 

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,0,false);
    }

}
