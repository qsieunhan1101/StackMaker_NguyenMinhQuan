using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private float angle = 0;
    private float sign = 1;
    private float offset = 1;

    private Vector3 rayOriginPos;

    float speed;
    Transform target;
    private enum direction{
        Right =  1,
        Left =-1,
        Forward ,
        Backward,
        None = 0,
    }
    //
    [SerializeField] private Transform cube;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        GetRayOriginPos();
        Ray ray = new Ray(transform.position + rayOriginPos, Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(transform.position + rayOriginPos, Vector3.down*5f);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Brick"))
            {
                //target.position = hit.collider.transform.position;
                Debug.Log("Va cham voi Brick"+ hit.collider.transform.position);

            }
           
        }
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    private void AddBrick()
    {

    }
    private void ReMoveBrick()
    {

    }
    private void ClearBrick()
    {

    }
    private void SetAmim()
    {

    }

    private float GetTouchDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }else if(touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;
                Vector2 swipeTouch = endPos - startPos;
                sign = (swipeTouch.y >= 0) ? 1 : -1;
                offset = (sign >= 0) ? 0 : 360;
                angle = Vector2.Angle(Vector2.right, swipeTouch) * sign + offset;
                //Debug.Log(angle);
                if (angle <= 45 || angle > 315)
                {
                    //Right
                    Debug.Log("Vuot sang phai");
                    rayOriginPos = new Vector3(1, 1, 0);
                }
                if (angle > 45 && angle <= 135)
                {
                    //Foward
                    Debug.Log("Vuot len tren");
                    rayOriginPos = new Vector3(0, 1, 1);

                }
                if (angle > 135f && angle <= 225)
                {
                    //Left
                    Debug.Log("Vuot sang trai");
                    rayOriginPos = new Vector3(-1, 1, 0);

                }
                if (angle > 225f && angle <= 315f)
                {
                    //Back
                    Debug.Log("Vuot xuong duoi");
                    rayOriginPos = new Vector3(0, 1, -1);
                }
            }
        }
        return angle;
    }

    private Vector3 GetRayOriginPos()
    {
        GetTouchDirection();
        return rayOriginPos;
    }

 /*   private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + new Vector3(1,0,0), cube.transform.position);
    }*/
}
