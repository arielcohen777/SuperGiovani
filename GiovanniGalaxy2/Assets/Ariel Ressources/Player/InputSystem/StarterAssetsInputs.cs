using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;
		public bool changeWep;
		public bool reload;
		public bool interact;
		public bool aim;
		public bool die;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
        {
			ShootInput(value.isPressed);
        }

		public void OnChangeWeapon(InputValue value)
        {
			SwitchWeaponInput(value.isPressed);
		}

		public void OnReload(InputValue value)
        {
			ReloadInput(value.isPressed);
        }

		public void OnInteract(InputValue value) 
		{
			InteractValue(value.isPressed);
		}

		public void OnDie(InputValue value)
        {
			DieValue(value.isPressed);
        }

#endif
		public void OnAim(InputValue value)
        {
			AimInput(value.isPressed);
        }

        public void InteractValue(bool newInteractState)
        {
			interact = newInteractState;
        }

		public void DieValue(bool newDieValue)
		{
			die = newDieValue;
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		public void ShootInput(bool newShootState)
        {
			shoot = newShootState;
        }

		public void SwitchWeaponInput(bool newWeaponState)
        {
			changeWep = newWeaponState;
        }

		private void ReloadInput(bool isPressed)
		{
			reload = isPressed;
		}

		public void AimInput(bool isPressed)
        {
			aim = isPressed;
        }

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}


		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}