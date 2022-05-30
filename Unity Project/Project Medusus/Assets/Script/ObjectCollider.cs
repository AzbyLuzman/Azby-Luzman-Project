using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    [SerializeField] private bool objectCollider;
    GameObject colliderUI;

    private void Start()
    {
        objectCollider = false;
        colliderUI = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCollider = true;
        colliderUI.SetActive(true);
        GameObject.Find("SceneManager").GetComponent<SceneBoundary>().ObjectEnter(this.gameObject.name, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCollider = false;
        colliderUI.SetActive(false);
        GameObject.Find("SceneManager").GetComponent<SceneBoundary>().ObjectEnter(this.gameObject.name, false);
    }
}
