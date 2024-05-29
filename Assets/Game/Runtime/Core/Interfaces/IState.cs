namespace Spellbrandt
{
    public interface IState<T>
    {
        T UniqueID { get; }

        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}
