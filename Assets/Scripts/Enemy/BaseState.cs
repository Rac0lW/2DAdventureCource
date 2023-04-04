
public abstract class BaseState
{
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void OnExit();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();
}
