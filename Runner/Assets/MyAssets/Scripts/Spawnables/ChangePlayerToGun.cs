using HyperCasual.Runner;
using UnityEngine;

public class ChangePlayerToGun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController.Instance.transform.GetChild(5).gameObject.SetActive(false);
        PlayerController.Instance.transform.GetChild(6).gameObject.SetActive(true);
        GunFire.Instance.StartFiring();
    }
}
