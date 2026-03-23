using UnityEngine;

public class ZombieWalkState : ZombieBaseState
{
    public ZombieBaseState chargeState;
    public ZombieBaseState hitState;

    public override void EnterState(ZombieStateManager zombie)
    {
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Walk", true);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        if (zombie.currentState != this) return;

        var ctx = zombie.GetComponent<ZombieAIContext>();

        float dist = Vector3.Distance(ctx.transform.position, ctx.Target.position);

        if (!ctx.Agent.hasPath)
        {
            ctx.Agent.SetDestination(ctx.Target.position);
        }
        else
        {
            ctx.Agent.destination = ctx.Target.position;
        }

        //Si la distancia entre el boss y el target es menor a la distancia para activar el charge, activa el telégrafo
        if (dist <= ctx.ChargeRange)
        {
            zombie.ChangeState(chargeState);
        }

    }
    public override void ExitState(ZombieStateManager zombie)
    {
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Animator.SetBool("Walk", false);
    }
}
