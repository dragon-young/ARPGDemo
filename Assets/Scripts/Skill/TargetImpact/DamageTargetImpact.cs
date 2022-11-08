using UnityEngine;
using ARPGDemo.Character;
using System.Collections;
using System.Collections.Generic;

namespace ARPGDemo.Skill
{
    //目标影响类：HP的减少【伤害】
    public class DamageTargetImpact: ITargetImpact
    {
        private int baseDamage = 0;

        /// 影响目标的操作【方法】     
        /// <param name="deployer">技能施放器</param>
        /// <param name="skillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        public void TargetImpact(SkillDeployer deployer, SkillData skillData,
            GameObject goTarget)
        {
            if(skillData.Owner != null && skillData.Owner.gameObject != null)
            {
                baseDamage = skillData.Owner.GetComponent<CharacterStatus>().Damage;
            }

            deployer.StartCoroutine(RepeatDamage(deployer, skillData));

        }
        /// <summary>
        /// 单次伤害
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="goTarget">伤害的目标物体</param>
        private void OnceDamage(SkillData skill,GameObject goTarget)
        {
            Debug.Log("goTarget:" + goTarget.name);
            // 先获取目标对象的状态组件
            var chStatus = goTarget.GetComponent<CharacterStatus>();
            Debug.Log(chStatus);
            var damageVal = skill.damage* baseDamage;
            chStatus.OnDamage((int)damageVal);
            // 将受击特效挂载到目标点
            if(skill.hitFxPrefab != null && chStatus.HitFxPos!=null)
            {
                var hitGo = GameObjectPool.instance.CreateObject(skill.hitFxName, skill.hitFxPrefab,
                chStatus.HitFxPos.position, chStatus.HitFxPos.rotation);
                hitGo.transform.parent = chStatus.HitFxPos;
                GameObjectPool.instance.CollectObject(hitGo, 0.5f);
            }
            



        }
        //重复伤害 
        private IEnumerator RepeatDamage(SkillDeployer deploy, SkillData skill)
        {
            // 定义攻击时间
            float attackTime = 0;
            do
            {
                // 如果有多个目标
                if (skill.attackTargets != null && skill.attackTargets.Length > 0)
                {
                    //对多个 目标执行伤害 
                    for (int i = 0; i < skill.attackTargets.Length; i++)
                    {
                        // 让每一个目标都会受到伤害
                        OnceDamage(skill, skill.attackTargets[i]);//**
                    }
                }
                //间隔一个时间，再次执行伤害
                yield return new WaitForSeconds(skill.damageInterval);
                attackTime += skill.damageInterval;//!! durationTime不是0，
                                                   //damageInterval也不能为0
                                                   //攻击一次之后，要重新选取目标
                skill.attackTargets = deploy.ResetTarget();//先去实现施放器的 方法，再完成这里
            }
            while (attackTime < skill.durationTime);//!!防止死循环   
        }
    }
}
