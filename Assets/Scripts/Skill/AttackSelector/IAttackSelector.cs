using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 攻击选择接口
    /// 选择  什么范围/区域 中的敌人 作为攻击目标
    /// 例如：圆形范围/区域 中或扇形范围/区域 中
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择哪些敌人作为要攻击的目标
        /// </summary>
        /// <param name="skillData">技能对象</param>
        /// <param name="skillTransform">变换对象：选择时的参考点 ；技能拥有者</param>
        /// <returns></returns>
        GameObject[] SelectTarget(SkillData skillData, Transform skillTransform);
    }
}
