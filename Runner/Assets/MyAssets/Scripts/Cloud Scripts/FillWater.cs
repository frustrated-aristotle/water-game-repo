using HyperCasual.Core;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,int rate)
        {
            TextMeshProUGUI t = UIManager.Instance.WaterCapacityUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            //t.text ="FIRST : "+ rate + " is rate and i: " + Random.Range(0,1000) + ": inventory"+inventory;
            t.text = "Cap: "+inventory.BucketCapacity + " and filled amount" + inventory.BucketFilledAmount;
            if (inventory.BucketCapacity >= inventory.BucketFilledAmount + rate)
            {
                TextMeshProUGUI ts = UIManager.Instance.WaterCapacityUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                ts.text ="FIRST : "+ rate + " is rate and i: " + Random.Range(0,1000);
                inventory.BucketFilledAmount = rate;
            }
            if (inventory.BucketCapacity < inventory.BucketFilledAmount)
            {
                TextMeshProUGUI ts = UIManager.Instance.WaterCapacityUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                ts.text = "SECOND : "+rate + " is rate and i: " + Random.Range(0,1000);
                inventory.MakeBucketFilledAmountEqualToCapacity();
            }
        }
        public void TakeTheEffect()
        {
        }
    }
}
