
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

        private void Start()
        {
            //Rate = (int)SaveManager.Instance.CloudRate;
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
    }
}

