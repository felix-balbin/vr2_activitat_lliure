using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    ZombieBaseState currentState;

    public ZombieAliveState aliveState = new ZombieAliveState();
    public ZombieWalkState walkState = new ZombieWalkState();
    public ZombieHitState hitState = new ZombieHitState();
    public ZombieAttackState attackState = new ZombieAttackState();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = aliveState;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void ChangeState(ZombieBaseState state)
    {
        currentState = state;
        state.EnterState(this);

    }
}
