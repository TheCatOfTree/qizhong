using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playing : MonoBehaviour
{
    public GameObject lenstion;
    public GameObject back;
    public GameObject close;
    public GameObject help;
    public void OnStarGame(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
        
    }

    public void show()
    {
        back.SetActive(true);
        lenstion.SetActive(true);
        close.SetActive(true);
        help.SetActive(true);
    }
    public void unshow()
    {
        back.SetActive(false);
        lenstion.SetActive(false);
        close.SetActive(false);
        help.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        back.SetActive(false);
        lenstion.SetActive(false);
        close.SetActive(false);
        help.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
