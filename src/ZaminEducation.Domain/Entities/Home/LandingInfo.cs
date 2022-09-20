using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaminEducation.Domain.Entities.Home
{
    public class LandingInfo
    {
        public LandingInfo(string title, string mainVideoUrl, string aboutZaminTitle, string whyZaminTitle, WhyZamin whyZaminValues, string supportTeamTitle, SocialNetwork socialNetwork) 
        {
            this.Title = title;
            this.MainVideoUrl = mainVideoUrl;
            this.AboutZaminTitle = aboutZaminTitle;
            this.WhyZaminTitle = whyZaminTitle;
            this.WhyZaminValues = whyZaminValues;
            this.SupportTeamTitle = supportTeamTitle;
            this.SocialNetwork = socialNetwork;
   
        }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainVideoUrl { get; set; }
        public string MainImageUrl { get; set; }
        
        // Section 2
        public string AboutZaminTitle { get; set; }
        public string AboutZaminDescription { get; set; }

        // Section 3
        public string WhyZaminTitle { get; set; }
        public string WhyZaminDescription { get; set; }
        public WhyZamin WhyZaminValues { get; set; }

        // Section 4
        // null

        // Section 5
        public string SupportTeamTitle { get; set; }
        public string SupportTeamDescription { get; set; }

        // Section 5
        public IEnumerable<string> SupportTeamLogoUrls { get; set; }

        // Section 6
        public SocialNetwork SocialNetwork { get; set; }
    }
}