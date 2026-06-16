using UnityEngine;

public class MovingPlatformParams : PlatformParams
{
    public IntVector3 EndGridPos;

    [SerializeField]
    private float MoveSpeed = 5f;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool movingToEnd = true;

    protected override void Start()
    {
        base.Start();

        startPos = GridToWorld(GridPos);
        endPos = GridToWorld(EndGridPos);
    }

    protected override void Update()
    {
        base.Update();

        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 target = movingToEnd
            ? endPos
            : startPos;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }
    }
}