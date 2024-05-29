using System.Collections.Generic;

namespace Spellbrandt
{
    public class StateMachine<T>
    {
        private readonly Dictionary<T, IState<T>> _states = new Dictionary<T, IState<T>>();
        private IState<T> _currentState => _states[_currentStateID];

        private T _currentStateID;

        public StateMachine(IState<T>[] states, T initialStateID)
        {
            Bind(states, initialStateID);
        }

        private void Bind(IState<T>[] states, T initialStateID)
        {
            foreach (var state in states)
            {
                _states.Add(state.UniqueID, state);
            }

            _currentStateID = initialStateID;
            _currentState.OnEnter();
        }

        public void OnUpdate()
        {
            _currentState.OnUpdate();
        }

        public void ChangeState(T stateID)
        {
            if (_states.ContainsKey(stateID) && !_currentStateID.Equals(stateID))
            {
                _currentState.OnExit();
                _currentStateID = stateID;
                _currentState.OnEnter();
            }
        }
    }
}
