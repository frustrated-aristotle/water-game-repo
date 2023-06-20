using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloveMovement : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f); // Her 3 saniyede bir bekle

            StartCoroutine(ScaleObject());
            
            yield return new WaitForSeconds(.25f); 
            
            StartCoroutine(ScaleDownObject());
            
        }
    }

    private IEnumerator ScaleObject()
    {
        float duration = .25f; // Süre
        float elapsedTime = 0f;
        float initialBlendShapeValue = 0;//GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        float targetBlendShapeValue = 100;
        float initCol = 5;
        float targetCol = 0;
        Vector3 targetV3 = new Vector3(0, 0, 0);
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(100f, 100f, 100f); // 100'e çıkmak için hedef scale

        while (elapsedTime < duration)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,
                Mathf.Lerp(initialBlendShapeValue, targetBlendShapeValue, elapsedTime / duration));
            Vector3 vector3 = GetComponent<CapsuleCollider>().center;
            vector3.x = Mathf.Lerp(initCol, targetCol, elapsedTime / duration);
            GetComponent<CapsuleCollider>().center = vector3;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, targetBlendShapeValue);
        GetComponent<CapsuleCollider>().center = targetV3;

    }

    private IEnumerator ScaleDownObject()
    {
        float duration = .25f; // Süre
        float elapsedTime = 0f;
        float initialBlendShapeValue = 100;//GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        float targetBlendShapeValue = 0;
        float initCol = 0;
        float targetCol = 5;
        Vector3 targetV3 = new Vector3(5, 0, 0);

        while (elapsedTime < duration)
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,
                Mathf.Lerp(initialBlendShapeValue, targetBlendShapeValue, elapsedTime / duration)); 
            Vector3 vector3 = GetComponent<CapsuleCollider>().center;
            vector3.x = Mathf.Lerp(initCol, targetCol, elapsedTime / duration);
            GetComponent<CapsuleCollider>().center = vector3;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, targetBlendShapeValue);
        GetComponent<CapsuleCollider>().center = targetV3;

    }
}
