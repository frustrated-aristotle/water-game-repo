using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using PlasticGui.Gluon.WorkspaceWindow.Views.WorkspaceExplorer;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will trigger a fail
    /// state with the GameManager.
    /// </summary>
    public class Gate : Spawnable
    {
        const string k_PlayerTag = "Player";

        [SerializeField]
        GateType m_GateType;
        
        [SerializeField] float m_Value;
        [SerializeField] private int heightValue;
        [SerializeField] private int widthValue;
        
        [SerializeField]
        RectTransform m_Text;

        bool m_Applied;
        [SerializeField] private bool canMoveX;
        [SerializeField] private bool canMoveZ;

        Vector3 m_TextInitialScale;

        private Inventory inventory;

        private float gateMoveInterval = 0.2f;
        private float gateMoveTimeInterval = 0.02f;
        

        

    private void Start()
    {
         
        if (transform.position.x == 0 && GameManager.Instance != null)
        {
            canMoveX = true;
            StartCoroutine(StartChange());

        }
    }

    private IEnumerator StartChange()
    {
        while (canMoveX)
        {
            ChangeDirection();
            yield return new WaitForSeconds(gateMoveTimeInterval);
        }
    }

    private bool isDirectionRight = true;
    private void ChangeDirection()
    {
        float posX = transform.position.x;
        
        if (!isDirectionRight)
        {
            posX -= gateMoveInterval;
            //left
        }
        else
        {
            posX += gateMoveInterval;
            //right
        }

        if (posX >= 3)
        {
            isDirectionRight = false;
        }
        else if (posX <= -3)
        {
            isDirectionRight = true;
        }
        Vector3 pos = transform.position;
        pos.x = posX;
        transform.position = pos;
        
    }

        enum GateType
        {
            ChangeSpeed,
            ChangeSize,
            ChangeHeight,
            ChangeWidth
        }

        /// <summary>
        /// Sets the local scale of this spawnable object
        /// and ensures the Text attached to this gate
        /// does not scale.
        /// </summary>
        /// <param name="scale">
        /// The scale to apply to this spawnable object.
        /// </param>
        public override void SetScale(Vector3 scale)
        {
            // Ensure the text does not get scaled
            if (m_Text != null)
            {
                float xFactor = Mathf.Min(scale.y / scale.x, 1.0f);
                float yFactor = Mathf.Min(scale.x / scale.y, 1.0f);
                m_Text.localScale = Vector3.Scale(m_TextInitialScale, new Vector3(xFactor, yFactor, 1.0f));

                m_Transform.localScale = scale;
            }
        }

        /// <summary>
        /// Reset the gate to its initial state. Called when a level
        /// is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            m_Applied = false;
        }

        protected override void Awake()
        {
            base.Awake();

            if (m_Text != null)
            {
                m_TextInitialScale = m_Text.localScale;
            }

            inventory = FindObjectOfType<Inventory>();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag) && !m_Applied)
            {
                ActivateGate();
            }
        }

        void ActivateGate()
        {
            switch (m_GateType)
            {
                /*
                 * 
                case GateType.ChangeSpeed:
                    PlayerController.Instance.AdjustSpeed(m_Value);
                break;

                case GateType.ChangeSize:
                    PlayerController.Instance.AdjustScale(m_Value);
                break;
                
                 */
                case GateType.ChangeHeight:
                {
                    PlayerController.Instance.AdjustHeight(heightValue);
                    int height = Inventory.Instance.BucketCapacity * 30 / 100;
                    if (heightValue<0)
                    {
                        Inventory.Instance.BucketCapacity = -height;
                    }
                    else
                    {
                        Inventory.Instance.BucketCapacity = height;
                    }
                    break;
                }
                case GateType.ChangeWidth:
                {
                    PlayerController.Instance.AdjustWidth(widthValue);
                    int width = Inventory.Instance.BucketCapacity * 30 / 100;
                    if (widthValue<0)
                    {
                        Inventory.Instance.BucketCapacity = -width;

                    }
                    else
                    {
                        Inventory.Instance.BucketCapacity = width;
                    }
                    break;
                }
            }
            m_Applied = true;
            if (widthValue < 0 || heightValue < 0 )
            {
                Debug.Log("inside ADJUST");
                Inventory.Instance.AdjustWaterFilledAmountBecauseNegativeGate();
            }
        }
    }
}