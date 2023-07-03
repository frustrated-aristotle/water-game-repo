using System;
using System.Collections.Generic;
using System.Linq;
using HyperCasual.Runner;
using UnityEngine;

public class InspectorFunc : MonoBehaviour
{

    public static InspectorFunc Instance;

    private void Awake()
    {
        Instance = this;
    }

    //This function will be called from GameManager when the level is loaded.
    [ContextMenu("Assign Step Values")]
    public void AssignStepValuesByCheckingZPosition(int startingStepZ)
    {
        int firstStep = startingStepZ;
        List<Target> targets = GameObject.FindObjectsOfType<Target>().ToList();
        foreach (Target target in targets)
        {
            Debug.LogError("ASSSIGNWORKS");
            float zValue = target.transform.position.z;
            int stepValue = (int)((zValue - firstStep) / 5 + 1);
            target.targetStep = stepValue;
            target.health = (int)(stepValue * 1.5f * 20);
            target.UpdateHealthText();
        }
    }
    
    
    [ContextMenu("Correct False Positions")]
    public void ChangePos()
    {
        List<Target> targets = new List<Target>();
        targets = GameObject.FindObjectsOfType<Target>().ToList();
        foreach (Target target in targets)
        {
            Vector3 targetPos = target.transform.position;
            int pos = CheckPos(target.transform);
            switch (pos)
            {
                case 0:
                    targetPos.x = -4;
                    break;
                case 1:
                    targetPos.x = 4;
                    break;
            }
            target.transform.position = targetPos;
        }
    }
    //lEFT: -3
    //RIGHT = 3
    [ContextMenu("Correct False Gate Positions")]
    public void CorrectFalseGatePos()
    {
        List<Gate> gates = GameObject.FindObjectsOfType<Gate>().ToList();
        foreach (Gate gate in gates)
        {
            Vector3 gatePos = gate.transform.position;
            float gateX = gatePos.x;
            float gateY = gatePos.y;
            float gateZ = gatePos.z;
            if (gateX < 0)
            {
                gatePos.x = -3;
            }
            else if (gateX > 0)
            {
                gatePos.x = 3;
            }

            gatePos.y = 4.75f;

            gate.transform.position = gatePos;
        }
    }
    
    [ContextMenu("Give Positions")]
    public void GivePosFromZero()
    {
        int lineIndex = 1;
        int placedTargetCount=0;
        int baseZ = 135+15;
        float leftX = -4f;
        float rightX = 4f;
        float middleX = 0f;
        Dictionary<int, float> xPosDictionary = new Dictionary<int, float>();
        xPosDictionary.Add(1,leftX);
        xPosDictionary.Add(2,middleX);
        xPosDictionary.Add(3,rightX);
        List<Target> targets = new List<Target>();
        targets = GameObject.FindObjectsOfType<Target>().ToList();
        int i = 1;
        foreach (Target target in targets)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.z = baseZ;
            lineIndex = (int)(placedTargetCount / 3) + 1;
            targetPos.z += lineIndex * 5f;
            placedTargetCount++;
            
            targetPos.x = xPosDictionary[i];
            targetPos.y = 4;
            if (i == 3)
            {
                i = 0;
            }

            i++;
            target.transform.position = targetPos;
        }
        
    }
    private int CheckPos(Transform targetTransform)
    {
        if (targetTransform.position.x < 0)
        {
            //Left
            return 0;
        }
        else if (targetTransform.position.x > 1)
        {
            //Right
            return 1;
        }
        else
            return 2;
    }
}
