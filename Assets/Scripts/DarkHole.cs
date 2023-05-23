using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHole : MonoBehaviour
{
    public GameObject asroidGameObject;
    public static GameObject _asto;
    public static GameObject _DarkHole;
    [SerializeField] private GameObject[] astroidsPrefs;
    //private List<GameObject> astroids = new List<GameObject>();
    public float minX, maxX;
    public float astroidValueY = 2f;


    private GameObject player;
    private float speed = 4f;
    private float maxDistance = 7f;
    public float minSpeed = 4f;
    public float maxSpeed = 9f;
    private void Awake()
    {
        for (int i = 0; i < 30; i++)
        {
            AstroidCreator();
        }
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (GameController.start && PlayerController.score > 0)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            //speed - speed / 2f * Time.deltaTime * (distance > maxDistance ? -1f : 1f) Bu formül, eðer mesafe maxDistance'dan büyükse, hýzý artýrmak için hýzýn yarýsýný ekler. Aksi takdirde, hýzý azaltmak için hýzýn yarýsýný çýkarýr.
            speed = Mathf.Clamp(speed - speed / 2f * Time.deltaTime * (distance > maxDistance ? -1f : 1f), minSpeed, maxSpeed);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);
            AstroidCreator();
            //GameController.astroids.RemoveAt(GameController.astroids.IndexOf(other.gameObject)-1);
            //Debug.Log(GameController.astroids.Count);
        }
    }
    private void LateUpdate()
    {
        if(asroidGameObject.transform.childCount < 30 && !PlayerController.death)
        {
            AstroidCreator();
        }
    }

    private void AstroidCreator()
    {
        //first astroid
        if (asroidGameObject.transform.childCount /*astroids.Count*/ == 0)
        {
            Vector3 astroidPosition = new Vector3(Random.Range(minX, maxX), 4f, 0);
            GameObject astroid = Instantiate(astroidsPrefs[Random.Range(0, astroidsPrefs.Length)], astroidPosition, Quaternion.identity);
            //astroids.Add(astroid);
            astroid.transform.SetParent(asroidGameObject.transform);
        }
        else
        {
            /*GameObject*/Transform lastAstroid = asroidGameObject.transform.GetChild(asroidGameObject.transform.childCount-1)/*astroids[astroids.Count - 1]*/;
            Vector3 astroidPosition = new Vector3(Random.Range(minX, maxX), lastAstroid.transform.position.y + astroidValueY, 0);
            GameObject astroid = Instantiate(astroidsPrefs[Random.Range(0, astroidsPrefs.Length)], astroidPosition, Quaternion.identity);
            //astroids.Add(astroid);
            astroid.transform.SetParent(asroidGameObject.transform);
        }
        _asto = asroidGameObject;
        _DarkHole = this.gameObject;
    }
}
