using UnityEngine;
namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色输入控制
    /// </summary>
    public class CharacterInputController:MonoBehaviour
    {
        /// <summary>
        /// 马达
        /// </summary>
        private CharacterMotor chMotor;

        private void Start()
        {
            chMotor = this.GetComponent<CharacterMotor>();
        }

        /// <summary>
        /// 摇杆移动执行的方法
        /// </summary>
        public void JoystickMove(MovingJoystick mov)
        {
            chMotor.Move(mov.joystickAxis.x, mov.joystickAxis.y);
        }

        /// <summary>
        /// 摇杆停止时执行的方法
        /// </summary>
        public void JoystickMoveEnd(MovingJoystick mov)
        {
            chMotor.Move(mov.joystickAxis.x, mov.joystickAxis.y);
        }

        public void EasyButtonSKill(string skillName)
        {
            //var chSkillMgr = GetComponent<CharacterSkillManager>();
            // SkillData skill = null;
            var chSkillSys = GetComponent<CharacterSkillSystem>();
            switch (skillName)
            {
                case "skill1":
                    // skill = chSkillMgr.PrepareSkill(12);
                    chSkillSys.AttackUseSkill(11, false);
                    break;
                case "skill2":
                    // skill = chSkillMgr.PrepareSkill(13);
                    chSkillSys.AttackUseSkill(12, false);
                    break;
            }
            // if (skill != null) chSkillMgr.DeploySkill(skill);
        }


        // 绑定事件
        private void OnEnable()
        {
            EasyJoystick.On_JoystickMove += JoystickMove;
            EasyJoystick.On_JoystickMoveEnd += JoystickMoveEnd;
            EasyButton.On_ButtonDown += EasyButtonSKill;
        }

        private void OnDisable()
        {
            EasyJoystick.On_JoystickMove -= JoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= JoystickMoveEnd;
            EasyButton.On_ButtonDown -= EasyButtonSKill;
        }
    }
}