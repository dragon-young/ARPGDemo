using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 施放器的初始化工厂类=施放器的配置工厂类
    /// </summary>
    class DeployerConfigFactory
    {
        /// <summary>
        /// 工厂方法设计模式：创建目标选择对象的方法 符合 定义
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static IAttackSelector CreateAttackSelector(SkillData skill)
        {
            IAttackSelector attackSelector = null;
            switch (skill.damageMode)
            { 
                case DamageMode.Circle:
                    attackSelector = new CircleAttackSelector();
                    break;
                case DamageMode.Sector:
                    attackSelector = new SectorAttackSelector();
                    break;
            }
            return attackSelector;
        }
        /// <summary>
        /// 创建 初始化 自身影响
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static List<ISelfImpact> CreateSelfImpact(SkillData skill)
        {
            List<ISelfImpact> list = new List<ISelfImpact>();
            list.Add(new CostSPSelfImpact());
            return list;
        }
        /// <summary>
        ///  创建 初始化 目标影响
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static List<ITargetImpact> CreateTargetImpact(SkillData skill)
        {
            return new List<ITargetImpact>() 
            { 
                new DamageTargetImpact()
            }; 
        }
    }
}
