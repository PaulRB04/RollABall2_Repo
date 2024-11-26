using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject ObjectAmover;

    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;


    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectAmover.transform.position = Vector3.MoveTowards(ObjectAmover.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if (ObjectAmover.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }

        if (ObjectAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
    }
}
