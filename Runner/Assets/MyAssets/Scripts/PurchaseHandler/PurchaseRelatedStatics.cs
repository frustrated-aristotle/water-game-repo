using HyperCasual.Runner;
using TMPro;
using UnityEngine;

namespace MyAssets.Scripts.PurchaseHandler
{
    public static class PurchaseRelatedStatics
    {
        public static void UpdateTextColors(TextMeshProUGUI t1, TextMeshProUGUI t2, UpgradeTypes firstType, UpgradeTypes secondType)
        {
            float firstCost = SaveManager.Instance.GenericGet(firstType);
            float secondCost = SaveManager.Instance.GenericGet(secondType);
            Color firstColor = Color.white, secondColor = Color.white;
            if (firstCost > SaveManager.Instance.Currency)
            {
                firstColor = Color.red;
            }
            if (secondCost > SaveManager.Instance.Currency)
            {
                secondColor = Color.red;                
            }
            t1.color = firstColor;
            t2.color = secondColor;
        }
    }
}