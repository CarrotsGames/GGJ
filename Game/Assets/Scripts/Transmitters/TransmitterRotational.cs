using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterRotational : Transmitter {
    [Header("Rotational Options")]
    public float rotationTime = 1f;
    public float rotationAmount = 90;
    public bool clockwiseRotation = true;

    public override void HandleClick(int direction)
    {
        base.HandleClick(direction);

        if (spark)
            spark.ConnectionBroken();

        if (transmitterController.AnyMoving)
            return;

        StartCoroutine(Rotate(direction));
    }

    private IEnumerator Rotate(float direction)
    {
        isMoving = true;
        Quaternion startingRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, direction * rotationAmount, 0) * startingRotation;

        float timer = 0f;
        while (timer <= rotationTime)
        {
            timer += Time.deltaTime;

            float t = timer / rotationTime;
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
        isMoving = false;

        transmitterController.UpdateConnections();
        transmitterController.DrawConnections();
    }
}
