using AppShared.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppShared.Dto
{
    public class UserProfileInfoDto : UserEditProfileDto
    {
        [Display(Name = "Роли")]
        public IEnumerable<string> RolesDisplay { get; set; }

        [Display(Name = "Количество заявок")]
        public int ZayavkaCount = 0;
        [Display(Name = "Количество комментариев")]
        public int CommentCount = 0;

        //public IEnumerable<StatusChangeHistory> StatusChanges { get; set; }
        //public IEnumerable<ZayvkaSummaryDto> ZayvkaList { get; set; }

        public UserProfileInfoDto()
        {

        }

        public UserProfileInfoDto(User user, IEnumerable<string> rolesDisplay,int zayvka, int comments) : base(user)
        {
            ZayavkaCount = zayvka;
            CommentCount = comments;
            //StatusChanges = statusChanges;
            //ZayvkaList = zayvkaList;
            RolesDisplay = rolesDisplay;

        }
    }
}
