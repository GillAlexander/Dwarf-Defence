using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IEUnit
{
    public float health;
    unitMajorStates currentUnitMajorState;
    unitStates currentUnitState;

    public void ApplyDamage() {
        throw new System.NotImplementedException();
    }

    public void Attack() {
        throw new System.NotImplementedException();
    }

    public void Dead() {
        throw new System.NotImplementedException();
    }

    public void GetClosestEnemy() {
        throw new System.NotImplementedException();
    }

    public unitMajorStates GetUnitState() {
        return currentUnitMajorState;
    }

    public void UpdateState() {
        throw new System.NotImplementedException();
    }

    public enum unitMajorStates { }
    public enum unitStates { }

}
