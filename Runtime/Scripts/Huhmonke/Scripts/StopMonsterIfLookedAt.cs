using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// and yes it was made by me(HuhMonke)

public class StopMonsterIfLookedAt : MonoBehaviour
{
    [Header("By HuhMonke")]
    public NavMeshAgent[] Monsters;

    [Space]
    public string MonsterTag = "Monster";

    [Space]
    public float StopRange = 10f;
    public float Fov = 90;

    [Space]
    public Color StopRangeColor = Color.red;
    public Color FovColor = Color.blue;

    private void Update()
    {
        foreach (NavMeshAgent monster in Monsters)
        {
            if(monster == null) continue;
            
            Vector3 playerdirec = (monster.transform.position - transform.position).normalized;

            float distnace = Vector3.Distance(transform.position, monster.transform.position);

            if(monster != null)
            {
                if (distnace <= StopRange)
                {
                    float angleMontser = Vector3.Angle(transform.forward, playerdirec);
                    if(angleMontser <= Fov / 2)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, playerdirec, out hit, StopRange))
                        {
                            if (hit.collider.CompareTag(MonsterTag))
                            {
                                monster.isStopped = true;
                                continue;
                            }
                        }
                    }
                }
                monster.isStopped = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = StopRangeColor;
        Gizmos.DrawWireSphere(transform.position, StopRange);
        Vector3 left = Direct(-Fov / 2);
        Vector3 right = Direct(Fov / 2);
        Gizmos.color = FovColor;
        Gizmos.DrawLine(transform.position, transform.position + left * StopRange);
        Gizmos.DrawLine(transform.position, transform.position + right * StopRange);
    }



    Vector3 Direct(float angl)
    {
        angl += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angl * Mathf.Deg2Rad), 0, Mathf.Cos(angl * Mathf.Deg2Rad));
    }
}
