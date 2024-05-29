namespace Spellbrandt
{
    public class PlayerMonsterRemovedEventArgs
    {
        public readonly Monster Monster;

        public PlayerMonsterRemovedEventArgs(Monster monster)
        {
            Monster = monster;
        }
    }
}
