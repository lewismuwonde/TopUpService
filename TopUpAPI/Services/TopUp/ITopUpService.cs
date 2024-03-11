using TopUpAPI.ViewModel;

namespace TopUpAPI.Services.TopUp
{
    public interface ITopUpService
    {
        Task<List<int>> GetTopUpOptions();
        Task<bool> TopUpBeneficiary(TopUpRequest request, int userId);
        Task<bool> IsBalanceSufficient(long userId, decimal amount);
    }
}
