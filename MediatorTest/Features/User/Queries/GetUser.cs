using System.Collections.Generic;
using MediatorTest.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MediatorTest.Features.User.Queries
{
    public class GetUser
    {
        public class Query : IRequest<List<UserViewModels>>
        {
            public string? Email { get; set; }
            public string? Username { get; set; }
        }

        public class UserViewModels
        {
            public string? Email { get; set; }
            public string? Username { get; set; }
            public Guid? Id { get; set; }
            public string? Phone { get; set; }
        }

        public class QueryHandler(MyDbContext context) : IRequestHandler<Query, List<UserViewModels>>
        {
            private readonly MyDbContext _context = context;
            public async Task<List<UserViewModels>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Users.AsQueryable();

                if (request.Email is not null)
                {
                    query = query.Where(x => x.Email.Contains(request.Email));
                }
                if (request.Username is not null)
                {
                    query = query.Where(x => x.Username.Contains(request.Username));
                }

                var users = await query.Select(
                    x => new UserViewModels
                    {
                        Email = x.Email,
                        Username = x.Username,
                        Id = x.Id,
                        Phone = x.Phone
                    }
                ).ToListAsync(cancellationToken);

                return users;
            }
        }

    }
}