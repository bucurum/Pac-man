using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable() 
    {
        StopAllCoroutines();
    }

    private void OnDisable() 
    {
        StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rb2d.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = .5f;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsedTime / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;  //wait a frame.
        }

        elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsedTime / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;  //wait a frame.
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        this.ghost.movement.rb2d.isKinematic = false;
        this.ghost.movement.enabled = true;
        this.ghost.scatter.Enable();
    }
}
