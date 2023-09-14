using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a win state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";
        
        private void Start()
        {
            //-0.87 , 12.72
            if (Application.isPlaying)
                Instantiate(GameManager.Instance.levelEndPrefab, transform.position, Quaternion.identity);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
                GunFire.Instance.StopFiring();
                GameManager.Instance.Win();
            }
        }
    }
}