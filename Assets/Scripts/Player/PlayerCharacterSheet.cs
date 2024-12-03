namespace Player
{
    public class PlayerCharacterSheet 
    {
        public int Level { get; private set; }
        public float Experience  { get; private set; }
        public float Strength  { get; private set; }
        public float Dexterity { get; private set; }
        public float Vitality  { get; private set; }
        public float Energy  { get; private set; }

        public float CurrentHitPoints { get; private set; }
        public float MaxHitPoints => Vitality * 1.8f + 100 * Level;

        public float CurrentMana  { get; private set; }
        public float MaxMana  => Energy * 1.5f + 50 * Level;

        public PlayerCharacterSheet(int level, float exp, float strength, float dexterity, float vitality, float energy)
        {
            Strength = strength;
            Dexterity = dexterity;
            Level = level;
            Experience = exp;
            Vitality = vitality;
            Energy = energy;
            
            CurrentHitPoints = MaxHitPoints;
            CurrentMana = MaxMana;
        }
    }
}
