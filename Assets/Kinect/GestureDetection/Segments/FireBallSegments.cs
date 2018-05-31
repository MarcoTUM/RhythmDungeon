using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class FireBallSegment1 : IRelativeGestureSegment {

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // right hand between spinemid and head
        Vector3 rightHand = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 spineMid = skeleton.getRawWorldPosition(JointType.SpineMid);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        if (rightHand.y > spineMid.y && rightHand.y < head.y)
        {
            //right hand left from spine
            if (rightHand.x < spineMid.x)
            {
                //right elbow under spinemid
                Vector3 rightElbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
                if (rightElbow.y < spineMid.y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
        return GesturePartResult.Fail;
    }

}


public class FireBallSegment2 : IRelativeGestureSegment
{

    public GesturePartResult CheckGesture(BasicAvatarModel skeleton)
    {
        // right hand between spinemid and head
        Vector3 rightHand = skeleton.getRawWorldPosition(JointType.HandRight);
        Vector3 spineMid = skeleton.getRawWorldPosition(JointType.SpineMid);
        Vector3 head = skeleton.getRawWorldPosition(JointType.Head);

        if (rightHand.y > spineMid.y && rightHand.y < head.y)
        {
            //right hand right from spineMid
            if (rightHand.x > spineMid.x)
            {
                //rightElbow above spineMid
                Vector3 rightElbow = skeleton.getRawWorldPosition(JointType.ElbowRight);
                if (rightElbow.y > spineMid.y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Pausing;
        }
        return GesturePartResult.Fail;
    }

}