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
            if (transform.GetComponent<StartPlayerCar>() != null)
            {
                if (collider.transform.GetComponent<CivilianCar>() != null)
                {
                    collider.transform.GetComponent<CivilianCar>().InGame = false;
                    collider.transform.GetComponent<CivilianCar>().Speed = 0f;
                }
            }

            if (transform.GetComponent<CivilianCar>() != null)
            {
                if (collider.transform.GetComponent<StartPlayerCar>() != null)
                {
                    collider.transform.GetComponent<StartPlayerCar>().ChangeSpeed(0f);
                }
                else if(collider != _myCollider && collider.transform.GetComponent<CivilianCar>() != null)
                {
                    collider.transform.GetComponent<CivilianCar>().InGame = false;
                    collider.transform.GetComponent<CivilianCar>().Speed = 0f;
                    Transform player = collider.transform.GetComponent<DistanceToPlayer>().PlayerPosition;
                    if(collider.transform.GetComponent<CrashMe>() == null) 
                    {
                        CrashMe crashCar = collider.gameObject.AddComponent<CrashMe>();
                        crashCar.Init(player, _upForce);
                    }
                }
            }
            
            //if(transform.GetComponent<EnemyCar>() != null)
            //{
            //    if (collider.transform.GetComponent<CivilianCar>() != null)
            //    {
            //        Debug.Log($"{transform.name} hit: {collider.transform.name}");
            //        collider.transform.GetComponent<CivilianCar>().ChangeSpeed(0f);
            //        collider.transform.GetComponent<CivilianCar>().InGame = false;
            //    }
            //}
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_boxCastPosition, _size);
    }
}
