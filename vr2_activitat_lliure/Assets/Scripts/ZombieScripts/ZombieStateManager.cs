using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    public ZombieBaseState initialState;
    public ZombieBaseState currentState;

    //public ZombieAliveState aliveState = new ZombieAliveState();
    //public ZombieWalkState walkState = new ZombieWalkState();
    //public ZombieHitState hitState = new ZombieHitState();
    //public ZombieChargeState chargeState = new ZombieChargeState();
    //public ZombieAttackState attackState = new ZombieAttackState();
    //public ZombieDeathState deathState = new ZombieDeathState();
    //public ZombieDanceState danceState = new ZombieDanceState();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = initialState;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(ZombieBaseState newState)
    {
        if (newState == currentState) return;
        newState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);

    }
}
