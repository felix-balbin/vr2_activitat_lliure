using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class ZombieAliveState : ZombieBaseState
{
    public ZombieBaseState walkState;
    public ZombieBaseState danceState;
    public int randomNum;
    public bool dancingZombie;
    public override void EnterState(ZombieStateManager zombie)
    {
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Agent.ResetPath();
        ctx.Agent.isStopped = true;
        ctx.Animator.SetBool("Alive", true);

        randomNum = Random.Range(1, 5);
    }
    public override void UpdateState(ZombieStateManager zombie)
    {
        if (zombie.currentState != this) return;

        var ctx = zombie.GetComponent<ZombieAIContext>();

        float dist = Vector3.Distance(ctx.transform.position, ctx.Target.position);

        if (randomNum == 3)
        {
            zombie.ChangeState(danceState);
        }
        else
        {
            zombie.ChangeState(walkState);
        }

        //Si la distancia entre el target y el boss es menor que la distancia de vista,
        //lo detecta y entra al estado de chase
        //if (dist < ctx.SightDistance - 1f)
        //{
        //    zombie.ChangeState(walkState);
        //}
    }
    public override void ExitState(ZombieStateManager zombie)
    {
        var ctx = zombie.GetComponent<ZombieAIContext>();
        ctx.Animator.SetBool("Alive", false);
    }
    public override void OnCollisionEnter(ZombieStateManager zombie)
    {
    }
}
