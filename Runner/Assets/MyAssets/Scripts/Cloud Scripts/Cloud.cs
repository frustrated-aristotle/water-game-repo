
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

        private int rate;

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
            Rate = (int)SaveManager.Instance.CloudRate;
            Rate = 24;
           
            RenderSettings.skybox = mat;
        }
        public override void ResetSpawnable()
        {
            applied = false;
        }

        private int i = 1;
        private void OnTriggerEnter(Collider col)
        {
            WaterFillHelper.FillWater(col,playerTag,filler,Rate);
        }
        private void OnTriggerStay(Collider col)
        {
            WaterFillHelper.FillWater(col,playerTag,filler,Rate);
        }
    }
}

