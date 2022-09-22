using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Commons;

namespace ZaminEducation.Domain.Entities.Users
{
    public class UserSocialNetwork : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }

        [DataType(DataType.Url)]
        public string TelegramLink { get; set; }

        [DataType(DataType.Url)]
        public string InstagramLink { get; set; }

        [DataType(DataType.Url)]
        public string FacebookLink { get; set; }

        [DataType(DataType.Url)]
        public string LinkedInLink { get; set; }

        [DataType(DataType.Url)]
        public string GithubLink { get; set; }

        [DataType(DataType.Url)]
        public string YoutubeLink { get; set; }
    }
}