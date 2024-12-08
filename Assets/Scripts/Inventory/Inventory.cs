using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _gun1;
    [SerializeField] private GameObject _gun2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gun1?.SetActive(true);
            _gun2?.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _gun1?.SetActive(false);
            _gun2?.SetActive(true);
        }
    }
}