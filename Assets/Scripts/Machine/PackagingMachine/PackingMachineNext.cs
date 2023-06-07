using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingMachineNext : MonoBehaviour
{
    public List<GameObject> packPrefab;
    public List<GameObject> packSpawn;

    public GameObject particlePrefab;
    public List<GameObject> particleSpawn;
    
    public float particleDuration = 2.0f;
   private void OnCollisionEnter(Collision other) {
        // if(other.gameObject.CompareTag("Beer") ||other.gameObject.CompareTag("PotionBig")|| other.gameObject.CompareTag("WineBottle")|| other.gameObject.CompareTag("WineJug") )
        // {
        //     StartCoroutine(CreateParticle());
        //     Destroy(other.gameObject);
        //     Instantiate(firstProcess, firstSpawn.transform.position, Quaternion.Euler(0,-90,0));
        // }
        if(other.gameObject.CompareTag("Cardboard_Box_Beer"))
        {
            Debug.Log("?1");
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(packPrefab[0], packSpawn[0].transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("Cardboard_Box_PotionBig"))
        {
            Debug.Log("?2");
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(packPrefab[1], packSpawn[1].transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("Cardboard_Box_WineBottle"))
        {
            Debug.Log("?3");
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(packPrefab[2], packSpawn[2].transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("Cardboard_Box_WineJug"))
        {
            Debug.Log("?4");
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(packPrefab[3], packSpawn[3].transform.position, Quaternion.Euler(0,-90,0));
        }
        else{
            Debug.Log("??");
             Destroy(other.gameObject);
        }
    }
        IEnumerator CreateParticle()
    {
        Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }

}


