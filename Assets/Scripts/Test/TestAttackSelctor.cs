using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    ///
    /// </summary>
    public class TestAttackSelctor : MonoBehaviour
    {
        CircleAttackSelector cas = new CircleAttackSelector();
        SectorAttackSelector sas = new SectorAttackSelector();
        SkillData skillData = new SkillData();
        public float attackAngle = 180;
        public float attackDistance = 5;

        private void Start()
        {
            skillData = new SkillData();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("圆形区域"))
            {
                // skillData.attackAngle
                skillData.attackType = SkillAttackType.Group;
                skillData.attackDistance = attackDistance;
                GameObject[] tempgo = cas.SelectTarget(skillData, this.transform);
                foreach (var item in tempgo)
                {
                    item.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            if (GUILayout.Button("扇形区域"))
            {
                skillData.attackAngle = attackAngle;
                skillData.attackType = SkillAttackType.Group;
                skillData.attackDistance = attackDistance;
                GameObject[] tempgo = sas.SelectTarget(skillData, this.transform);
                print(tempgo);
                if (tempgo == null) return;
                foreach (var item in tempgo)
                {
                    item.GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }


    }
}
