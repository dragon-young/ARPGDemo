using System.Collections.Generic;
using UnityEngine;
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 扇形区域的敌人
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择 扇形范围/区域 中的敌人作为要攻击的 目标
        /// </summary>
        /// <param name="skillData">技能对象</param>
        /// <param name="skillTransform">变换对象：选择时的参考点 ；技能拥有者</param>
        /// <returns></returns>
        public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            // 1. 找敌人？ 怎么找，通过标签找
            List<GameObject> listEnemies = new List<GameObject>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                var tempEnemies = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                if (tempEnemies != null && tempEnemies.Length > 0) listEnemies.AddRange(tempEnemies); 
            }
            if (listEnemies.Count == 0) return null;
            
            // 2. 找到敌人之后，如何筛选？
            // 条件1：敌人在玩家的攻击范围内
            // 条件2：敌人的生命值大于0
            // 条件3：敌人在玩家的攻击角度内
            var enemies = listEnemies.FindAll(p =>
                Vector3.Distance(skillTransform.position, p.transform.position) < skillData.attackDistance &&
                p.gameObject.GetComponent<Character.CharacterStatus>().HP > 0 &&
                Vector3.Angle(skillTransform.forward, (p.transform.position - skillTransform.position)) < skillData.attackAngle * 0.5f
            );
            if (enemies == null || enemies.Count == 0) return null;

            // 更加攻击类型再次筛选敌人
            switch (skillData.attackType)
            {
                case SkillAttackType.Single:
                    var tempGo =  ArraysHelper.Min(enemies.ToArray(), 
                        p => Vector3.Distance(skillTransform.position, p.transform.position));
                    return new GameObject[] { tempGo };
                case SkillAttackType.Group:
                    return enemies.ToArray();
            }
            return null;


        }
    }
}
