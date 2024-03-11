using Microsoft.AspNetCore.Cors.Infrastructure;
using TopUpAPI.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using TopUpDB.Entity;
using TopUpDB.Interface;

namespace TopUpAPI.Services.Beneficiaries
{
    public class BeneficiariesService : IBeneficiariesService
    {
        private readonly IBeneficiary _ibeneficiary;

        public BeneficiariesService(IBeneficiary ibeneficiary)
        {
            _ibeneficiary = ibeneficiary;
        }

        public async Task<List<BeneficiaryVM>> GetActiveBeneficiaries(int userId)
        {
            var response = new List<BeneficiaryVM>();
            var beneficiaries = await _ibeneficiary.GetActiveBeneficiaries(userId);
            if (beneficiaries != null && beneficiaries.Any())
            {

                foreach (var item in beneficiaries)
                {
                    var beneficiary = new BeneficiaryVM();
                    beneficiary.NickName = item.NickName;
                    beneficiary.PhoneNumber = item.PhoneNumber;
                    beneficiary.Id = item.Id;
                    response.Add(beneficiary);
                }
            }
            return response;
        }


        public async Task<AddBeneficiaryModel> AddBeneficiary(AddBeneficiaryModel request, int userId)
        {
            var beneficiary = new TopUpBeneficiary
            {
                UserId = userId,
                NickName = request.NickName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                CreateDate = DateTime.UtcNow,
            };
            var addBeneficiary = await _ibeneficiary.AddBeneficiary(beneficiary);
            return new AddBeneficiaryModel
            {
                PhoneNumber = addBeneficiary.PhoneNumber,
                NickName = addBeneficiary.NickName,
            };
        }

        public async Task<bool> ValidateActiveBeneficiariesCount(int userId)
        {
            var beneficiariesCount = await _ibeneficiary.GetActiveBeneficiariesCount(userId);
            if (beneficiariesCount > 4)
                return false;

            return true;
        }

        public async Task<TopUpBeneficiary> GetBeneficiaryById(int beneficiaryId)
        {
            return await _ibeneficiary.GetBeneficiaryById(beneficiaryId);
        }
    }
}
