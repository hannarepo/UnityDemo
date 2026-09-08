using UnityEngine;

namespace UnityDemo.Player
{
    public class MovementState : PlayerStateBase
    {
        private float _turnSmoothVelocity;
		private float _turnSmoothTime = 0.1f;

        public MovementState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        protected virtual void Move(float speed)
        {
            // Calculate character rotation using Atan2 to get the correct rotation angle based on input direction
            // and camera direction.
            // The angle is smoothed using SmoothDampAngle to create a smooth transition between angles.
            Vector2 movementInput = _playerContext.Controls.Game.Move.ReadValue<Vector2>();

            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.y) *
                Mathf.Rad2Deg + _playerContext.CameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_playerContext.PlayerTransform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                _turnSmoothTime);
            _playerContext.PlayerTransform.rotation = Quaternion.Euler(0, angle, 0);

            // Calculate character movement.
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            moveDirection.Normalize();

            // Move character
            _playerContext.PlayerTransform.position += moveDirection * speed * Time.deltaTime;
        }
    }
}