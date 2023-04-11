using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playing : MonoBehaviour
{
    public void OnStarGame(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
