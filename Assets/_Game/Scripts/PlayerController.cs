using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Direct
    {
        Right,
        Left,
        Forward,
        Backward,
        None,
    }
    private Vector2 startPos;
    private Vector2 endPos;
    private float angle = 0;
    private float sign = 1;
    private float offset = 1;

    [SerializeField] private float heightBrickMesh = 0.3f;
    [SerializeField] private bool isMoving = false;

    private Vector3 rayOriginPos;

    [SerializeField] private float speed = 5f;
    [SerializeField] Vector3 target;
    [SerializeField] private Direct currentDirect = Direct.None;
    //
    [SerializeField] private bool isFinish = false;

    [SerializeField] private GameObject prefabBrickMesh;
    [SerializeField] private GameObject playerAnimPos;
    [SerializeField] private Transform brickParentTranform;
    [SerializeField] private List<GameObject> listBrick;


    public delegate void FinishDelegate();
    public static FinishDelegate finishEvent;


    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSetUpDirection();

        Move();

        if (currentDirect != Direct.None)
        {
            isMoving = true;
        }
        if (currentDirect == Direct.None)
        {
            isMoving = false;
        }
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentDirect = Direct.None;
        }
        BrickController();
    }


    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void MoveSetUpDirection()
    {
        GetRayOriginPos();
        if (isFinish == false)
        {
            Ray ray = new Ray(transform.position + rayOriginPos + Vector3.up, Vector3.down);
            RaycastHit hit;
            Debug.DrawRay(transform.position + rayOriginPos + Vector3.up, Vector3.down * 10, Color.red);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Brick") || hit.collider.CompareTag("UnBrick") || hit.collider.CompareTag("HiddenBrick"))
                {
                    target = hit.collider.transform.position;
                    target.y = target.y + 0.5f;
                }
                if (hit.collider.CompareTag("Finish"))
                {
                    ClearBrick();
                    isFinish = true;
                    finishEvent?.Invoke();
                }
            }
        }

    }

    private void BrickController()
    {
        Ray rayy = new Ray(transform.position + Vector3.up + -rayOriginPos * 0.5f, Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up + -rayOriginPos * 0.5f, Vector3.down * 30, Color.blue);
        if (Physics.Raycast(rayy, out hit))
        {
            if (hit.collider.CompareTag("Brick"))
            {
                MeshRenderer mrBrick = hit.collider.GetComponent<MeshRenderer>();
                mrBrick.enabled = false;
                hit.collider.gameObject.tag = "HiddenBrick";
                AddBrick();
            }
            if (hit.collider.CompareTag("UnBrick"))
            {
                RemoveBrick();
                hit.collider.gameObject.tag = "HiddenBrick";
                GameObject lineColor = hit.collider.transform.GetChild(0).gameObject;
                lineColor.SetActive(true);
            }
        }
    }
    private void AddBrick()
    {
        if (prefabBrickMesh != null)
        {
            GameObject newBrick = Instantiate(prefabBrickMesh, brickParentTranform);
            listBrick.Add(newBrick);
            newBrick.name = $"Brick {listBrick.Count - 1}";
            newBrick.transform.position = new Vector3(brickParentTranform.position.x, ((int)listBrick.Count - 1) * heightBrickMesh - 0.4f, brickParentTranform.position.z);
            playerAnimPos.transform.position = new Vector3(playerAnimPos.transform.position.x, (int)listBrick.Count * heightBrickMesh - 0.4f, playerAnimPos.transform.position.z);
        }
    }
    private void RemoveBrick()
    {
        if (listBrick.Count > 0)
        {
            Destroy(listBrick[listBrick.Count - 1]);
            listBrick.RemoveAt(listBrick.Count - 1);
        }
        playerAnimPos.transform.position = new Vector3(playerAnimPos.transform.position.x, (int)listBrick.Count * heightBrickMesh, playerAnimPos.transform.position.z);
    }
    private void ClearBrick()
    {
        foreach (GameObject brick in listBrick)
        {
            Destroy(brick);
        }
        listBrick.Clear();
        playerAnimPos.transform.position = new Vector3(playerAnimPos.transform.position.x, (int)listBrick.Count * heightBrickMesh, playerAnimPos.transform.position.z);
    }
    private void SetAmim()
    {

    }

    private void GetTouchDirection()
    {
        if (isMoving)
        {
            return;
        }
        if (!isMoving)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
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
                        currentDirect = Direct.Right;
                    }
                    if (angle > 45 && angle <= 135)
                    {
                        //Foward
                        Debug.Log("Vuot len tren");
                        currentDirect = Direct.Forward;

                    }
                    if (angle > 135f && angle <= 225)
                    {
                        //Left
                        Debug.Log("Vuot sang trai");
                        currentDirect = Direct.Left;

                    }
                    if (angle > 225f && angle <= 315f)
                    {
                        //Back
                        Debug.Log("Vuot xuong duoi");
                        currentDirect = Direct.Backward;
                    }
                }
            }
        }

    }

    private Vector3 GetRayOriginPos()
    {
        GetTouchDirection();
        if (currentDirect == Direct.Right)
        {
            rayOriginPos = Vector3.right;
        }
        if (currentDirect == Direct.Left)
        {
            rayOriginPos = Vector3.left;
        }
        if (currentDirect == Direct.Forward)
        {
            rayOriginPos = Vector3.forward;
        }
        if (currentDirect == Direct.Backward)
        {
            rayOriginPos = Vector3.back;
        }
        if (currentDirect == Direct.None)
        {
            rayOriginPos = Vector3.zero;
        }
        return rayOriginPos;
    }

    /*   private void OnDrawGizmos()
       {
           Gizmos.color = Color.green;
           Gizmos.DrawLine(transform.position + new Vector3(1,0,0), cube.transform.position);
       }*/

    private void Push()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Push();
        }
    }
}
