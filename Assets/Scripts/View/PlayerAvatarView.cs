using Aestar.Services;
using UnityEngine;
using Zenject;

namespace Aestar.View
{
    public class PlayerAvatarView : MonoBehaviour
    {
        private const string _velocityAnimKey = "Velocity";
        private const string _shopBuildingTag = "Shop";

        [SerializeField]
        private Transform _rootTransform;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private int _moveSpeed;

        [Inject]
        private InputService _inputService;

        private void Awake()
        {
            _inputService.OnMoveInput += ApplyMovement;
        }

        private void OnDestroy()
        {
            _inputService.OnMoveInput -= ApplyMovement;
        }

        private void ApplyMovement(Vector3 velocity)
        {
            velocity = Vector3.ClampMagnitude(velocity, 1);
            _animator.SetFloat(_velocityAnimKey, velocity.sqrMagnitude);
            _rootTransform.LookAt(_rootTransform.position + velocity, Vector3.up);
            _characterController.SimpleMove(velocity * _moveSpeed);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            ChekShopOpening(hit);
        }

        private void ChekShopOpening(ControllerColliderHit hit)
        {
            if (hit != null)
            {
                if (hit.collider.CompareTag(_shopBuildingTag))
                {
                    var _visitedShop = hit.collider.GetComponent<Shop>();
                    _visitedShop.OpenShop();
                }
            }
        }
    }
}
