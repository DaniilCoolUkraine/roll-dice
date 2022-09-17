public interface IDamageable
{
   void TakeDamage(int amount);
   void Damage(int amount, IDamageable unit);
}
