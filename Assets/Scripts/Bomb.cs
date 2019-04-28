using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject destroyableBlock;
    public GameObject bombPrefab;
    public LayerMask levelMask;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        string cleanup = Textarea.stringToEdit.Replace("/n", "");
        string[] commands = cleanup.Split(';');
        foreach (string command in commands)
        {
            switch (command)
            {
                case "Explode":
                    Invoke("Explode", 3f);
                    break;
                case "Generate":
                    Invoke("Generate", 1.5f);
                    break;
                case "MultiBomb":
                    Invoke("MultiBomb", 3f);
                    break;
                case "BeamUp":
                    Invoke("Up", 3f);
                    break;
                case "BeamLeft":
                    Invoke("Left", 3f);
                    break;
                case "BeamDown":
                    Invoke("Down", 3f);
                    break;
                case "BeamRight":
                    Invoke("Right", 3f);
                    break;
                default:
                    if (Textarea.stringToEdit.Equals("UpUpDownDownLeftRightLeftRightBA"))
                    {
                        GlobalStateManager.KonamiWin();
                    }
                    break;
            }
            //yield return new WaitForSeconds(.5f);
        }
        //StartCoroutine(BombPipe());
        //Textarea.stringToEdit = "Explode;";
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1        
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;        
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4

    }

    void MultiBomb()
    {
        StartCoroutine(CreateMore(Vector3.forward));
        StartCoroutine(CreateMore(Vector3.right));
        StartCoroutine(CreateMore(Vector3.back));
        StartCoroutine(CreateMore(Vector3.left));
        Explode();
    }

    void Up()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1        
        StartCoroutine(Beam(Vector3.forward));
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
    }
    void Left()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1        
        StartCoroutine(Beam(Vector3.left));
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
    }
    void Right()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1        
        StartCoroutine(Beam(Vector3.right));
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
    }
    void Down()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1        
        StartCoroutine(Beam(Vector3.back));
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
    }
    /*private IEnumerator BombPipe()
    {
        string[] commands = Textarea.stringToEdit.Split(';');
        foreach (string command in commands)
        {
            switch (command)
            {
                case "Explode":
                    Invoke("Explode", 3f);
                    break;
                case "Generate":
                    Invoke("Generate", 3f);
                    break;
                case "MultiBomb":
                    Invoke("MultiBomb", 3f);
                    break;
                case "BeamUp":
                    Invoke("Up", 3f);
                    break;
                case "BeamLeft":
                    Invoke("Left", 3f);
                    break;
                case "BeamDown":
                    Invoke("Down", 3f);
                    break;
                case "BeamRight":
                    Invoke("Right", 3f);
                    break;
                default:
                    if (Textarea.stringToEdit.Equals("UpUpDownDownLeftRightLeftRightBA"))
                    {
                        GlobalStateManager.KonamiWin();
                    }
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
        
    }*/
    private IEnumerator Beam(Vector3 direction)
    {
        for (int i = 1; i < 5; i++)
        {
            //2
            RaycastHit hit;
            //3
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
              i, levelMask);

            //4
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction),
                  //5 
                  explosionPrefab.transform.rotation);
                //6
            }
            else
            { //7
                //Debug.Log(hit.collider.tag.Equals("Destroyable"));
                if (hit.collider.gameObject.GetComponent<Destroyables>() != null)
                {
                    hit.collider.gameObject.GetComponent<Destroyables>().Exec();
                    //If the thing we hit DOESN'T have a spesific script. Run this code
                }
            }

            //8
            yield return new WaitForSeconds(.05f);
        }
    }

    private IEnumerator CreateMore(Vector3 direction)
    {
        RaycastHit hit;
        //3
        Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
          1, levelMask);

        //4
        if (!hit.collider)
        {
            Instantiate(bombPrefab, transform.position + (1 * direction),
              //5 
              explosionPrefab.transform.rotation);
            //6
        }
        yield return new WaitForSeconds(.05f);
    }
    void Generate()
    {     
        GetComponent<MeshRenderer>().enabled = false; //2
        exploded = true;        
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
        Instantiate(destroyableBlock, transform.position, Quaternion.identity);
    }

private IEnumerator CreateExplosions(Vector3 direction)
    {
        //1
        for (int i = 1; i < 2; i++)
        {
            //2
            RaycastHit hit;
            //3
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
              i, levelMask);

            //4
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction),
                  //5 
                  explosionPrefab.transform.rotation);
                //6
            }
            else
            { //7
                //Debug.Log(hit.collider.tag.Equals("Destroyable"));
                if (hit.collider.gameObject.GetComponent<Destroyables>() != null)
                {
                    hit.collider.gameObject.GetComponent<Destroyables>().Exec();
                    //If the thing we hit DOESN'T have a spesific script. Run this code
                }
            }

            //8
            yield return new WaitForSeconds(.05f);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        { // 1 & 2  
            CancelInvoke("Explode"); // 2
            Explode(); // 3
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
