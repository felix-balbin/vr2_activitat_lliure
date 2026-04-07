using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
            if (zombie.pendingDeath)
            {
                zombie.isDeath = true;
                zombie.zombieInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                zombie.ctx.Animator.ResetTrigger("Death");
                zombie.ctx.Animator.SetTrigger("Death");

                zombie.score.AddScore(1);
                zombie.ChangeState(deathState);
                zombie.pendingDeath = false;

            }
            else
            {
                zombie.ChangeState(zombie.lastState);
            }
        }
    }
    public override void ExitState(ZombieStateManager zombie)
    {

    }
}
