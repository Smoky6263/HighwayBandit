
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public virtual int ID { get; }
    public virtual float HalfLengthOfCar { get; set; }
    public abstract float Speed { get; }
    public abstract bool InGame { get; set; }
    public virtual bool CarCrashed { get; set; }
    public virtual bool LastCar { get; set; }

    public abstract void ChangeSpeed(float value);
    public abstract void Init(int id, float maxSpeed, float speed, float halfLength, float halfHeight);
    public abstract void OnCrash();
    public virtual void ResetParams(float speed)
    {

    }
}
