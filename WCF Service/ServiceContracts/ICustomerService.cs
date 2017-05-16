using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCF_Service.DataTransferObjects;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        int createCustomer(string Username);

        [OperationContract]
        int getCustIDByUsername(string username);

        [OperationContract]
        List<AddressDTO> getAddressesByCustID(int customerID);

        [OperationContract]
        int createAddress(AddressDTO newAddress, int customerID);
    }
}
