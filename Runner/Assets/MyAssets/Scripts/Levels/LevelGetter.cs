using System;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using UnityEditor;
using UnityEngine;

namespace MyAssets.Scripts.Levels
{
    public class LevelGetter : MonoBehaviour
    {
        [SerializeField] private SequenceManager sequenceManager;
        [ContextMenu("Get All Levels")]
        public void GetLevels()
        {
            List<AbstractLevelData> data = new List<AbstractLevelData>();
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    Debug.Log("For working");
                    string levelName = "Level " + i;
                    string path = $"Assets/Runner/Environment/Levels/{levelName}.asset";
                    AbstractLevelData asset = (AbstractLevelData)AssetDatabase.LoadAssetAtPath(path, typeof(AbstractLevelData)); // Assetin türünü belirtin (örneğin, GameObject).
                    data.Add(asset);
                }
                sequenceManager.Levels = data.ToArray();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        #region Delete

        public SaveManager sm;
        #endregion
        [ContextMenu("Set level  to zero")]
        public void SetLevelToZero()
        {
            sm.LevelProgress = 0;
            Debug.Log(sm.LevelProgress);
            
        }
    }
}