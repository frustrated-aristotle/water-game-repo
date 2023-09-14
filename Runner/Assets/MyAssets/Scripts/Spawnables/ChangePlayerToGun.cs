using HyperCasual.Runner;
using UnityEngine;

public class ChangePlayerToGun : MonoBehaviour
{
    int count = 1;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController.Instance.transform.GetChild(5).gameObject.SetActive(false);
        PlayerController.Instance.transform.GetChild(6).gameObject.SetActive(true);
        Vector3 size = PlayerController.Instance.GetComponent<BoxCollider>().size;
        size.x = 1.35f;
        size.z = 5f;
        PlayerController.Instance.GetComponent<BoxCollider>().size = size;
        PlayerController.Instance.GetComponent(typeof(CapsuleCollider)).gameObject.SetActive(true);
        if (!GunFire.Instance.IsFiring)
        {
            SaveManager.Instance.BulletPower = BulletPower();
            GunFire.Instance.StartFiring();
        }
        count++;
    }

    private float BulletPower()
    {
        int filledAmount = Inventory.Instance.BucketFilledAmount;
        int rate = 0;
        int min = 0;
        int max = 100;
        if (filledAmount<=max && filledAmount >min)
        {
            rate = 10;
            return rate;
        }
        else
        {
            min = 100;
            max = 150;
            rate = 15;
            for (int i = 0; i < 16; i++)
            {
                if (filledAmount>min && filledAmount <= max)
                {
                    return rate;
                }
                else if (filledAmount >= 950 )
                {
                    return 100;
                }
                else
                {
                    min += 50;
                    max += 50;
                    rate += 5;
                }
            }
        }
        return 0;
    }

    private void FindAndAssignBulletPower()
    {
        int filledAmount = Inventory.Instance.BucketFilledAmount;
        int rate = 0;
        if (filledAmount<=100 && filledAmount >0)
        {
            rate = 10;
        }
        else if (filledAmount>100 && filledAmount <= 150)
        {
            rate = 15;
        }
        else if (filledAmount > 150 && filledAmount <= 200)
        {
            rate = 20;
        }
        else if (filledAmount > 200 && filledAmount <= 250)
        {
            rate = 25;
        }
        SaveManager.Instance.BulletPower = rate;
        Debug.Log($@"Bucket filled amount is :{filledAmount} and the rate is now {rate}");
    }
}
