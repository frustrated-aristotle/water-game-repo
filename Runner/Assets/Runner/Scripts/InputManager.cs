using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple Input Manager for a Runner game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the InputManager.
        /// </summary>
        [SerializeField]private RectTransform bucketCapacityUpgradeButton;
        [SerializeField]private RectTransform flowRateUpgradeButton;
        [SerializeField]private RectTransform valueManipulatorButton;
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
            bucketCapacityUpgradeButton = UIManager.Instance.bucketCapacityUpgradeButton;
            flowRateUpgradeButton = UIManager.Instance.flowRateUpgradeButton;
            valueManipulatorButton = UIManager.Instance.valueManipulatorButton;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }

#if UNITY_EDITOR
            m_InputPosition = Mouse.current.position.ReadValue();
            if (Mouse.current.leftButton.isPressed)
            {
                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#else
            if (Touch.activeTouches.Count > 0)
            {
                m_InputPosition = Touch.activeTouches[0].screenPosition;

                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#endif

            if (m_HasInput && (!CheckRectContainsScreenPoint() || PlayerController.Instance.isRunning))
            {
                PlayerController.Instance.isRunning = true;
                Hud hud = FindObjectOfType<Hud>();
                hud?.ToggleButtonActiveState(false);
                float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
                PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);
                
            }
            else
            {
                PlayerController.Instance.CancelMovement();
            }

            m_PreviousInputPosition = m_InputPosition;
        }
        
        private bool CheckRectContainsScreenPoint()
        {
            bool b1 = RectTransformUtility.RectangleContainsScreenPoint(bucketCapacityUpgradeButton, m_InputPosition);
            bool b2 = RectTransformUtility.RectangleContainsScreenPoint(flowRateUpgradeButton, m_InputPosition);
            bool b3 = RectTransformUtility.RectangleContainsScreenPoint(valueManipulatorButton, m_InputPosition);
            return b1 || b2 || b3;
        }
    } 
    
}

