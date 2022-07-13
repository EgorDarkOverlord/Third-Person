public abstract class State<T> where T : StateMachine<T>
{
    protected T context;

    public State(T context)
    {
        this.context = context;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
    }
}

