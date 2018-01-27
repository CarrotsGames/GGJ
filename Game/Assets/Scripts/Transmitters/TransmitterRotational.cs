using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterRotational : Transmitter {
    [Header("Rotational Options")]
    public float rotationTime = 1f;
    public float rotationAmount = 90;
    public bool clockwiseRotation = true;

    private bool isRotating;

    public override void HandleClick()
    {
        if (isRotating)
            return;

        StartCoroutine(Rotate(clockwiseRotation ? 1 : -1));
    }

    private IEnumerator Rotate(float direction)
    {
        isRotating = true;
        Quaternion startingRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, direction * rotationAmount, 0) * startingRotation;

        float timer = 0f;
        while(transform.rotation != targetRotation)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, timer);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;

        CheckConnections();
    }
}
