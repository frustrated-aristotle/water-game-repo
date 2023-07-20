
public static class TargetManager
{
    private static int armoryToTargetDistance = 30;
    private static int stepDistance = 15;
    public static int GiveHealth(int stepValue)
    {
        int health = (stepValue == 1) ? 20 : (stepValue - 1) * 50;        
        return health;
    }

    public static int StepValue(int zPosTarget, int zPosArmory)
    {
        // stepValue = (int)((int)(posZValue - baseZ-30) / InspectorFunc.Instance.stepDistance + 1);
        int stepValue;
        stepValue = (zPosTarget - zPosArmory - armoryToTargetDistance) / stepDistance + 1;
        return stepValue;
    }
}
