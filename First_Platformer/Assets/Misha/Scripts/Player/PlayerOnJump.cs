using System.Collections;
using UnityEngine;

public class PlayerOnJump : MonoBehaviour
{
    private bool _onJump = false;

    public const string _onJumpCoroutine = "PlayerOnJumpCoroutine";

    private void Awake()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        _onJump = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.GetComponentInParent<CivilianCar>() != null) 
        {
            CivilianCar civilianCar = collision.transform.GetComponentInParent<CivilianCar>();
            Vector3 carDirection = civilianCar.transform.forward;
            float speed = civilianCar.Speed;
            _onJump = true;
            StartCoroutine(PlayerOnJumpCoroutine(carDirection, speed));
        }
    }

    private IEnumerator PlayerOnJumpCoroutine(Vector3 direction, float speed)
    {
        while (_onJump)
        {
            DoMove(direction, speed);
            Debug.Log("I am on Jump!");
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    private void DoMove(Vector3 direction,float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void CancelCoroutine(string coroutineName)
    {
        StopCoroutine(coroutineName);
    }

    private void OnDestroy()
    {
        CancelCoroutine(_onJumpCoroutine);
    }
    private void OnDisable()
    {
        CancelCoroutine(_onJumpCoroutine);
    }
    private void OnApplicationQuit()
    {
        CancelCoroutine(_onJumpCoroutine);
    }
}
