using System.Collections;
using UnityEngine;

public class PlayerOnJump : MonoBehaviour
{
    private bool _onJump = false;

    public const string _onJumpCoroutine = "PlayerOnJumpCoroutine";

    private void OnCollisionEnter(Collision collision)
    {
        _onJump = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponentInParent<CivilianCar>() != null)
        {
            CivilianCar civilianCar = collision.transform.GetComponentInParent<CivilianCar>();
            Vector3 carDirection = civilianCar.transform.forward;
            float speed = civilianCar.Speed;
            _onJump = true;
            StartCoroutine(PlayerOnJumpCoroutine(carDirection, speed));
        }
        if(collision.transform.GetComponentInParent<StartPlayerCar>() != null)
        {
            StartPlayerCar playerCar = collision.transform.GetComponentInParent<StartPlayerCar>();
            Vector3 playerCardirection = playerCar.transform.forward;
            float speed = playerCar.Speed;
            _onJump = true;
            StartCoroutine(PlayerOnJumpCoroutine(playerCardirection, speed));
        }
    }

    private IEnumerator PlayerOnJumpCoroutine(Vector3 direction, float speed)
    {
        while (_onJump)
        {
            DoMove(direction, speed);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    private void DoMove(Vector3 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void CancelOnJumpCoroutine()
    {
        StopCoroutine(_onJumpCoroutine);
    }

    private void OnDestroy()
    {
        CancelOnJumpCoroutine();
    }
    private void OnDisable()
    {
        CancelOnJumpCoroutine();
    }
    private void OnApplicationQuit()
    {
        CancelOnJumpCoroutine();
    }
}