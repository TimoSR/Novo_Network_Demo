using Mirror;
using UnityEngine;

namespace Old
{
    public class InputManager : NetworkBehaviour
    {
    
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        //Singleton 

        public static InputManager Instance { get; private set; }

        public static Controls Controls { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);   
            }
            else
            {
                Instance = this;
            }
        
            if (Controls != null) { return; }
        
            Controls= new Controls();

        }
    
        [ClientCallback]
        private void OnEnable() => Controls.Enable();

        [ClientCallback]
        private void OnDisable() => Controls.Disable();

        private Vector2 GetPlayerMovement()
        {
            return Controls.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetPointerDelta()
        {
            return Controls.Player.Look.ReadValue<Vector2>();
        }

        public bool PlayerJumped()
        {
            return Controls.Player.Jump.triggered;
        }

    }
}
