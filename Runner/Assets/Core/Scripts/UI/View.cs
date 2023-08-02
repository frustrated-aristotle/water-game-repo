using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
using TMPro;
using UnityEngine;

namespace HyperCasual.Core
{
    /// <summary>
    /// The base class for all UI elements that can be registered in UIManager
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        private void OnEnable()
        {
            PurchaseHandler.UpdateCosts += PurchaseHandler.UpdateCost;
            PurchaseHandler.UpdateValues += PurchaseHandler.UpdateValue;
        }
        /// <summary>
        /// Initializes the View
        /// </summary>
        public virtual void Initialize()
        {
        }
        
        /// <summary>
        /// Makes the View visible
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the view
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}