using System;
using System.Collections.Generic;
using System.Threading;
using FlaxEngine;

namespace Game
{

    public interface ITrackCameraFraction
    {
        public float Fraction { get; }
    }


    /// <summary>
    /// TrackedCamera Script.
    /// </summary>
    /// 

    public class PathCamera : Script
    {
        public CameraSlot CameraSlot;
        public Spline SplinePath;
        public Actor Target;
        public float Speed = 1f;
        public float currentTime;

        private bool useExternFraction;


        public bool SkipPoints;
        private ITrackCameraFraction fraction;

        [Tooltip($"Tentará encontrar um {nameof(ITrackCameraFraction)} no ator {nameof(Target)}")]
        public bool UseExternFraction
        {
            set
            {
                useExternFraction = value;
                if(value)
                    fraction = Target.GetScript<ITrackCameraFraction>();
            }
            get => useExternFraction;
        }
        /// <inheritdoc/>
        public override void OnStart()
        {

            UseExternFraction = useExternFraction;
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }
        Vector3 currentPos;
        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if(fraction == null || UseExternFraction)
            {
                var time = SplinePath.GetSplineTimeClosestToPoint(Target.Position);



                if (SkipPoints)
                {
                    var transform = SplinePath.GetSplineTransform(time);
                    
                    currentPos = Vector3.Lerp(currentPos, transform.Translation, Speed * Time.DeltaTime);
                    CameraSlot.Actor.Position = currentPos;
                    
                }
                else
                {
                    currentTime = Mathf.Lerp(currentTime, time, Speed * Time.DeltaTime);
                    //var upperValue = (time);
                    //Debug.Log($"De {currentTime} para {upperValue}");
                    //currentTime = Mathf.CubicInterp(currentTime, -1f, upperValue, 1f, Speed * Time.DeltaTime);
                    var position = SplinePath.GetSplinePoint(currentTime);
                    CameraSlot.Actor.Position = position;
                }
              
                
                //currentTime += Time.DeltaTime * Speed;
                //var transform = Track.GetSplineTransform(currentTime);
                //CameraSlot.Actor.Position = transform.Translation;
            }
        }

    }
}
