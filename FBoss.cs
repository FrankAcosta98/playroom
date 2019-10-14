using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBoss : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    private GameObject[] boxes; 
    [SerializeField]
    public float speed;
    public float cooldown;
    private int Cpoint = 0;
    private float wait;
    private bool hmm = false;
    private bool chilling = false;
    public float chill;
    private float chillLevel;
    public float AngerLvl;
    private float anger;
    private int fase;
    public Collider2D CdV;
    public Rigidbody2D rb;
    
    void Start(){
        wait = cooldown;
        transform.position = points[Cpoint].transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        chillLevel = chill;
    }
    // Update is called once per frame
void Update()
    {
        if (anger>=AngerLvl)
            Puch();
        else if (hmm)
            Hunt();
        else
            Move();
        if (chillLevel <= 0){
            hmm = false;
            GameObject.FindGameObjectWithTag("Focus").tag = "detectable";
            }
        if (chilling)
            chillLevel -= Time.deltaTime;
        anger+=Time.deltaTime;
    }
    private void Puch(){
        //lights out
        //https://www.youtube.com/watch?v=kTvBRkPTvRY
        //https://www.youtube.com/watch?v=tdSmKaJvCoA
        transform.position = points[Cpoint].transform.position;
        switch (fase) 
        {
            case 1:{// 4 cajas random la primera caja es usable
                GameObject[] boxes = new GameObject[4];
            }break;
            case 2:{// 5 cajas radom la primera caja es usable
                GameObject[] boxes = new GameObject[5];
            }break;
            case 3:{// 6 cajas random la primeera es usable y ultima llave
                GameObject[] boxes = new GameObject[6];
            fase=1; //para que de 2 osea la ultima fase se repite
            }break;
        }
        //spawnear cosas random en el Vect2 area
        fase++;
        anger=0.0f;
    }
    private void Move(){
        transform.position = Vector2.MoveTowards(transform.position, points[Cpoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[Cpoint].position) < 0.2f){
            if (wait <= 0){
                if (Cpoint + 1 == points.Length)
                {
                    Cpoint = 0;
                }
                else{
                    Cpoint += 1;
                }
                wait = cooldown;
            }
            else{
                wait -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D prey){
        if (prey.gameObject.transform.tag == "detectable" && prey.GetType() == typeof(CircleCollider2D)){
            hmm = true;
            prey.gameObject.tag = "Focus";
            chillLevel = chill;
        }

        if (prey.gameObject.transform.tag == "Focus" && prey.GetType() == typeof(BoxCollider2D)){
            Debug.Log("Ya te cargo el payaso");
            Destroy(prey.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D prey){
        chilling = true;
    }
    private void Hunt(){
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);
    }
}
