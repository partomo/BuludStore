// using AutoMapper;
// using Bazta.Application.Interfaces;
// using Bazta.Identity.Application.Commands.Users;
// using Bazta.Identity.Application.DTOs;
// using Bazta.Identity.Domain.Entities;
// using Bulud.Base.Exceptions;
// using MediatR;
// using Microsoft.AspNetCore.Identity;
//
// namespace Bazta.Identity.Application.Handlers.Users;
//
// public class EditUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, ICurrentUserContext currentUser)
//     : IRequestHandler<EditUserCommand, UserDto>
// {
//     public async Task<UserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
//     {
//         if (currentUser.IsCountyAdmin())
//         {
//             if (request.CountyId != currentUser.CountyId && request.ProvinceId != currentUser.ProvinceId)
//                 throw new ForbiddenException("شما فقط می توانید برای شهرستان خود کاربر تعریف کنید.");
//         }
//         else if (currentUser.IsProvinceAdmin())
//         {
//             if (request.ProvinceId != currentUser.ProvinceId)
//                 throw new ForbiddenException("شما فقط می توانید برای استان خود کاربر تعریف کنید.");
//         }
//         
//         var user = await userManager.FindByIdAsync(request.Id);
//         if (user == null)
//         {
//             throw new NotFoundException("کاربر مورد نظر در سامانه یافت نشد.");
//         }
//         
//         var roles = await userManager.GetRolesAsync(user);
//         await userManager.RemoveFromRolesAsync(user, roles);
//         await userManager.AddToRolesAsync(user, new List<string> { request.Role });
//         
//
//         mapper.Map(request, user);
//         await userManager.UpdateAsync(user);
//
//         return mapper.Map<UserDto>(user);
//     }
// }