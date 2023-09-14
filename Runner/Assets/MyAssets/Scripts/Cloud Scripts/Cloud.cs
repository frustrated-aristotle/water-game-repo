
using System;
using HyperCasual.Core;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class Cloud : Spawnable
    {
        private const string playerTag = "Player";
        private IFillTheBucket filler;
        [SerializeField] private Material mat;

        protected override void Awake()
        {
            base.Awake();
            filler = GetComponent<IFillTheBucket>();
        }

        [SerializeField]
        private CloudType cloudType;

        [SerializeField]private int rate;
        

        public int Rate
        {
            get => rate;
            set
            {
                rate = value;
            }
        }
        
        private bool applied;
        
        
        enum CloudType
        {
            Normal,
            Acid,
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            RenderSettings.skybox = mat;
        }

        public override void ResetSpawnable()
        {
            applied = false;
        }

        private int i = 1;
        private void OnTriggerEnter(Collider col)
        {
            WaterFillHelper.FillWater(col,playerTag,filler,"CloudRate");
        }
        private void OnTriggerStay(Collider col)
        {
            WaterFillHelper.FillWater(col,playerTag,filler,"CloudRate");
        }
        private void FixedUpdate()
        {
            if (CanMoveOnX)
                MoveCloud();
        }

        private float cloudMoveInterval;
        private float gateMoveTimeInterval = 0.02f;
        private void MoveCloud()
        {
            cloudMoveInterval = SaveManager.Instance.CloudMovementSpeedOnX;
            float posX = transform.position.x;
            if (!isDirectionRight)//left
            {
                posX -= cloudMoveInterval; 
                
            }
            else                  //right
            {
                posX += cloudMoveInterval;
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
    }
}

