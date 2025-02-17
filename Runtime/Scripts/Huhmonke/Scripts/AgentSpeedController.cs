using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpeedController : MonoBehaviour
{
    [Header("By HuhMonke")]
    public NavMeshAgent agent;
    public Animator anim;
    public float DefaultSpeed = 2.5f;
    public float AnimSpeedMultiplier = 10f;


    private void Update()
    {
        float sped = agent.velocity.magnitude / DefaultSpeed;
        anim.speed = sped * AnimSpeedMultiplier;
    }
}
