using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorTransitionController : MonoBehaviour
{
    public List<GameObject> transitionWalls = new List<GameObject>();

    public GameObject player;

    public float fadeSpeed = 0.1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TransitionStarted");        
            
            foreach (GameObject transitionWall in transitionWalls)
            {
                //StartCoroutine(FadeOutObject(transitionWall));
                transitionWall.SetActive(false);
                //transitionWall.GetComponent<MeshRenderer>().enabled = false;
            }

            Debug.Log("TransitionEnded");
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TransitionStarted");        
            
            foreach (GameObject transitionWall in transitionWalls)
            {

                transitionWall.SetActive(true);
                //transitionWall.GetComponent<MeshRenderer>().enabled = false;
            }

            Debug.Log("TransitionEnded");
        }

        
    }

    public IEnumerator FadeOutObject(GameObject fadeObject)
    {        
        while (fadeObject.GetComponent<Renderer>().material.color.a > 0)
        {
            Color objectColor = fadeObject.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeObject.GetComponent<Renderer>().material.color = objectColor;            
        }
        yield return null;
    }
}
