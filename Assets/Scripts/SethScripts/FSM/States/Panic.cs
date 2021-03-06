using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW.AI
{
    public class Panic : IState
    {
        private readonly Sapling _sapling;

        public Panic(Sapling s)
        {
            _sapling = s;
        }
        public void Tick() { }

        public void OnEnter()
        {
            //_sapling.animator.SetBool("enemyNear", _sapling.enemyNear);
            //_sapling.animator.SetBool("isGrounded", _sapling.isGrounded);
            _sapling.animator.Play("Panic");
            Debug.Log("State: Panic");
        }

        public void OnExit()
        {
            //_sapling.animator.SetBool("enemyNear", false);
            //_sapling.animator.SetBool("isGrounded", _sapling.isGrounded);
        }
    }
}
