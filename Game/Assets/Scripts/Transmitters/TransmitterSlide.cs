using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlideDirection { VERTICAL, HORIZONTAL }

public class TransmitterSlide : Transmitter {
    [Header("Slide Options")]
    public float slideTime = 1f;
    public SlideDirection slideDirection;
    public LayerMask islandMask;

    public override void HandleClick(int direction)
    {
        base.HandleClick(direction);
        Slide(direction);
    }

    private void Slide(int direction)
    {
        if (IsMoving)
            return;

        Vector3 d = slideDirection == SlideDirection.VERTICAL ? Vector3.forward : Vector3.right;
        if (direction < 0)
            d = -d;

        Ray ray = new Ray(transform.position, d);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, map.nodeSpacing, islandMask) == false &&
            map.GetNearestNode(transform.position + d * map.nodeSpacing) != map.GetNearestNode(transform.position))
        {
            StartCoroutine(Move(d * map.nodeSpacing));
        }
        else
            Slide(-direction);
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        float timer = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction;

        while (timer <= slideTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / slideTime);

            if (spark)
                spark.transform.position = Vector3.Lerp(startPosition, endPosition, timer / slideTime);

            yield return null;
        }

        transform.position = endPosition;

        isMoving = false;
        transmitterController.UpdateConnections();
    }
}
