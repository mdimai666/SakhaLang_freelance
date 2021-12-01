using AppShared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AppShared.Dto
{
    public class EditRolesViewModelDto
    {
        public List<Role> Roles { get; set; }
        //public List<Claim> Claims { get; set; }

        public IEnumerable<RoleClaimsDto> RoleClaims { get; set; }
    }

    public class ClaimShortDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SlugName { get; set; }
        public string Title { get; set; }

        public bool IsCheck { get; set; }
    }

    public class ModelClasimsDto
    {
        public string ModelTypeFillName { get; set; }
        public string Model { get; set; }
        public IEnumerable<ClaimShortDto> Claims { get; set; }
    }

    public class RoleShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleShortDto()
        {

        }
        public RoleShortDto(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }

    public class RoleClaimsDto
    {
        public RoleShortDto Role { get; set; }
        public IEnumerable<ModelClasimsDto> Models { get; set; }
    }
}
