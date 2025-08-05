using AutoMapper;
using Core.Data.DTO;
using Core.Data.Entities;

namespace Core.Utils
{
    public class MappingRegister
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Role, RoleVM>().ReverseMap();
                config.CreateMap<User, UserVM>().ReverseMap();
                config.CreateMap<Permission, PermissionVM>().ReverseMap();
                config.CreateMap<Branch, BranchVM>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
