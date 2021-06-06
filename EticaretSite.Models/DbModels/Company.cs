using System;
using System.ComponentModel.DataAnnotations;

namespace EticaretSite.Models.DbModels
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostaCode { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsAuthorizedCompany { get; set; }


    }
}
