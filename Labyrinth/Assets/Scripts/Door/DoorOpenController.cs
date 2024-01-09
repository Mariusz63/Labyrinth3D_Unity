using Assets.Scripts.Door;
using System.Collections;
using UnityEngine;

public class DoorOpenController : MonoBehaviour
{
    [Header("Door Open Parameters")]
    [SerializeField] public GameObject door;
    [SerializeField] public float openRot, closeRot, speed;
    [SerializeField] bool isOpen = false;

    private void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;

        if (isOpen)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed* Time.deltaTime);
            }
        }
        else
        {

            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed* Time.deltaTime);
            }
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }

    private IEnumerator AutoClose()
    {
        while (isOpen)
        {
            yield return new WaitForSeconds(5);

            if (Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > 3)
            {
                isOpen = false;

            }
        }
    }

}
