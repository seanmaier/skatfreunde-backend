namespace skat_back.calculations.interfaces;

public interface ICalculationStrategy
{
    Task CalculateAsync(AppDbContext context);
}