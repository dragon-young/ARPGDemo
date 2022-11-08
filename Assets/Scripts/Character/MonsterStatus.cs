namespace ARPGDemo.Character
{
    /// <summary>
    /// 小怪状态
    /// </summary>
    class MonsterStatus:CharacterStatus
    {
        public int GiveExp;

        public override void Dead()
        {

        }

        public override void OnDamage(int damageVal)
        {
            base.OnDamage(damageVal);
        }
    }
}