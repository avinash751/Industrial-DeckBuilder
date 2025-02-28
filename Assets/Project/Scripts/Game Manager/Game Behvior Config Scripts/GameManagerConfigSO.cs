using System;
using System.Collections.Generic;
using UnityEngine;
using GameManagerSystem.GameBehaviors;
using Unity.VisualScripting;
using System.Linq;

namespace GameManagerSystem.Configuration
{

    [CreateAssetMenu(fileName = "GameManagerConfig", menuName = "Game Manager/Game Manager Config", order = 0)]
    public class GameManagerConfigSO : ScriptableObject
    {
        [Header("Game Behaviors Configuration")]
        public List<BehaviorConfiguration> behaviorConfigurations = new List<BehaviorConfiguration>();

        [System.Serializable]
        public class BehaviorConfiguration
        {
            public string behaviorName;
            public BaseGameBehaviourConfigSO configSO;
        }

        private void OnValidate()
        {
            if (behaviorConfigurations.Count == 0)
            {
                Debug.LogWarning("No Behavior Configurations found in GameManagerConfigSO. Please add some.");
            }
            else
            {
                BehaviorConfiguration lastConfig = behaviorConfigurations.Last();
                lastConfig.behaviorName = lastConfig.configSO.BehaviorType.ToString() +" Behaviour";
            }

        }

        public void InitializeGameConfigurations(GameManager gameManager)
        {
            gameManager.ClearAllBehaviours();
            foreach (BehaviorConfiguration behaviorConfiguration in behaviorConfigurations)
            {
                switch (behaviorConfiguration.configSO.BehaviorType)
                {
                    case GameBehaviorType.Start:
                        gameManager.AddGameBehaviour(new StartBehavior(gameManager, behaviorConfiguration.configSO));
                        break;
                    case GameBehaviorType.Play:
                        gameManager.AddGameBehaviour(new PlayBehavior(gameManager, behaviorConfiguration.configSO));
                        break;
                    case GameBehaviorType.Paused:
                        gameManager.AddGameBehaviour(new PauseBehavior(gameManager, behaviorConfiguration.configSO));
                        break;
                    case GameBehaviorType.Win:
                        gameManager.AddGameBehaviour(new WinBehavior(gameManager, behaviorConfiguration.configSO));
                        break;
                    case GameBehaviorType.Lose:
                        gameManager.AddGameBehaviour(new LoseBehavior(gameManager, behaviorConfiguration.configSO));
                        break;
                }
            }
        }
    }
}