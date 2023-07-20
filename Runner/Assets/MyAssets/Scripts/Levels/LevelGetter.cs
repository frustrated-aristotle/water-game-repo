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
        [SerializeField] private int wantedLevelZeroIndexed;
        [SerializeField] private int initialCapacity;
        [SerializeField] private int initialMoney;
        [SerializeField] private SequenceManager sequenceManager;
        [ContextMenu("Get All Levels")]
        public void GetLevels()
        {
            List<AbstractLevelData> data = new List<AbstractLevelData>();
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    string levelName = "Level " + i;
                    string path = $"Assets/Runner/Environment/Levels/{levelName}.asset";
                    //AbstractLevelData asset = (AbstractLevelData)AssetDatabase.LoadAssetAtPath(path, typeof(AbstractLevelData)); // Assetin türünü belirtin (örneğin, GameObject).
                    //data.Add(asset);
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
        public void SetLevelToZero()
        {
            sm.LevelProgress = wantedLevelZeroIndexed;
        }

        //Initial Money is now 150 and capacity is 200!
        [ContextMenu("Arrange Things Before the Build")]
        public void BeforeBuild()
        {
            SetLevelToZero();
            SetMoney();
            SetCapacity();
            SetBulletPower();
            
        }

        private void SetBulletPower()
        {
            sm.BulletPower = 10;
        }

        private void SetMoney()
        {
            sm.Currency = initialMoney;
        }

        private void SetCapacity()
        {
            sm.Capacity = initialCapacity;
        }
    }
}