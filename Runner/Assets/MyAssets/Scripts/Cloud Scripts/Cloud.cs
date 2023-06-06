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
            _inventory = FindObjectOfType<Inventory>();
            filler = GetComponent<IFillTheBucket>();
        }

        [SerializeField]
        private CloudType cloudType;

        [SerializeField] 
        private float rate;

        private bool applied;
        private Inventory _inventory;
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
            if (col.CompareTag(playerTag))
            {
                filler.FillTheBucket(_inventory,rate);
                filler.TakeTheEffect();
            }
        }
        private void OnTriggerStay(Collider col)
        {
            fonk(col);
        }
    }
}

