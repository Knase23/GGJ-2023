using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour
{
   private PlayerMovement _movement;
   private Rigidbody _rigidbody;
   private PlayerRooting _rooting;
   public InputActionReference jumpAction;
   public InputActionReference jumpDirectionAction;
   public LayerMask mask;


   public Animator Animator;
   // Start is called before the first frame update
   public float HorizontalPower = 1.5f;
   public float VerticalPower = 1;

   [SerializeField] private List<Transform> _isGroundedRaycastOrigins = new List<Transform>();
   [SerializeField] private AudioCue _jumpSFX = null;
   [SerializeField] private AudioCue _landSFX = null;
   [SerializeField] private ParticleSystem _landingParticles = null;
   [SerializeField] private ParticleSystem _fartTrailParticles = null;

   public bool HasJumped = false;
   public bool IsGrounded = true;

   private Coroutine _bigFart = null;
   void Start()
   {
      _movement = GetComponent<PlayerMovement>();
      _rigidbody = GetComponent<Rigidbody>();
      _rooting = GetComponent<PlayerRooting>();
      jumpAction.ToInputAction().performed += OnPerformed;
   }

   public void TriggerParticles()
   {
      _landingParticles.Play();
   }

   public void EnableFartTrail()
   {
      _fartTrailParticles.Play();
      if (_bigFart != null)
      {
         StopCoroutine(_bigFart);
      }
      _bigFart = StartCoroutine(DisableFartTrail(0.8f));

   }

   private IEnumerator DisableFartTrail(float time)
   {
      yield return new WaitForSeconds(time);
      _fartTrailParticles.Stop();
   }

   private void OnPerformed(InputAction.CallbackContext obj)
   {
      //_rigidbody.useGravity = true;
      //_movement.enabled = false;
      //float inputDirection = jumpDirectionAction.ToInputAction().ReadValue<float>(); //Save if we go 2D movement;
      if (IsGrounded)
      {
         _rigidbody.velocity = ((_rigidbody.velocity * HorizontalPower) + transform.up * VerticalPower);
         HasJumped = true;
         IsGrounded = false;
         if (_rigidbody.velocity.x >= 0)
         {
            Player.Instance.Rotator.TriggerFlip(true, false);
         }
         else
         {
            Player.Instance.Rotator.TriggerFlip(false, false);
         }
         _jumpSFX.PlayOneShot(AudioManager.Instance.SfxSource);
         EnableFartTrail();
      }
   }

   private void Update()
   {
      CheckForGround();
      Animator.SetBool("IsGrounded", IsGrounded);

   }

   private void OnCollisionEnter(Collision collision)
   {
      if (IsGrounded && HasJumped)
      {
         Player.Instance.Rotator.SnapToRotation(0f);
         _landSFX.PlayOneShot(AudioManager.Instance.SfxSource);
         TriggerParticles();
         HasJumped = false;
      }
      //_movement.enabled = true;
      //_rigidbody.useGravity = false;
   }

   private void CheckForGround()
   {
      if (_rooting.IsRooted)
      {
         IsGrounded = false;
         return;
      }
      bool isGrounded = false;
      foreach (var origin in _isGroundedRaycastOrigins)
      {
         if (Physics.Raycast(origin.transform.position, -transform.up, 0.75f, mask))
         {
            isGrounded = true;
         }
      }
      IsGrounded = isGrounded;
   }
   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawRay(transform.position, Vector3.up);
   }
}
