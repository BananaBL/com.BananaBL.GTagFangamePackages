/*Copyright © [Flimcy]

All rights reserved.

This script and its content are protected under copyright law.This script or any portion thereof may not be reproduced or used in any manner whatsoever without the express written permission of the copyright owner, except for the use of brief quotations in a review.

Unauthorized use, reproduction, or distribution of this script, or any part thereof, may result in severe civil and criminal penalties, and will be prosecuted to the maximum extent possible under the law.

DO NOT DELETE THIS FROM THE SCRIPT!!!

For permission requests, contact:
[Flimcy]
[Flimcy]*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class NetworkedHorrorAI : MonoBehaviour
{
    [Header("The NavMeshAgent component attached to this game object")]
    [Space]
    private float MBF;
    public NavMeshAgent agent;
    [Header("___________________________________")]
    [Header("The tag the monster will chase")]
    [Space]
    public string tagString = "AiTarget";
    [Header("___________________________________")]
    [Header("The range the monster will see the player")]
    [Space]
    public float detectRange = 10f;
    private bool MadeBF;
    [Header("___________________________________")]
    [Header("The speed the monster will run")]
    [Space]
    public float chaseSpeed = 5f;
    [Header("___________________________________")]
    [Header("The positions the monster will walk to when player is not in range")]
    [Space]
    public Transform[] wanderpoints;

    private int destPoint = 0;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            agent.enabled = true;
            GameObject[] players = GameObject.FindGameObjectsWithTag(tagString);
            GameObject target = null;

            if (players.Length > 0)
            {
                float minDistance = detectRange;
                foreach (GameObject player in players)
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = player;
                    }
                }
            }

            if (target != null)
            {
                agent.destination = target.transform.position;
                agent.speed = chaseSpeed;
            }
            else
            {
                PointFollow();
            }
        }
        else
        {
            //agent.enabled = false;
            PointFollow();
        }
    }

    void PointFollow()
    {
        if (wanderpoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.destination = wanderpoints[destPoint].position;
            destPoint = (destPoint + 1) % wanderpoints.Length;
        }
    }
}
