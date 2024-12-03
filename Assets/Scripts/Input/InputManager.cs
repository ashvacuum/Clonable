using System;
using Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public TopDownInput PlayerInput { get; private set; }

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null) return;
            
            Instance = this;
            PlayerInput = new TopDownInput();
            PlayerInput.Enable();
        }

        private void OnDestroy()
        {
            PlayerInput.Disable();
        }
    }
}