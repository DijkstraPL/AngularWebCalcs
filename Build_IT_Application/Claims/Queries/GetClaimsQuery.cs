using AutoMapper;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_WebApplication.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Claims.Queries
{
    [Authorize(Roles = "Administrator,Designer")]
    public class GetClaimsQuery : IRequest<List<ClaimResource>>
    {
    }

    public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, List<ClaimResource>>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;

        public GetClaimsQueryHandler(IClaimRepository claimRepository, IMapper mapper)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ClaimResource>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetAll().ToListAsync(cancellationToken);
            var claimsResults = _mapper.Map<List<ClaimResource>>(claims);
            return claimsResults;
        }
    }

}
