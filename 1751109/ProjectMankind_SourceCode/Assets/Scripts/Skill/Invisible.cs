using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Skill
{
    public float invisibleSpeed;
    public float invisibleTime = 5f;
    float time = -1;
    public float delayTime = 4f;
    float dtime = -1;
    Color oldColor,newColor,color;
    [HideInInspector]
    public bool hide = false;
    // Start is called before the first frame update
    private void Awake() {
        oldColor = GetComponent<SpriteRenderer>().color;
        newColor = new Color(oldColor.r,oldColor.g,oldColor.b,0.2f);
        color = oldColor;
    }

    override public void TriggerSkill() {

        if (hide || dtime >= float.Epsilon) return;

        color = newColor; 
        hide = true;
        time = invisibleTime;
    }

    // Update is called once per frame
    void Update()
    {
         GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color,color, invisibleSpeed * Time.deltaTime); 

        if (dtime >= float.Epsilon) {
            dtime -= Time.deltaTime;
            UIManagement.instance.UpdateSkill(skillID,(int)dtime,true);
            return;
        }

        if (time < 0) return;
        time -= Time.deltaTime;
        UIManagement.instance.UpdateSkill(skillID,(int)time,true);

        if (time < 0) {
            dtime = delayTime;
            color = oldColor; 
            hide = false;
        }
    }

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,0,true);
    }
}
