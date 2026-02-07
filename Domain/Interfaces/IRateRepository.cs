using Domain.Models;

namespace Domain.Interfaces;

public interface IRateRepository
{
    RateConfig GetCurrentRates(DateTime forDate);
}
