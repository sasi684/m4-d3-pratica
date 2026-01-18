using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    public bool IsGrounded;

    private void Update()
    {
       IsGrounded = Physics.CheckSphere(transform.position, 0.02f, _groundLayer);
    }
}
