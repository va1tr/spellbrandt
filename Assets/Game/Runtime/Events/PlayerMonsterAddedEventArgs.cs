namespace Spellbrandt
{
    public class PlayerMonsterAddedEventArgs
    {
        public readonly Monster Monster;

        public PlayerMonsterAddedEventArgs(Monster monster)
        {
            Monster = monster;
        }
    }
}
