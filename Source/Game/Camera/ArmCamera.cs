

using System;
using System.Collections.Generic;
using System.Linq;
using FlaxEngine;
using FlaxEngine.Assertions;

namespace Game;

/// <summary>
/// Câmera clássica em terceira pessoa olhando para o alvo. 
/// A câmera em si precisa ser um filho deste objeto, caso não haja, uma será criada automaticamente
/// </summary>
public class CameraArm : Script, ICameraSlot
{

    public Actor Target;
    
    public float Speed = 500f;
    public float CameraRadius = 200f;
    public bool DisableInput;

    public float MinAngle = 10f;
    public float MaxAngle = 170f;

    /// <summary>
    /// Distância inicial entre a câmera e o alvo
    /// </summary>
    private float distance;
    
    private Vector3 targetLastPosition;
    [Header("Info")]
    [ShowInEditor, ReadOnly]
    private float dotFraction = 1f;
    [ShowInEditor, ReadOnly]
    private Vector2 mouse;
    [ShowInEditor, ReadOnly]
    private Vector3 targetPositionDiff;
    /// <summary>
    /// Câmera alvo
    /// </summary>
    [ShowInEditor, ReadOnly]
    private Camera camera;

    [HideInEditor]
    public Camera Camera
    {
        get => camera;
        set
        {
            camera = value;
            distance = Vector3.Distance(camera.Position, Target.Position);
        }
    }

    public Vector3 CameraDirection => (Actor.Position - Target.Position).Normalized;

    public bool IsInvalidAngle => Mathf.IsNotInRange(Vector3.Angle(Target.Transform.Up, CameraDirection), MinAngle, MaxAngle);

    
    [EditorAction]
    public void Setup()
    {
        camera ??= Actor.GetChild<Camera>();
        if (camera == null)
        {
            camera = new Camera();
            camera.Parent = this.Actor;
        }
    }

    /// <inheritdoc/>
    public override void OnStart()
    {
        distance = Vector3.Distance(Actor.Position, Target.Position);
        targetLastPosition = Target.Position;

        Setup();
        
        
    }


    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        mouse = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));

        //Última posição antes de se movimentar, usado em caso de entrar em posição inválida
        var lastPosition = Actor.Position;

        //Rotação angular
        Actor.RotateAround(Target.Position, Actor.Transform.Up, mouse.X * Speed);
        Actor.RotateAround(Target.Position, Actor.Transform.Right, mouse.Y * Speed * dotFraction); //dotFraction suaviza movimento em superfícies íngrimes em relação ao àngulo da camera


        if (IsInvalidAngle)
        {
            Actor.Position = lastPosition;

        }

        //Acompanha o deslocamento do alvo
        targetPositionDiff = Target.Position - targetLastPosition;
        Actor.AddMovement(targetPositionDiff);

        //Para nao entrar dentro da parede
        var cameraDirection = CameraDirection;
        if (Physics.SphereCast(Target.Position, CameraRadius, cameraDirection, out var hit, distance, ((uint)LayerEnum.World)))
        {

            //Posicionando logo acima de acordo com a superfície
            Actor.Position = hit.Point + hit.Normal * CameraRadius;
            dotFraction = Mathf.Abs(Vector3.Dot(cameraDirection, hit.Normal));

        }
        else
            dotFraction = 1f;

        Actor.LookAt(Target.Position, Vector3.Up);

        targetLastPosition = Target.Position;
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


    }
}
