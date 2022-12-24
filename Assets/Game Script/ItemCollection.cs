using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollection : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private TMP_Text cherriesText;
    [SerializeField] private AudioSource colectAudiosource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            colectAudiosource.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
