
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
            Debug.Log("cloud rate BEFORE: "  + rate);
            base.OnEnable();
            Rate = (int)SaveManager.Instance.CloudRate;
            Debug.Log("cloud rate AFTER: "  + rate+ " \n++++++++++++++++++++++++++++++++++++");

            //            rate = (int)VariableManager.Instance.CloudRate;
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
                filler.FillTheBucket(Inventory.Instance,Rate);
                filler.TakeTheEffect();
            }
        }
        private void OnTriggerStay(Collider col)
        {
            fonk(col);
        }
    }
}

