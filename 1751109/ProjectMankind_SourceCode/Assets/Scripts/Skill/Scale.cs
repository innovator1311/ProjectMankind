using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : Skill
{
    public float scaleNumber;
    public float scaleSpeed;
    public float scaleTime = 5;
    [HideInInspector]
    public float time = -1;
    Vector3 oldScale,newScale,scale;

    private void Start() {
        oldScale = transform.localScale;
        newScale = new Vector3(transform.localScale.x/scaleNumber,transform.localScale.y/scaleNumber,transform.localScale.z);
        scale = oldScale;
    }

    // Update is called once per frame
    override public void TriggerSkill() {

            scale = newScale; 
            time = scaleTime;            
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp (transform.localScale, scale, scaleSpeed * Time.deltaTime);
        
        if (time < 0) return;
        time -= Time.deltaTime;
        UIManagement.instance.UpdateSkill(skillID,(int)time,true);

        if (time < 0) scale = oldScale;
    }

    override public void InitSkillPoint(int id) {
        skillID = id;
        UIManagement.instance.UpdateSkill(skillID,0,true);
    }

       
    
}
