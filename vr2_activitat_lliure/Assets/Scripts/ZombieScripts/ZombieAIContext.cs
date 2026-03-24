using UnityEngine;
using UnityEngine.AI;

public class ZombieAIContext : MonoBehaviour
{
    public Transform Target;
    public float SightDistance = 10f;
    public float AttackRange = 2f;
    public float ChargeRange = 4f;
    public int Health = 3;
    public int randomDancer;

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Animator Animator;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        randomDancer = Random.Range(1, 3);
        //prueba :

        // Forzar: que el Animator no aplique root motion
        if (Animator != null) Animator.applyRootMotion = false;

        // Forzar: que el NavMeshAgent actualice posicion y rotacion
        if (Agent != null)
        {
            Agent.updatePosition = true;
            Agent.updateRotation = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
