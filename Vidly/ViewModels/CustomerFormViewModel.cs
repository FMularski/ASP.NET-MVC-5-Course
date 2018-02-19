using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public CustomerFormViewModel()
        {
            Id = 0;
        }

        public CustomerFormViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            BirthDate = customer.BirthDate;
            MembershipTypeId = customer.MembershipTypeId;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
        }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Customer's name is required.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Required(ErrorMessage = "Memberhip Type is required.")]
        public byte? MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public string HeaderString
        {
            get
            {
                if (Id != 0)
                    return "Edit Customer";

                return "New Customer";
            }
        }

        public Customer CustomerBasedOnViewModel
        {
            get
            {
                return new Customer
                {
                    Name = this.Name,
                    BirthDate = this.BirthDate,
                    MembershipTypeId = this.MembershipTypeId.Value,
                    IsSubscribedToNewsletter = this.IsSubscribedToNewsletter
                };
            }
        }
    }
}