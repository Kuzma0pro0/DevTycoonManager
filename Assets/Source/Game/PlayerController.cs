using UnityEngine;
using DevIdle.Core;

namespace DevIdle.Game
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;

                player.Init();
            }
        }

        private Player player;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            Player.Update(Time.deltaTime);
        }
    }
}