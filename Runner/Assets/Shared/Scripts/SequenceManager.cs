using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using PauseState = HyperCasual.Core.PauseState;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This singleton determines the state of the game based on observed game events.
    /// </summary>
    [Serializable]
    public class SequenceManager : AbstractSingleton<SequenceManager>
    {
        [SerializeField]
        GameObject[] m_PreloadedAssets;
        [SerializeField]
        AbstractLevelData[] m_Levels;
        [SerializeField]
        GameObject[] m_LevelManagers;
        public AbstractLevelData[] Levels
        {
            get => m_Levels;
            set => m_Levels = value;
        }

        [Header("Events")]
        [SerializeField]
        AbstractGameEvent m_ContinueEvent;
        [SerializeField]
        AbstractGameEvent m_BackEvent;
        [SerializeField]
        AbstractGameEvent m_WinEvent;
        [SerializeField]
        AbstractGameEvent m_LoseEvent;
        [SerializeField]
        AbstractGameEvent m_PauseEvent;
        //My event
        [SerializeField]
        AbstractGameEvent proceedToResultEvent;
        
        
        [Header("Other")]
        [SerializeField]
        float m_SplashDelay = 2f;

        readonly StateMachine m_StateMachine = new ();
        IState m_SplashScreenState;
        IState m_MainMenuState;
        IState m_LevelSelectState;
        readonly List<IState> m_LevelStates = new();
        public IState m_CurrentLevel;

        SceneController m_SceneController;
        
        /// <summary>
        /// Initializes the SequenceManager
        /// </summary>
        public void Initialize()
        {
            m_SceneController = new SceneController(SceneManager.GetActiveScene());
            
            InstantiatePreloadedAssets();

            m_SplashScreenState = new State(ShowUI<SplashScreen>);
            m_StateMachine.Run(m_SplashScreenState);
            
            CreateMenuNavigationSequence();
            CreateLevelSequences();
            SetStartingLevel(0);
        }

        void InstantiatePreloadedAssets()
        {
            foreach (var asset in m_PreloadedAssets)
            {
                Instantiate(asset);
            }
        }

        void CreateMenuNavigationSequence()
        {
            //Create states
            var splashDelay = new DelayState(m_SplashDelay); 
            m_MainMenuState = new State(OnMainMenuDisplayed);
            m_LevelSelectState = new State(OnLevelSelectionDisplayed);
            
            //Connect the states
            m_SplashScreenState.AddLink(new Link(splashDelay));
            splashDelay.AddLink(new Link(m_MainMenuState));
            m_MainMenuState.AddLink(new EventLink(m_ContinueEvent, m_LevelSelectState));
            m_LevelSelectState.AddLink(new EventLink(m_BackEvent, m_MainMenuState));
        }

        void CreateLevelSequences()
        {
            m_LevelStates.Clear();
            
            //Create and connect all level states
            IState lastState = null;
            foreach (var level in m_Levels)
            {
                IState state = null;
                if (level is SceneRef sceneLevel)
                {
                    state = CreateLevelState(sceneLevel.m_ScenePath);
                }
                else
                {
                    state = CreateLevelState(level);
                }
                lastState = AddLevelPeripheralStates(state, m_LevelSelectState, lastState);
            }

            //Closing the loop: connect the last level to the level-selection state
            var unloadLastScene = new UnloadLastSceneState(m_SceneController);
            lastState?.AddLink(new EventLink(m_ContinueEvent, unloadLastScene));
            unloadLastScene.AddLink(new Link(m_LevelSelectState));
        }

        /// <summary>
        /// Creates a level state from a scene
        /// </summary>
        /// <param name="scenePath"></param>
        /// <returns></returns>
        IState CreateLevelState(string scenePath)
        {
            return new LoadSceneState(m_SceneController, scenePath);
        }
        
        /// <summary>
        /// Creates a level state from a level data
        /// </summary>
        /// <param name="levelData"></param>
        /// <returns></returns>
        IState CreateLevelState(AbstractLevelData levelData)
        {
            return new LoadLevelFromDef(m_SceneController, levelData, m_LevelManagers);
        }
        
        IState AddLevelPeripheralStates(IState loadLevelState, IState quitState, IState lastState)
        {
            //Create states
            m_LevelStates.Add(loadLevelState);
            var gameplayState = new State(() => OnGamePlayStarted(loadLevelState));
            var winState = new PauseState(() => OnWinScreenDisplayed(loadLevelState));
            var loseState = new PauseState(ShowUI<GameoverScreen>);
            var pauseState = new PauseState(ShowUI<PauseMenu>);
            //MyCode:
            var proceedToResultState = new PauseState(ShowUI<ResultScreen>);
            var unloadLose = new UnloadLastSceneState(m_SceneController);
            var unloadPause = new UnloadLastSceneState(m_SceneController);

            //Connect the states
            lastState?.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
            loadLevelState.AddLink(new Link(gameplayState));

            gameplayState.AddLink(new EventLink(m_WinEvent, winState));
            gameplayState.AddLink(new EventLink(m_LoseEvent, loseState));
            gameplayState.AddLink(new EventLink(m_PauseEvent, pauseState));
            
            winState.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
            winState.AddLink(new EventLink(m_BackEvent, unloadLose));
            winState.AddLink(new EventLink(proceedToResultEvent, proceedToResultState));
            
            loseState.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
            loseState.AddLink(new EventLink(m_BackEvent, unloadLose));
            loseState.AddLink((new EventLink(proceedToResultEvent, proceedToResultState)));
            
            //Newly added at 6/10/2023 June at 1:07
            proceedToResultState.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
            unloadLose.AddLink(new Link(quitState));

            pauseState.AddLink(new EventLink(m_ContinueEvent, gameplayState));
            pauseState.AddLink(new EventLink(m_BackEvent, unloadPause));
            unloadPause.AddLink(new Link(m_MainMenuState));
            
            return winState;
        }

        /// <summary>
        /// Changes the starting gameplay level in the sequence of levels by making a slight change to its links
        /// </summary>
        /// <param name="index">Index of the level to set as starting level</param>
        public void SetStartingLevel(int index)
        {
            m_LevelSelectState.RemoveAllLinks();
            m_LevelSelectState.AddLink( new EventLink(m_ContinueEvent, m_LevelStates[index]));
            m_LevelSelectState.AddLink(new EventLink(m_BackEvent, m_MainMenuState)); 
            m_LevelSelectState.EnableLinks();
        }

        void ShowUI<T>() where T : View
        {
            UIManager.Instance.Show<T>();
        }
        
        void OnMainMenuDisplayed()
        {
  //          SequenceManager.Instance.SetStartingLevel(SaveManager.Instance.LevelProgress);
 //         m_ContinueEvent.Raise();
            
            //ShowUI<MainMenu>();
            SetStartingLevel(SaveManager.Instance.LevelProgress);
            m_ContinueEvent.Raise();
 AudioManager.Instance.PlayMusic(SoundID.MenuMusic);
        }

        void OnWinScreenDisplayed(IState currentLevel)
        {
            UIManager.Instance.Show<GameoverScreen>();
            var currentLevelIndex = m_LevelStates.IndexOf(currentLevel);
            
            if (currentLevelIndex == -1)
                throw new Exception($"{nameof(currentLevel)} is invalid!");
            
            var levelProgress = SaveManager.Instance.LevelProgress;
            Debug.Log("current level is : " + currentLevel);
            SaveManager.Instance.LevelProgress = levelProgress + 1;

            if (currentLevelIndex == levelProgress && currentLevelIndex < m_LevelStates.Count - 1)
            {
                Debug.Log("LevelProgress: " + currentLevelIndex);
            }
            else
            {
                Debug.Log($"Current Level index: {currentLevelIndex} and the level progress is = {levelProgress}");
                
            }
        }

        void OnLevelSelectionDisplayed()
        {
            ShowUI<LevelSelectionScreen>();
            AudioManager.Instance.PlayMusic(SoundID.MenuMusic);
        }
        
        void OnGamePlayStarted(IState current)
        {
            m_CurrentLevel = current;
            ShowUI<Hud>();
            /*
            ShowUI<UpgradeCapacityScreen>();
            ShowUI<UpgradeFlowScreen>();            
            */
            AudioManager.Instance.StopMusic();
        }

    }
}
