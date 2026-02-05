using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _pet = new GameObject[3];
    [SerializeField] private GameObject _firePrefab;

    public void SpawnPet(int value, Transform player)
    {
        float damage = 0;
        if (value == 1)
            damage = 60;
        else if (value == 2)
            damage = 200;
        else if (value == 3)
            damage = 750;

        GameObject pet = Instantiate(_pet[value - 1]);
        Pet scPet = pet.GetComponent<Pet>();
        scPet.SetParam(damage, player, _firePrefab);
    }
}
