public class DamageEffect
{
    private Unit _invoker;
    private int _skillTemplate;
    public DamageEffect(Unit invoker, int skillTemplate)
    {
        _invoker = invoker;
        _skillTemplate = skillTemplate;
    }
    public Damage CalculateDamage(Unit target)
    {
        return new Damage();
    }
}