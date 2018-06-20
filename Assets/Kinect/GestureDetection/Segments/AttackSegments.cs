using UnityEngine;
using Windows.Kinect;

public class AttackSegment1 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.SpineMid);

        // left and right hand above head
        if (handLeft.y <= spine.y && handRight.y > head.y)
        {
            //Debug.Log("Segment1 Success");
            return GesturePartResult.Succeed;
        }
        return GesturePartResult.Pausing;
    }
}
public class AttackSegment2 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
        Vector3 spine = skeleton.getRawWorldPosition(JointType.Head);

        // left and right hand above head
        if (handLeft.y <= spine.y && handRight.y <= spine.y)
        {
            //Debug.Log("Segment1 Success");
            return GesturePartResult.Succeed;
        }
        return GesturePartResult.Pausing;
    }
}
