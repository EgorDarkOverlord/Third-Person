public abstract class StateMachine<T> where T : StateMachine<T>
{
    public State<T> CurrentState { get; protected set; }

    public void Update()
    {
        CurrentState.Update();
    }

    public void SwitchState(State<T> state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}