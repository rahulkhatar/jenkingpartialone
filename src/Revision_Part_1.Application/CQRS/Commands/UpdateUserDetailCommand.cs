using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Revision_Part_1.Application.Interfaces;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;

namespace Revision_Part_1.Application.CQRS.Commands;

public class UpdateUserDetailCommand : IRequest
{
    public DateOnly? DOB { get; set; }
    public string? Nationality { get; set; }
}

public class UpdateUserDetailCommandHandler(ILogger<UpdateUserDetailCommandHandler> _logger, IUserContext userContext, IUserStore<Domain.Entities.User> userStore) : IRequestHandler<UpdateUserDetailCommand>
{
    public async Task Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrent();
        
        _logger.LogInformation("Updating user with Id: {UserId} and {@Request}", user!.Id, request);
        
        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken);

        if(dbUser == null)
            throw new NotFoundException(nameof(Domain.Entities.User), user.Id);

        dbUser.DateOfBirth = request.DOB;
        dbUser.Nationality = request.Nationality!;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}