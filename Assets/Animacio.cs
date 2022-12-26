using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacio : MonoBehaviour
{
    Vector3 petit = new Vector3(0, 0, 0);
    Vector3 gran, novaEscala;
    float speed = 2.2f;
    public bool start = false;
    Renderer r;

    public void CanviarModel(Material m, string nom){
        gameObject.name = nom;
        r.material = m;
    }

    public void FerPetit(){
        novaEscala = petit;
        start = true;
    }

    public void FerGran(){
        novaEscala = gran;
        start = true;
    }

    void Start(){
        gran = transform.localScale;
        r = GetComponentInChildren<Renderer>();
    }
    
    void Update ()
    {
        if(start) transform.localScale = Vector3.Lerp (transform.localScale, novaEscala, speed * Time.deltaTime);
        if(transform.localScale==petit || transform.localScale==gran) start = false;
    }
}
