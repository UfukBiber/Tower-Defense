using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destinations : MonoBehaviour
{
    public static Transform[] destinations;
    // Start is called before the first frame update
    void Awake()
    {
        destinations = new Transform[transform.childCount];
        for (int i = 0; i < destinations.Length; i++)
        {
            destinations[i] = transform.GetChild(i);
        }
    }
}
