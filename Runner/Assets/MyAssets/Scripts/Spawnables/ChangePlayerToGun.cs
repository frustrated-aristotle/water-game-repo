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
            GunFire.Instance.StartFiring();
        }
        Debug.Log("fired count : " + count);
        count++;
        //PlayerController.Instance.m_AutoMoveForward = false;
    }
}
