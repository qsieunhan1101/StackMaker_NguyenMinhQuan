using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private GameObject chestClose;

    [SerializeField] private ParticleSystem leftParticle;
    [SerializeField] private ParticleSystem rightParticle;
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
        PlayerController.finishEvent += OpenChest;
    }
    private void OnDisable()
    {
        PlayerController.finishEvent -= OpenChest;
    }
    public void OpenChest() 
    {
        chestClose.GetComponent<MeshRenderer>().enabled = false;
        chestOpen.SetActive(true);
        leftParticle.Play();
        rightParticle.Play();
    }
}
