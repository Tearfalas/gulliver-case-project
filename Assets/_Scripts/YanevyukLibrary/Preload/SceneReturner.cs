using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReturner : MonoBehaviour
{
    public int frameWait;
    private void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        for(int i = 0; i< frameWait;i++){
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

}
