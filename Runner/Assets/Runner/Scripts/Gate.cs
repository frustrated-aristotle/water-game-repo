using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        Vector3 m_TextInitialScale;

        private Inventory inventory;
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
            //float h = heightValue;
            //float w = widthValue;
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
                        Debug.Log("BucXket Noldu : " + Inventory.Instance.BucketCapacity);

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
                        Debug.Log("BucXket Noldu : " + Inventory.Instance.BucketCapacity);

                    }
                    else
                    {
                        Inventory.Instance.BucketCapacity = width;
                    }
                    break;
                }
            }
            m_Applied = true;
            //negative gate
            if (widthValue < 0 || heightValue < 0 )
            {
                Inventory.Instance.AdjustWaterFilledAmountBecauseNegativeGate();
            }
        }
    }
}