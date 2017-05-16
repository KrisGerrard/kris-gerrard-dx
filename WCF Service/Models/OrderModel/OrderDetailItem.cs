using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCF_Service.Models
{
    [DataContract]
    public class OrderDetailItem
    {

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        public OrderDetailItem(int ProductID, int Quantity)
        {
            this.ProductID = ProductID;
            this.Quantity = Quantity;
        }
    }
}