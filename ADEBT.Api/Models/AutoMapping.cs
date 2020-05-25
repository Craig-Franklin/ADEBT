using ADEBT.Api.Models.Entities;

namespace ADEBT.Api.Models
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<Debt, DebtDto>();
        }
    }
}
