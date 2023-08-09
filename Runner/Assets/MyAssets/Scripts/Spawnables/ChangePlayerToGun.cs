using HyperCasual.Runner;
using UnityEngine;

public class ChangePlayerToGun : MonoBehaviour
{
    int count = 1;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController.Instance.transform.GetChild(5).gameObject.SetActive(false);
        PlayerController.Instance.transform.GetChild(6).gameObject.SetActive(true);
        if (!GunFire.Instance.IsFiring)
        {
            //We will get the bullet power here.
            SaveManager.Instance.BulletPower = BulletPower();
            Debug.Log("BULLETPOWER: " + SaveManager.Instance.BulletPower);
            Debug.Log("Fire Rate : " + GunFire.Instance.Rate);//FindAndAssignBulletPower();
            GunFire.Instance.StartFiring();
            //other.GetComponent<CapsuleCollider>().gameObject.SetActive(false);
            //other.GetComponent<CapsuleCollider>().enabled = false;
        }
        Debug.Log("fired count : " + count);
        count++;
        //PlayerController.Instance.m_AutoMoveForward = false;
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
