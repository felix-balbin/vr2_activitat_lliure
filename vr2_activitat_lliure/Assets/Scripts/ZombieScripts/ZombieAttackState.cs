using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ZombieAttackState : ZombieBaseState
{
    public ZombieBaseState walkState;
    private float timer;
    public float AttackDuration = 1f;


    public override void EnterState(ZombieStateManager zombie)
    {
        timer = 0;
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Attack", true);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        timer += Time.deltaTime;

        if (timer > AttackDuration)
        {
            zombie.ChangeState(walkState);
        }
    }
    public override void ExitState(ZombieStateManager zombie)
    {
    }

}
