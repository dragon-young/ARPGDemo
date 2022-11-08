using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
/// <summary>
/// 圆形攻击选择类：选择  圆形范围/区域 中的敌人 作为攻击目标  
/// </summary>
public class CircleAttackSelector : IAttackSelector
{
    public GameObject[] SelectTarget(SkillData skillData, Transform skillTransform)
    {
        // 1. 如何去找敌人？通过射线？通过tag标签？
        // 如果有tag标签就用tag去找，性能高。
        // 但是如果指明半径的话，那就用射线把！
        var colliders = Physics.OverlapSphere(skillTransform.position, skillData.attackDistance);
        // 如果没有碰到敌人，返回null
        if (colliders == null || colliders.Length == 0) return null;

        // 2. 找到敌人之后，怎么办？
        // 先筛选物体，物体的tag标签为敌人或者boss【"enemy", "boss"】
        // 再次筛选问题，敌人的生命值必须大于0
        var newCollider = ArraysHelper.FindAll(colliders, p =>
            Array.IndexOf(skillData.attackTargetTags, p.tag) >= 0 &&
            p.gameObject.GetComponent<Character.CharacterStatus>().HP > 0
        );
        if (newCollider == null || newCollider.Length == 0) return null;

        // 3. 剩下的，就是筛选完成后的敌人了
        // 根据攻击类型，返回单个还是多个
        switch (skillData.attackType)
        {
            case SkillAttackType.Single:    // 如果是单个，那么选择距离最近的一个敌人
                var collider = ArraysHelper.Min(newCollider, p => 
                    Vector3.Distance(skillTransform.position, p.transform.position));
                return new GameObject[] { collider.gameObject };
            case SkillAttackType.Group:
                return ArraysHelper.Select(newCollider, p => p.gameObject);
        }


        return null;
    }
}
}
