using Abp.Application.Services.Dto;

namespace DoAspnetboilerplateLdap.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

