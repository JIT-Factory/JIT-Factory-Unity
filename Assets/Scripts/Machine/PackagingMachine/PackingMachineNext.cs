using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingMachineNext : MonoBehaviour
{
    public GameObject packPrefab;
    public GameObject packSpawn;

    public GameObject particlePrefab;
    public GameObject particleSpawn;
    
    public float particleDuration = 2.0f;
   private void OnCollisionEnter(Collision other) {
       
        if(other.gameObject.CompareTag("CardboardBox"))
        {
            
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(packPrefab, particleSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else{
             Destroy(other.gameObject);
        }
    }
        IEnumerator CreateParticle()
    {
        Vector3 particlePosition = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }

}



