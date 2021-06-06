using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EticaretSite.Models.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        // id vermemize gerek yok IdentityUser(from metadata) dan geliyor
        [Required]
        public string Name { get; set; }

        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostaCode { get; set; }
        

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company{get; set;}


        [NotMapped]
        public string Role { get; set; }

    }
}
