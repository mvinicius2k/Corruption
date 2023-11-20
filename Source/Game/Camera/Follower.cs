using FlaxEditor.Gizmo;
using FlaxEngine;
using FlaxEngine.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;


public class Follower : Script
{
    public Actor Target, LookTo;

    public float Speed = 500f;
    public Vector3 Angle;
    public float DistanceFromTarget;
    public float AproximationSpeed = 1000f;
    public float DistanceMargin = 200f;
    public float CameraRadius = 200f;


    private Vector3 lastPosition;
    private Collider wall;
    private Vector3 surfaceDirection; //Direção entre a câmera e o alvo projetada na superfície
    private Vector3 closestPoint;


    /// <summary>
    /// Move a câmera em uma unidade de acordo com a superfície. O comportamente é de como se a câmera deslizasse sobre ela.
    /// </summary>
    /// <param name="surfaceNormal"></param>
    /// <param name="movementDirection"></param>
    /// <param name="closestPoint"></param>
    /// <param name="speed"></param>
    private void MoveSlidingByWall(Vector3 surfaceNormal, Vector3 movementDirection, Vector3 closestPoint, float speed)
    {

        surfaceDirection = Vector3.ProjectOnPlane(Target.Position - Actor.Position, surfaceNormal).Normalized;


        Actor.Position = closestPoint + surfaceNormal * CameraRadius;

        if (movementDirection == Vector3.Zero)
            return;
        var dot = Vector3.Dot(movementDirection, surfaceNormal);
        if (dot < 0f)
        {
            Actor.AddMovement(surfaceDirection * speed * Mathf.Abs(dot));
            Debug.Log(dot);
        }

    }

    public override void OnStart()
    {
        lastPosition = Actor.Position;
    }

    public override void OnUpdate()
    {
        
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;


        var mouseInput = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY)); //Suavizar entrada da camera 
        var normalizedSpeed = Speed * Time.DeltaTime;
        var normalizedAproximationSpeed = AproximationSpeed * Time.DeltaTime;

        lastPosition = Actor.Position;
        //Movimento livre
        Actor.RotateAround(Target.Position, Transform.Up, mouseInput.X * normalizedSpeed);
        Actor.RotateAround(Target.Position, Transform.Right, mouseInput.Y * normalizedSpeed);


        var movementDirection = (Actor.Position - lastPosition).Normalized; //Direção de acordo com a útilma posição 

        //Testando colisões. 
        //Linecast verificará se a câmera "pulou" um obstáculo no movimento rápido
        //Overlap verificará se uma colisão está acontecendo com o raio da câmera
        if (Physics.LineCast(lastPosition, Actor.Position, out var castHits, ((uint)LayerEnum.World)))
        {
            wall = (Collider)castHits.Collider;
            wall.ClosestPoint(lastPosition, out closestPoint);
            MoveSlidingByWall(castHits.Normal, movementDirection, closestPoint, normalizedAproximationSpeed * 0.5f);
        }
        else if (Physics.OverlapSphere(Actor.Position, CameraRadius, out Collider[] hits, ((uint)LayerEnum.World)))
        {

            //Obtendo collider mais próximo
            var nearestDistance = float.MaxValue;
            foreach (Collider collider in hits)
            {
                collider.ClosestPoint(Actor.Position, out var closest);
                var pointDistance = Vector3.Distance(Actor.Position, closest);
                if (pointDistance < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(Actor.Position, closestPoint);
                    closestPoint = closest;
                    wall = collider;

                }
            }

            //Obtendo normal da superfície
            var rayDirection = (closestPoint - Actor.Position).Normalized;
            if (Physics.RayCast(Actor.Position, rayDirection, out var hitInfo, layerMask: ((uint)LayerEnum.World)))
            {
                MoveSlidingByWall(hitInfo.Normal, movementDirection, closestPoint, normalizedAproximationSpeed * 0.5f);
            }
        }
        else
        {
            wall = null;
            surfaceDirection = Vector3.Zero;
        }

        if (LookTo != null)
            Actor.LookAt(LookTo.Position, Vector3.Up);

        //Corrigindo distanciamento
        var distance = Vector3.Distance(Actor.Position, Target.Position);
        if (!Mathf.WithinEpsilon(distance, DistanceFromTarget, DistanceMargin))
        {
            //if (distance < DistanceFromTarget)
            //    Actor.AddMovement(Actor.Transform.Backward * normalizedAproximationSpeed);

            //else
            //    Actor.AddMovement(Actor.Transform.Forward * normalizedAproximationSpeed);
        }




    }



    public override void OnDebugDrawSelected()
    {
        DebugDraw.DrawWireSphere(new BoundingSphere
        {
            Center = Actor.Position,
            Radius = CameraRadius
        }, Color.Orange);

        if (Target == null)
            return;

        if (wall != null)
        {
            DebugDraw.DrawSphere(new BoundingSphere
            {
                Center = closestPoint,
                Radius = 10f,
            }, Color.BlueViolet);

            DebugDraw.DrawRay(closestPoint, surfaceDirection, Color.Blue);
        }

        DebugDraw.DrawWireSphere(new BoundingSphere
        {
            Center = Target.Position,
            Radius = DistanceFromTarget
        }, Color.Red);

    }


}
