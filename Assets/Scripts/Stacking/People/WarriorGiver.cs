using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WarriorGiver : MonoBehaviour
{
    [SerializeField] private List<Warrior> _warriors;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WarriorGroup warriorGroup))
        {
            int remainder = warriorGroup.SendWarriors(_warriors);
            if (remainder > 0)
            {
                var warriors = new List<Warrior>();
                for (int i = 0; i < remainder; i++)
                {
                    warriors.Add(_warriors[_warriors.Count - 1 - i]);
                }
                _warriors = warriors;
            }
            else
            {
                _warriors.Clear();
            }
            
        }
    }
}
