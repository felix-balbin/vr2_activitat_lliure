using UnityEngine;

public class ZombieWalkState : ZombieBaseState
{
    public ZombieBaseState chargeState;
    public ZombieBaseState hitState;

    public override void EnterState(ZombieStateManager zombie)
    {
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = false;
        ctx.Animator.SetBool("Alive", false);

        ctx.Animator.SetBool("Walk", true);
        ctx.Agent.speed = ctx.WalkSpeed;
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        if (zombie.currentState != this) return;

        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();

        Vector3 dir = (ctx.Target.position - ctx.transform.position).normalized;

        float dist = Vector3.Distance(ctx.transform.position, ctx.Target.position);

        Vector3 targetPos = ctx.Target.position - dir * ctx.AttackRange;

        if (!ctx.Agent.hasPath)
        {
            ctx.Agent.SetDestination(targetPos);
        }
        else
        {
            ctx.Agent.destination = targetPos;
        }

        //Si la distancia entre el boss y el target es menor a la distancia para activar el charge, activa el telégrafo
        if (dist <= ctx.ChargeRange + 0.2f)
        {
            zombie.ChangeState(chargeState);
        }

    }
    public override void ExitState(ZombieStateManager zombie)
    {
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Walk", false);
    }
}
