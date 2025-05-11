using skat_back.data;

namespace skat_back.calculations.interfaces;

public interface ICalculationStrategy
{
    Task CalculateAsync(AppDbContext context);
}