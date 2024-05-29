namespace Spellbrandt
{
    public class PlayerSummonEventArgs
    {
        public readonly Monster Monster;

        public PlayerSummonEventArgs(Monster monster)
        {
            Monster = monster;
        }
    }
}