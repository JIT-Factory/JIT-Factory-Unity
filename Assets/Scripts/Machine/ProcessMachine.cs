using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMachine : MonoBehaviour
{
    public GameObject firstProcess;
    public GameObject firstSpawn;
    public GameObject SecondProcess;
    public GameObject SecondSpawn;
    public GameObject ThirdProcess;
    public GameObject ThirdSpawn;
    public AudioClip audioClip;
    public GameObject particlePrefab;
    public GameObject particleSpawn;
    public float particleDuration = 2.0f;
   private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Interior"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(firstProcess, firstSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("1stProcess"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(SecondProcess, SecondSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("2stProcess"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            StartCoroutine(CreateParticle());
            Destroy(other.gameObject);
            Instantiate(ThirdProcess, ThirdSpawn.transform.position, Quaternion.Euler(0,-90,0));
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
