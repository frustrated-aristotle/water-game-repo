using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A scriptable object that stores all information
    /// needed to load and set up a Runner level.
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "Runner/LevelDefinition", order = 1)]
    public class LevelDefinition : AbstractLevelData
    {
        [ContextMenu("DeleteMoney")]
        public void DeleteMoney()
        {
            Debug.LogError("Works");
            List<SpawnableObject> obj = new List<SpawnableObject>();
            foreach (var s in Spawnables)
            {
                Debug.LogError("wOW");
                if (!s.SpawnablePrefab.GetComponent<Collectable>())
                {
                    Debug.LogError("NOT: " + s.SpawnablePrefab);

                    obj.Add(s);
                }
                else
                {
                    Debug.LogError("It most probably is money: " + s.SpawnablePrefab);
                }
            }

            Spawnables = obj.ToArray();
        }
        
        /// <summary>
        /// The Length of the level.
        /// </summary>
        public float LevelLength = 100.0f;

        /// <summary>
        /// The amount of extra terrain to be added before the start of the level.
        /// </summary>
        public float LevelLengthBufferStart = 5.0f;

        /// <summary>
        /// The amount of extra terrain to be added after the end of the level.
        /// </summary>
        public float LevelLengthBufferEnd = 5.0f;

        /// <summary>
        /// The width of the level.
        /// </summary>
        public float LevelWidth = 5.0f;

        /// <summary>
        /// The thickness of the level.
        /// </summary>
        public float LevelThickness = 0.1f;

        /// <summary>
        /// True means that spawnables should snap to a grid in this level.
        /// </summary>
        public bool SnapToGrid = true;

        /// <summary>
        /// The size of the grid that spawnables will snap to if SnapToGrid 
        /// is true.
        /// </summary>
        public float GridSize = 1.0f;

        /// <summary>
        /// The material applied to the generated terrain for this level.
        /// </summary>
        public Material TerrainMaterial;

        /// <summary>
        /// A prefab placed at the start point of this level.
        /// </summary>
        public GameObject StartPrefab;

        /// <summary>
        /// A prefab placed at the end of this level. This prefab should 
        /// contain collision logic to complete the level.
        /// </summary>
        public GameObject EndPrefab;

        /// <summary>
        /// An array of all SpawnableObjects that exist in this level.
        /// </summary>
        public SpawnableObject[] Spawnables;

        [System.Serializable]
        public class SpawnableObject
        {
            /// <summary>
            /// The prefab spawned by this SpawnableObject.
            /// </summary>
            public GameObject SpawnablePrefab;

            /// <summary>
            /// The world position of this SpawnableObject.
            /// </summary>
            public Vector3 Position = Vector3.zero;

            /// <summary>
            /// The rotational euler angles of this SpawnableObject.
            /// </summary>
            public Vector3 EulerAngles = Vector3.zero;

            /// <summary>
            /// The world scale of this SpawnableObject.
            /// </summary>
            public Vector3 Scale = Vector3.one;

            /// <summary>
            /// The base color to be applied to the materials on 
            /// this SpawnableObject.
            /// </summary>
            public Color BaseColor = Color.white;

            /// <summary>
            /// True if this object should snap to a levels grid. 
            /// Setting this value to false will make this SpawnableObject
            /// ignore the level's snap settings.
            /// </summary>
            public bool SnapToGrid = true;
            public bool CanMove = false;
            public bool isDirectionRight = false;
        }

        /// <summary>
        /// Store all values from updatedLevel into this LevelDefinition.
        /// </summary>
        /// <param name="updatedLevel">
        /// The LevelDefinition that has been edited in the Runner Level Editor Window.
        /// </param>
        public void SaveValues(LevelDefinition updatedLevel)
        {
            // TODO - Add Tests for this!
            LevelLength = updatedLevel.LevelLength;
            LevelLengthBufferStart = updatedLevel.LevelLengthBufferStart;
            LevelLengthBufferEnd = updatedLevel.LevelLengthBufferEnd;
            LevelWidth = updatedLevel.LevelWidth;
            LevelThickness = updatedLevel.LevelThickness;
            SnapToGrid = updatedLevel.SnapToGrid;
            GridSize = updatedLevel.GridSize;
            TerrainMaterial = updatedLevel.TerrainMaterial;
            StartPrefab = updatedLevel.StartPrefab;
            EndPrefab = updatedLevel.EndPrefab;
            Spawnables = updatedLevel.Spawnables;

            //When we click to the save button
            //We will loop through all Spawnable typed objects on the scene.
            //In this loop, we will loop through all Spawnables untill finding the same positioned object.
            //After finding it, we will give the SpawnableObject its CanMove property's value.
            List<Spawnable> spawnablesList = new List<Spawnable>();
            spawnablesList = GameObject.FindObjectsOfType<Spawnable>().ToList();
            Debug.Log("Count: "+spawnablesList.Count);
            foreach (Spawnable spawnable in spawnablesList)
            {
                Vector3 pos = spawnable.transform.position;
                foreach (SpawnableObject spawnableObject in Spawnables)
                {
                    if (pos == spawnableObject.Position)
                    {
                        spawnableObject.CanMove = spawnable.CanMoveOnX;
                        spawnableObject.isDirectionRight = spawnable.isDirectionRight;
                    }
                }
            }
        }
      
    }
    
}