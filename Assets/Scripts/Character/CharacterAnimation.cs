using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色动画系统
    /// </summary>
    /// 
    public class CharacterAnimation:MonoBehaviour
    {
        /// <summary>
        /// 动画组件
        /// </summary>
        private Animator anim;

        private void Start()
        {
            anim = this.GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// 上一个动画
        /// </summary>
        private string preAnim = "idle";

        /// <summary>
        /// 播放动画
        /// </summary>
        public void PlayAnimation(string animName)
        {
            anim.SetBool(preAnim, false);
            anim.SetBool(animName, true);
            preAnim = animName;
        }
    }
}