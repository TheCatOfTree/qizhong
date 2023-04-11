using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class inspect : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inspects();
        inspects2();
    }


    public void inspects()
    {
        GameObject closest = GameObject.FindWithTag("unfinding");
        int layA = LayerMask.NameToLayer("wall");
        LayerMask bricksLayer = 1 << LayerMask.NameToLayer("wall");//LayerMask bricksLayer = 1 << layA;
        RaycastHit hit;
        if (GameObject.FindGameObjectsWithTag("unfinding") != null)
        {

            GameObject[] gos;

            gos = GameObject.FindGameObjectsWithTag("unfinding");
            foreach (GameObject go in gos)
            {
                if (!Physics.Linecast(this.transform.position, go.transform.position, out hit, (1 << layA)))
                {
                    go.transform.tag = "finding";
                }

            }

        }
    }
    public void inspects2()
    {
        GameObject closest = GameObject.FindWithTag("finding");
        int layA = LayerMask.NameToLayer("wall");
        LayerMask bricksLayer = 1 << LayerMask.NameToLayer("wall");//LayerMask bricksLayer = 1 << layA;
        RaycastHit hit;
        if (GameObject.FindGameObjectsWithTag("finding") != null)
        {

            GameObject[] gos;

            gos = GameObject.FindGameObjectsWithTag("finding");
            foreach (GameObject go in gos)
            {
                if (Physics.Linecast(this.transform.position, go.transform.position, out hit, (1 << layA)))
                {
                    go.transform.tag = "unfinding";
                }

            }

        }
    }
    
}
