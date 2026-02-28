using UnityEngine;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public PlayerController player;

        // Buat list dengan tipe data Enemy
        public Enemy[] enemies;
        public UIHandler uiHandler;

        private bool gameEnded = false;
        int enemiesFixed = 0;

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(Helpers.ProjectileLayer, Helpers.ProjectileLayer, true);
            Physics2D.IgnoreLayerCollision(Helpers.PlayerLayer, Helpers.ProjectileLayer, true);
            
            for (int i = 0; i < 32; i++)
                Physics2D.IgnoreLayerCollision(Helpers.ConfinerLayer, i, true);
        }

        void Start()
        {
            enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            foreach (var enemy in enemies)
            {
                enemy.OnFixed += HandleEnemyFixed;
            }
            uiHandler.SetCounter(0, enemies.Length);

            player.OnTalkedToNPC += HandlePlayerTalkedToNPC;
        }
        
        void Update()
        {
            if (gameEnded) return;

            // Lose condition
            if (player.health < 0)
            {
                gameEnded = true;
                uiHandler.DisplayLoseScreen();
                Invoke(nameof(ReloadScene), 3f);
            }
        }

        bool AllEnemiesFixed()
        {
            return true;
        }


        void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        void HandleEnemyFixed()
        {
            enemiesFixed++;
            uiHandler.SetCounter(enemiesFixed, enemies.Length);
        }

        void HandlePlayerTalkedToNPC()
        {
        
            if (AllEnemiesFixed())
            {
                uiHandler.DisplayWinScreen();
                Invoke(nameof(ReturnToMainMenu), 3f);
            }
            else
            {

            }
        }
    }