using UnityEngine;

public class CarAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string _onPlayerJump = "OnPlayerJump";
    private const string _onPlayer = "OnPlayer";

    public void OnPlayerJump()
    {
        _animator.SetTrigger(_onPlayerJump);
    }
    public void OnPlayer(bool value)
    {
        _animator.SetBool(_onPlayer, value);
    }
}
