using System;
using System.Collections.Generic;
using System.Linq;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using UnityEngine;

public class GateMovementDirection : MonoBehaviour
{
    public List<Gate> gates = new List<Gate>();
    public bool isDone = false;
    public static GateMovementDirection Instance;

    private void Awake()
    {
        isDone = true;
        if (Instance == null)
        {
            Instance = this;
        }
        Debug.Log(SaveManager.Instance.LevelProgress);
        var level = (LevelDefinition)SequenceManager.Instance.Levels[SaveManager.Instance.LevelProgress];
        var spawnables = level.Spawnables;
        int index = 0;
        foreach (var spawnable in spawnables)
        {
            if (spawnable.Position.x == 0 && spawnable.SpawnablePrefab)
            {
                if (spawnable.SpawnablePrefab.GetComponent<Gate>())
                {
                    gates.Add((spawnable.SpawnablePrefab.GetComponent<Gate>()));
                    gates[index].transform.position = spawnable.Position;
                    index++;
                }
            }
        }
        if (gates.Count > 1)
        {
            GiveTheirDirections();
        }
        foreach (Gate gate in gates)
        {
            Debug.Log(gate);
            gate.gateMovementDirection = this;
        }
    }

    public void GateMovementDirectionMainMethod()
    {
       
    }

    private void GiveTheirDirections()
    {
        List<Gate> tempGates = new List<Gate>();
        tempGates.AddRange(gates);
        foreach (Gate gate in gates)
        {
            tempGates.Remove(gate);
            if (tempGates.Count > 0)
            {
                foreach (Gate gate2 in tempGates)
                {
                    if (gate.transform.position.z + 15 == gate2.transform.position.z)
                    {
                        gate2.isDirectionRight = !gate.isDirectionRight;
                    }
                }
            }
        }
    }

    
    public void AddGate(Gate add)
    {
        if (add.transform.position.x == 0)
        {
            add.isDirectionRight = false;
            gates.Add(add);
        }
    }
}
