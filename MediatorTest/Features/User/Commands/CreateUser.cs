using MediatorTest.Models;
using MediatR;

namespace MediatorTest.Features.User.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<Command>
        {
            public string? Username { get; set; } = null!;
            public string? Email { get; set; } = null!;
            public string? FirstName { get; set; } = null!;
            public string? LastName { get; set; } = null!;
            public string? Password { get; set; } = null!;
            public string? Phone { get; set; } = null!;
        }

        public class Handler(MyDbContext context) : IRequestHandler<Command, Command>
        {
            private readonly MyDbContext _context = context;
            public async Task<Command> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = new Models.User
                    {
                        Username = request.Username,
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Password = request.Password,
                        Phone = request.Phone
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return await Task.FromResult(request);
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error creating user");
                    throw;
                }
            }
        }
    }
}