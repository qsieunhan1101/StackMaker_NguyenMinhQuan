using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    [SerializeField] Vector2 touchStartPos;
    [SerializeField] Vector2 touchEndPos;
    private float angle=0;
    private float sign=1;
    private float offset=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TouchTest1();
    }

    private void TouchTest1()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                //Debug.Log("StartPos " + touchStartPos);
            }else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                //Debug.Log("EndPos " + touchEndPos);

                Vector2 swipeDetal = touchEndPos - touchStartPos;
                //Debug.Log(swipeDetal);
                sign = (swipeDetal.y >= 0) ? 1 : -1;
                offset = (sign >= 0) ? 0 : 360;
                angle = Vector2.Angle(Vector2.right, swipeDetal) * sign + offset;
                Debug.Log("Goc "+angle);
            }
        }

    }
}
