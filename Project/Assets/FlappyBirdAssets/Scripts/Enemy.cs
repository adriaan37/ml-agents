using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]public float speed = 3f;

    private void Update()
    {
        transform.Translate(new Vector3(-speed*Time.deltaTime,0f,0f), Space.World);
    }

}
