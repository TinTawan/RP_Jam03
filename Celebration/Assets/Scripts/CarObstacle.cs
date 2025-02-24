using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] Transform[] points;

    [SerializeField] float moveTime = 2f;

    Material carMat;

    Transform movePos;

    private void Start()
    {
        movePos = points[0];

        carMat = GetComponentInChildren<MeshRenderer>().materials[0];

        Color col = Random.ColorHSV();
        carMat.color = col;
    }


    private void Update()
    {
        float distToL = Vector3.Distance(car.transform.position, points[0].position);
        float distToR = Vector3.Distance(car.transform.position, points[1].position);

        if (distToL <= 1f)
        {
            movePos = points[1];
        }
        if(distToR <= 1f)
        {
            movePos = points[0];
        }

        MoveTo(movePos);
    }

    void MoveTo(Transform pos)
    {
        car.transform.LookAt(pos);

        //smoothly slows down closer to destination
        car.transform.position = Vector3.Lerp(car.transform.position, pos.position, moveTime * Time.deltaTime);

        //set speed moving
        //car.transform.position = Vector3.MoveTowards(car.transform.position, pos.position, moveTime * Time.deltaTime);
    }

}
