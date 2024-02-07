using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCTalking : MonoBehaviour
{
    bool near = false;
    bool interacting = false;
    public string words = "Super Cool Text!";

    public GameObject icon;
    public TMP_Text text;
    void Start()
    {
        icon.SetActive(false);
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        if(near && !interacting)
        {
            icon.SetActive(true);
        }
        else if(!near)
        {
            icon.SetActive(false);
            text.gameObject.SetActive(false);
        }

        if(near && Input.GetKey(KeyCode.E))
        {
            interacting = true;
            text.gameObject.SetActive(true);
            text.text = words;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Player"))
        {
            near = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        near = false;
        interacting = false;
    }
}
