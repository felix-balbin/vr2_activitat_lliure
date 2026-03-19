using System;
using UnityEngine;

public class ZombieHitState : ZombieBaseState
{
    public ZombieBaseState walkState;
    public ZombieBaseState deathState;
    private float timer;
    public float HitDuration = 1f;
    public override void EnterState(ZombieStateManager zombie)
    {
        timer = 0;
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Hit", true);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        timer += Time.deltaTime;

        if (timer > HitDuration)
        {
            zombie.ChangeState(walkState);
        }
        else
        {
            Debug.Log("Recibe otro hit");
        }
        //terminar, depende del hit de la bala que depende del collision entre bala y zombie
    }
    public override void ExitState(ZombieStateManager zombie)
    {
    }
    public override void OnCollisionEnter(ZombieStateManager zombie)
    {
    }
}
