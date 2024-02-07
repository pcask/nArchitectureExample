using Core.Entities.Security;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IClaimRepository : IRepository<Claim>, IAsyncRepository<Claim>
{
}
