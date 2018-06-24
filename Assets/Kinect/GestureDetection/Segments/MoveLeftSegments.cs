using UnityEngine;
using Windows.Kinect;

public class MoveLeftSegment1 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // left hand in front of left shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);

        if (handLeft.z > elbowLeft.z && handRight.y < shoulderCenter.y)
        {
            // left hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);
            Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);

            if (handLeft.y < head.y && handLeft.y > hipCenter.y)
            {
                // left hand right of left shoulder
                if (handLeft.x > shoulderLeft.x)
                {
                    //Debug.Log("Segment1 Success");
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }
}
public class MoveLeftSegment2 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // left hand in front of left shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);

        if (handLeft.z > elbowLeft.z && handRight.y < shoulderCenter.y)
        {
            // left hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);

            if (handLeft.y < head.y && handLeft.y >= shoulderCenter.y - 0.2f)
            {
                // left hand right of left shoulder
                if (handLeft.x + 0.4f <= shoulderLeft.x)
                {
                    //Debug.Log("Segment1 Success");
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }
}
