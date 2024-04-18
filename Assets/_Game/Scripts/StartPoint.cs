using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player");
        playerPosition.transform.position = startPoint.localPosition;
      
    }

    // Update is called once per frame
    void Update()
    {
    }
}
