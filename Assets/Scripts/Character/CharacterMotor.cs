using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色马达
    /// </summary>
    public class CharacterMotor:MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 5;
        /// <summary>
        /// 转向速度
        /// </summary>
        public float rotationSpeed = 1;
        /// <summary>
        /// 动画系统
        /// </summary>
        private CharacterAnimation chAnim;
        /// <summary>
        /// 角色控制器
        /// </summary>
        private CharacterController chController;


        private void Start()
        {
            chAnim = this.GetComponent<CharacterAnimation>();
            chController = this.GetComponent<CharacterController>();
        }



        /// <summary>
        /// 移动
        /// </summary>
        public void Move(float x, float z)
        {
            // 移动的步骤： 1. 转向目标点 2. 朝着目标点移动 3. 播放动画
            if(x != 0 || z != 0)
            {
                // 转向目标点
                TransformHelper.LookAtTarget(new Vector3(x, 0, z), transform, rotationSpeed);
                // 移动
                chController.Move(new Vector3(transform.forward.x, -1, transform.forward.z) * moveSpeed * Time.deltaTime);
                // 播放动画
                chAnim.PlayAnimation("run");
            } else
            {
                chAnim.PlayAnimation("idle");
            }
        }

    }
}