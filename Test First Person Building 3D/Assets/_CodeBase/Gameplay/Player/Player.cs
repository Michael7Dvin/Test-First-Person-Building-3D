﻿using _CodeBase.Gameplay.Building;
using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(LookAround), typeof(Mover), typeof(Raycaster))]
    [RequireComponent(typeof(PickUpInteraction))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        
        private BuildRotator _buildRotator;
        private BuildSnapping _buildSnapping;
        private BuildPlacing _buildPlacing;
        private IInputService _inputService;

        public void Construct(IInputService inputService, BuildRotator buildRotator, BuildSnapping buildSnapping, BuildPlacing buildPlacing)
        {
            _inputService = inputService;
            _buildRotator = buildRotator;
            _buildSnapping = buildSnapping;
            _buildPlacing = buildPlacing;
            
            _inputService.RotateAwayPerformed += _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed += _buildRotator.RotateTowards;
            
            _inputService.PickUpPressed += OnInteractionInput;
            
            Raycaster.CurrentTargetChanged += _buildSnapping.OnRaycasterTargetChanged;
        }

        private void OnInteractionInput()
        {
            PickUpInteraction.PickUp();
            _buildPlacing.Place();
        }

        public LookAround LookAround { get; private set; }
        public Mover Mover { get; private set; }
        public Raycaster Raycaster { get; private set; }
        public PickUpInteraction PickUpInteraction { get; private set; }
        
        private void Awake()
        {
            LookAround = GetComponent<LookAround>();
            Mover = GetComponent<Mover>();
            Raycaster = GetComponent<Raycaster>();
            
            PickUpInteraction = GetComponent<PickUpInteraction>();
        }

        private void Update()
        {
            _buildSnapping.Update();
        }

        private void OnDestroy()
        {
            _inputService.RotateAwayPerformed -= _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed -= _buildRotator.RotateTowards;
            
            Raycaster.CurrentTargetChanged -= _buildSnapping.OnRaycasterTargetChanged;
        }
    }
}