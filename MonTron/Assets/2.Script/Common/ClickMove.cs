using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    LineRenderer lr;
    Coroutine draw;

    public Transform destTransform;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.material.color = Color.blue;
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            agent.SetDestination(destTransform.position);
            anim.SetFloat("Speed", 2.0f);
            anim.SetFloat("MotionSpeed", 2.0f);
            draw = StartCoroutine(DrawPath());

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if(Physics.Raycast(ray, out RaycastHit hit))
            //{
            //    agent.SetDestination(hit.point);
            //    anim.SetFloat("Speed", 2.0f);
            //    anim.SetFloat("MotionSpeed", 2.0f);

            //    if (draw != null) StopCoroutine(draw);
            //    draw = StartCoroutine(DrawPath());
            //}


        }

        else if(agent.remainingDistance < 0.1f)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("MotionSpeed", 0f);

            lr.enabled = false;
            if (draw != null) StopCoroutine(draw);
        }
    }

    IEnumerator DrawPath()
    {
        Debug.Log("sdf");
        lr.enabled = true;
        yield return null;
        while (true)
        {
            int cnt = agent.path.corners.Length;
            lr.positionCount = cnt;
            for(int i = 0; i< cnt; i++)
            {
                lr.SetPosition(i, agent.path.corners[i]);
            }
            yield return null;
        }
    }
}
