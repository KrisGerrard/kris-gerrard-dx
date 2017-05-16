using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Address Data Transfer Object
/// This class is used to wrap Address objects for user with the Customer Service.
/// Constructor uses existing Address object to create equivilant AddressDTO.
/// </summary>
namespace WCF
{
    [DataContract]
    public class AddressDTO
    {
        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public int AddressType { get; set; }

        [DataMember]
        public string AddressTypeName { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string Suburb { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        public AddressDTO(Address address)
        {
            this.AddressID = address.AddressID;
            this.AddressType = address.AddressType;
            this.AddressTypeName = address.AddressType1.Type.ToString();
            this.Street = address.Street;
            this.Suburb = address.Suburb;
            this.City = address.City;
            this.PostalCode = address.PostalCode;
            this.Country = address.Country;
        }
    }
}