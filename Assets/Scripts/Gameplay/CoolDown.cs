public class CoolDown
{
    public float coolDownTime;
    public float coolDownTimer;
    public bool isCoolingDown;
    
    //constructor
    public CoolDown()
    {
        coolDownTime = 0;
        coolDownTimer = 0;
        isCoolingDown = false;
    }
    
    public void SetCoolDown(float cd)
    {
        this.coolDownTimer = cd;
        coolDownTime = cd;
        isCoolingDown = true;
    }

    public void Update(float deltaTime)
    {
        if (isCoolingDown)
        {
            coolDownTime -= deltaTime;
            if (coolDownTime <= 0)
            {
                isCoolingDown = false;
                coolDownTime = 0;
            }
        }
    }
}
