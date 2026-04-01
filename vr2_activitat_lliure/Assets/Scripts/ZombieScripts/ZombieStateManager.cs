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

    public int nHits;

    public Renderer rendererMesh;
    public Material matNormal;
    public Material matYellow;
    public Material matRed;

    private void Awake()
    {
        ctx = GetComponent<ZombieAIContext>();
        rendererMesh = GetComponentInChildren<Renderer>();

        aliveState.walkState = walkState;

        aliveState.danceState = danceState;

        walkState.chargeState = chargeState;

        chargeState.attackState = attackState;

        attackState.walkState = walkState;


        hitState.walkState = walkState;

        hitState.danceState = danceState;

        hitState.chargeState = chargeState;

        hitState.attackState = attackState;

        hitState.deathState = deathState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = aliveState;
        currentState.EnterState(this);
        rendererMesh.material = matNormal;
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
        if (isDeath) return;
        ctx.Health -= damage;
        GameObject impacto = Instantiate(bulletHole, hitPoint, Quaternion.LookRotation(hitNormal));
        nHits++;
        if (ctx.Health <= 0)
        {
            if (!isDeath)
            {
                isDeath = true;
                //score.AddScore(1);
                //ChangeState(deathState);
            }

        }

        if (currentState == deathState)
        {
            return;
        }

        if (currentState == hitState)
        {
            hitState.EnterState(this);
        }
        else
        {
            lastState = currentState;
            ChangeState(hitState);
        }
        if (nHits == 1)
        {
            rendererMesh.material = matYellow;
        }
        else if(nHits == 2)
        {
            rendererMesh.material = matRed;
        }

    }
}
