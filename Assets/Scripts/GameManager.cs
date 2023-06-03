using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UnloadUnusedAssetsRoutine());
    }

    private IEnumerator UnloadUnusedAssetsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // 1분마다 호출

            Resources.UnloadUnusedAssets();
        }
    }
}
