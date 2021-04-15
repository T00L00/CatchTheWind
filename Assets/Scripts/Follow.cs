using UnityEngine;
using System.Collections;
public class Follow : MonoBehaviour
{

    public GameObject objectToFollow;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - objectToFollow.transform.position;
    }
    void Update()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}