using UnityEngine;

public class ZombieChargeState : ZombieBaseState
{
    public ZombieBaseState attackState;
    public float ChargeDuration = 0.7f;
    private float timer;
    public override void EnterState(ZombieStateManager zombie)
    {
        timer = 0;
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Charge", true);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        timer += Time.deltaTime;

        //var ctx = zombie.GetComponent<ZombieAIContext>();

        //mover el boss al target
        //ctx.transform.position += dir * ChargeSpeed * Time.deltaTime;
        //Si pasa el tiempo de carga, ataca
        if (timer >= ChargeDuration)
        {
            zombie.ChangeState(attackState);
        }

    }
    public override void ExitState(ZombieStateManager zombie)
    {
    }
}
