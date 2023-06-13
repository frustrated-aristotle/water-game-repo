using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Runner
{
    public class Cloud : Spawnable
    {
        private const string playerTag = "Player";
        public TextMeshProUGUI text;
        public GameObject go;
        private IFillTheBucket filler;
        protected override void Awake()
        {
            base.Awake();
            filler = GetComponent<IFillTheBucket>();
        }

        [SerializeField]
        private CloudType cloudType;

        [SerializeField] 
        private int rate;

        private bool applied;
        enum CloudType
        {
            Normal,
            Acid,
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

