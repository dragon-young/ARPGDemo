using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// ����ѡ��ӿ�
    /// ѡ��  ʲô��Χ/���� �еĵ��� ��Ϊ����Ŀ��
    /// ���磺Բ�η�Χ/���� �л����η�Χ/���� ��
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// ѡ��Ŀ�귽����ѡ����Щ������ΪҪ������Ŀ��
        /// </summary>
        /// <param name="skillData">���ܶ���</param>
        /// <param name="skillTransform">�任����ѡ��ʱ�Ĳο��� ������ӵ����</param>
        /// <returns></returns>
        GameObject[] SelectTarget(SkillData skillData, Transform skillTransform);
    }
}
