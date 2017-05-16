using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WCF_Service.Models;

/// <summary>
/// Order Data Transfer Object
/// This class is used to wrap Order objects for use with the Order Service.
/// </summary>
namespace WCF_Service.DataTransferObjects
{
    [DataContract]
    [KnownType(typeof(OrderDetailItem))]
    public class OrderDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int CustomerID { get; set; }

        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public bool Complete { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public List<OrderDetailItem> products { get; set; }
    }
}
