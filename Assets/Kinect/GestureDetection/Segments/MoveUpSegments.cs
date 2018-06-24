using UnityEngine;
using Windows.Kinect;

public class MoveUpSegment1 : IRelativeGestureSegment
{

    /// <summary>
    /// Checks the gesture.
    /// </summary>
    /// <param name="skeleton">The skeleton.</param>
    /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // left and right hand in front of left and right shoulder
        Vector3 handLeft = skeleton.getRawWorldPosition(JointType.HandLeft);
        Vector3 shoulderCenter = skeleton.getRawWorldPosition(JointType.SpineShoulder);
        Vector3 handRight = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 elbowLeft = skeleton.getRawWorldPosition(JointType.ElbowLeft);
        Vector3 elbowRight = skeleton.getRawWorldPosition(JointType.ElbowRight);

        Vector3 shoulderRight = skeleton.getRawWorldPosition(JointType.ShoulderRight);
        Vector3 shoulderLeft = skeleton.getRawWorldPosition(JointType.ShoulderLeft);

        if (handLeft.x > shoulderLeft.x && handRight.x < shoulderRight.x)
        {
            // left and right hand below shoulder height but above hip height
            Vector3 head = skeleton.getRawWorldPosition(JointType.Head);
            Vector3 hipCenter = skeleton.getRawWorldPosition(JointType.SpineBase);
            Vector3 spineMid = skeleton.getRawWorldPosition(JointType.SpineMid);

            if ((handLeft.y < head.y && handLeft.y > spineMid.y) && (handRight.y < head.y && handRight.y > spineMid.y))
            {
                //Debug.Log("Segment1 Success");
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }
}
public class MoveUpSegment2 : IRelativeGestureSegment
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

        // left and right hand above head
        if (handLeft.y >= head.y && handRight.y >= head.y)
        {
            //Debug.Log("Segment1 Success");
            return GesturePartResult.Succeed;
        }
        return GesturePartResult.Pausing;
    }
}
