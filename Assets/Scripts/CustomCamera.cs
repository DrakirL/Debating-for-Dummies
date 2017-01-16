using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour
{
    public float shakePwr;
    public float shakeDur;

    public Vector3[] positions;

    public float politicianSize;
    public float fullscreenSize;
    public float zoomDuration;

    //Changes the perspective to match whoever is talking
	IEnumerator ZoomCamera (int who, Quote citat)
    {
        float tempDur = 0;

        //Zoom to show player
        if (who != 2)
        {
            while (tempDur < zoomDuration)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, politicianSize, tempDur);
                Camera.main.transform.position = transform.position = Vector3.Lerp(transform.position, positions[who], tempDur);
                tempDur += 0.02f;

                yield return new WaitForSeconds(0.02f);
            }

            //Jump to the new position, just in case.
            transform.position = positions[who];
            Camera.main.orthographicSize = politicianSize;
        }

        //Zoom out to show the entire set
        else if (who == 2)
        {
            while (tempDur < zoomDuration)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fullscreenSize, tempDur);
                Camera.main.transform.position = transform.position = Vector3.Lerp(transform.position, positions[2], tempDur);
                tempDur += 0.02f;

                yield return new WaitForSeconds(0.02f);
            }

            //Jump to the new position.
            transform.position = positions[2];
            Camera.main.orthographicSize = fullscreenSize;
        }

        StartCoroutine(CameraEffect(who, citat));
	}
    
    IEnumerator CameraEffect(int who, Quote quote)
    {
        //Creates the shake effect if you picked it
        if (quote.effect == Quote.Effects.Shake)
        {
            yield return new WaitForSeconds(zoomDuration);

            //Save starting position, set the first shake position and prepare the timer
            Vector3 startPos = positions[who];
            Vector2 tempPos = Random.insideUnitCircle * shakePwr;
            float tempDur = shakeDur;

            //As long as there is time left, teleport to the shake position and set a new one
            while (tempDur > 0)
            {
                transform.position = new Vector3 (startPos.x + tempPos.x, startPos.y + tempPos.y, transform.position.z);
                tempDur -= 0.05f;
                yield return new WaitForSeconds(0.05f);
                tempPos = Random.insideUnitCircle * shakePwr;
            }

            //Return to the starting position
            transform.position = startPos;
        }
    }

    public void Zoom(int who, Quote citat)
    {
        StartCoroutine(ZoomCamera(who, citat));
    }
}