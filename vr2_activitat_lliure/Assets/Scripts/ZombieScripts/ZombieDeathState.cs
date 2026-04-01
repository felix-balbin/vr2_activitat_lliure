using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ZombieDeathState : ZombieBaseState
{
    private float timer;
    public float DissapearDuration = 3f;
    public override void EnterState(ZombieStateManager zombie)
    {
        timer = 0;
        var ctx = zombie.ctx;
        //var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.isStopped = true;
        ctx.Animator.ResetTrigger("Death");
        ctx.Animator.SetTrigger("Death");
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        timer += Time.deltaTime;

        if (timer > DissapearDuration)
        {
            GameObject.Destroy(zombie.gameObject);
        }
    }
    public override void ExitState(ZombieStateManager zombie)
    {
    }
}
