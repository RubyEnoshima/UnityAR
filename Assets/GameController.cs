using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text textObjectiu;
    public Text textPunts;
    public Text textRecord;
    public Text textTemps;
    public Button Boto;

    public GameObject Bala;
    GameObject BalaDisparada;
    public float forca = 1f;
    bool tocat = false;

    public GameObject[] Dianes;
    public GameObject[] Objectius;
    public Material[] Materials;
    int Objectiu = 0; // entre 0 i 2

    int punts = 0;
    int record = 0;
    int temps = 60;

    bool comencat = false;

    void Shuffle() {
        for (int i = 0; i < Materials.Length - 1; i++) {
            int rnd = Random.Range(i, Materials.Length);
            var tempGO = Materials[rnd];
            Materials[rnd] = Materials[i];
            Materials[i] = tempGO;
        }
    }

    void RandomitzarDianes(){
        Shuffle();
        int i = 0;
        foreach(GameObject diana in Dianes){
            // diana.SetActive(true);
            diana.GetComponent<Animacio>().CanviarModel(Materials[i],Materials[i].name);
            diana.GetComponent<Animacio>().FerGran();
            diana.transform.localPosition = Vector3.zero;
            i++;
        }
        Objectiu = Random.Range(0,3);
        textObjectiu.text = Materials[Objectiu].name;
        textObjectiu.transform.parent.GetComponent<Image>().color = Materials[Objectiu].color;
        textObjectiu.transform.parent.GetComponent<Image>().color -= new Color(0,0,0,0.6f);
    }

    public void ComprovarDiana(GameObject Objecte){
        if(Objecte.GetComponent<Renderer>().material.color==Materials[Objectiu].color){
            punts++;
        }else{
            if(punts!=0) punts--;
        }
        textPunts.text = punts.ToString();
        foreach(GameObject diana in Dianes){
            diana.GetComponent<Animacio>().FerPetit();
        }
        Invoke("RandomitzarDianes",1.25f);
    }

    void PassarTemps(){
        temps--;
        textTemps.text = temps.ToString();

        if(temps==0){
            CancelInvoke();
            if(punts>record){
                record = punts;
                textRecord.text = "Record: "+record.ToString();
            }
            Boto.gameObject.SetActive(true);
            comencat = false;
        }
    }

    public void Comencar(){
        RandomitzarDianes();
        InvokeRepeating("PassarTemps",0f,1f);
        Boto.gameObject.SetActive(false);
        comencat = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comencat && Input.touchCount > 0 && !tocat)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase != TouchPhase.Began) return;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            
            BalaDisparada = Instantiate(Bala,ray.origin,Quaternion.identity);
            BalaDisparada.transform.forward = ray.direction.normalized;

            BalaDisparada.GetComponent<Rigidbody>().AddForce(ray.direction.normalized * forca, ForceMode.Impulse);
            BalaDisparada.GetComponent<Bala>().GameController = this;

            tocat = true;
        }else if(BalaDisparada == null){
            tocat = false;
        }
    }
}
