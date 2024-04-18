using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private GameObject chestClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        PlayerController.ChestEvent += OpenChest;
    }
    private void OnDisable()
    {
        PlayerController.ChestEvent -= OpenChest;
    }
    public void OpenChest() 
    {
        chestClose.GetComponent<MeshRenderer>().enabled = false;
        chestOpen.SetActive(true);
    }
}
