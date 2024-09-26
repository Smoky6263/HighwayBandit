using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private float _upForce;

    private Collider _myCollider;

    private Vector3 _size;
    private Vector3 _halfSize;
    private Vector3 _boxCastPosition;

    private void Awake() 
    {
        _myCollider = GetComponent<Collider>();
        _size = GetComponent<Collider>().bounds.size;
        _halfSize = _size / 2;
    }

    private void FixedUpdate() => CollisionCheck();
    private void CollisionCheck()
    {
        _boxCastPosition = GetComponent<Collider>().bounds.center;
        Collider[] hitColliders = Physics.OverlapBox(_boxCastPosition, _halfSize, Quaternion.identity);

        foreach (Collider collider in hitColliders)
        {
            StartCarOnCollision(collider);
            CivilianCarOnCollision(collider);
                        
            //if(transform.GetComponent<EnemyCar>() != null)
            //{
            //    if (collider.transform.GetComponent<CivilianCar>() != null)
            //    {
            //        if (collider.transform.GetComponent<CivilianCar>().InGame == false) 
            //        {
            //            collider.transform.GetComponent<CivilianCar>().ChangeSpeed(0f);
            //            collider.transform.GetComponent<CivilianCar>().InGame = false;
            //        }
            //
            //    }
            //}
        }
    }

    private void CivilianCarOnCollision(Collider collider)
    {
        if (transform.GetComponent<CivilianCar>() != null)
        {
            CivilianCarHitStartCar(collider);
            CivilianCarHitCivilianCar(collider);
        }
    }
    private void StartCarOnCollision(Collider collider)
    {
        if (transform.GetComponent<StartPlayerCar>() != null)
        {
            if (collider.transform.GetComponent<CivilianCar>() != null)
            {
                if (collider.transform.GetComponent<CivilianCar>().InGame)
                {
                    collider.GetComponent<CoinController>().CoinOnCarCrash(transform.forward);
                    collider.transform.GetComponent<DistanceToPlayer>().SetCrashedCar();
                    collider.transform.GetComponent<CivilianCar>().InGame = false;
                    collider.transform.GetComponent<CivilianCar>().Speed = 0f;
                    if (collider.transform.GetComponent<CrashMe>() == null)
                    {
                        CrashMe crashCar = collider.gameObject.AddComponent<CrashMe>();
                        crashCar.Init(_upForce);
                    }
                    transform.GetComponent<StartPlayerCar>().DestroyMe();
                }
            }
        }
    }


    private void CivilianCarHitStartCar(Collider collider)
    {
        if (collider.transform.GetComponent<StartPlayerCar>() != null)
        {
            collider.transform.GetComponent<StartPlayerCar>().ChangeSpeed(0f);
            if (collider.transform.GetComponent<CrashMe>() == null)
            {
                CrashMe crashCar = collider.gameObject.AddComponent<CrashMe>();
                crashCar.Init(_upForce);
            }
        }
    }
    private void CivilianCarHitCivilianCar(Collider collider)
    {
        if (collider != _myCollider && collider.transform.GetComponent<CivilianCar>() != null)
        {
            if (collider.transform.GetComponent<CivilianCar>().InGame)
            {
                collider.GetComponent<CoinController>().CoinOnCarCrash(transform.forward);
                collider.transform.GetComponent<DistanceToPlayer>().SetCrashedCar();
                collider.transform.GetComponent<CivilianCar>().InGame = false;
                collider.transform.GetComponent<CivilianCar>().Speed = 0f;

                if (collider.transform.GetComponent<CrashMe>() == null)
                {
                    CrashMe crashCar = collider.gameObject.AddComponent<CrashMe>();
                    crashCar.Init(_upForce);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_boxCastPosition, _size);
    }
}
