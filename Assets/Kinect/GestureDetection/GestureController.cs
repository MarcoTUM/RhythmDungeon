﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Windows.Kinect;

public class GestureController : MonoBehaviour
{
    public BasicAvatarModel AvatarModel;
    
           
    /// <summary>
    /// The list of all gestures we are currently looking for
    /// </summary>
    private List<Gesture> gestures = new List<Gesture>();

    /// <summary>
    /// Initializes a new instance of the <see cref="GestureController"/> class.
    /// </summary>
    void Start()
    {
            
       
    }

    /// <summary>
    /// Occurs when [gesture recognised].
    /// This is the second EventHandler we use.
    /// </summary>
    public event EventHandler<GestureEventArgs> GestureRecognizedInController;

    void Update()
    {
        UpdateAllGestures(AvatarModel);
    }

    /// <summary>
    /// Updates all gestures.
    /// </summary>
    /// <param name="data">The skeleton data.</param>
    public void UpdateAllGestures(BasicAvatarModel data)
    {
        foreach (Gesture gesture in this.gestures)
        {
            gesture.UpdateGesture(data);
        }
    }

    /// <summary>
    /// Adds the gesture.
    /// </summary>
    /// <param name="name">The gesture type.</param>
    /// <param name="gestureDefinition">The gesture definition.</param>
    public void AddGesture(string name, IRelativeGestureSegment[] gestureDefinition)
    {
        Gesture gesture = new Gesture(name, gestureDefinition);
        // Associate our eventHandler with a method.
        gesture.GestureRecognizedInGesture += OnGestureRecognized;
        this.gestures.Add(gesture);
        
    }

    public void RemoveGesture(string name)
    {
        foreach (Gesture gesture in this.gestures)
        {
            if (gesture.name == name)
            {
                gestures.Remove(gesture);
            }
        }       
    }

    /// <summary>
    /// Handles the GestureRecognized event of the g control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="KinectSkeltonTracker.GestureEventArgs"/> instance containing the event data.</param>
    private void OnGestureRecognized(object sender, GestureEventArgs e)
    {
        if (this.GestureRecognizedInController != null)
        {
            // fire the second event
            this.GestureRecognizedInController(this, e);
        }

        foreach (Gesture g in this.gestures)
        {
            g.Reset();
        }
    }



    public bool detectPlayer()
    {
        return AvatarModel.detectPlayer();
    }

    public bool getRightHandState() //returns true if right Hand is closed
    {
        bool temp = AvatarModel.getRightHandState() == HandState.Closed;
        return temp;
    }

     public Vector2 getMappedRightHandPosition()
    {
        Vector3 spineShoulder = AvatarModel.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRightRel = AvatarModel.getRawWorldPosition(JointType.HandRight) - spineShoulder;
        Vector3 handLeftRel = AvatarModel.getRawWorldPosition(JointType.HandLeft) - spineShoulder;
        Vector2 result;
        result.x = (handRightRel.x - 0.1f)/0.6f;
        result.y = (handRightRel.y + 0.2f)/0.4f;
        return result;
    }
}
