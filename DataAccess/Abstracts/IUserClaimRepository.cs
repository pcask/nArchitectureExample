using Core.Entities.Security;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IUserClaimRepository : IRepository<UserClaim>, IAsyncRepository<UserClaim>
{
}
