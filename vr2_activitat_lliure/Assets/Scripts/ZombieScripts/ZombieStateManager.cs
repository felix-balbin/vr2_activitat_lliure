using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    public ZombieBaseState lastState;
    public ZombieBaseState currentState;

    public ZombieAliveState aliveState = new ZombieAliveState();
    public ZombieWalkState walkState = new ZombieWalkState();
    public ZombieHitState hitState = new ZombieHitState();
    public ZombieChargeState chargeState = new ZombieChargeState();
    public ZombieAttackState attackState = new ZombieAttackState();
    public ZombieDeathState deathState = new ZombieDeathState();
    public ZombieDanceState danceState = new ZombieDanceState();

    public ZombieAIContext ctx;
    public GameObject bulletHole;

    public bool isDeath;
    public Score score;

    private void Awake()
    {
        ctx = GetComponent<ZombieAIContext>();
    }

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

    public void ChangeState(ZombieBaseState newState)
    {
        if (newState == currentState) return;
        lastState = currentState;
        currentState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);

    }

    public void TakeHit(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        ctx.Health -= damage;
        GameObject impacto = Instantiate(bulletHole, hitPoint, Quaternion.LookRotation(hitNormal));
        if (ctx.Health <= 0)
        {
            if (!isDeath)
            {
                isDeath = true;
                score.AddScore(1);

            }
            ChangeState(deathState);
            return;
        }

        if (currentState == deathState)
        {
            return;
        }

        if (currentState == hitState)
        {
            currentState.EnterState(this);
        }
        else
        {
            lastState = currentState;
            ChangeState(hitState);
        }
    }
}
