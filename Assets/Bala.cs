using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject Explosio;
    public GameController GameController;
    float vida = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TreureVida",0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TreureVida(){
        vida--;
        if(vida<=0f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(Explosio, position, rotation);
        GameController.ComprovarDiana(other.gameObject);
        Destroy(gameObject);
    }
}
