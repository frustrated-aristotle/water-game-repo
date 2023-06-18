
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class Cloud : Spawnable
    {
        private const string playerTag = "Player";
        
        public TextMeshProUGUI text;
        
        public GameObject go;
        
        private IFillTheBucket filler;
        [SerializeField] private Material mat;
        protected override void Awake()
        {
            base.Awake();
            filler = GetComponent<IFillTheBucket>();
        }

        [SerializeField]
        private CloudType cloudType;

        [SerializeField] 
        private int rate;

        public int Rate
        {
            get => rate;
            set
            {
                rate = value;
                Debug.LogError("Rate Is: "+ rate);
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
//            rate = (int)VariableManager.Instance.CloudRate;
            Debug.LogError("cloud rate : "  + rate);
            RenderSettings.skybox = mat;
            
        }

     

        //Maybe we can add the SetScale method

        public override void ResetSpawnable()
        {
            applied = false;
        }

        private void OnTriggerEnter(Collider col)
        {
            fonk(col);
        }

        void fonk(Collider col)
        {
            Debug.Log("Inventory bucket capactiy: " + Inventory.Instance.tempBucketCapacity);
            if (col.CompareTag(playerTag))
            {
                filler.FillTheBucket(Inventory.Instance,rate);
                filler.TakeTheEffect();
            }
        }
        private void OnTriggerStay(Collider col)
        {
            fonk(col);
        }
    }
}

