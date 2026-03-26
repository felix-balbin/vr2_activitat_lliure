using System;
using UnityEngine;

public class ZombieHitState : ZombieBaseState
{
    public ZombieBaseState walkState;
    public ZombieBaseState danceState;
    public ZombieBaseState chargeState;
    public ZombieBaseState attackState;

    public ZombieBaseState deathState;
    private float timer;
    public float HitDuration = 1f;
    public override void EnterState(ZombieStateManager zombie)
    {
        timer = 0;
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;

        //reiniciar animación
        ctx.Animator.ResetTrigger("Hit");
        ctx.Animator.SetTrigger("Hit");
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        timer += Time.deltaTime;

        if (timer > HitDuration)
        {
            zombie.ChangeState(zombie.lastState);
        }
    }
    public override void ExitState(ZombieStateManager zombie)
    {

    }
}
