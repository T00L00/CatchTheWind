using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW
{
    public class ObstacleEatingPlant : MonoBehaviour
    {
        Animator animator;
        Sapling sapling;

        private void Awake()
        {
            sapling = GameObject.Find("Sapling").GetComponent<Sapling>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == StaticFields.SAPLING_TAG)
            {
                animator.Play("Eat");
                sapling.gameObject.SetActive(false);
            }
        }
    }
}