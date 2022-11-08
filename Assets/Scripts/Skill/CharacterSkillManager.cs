using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;

namespace ARPGDemo.Skill
{
    public class CharacterSkillManager:MonoBehaviour
    {

        public List<SkillData> skills = new List<SkillData>();
   
        //1.初始化
        private void Start()
        {
            // 遍历技能列表，为每个技能进行初始化
            foreach (var skill in skills)
            {
                // 1. 这个技能归谁(技能管理类挂载到谁，就归谁)
                skill.Owner = this.gameObject;

                // 2. 需要加载技能预制件和特效预制件(通过Resources加载)
                if (!(string.IsNullOrEmpty(skill.prefabName)) && skill.skillPrefab == null)    // 技能预制件
                {
                    skill.skillPrefab =  LoadPrefab(skill.prefabName);
                }
                if (!(string.IsNullOrEmpty(skill.hitFxName)) && skill.hitFxPrefab == null)   // 特效预制件
                {
                    skill.hitFxPrefab = LoadPrefab(skill.hitFxName);
                }

            }
        }
        /// <summary>
        /// 动态加载 预制件资源 
        /// </summary>
        /// <param name="resName">预制件资源名称</param>
        private GameObject LoadPrefab(string resName)
        {
            // 从资源中根据名字加载对象
            var prefabGo = Resources.Load<GameObject>(resName);
            #region 将该对象放进对象池中，并立刻回收。这样以后就可以直接从对象池拿到(防止第一次使用技能时出现的卡帧现象)
            var tempGo = GameObjectPool.instance.CreateObject(resName, prefabGo, transform.position, transform.rotation);
            GameObjectPool.instance.CollectObject(tempGo);
            #endregion
            return prefabGo;
        }
        //2.准备技能
        public SkillData PrepareSkill(int id)
        {
            // 1.根据id从技能列表中查找所属技能
            SkillData skill = skills.Find(p => p.skillID == id);
            // 2. 找到技能后，根据冷却时间和玩家的魔法值返回
            if (skill == null) return skill;
            if(skill.coolRemain == 0 && skill.costSP <= skill.Owner.GetComponent<CharacterStatus>().SP)
            {
                return skill;
            }
            return null;
        }
        //3.施放技能  调用施放器的施放的方法即可
        public void DeploySkill(SkillData skillData)
        {
            // 我需要技能，所以我需要创建技能预制件
            var tempGo = GameObjectPool.instance.CreateObject(skillData.prefabName, skillData.skillPrefab, transform.position,
                transform.rotation);
            // 为技能预制件对象设置 当前使用这个技能
            var deployer = tempGo.GetComponent<SkillDeployer>();
            //3 调用施放器的施放的方法
            deployer.skillData = skillData;
            deployer.DeploySkill();
            // 4. 冷却计时
            StartCoroutine(CoolTimeDown(skillData));

        }
        //4.技能冷却处理
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
            skillData.coolRemain = skillData.coolTime;
            while(skillData.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                skillData.coolRemain-=1;
            }
            skillData.coolRemain = 0;
        }
        //5.获取技能冷却剩余时间
        public int GetSkillCoolRemain(int id)
        {
            return skills.Find(p=>p.skillID == id).coolRemain;
        }

    }
}
