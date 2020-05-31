using System.ComponentModel.DataAnnotations;

namespace DoAspnetboilerplateLdap.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}