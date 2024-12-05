namespace Player
{
    public class EnemyCombatReceiver : CombatReceiver
    {
        public override void Die()
        {
            base.Die();
            //TODO: We'll notify the AI when the CombatReceiver dies

            //TODO: We'll grant the player experience
        }
    }
}
