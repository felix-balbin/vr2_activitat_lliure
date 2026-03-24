using UnityEngine;

public class ZombieDanceState : ZombieBaseState
{
    public ZombieBaseState hitState;

    public override void EnterState(ZombieStateManager zombie)
    {
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Dance", true);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
    }
    public override void ExitState(ZombieStateManager zombie)
    {
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Animator.SetBool("Dance", false);
    }
}
