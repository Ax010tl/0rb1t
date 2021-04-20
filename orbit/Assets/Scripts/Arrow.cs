using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] Color slow = Color.yellow;
    [SerializeField] Color fast = Color.red;
    [SerializeField] GameObject arrowTip;
    [SerializeField] GameObject pivot;
    [SerializeField] GameObject rocket;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        var arrow = arrowTip.transform.position;
        lineRenderer.SetPosition(0, rocket.transform.position);
        lineRenderer.SetPosition(1, arrow);

        updateTrajectoryAngle();
    }

    void updateTrajectoryAngle(){
        Vector2 dis = arrowTip.transform.position - pivot.transform.position;
        float angleRadians = (float) Math.Atan2(dis.y, dis.x);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);
        arrowTip.transform.rotation = Quaternion.Euler(0, 0, angleDegrees-90);
    }
}
