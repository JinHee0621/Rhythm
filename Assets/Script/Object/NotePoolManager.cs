using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoolManager : MonoBehaviour
{

    public int poolLength;
    [SerializeField]
    GameObject[] noteObjects;

    public int ObjectIndex;


    private void Awake()
    {
        noteObjects = new GameObject[poolLength];
    }


}


