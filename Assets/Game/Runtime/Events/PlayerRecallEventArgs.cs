namespace Spellbrandt
{
    public class PlayerRecallEventArgs
    {
        public readonly Monster Monster;

        public PlayerRecallEventArgs(Monster monster)
        {
            Monster = monster;
        }
    }
}
