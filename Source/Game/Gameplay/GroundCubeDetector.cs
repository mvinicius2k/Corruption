using FlaxEditor.Gizmo;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Game;

public class GroundCubeDetector : Script, IGroundDetector
{
    public Vector3 HalfExtends;
    public EntityMovement EntityMovement;
    public float Distance;
    [Range(0f, 90f), ShowInEditor, Serialize]
    private float maxSlopeAngle = 30f;


    [ShowInEditor, ReadOnly]
    private Observable<bool> sliding = new (),
                              grounded = new ();
    [ShowInEditor, ReadOnly]
    private Observable<Collider> currentGround = new();



    public IObservable<Collider> CurrentGround => currentGround;
    public IObservable<bool> Grounded => grounded;

    public IObservable<bool> Sliding => sliding;

    private void OnEntityJumping(bool jumpingValue)
    {
        if (jumpingValue)
        {
            //Debug.Log("Entrando em pulo");
            grounded.Value = false;
        }
        else
        {
            //Debug.Log("Saindo do pulo");
        }
    }

    public override void OnEnable()
    {
        EntityMovement.Jumping.OnChange += OnEntityJumping;
    }
    public override void OnDisable()
    {
        EntityMovement.Jumping.OnChange -= OnEntityJumping;
    }

    public override void OnUpdate()
    {
        if (Physics.BoxCast(Actor.Position, HalfExtends, Transform.Down, out var hit, Actor.Orientation, Distance, ((uint)LayerEnum.World)))
        {

            grounded.Value = true;
            currentGround.Value = (Collider)hit.Collider;
            //if(Vector3.Angle(hit.Normal, Vector3.Up) > 90f - maxSlopeAngle)
            //{
            //    Sliding = true;
            //}
            //else
            //{
            //    Sliding = false;
            //}
        }
        else
        {
            grounded.Value = false;
            currentGround.Value = null;
        }
    }



    public override void OnDebugDrawSelected()
    {
        DebugDraw.DrawLine(Actor.Position, Actor.Position + Transform.Down * Distance, Color.Aqua);
        DebugDraw.DrawWireBox(new OrientedBoundingBox
        {
            Extents = HalfExtends,
            Transformation = Actor.Transform
        }, Color.Blue);

        var end = Actor.Transform;
        end.Translation += Transform.Down * Distance;

        DebugDraw.DrawWireBox(new OrientedBoundingBox
        {
            Extents = HalfExtends,
            Transformation = end
        }, Color.IndianRed);

    }
}



