﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTest___WCF.CustomerService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AddressDTO", Namespace="http://schemas.datacontract.org/2004/07/WCF")]
    [System.SerializableAttribute()]
    public partial class AddressDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AddressIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AddressTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressTypeNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostalCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SuburbField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AddressID {
            get {
                return this.AddressIDField;
            }
            set {
                if ((this.AddressIDField.Equals(value) != true)) {
                    this.AddressIDField = value;
                    this.RaisePropertyChanged("AddressID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AddressType {
            get {
                return this.AddressTypeField;
            }
            set {
                if ((this.AddressTypeField.Equals(value) != true)) {
                    this.AddressTypeField = value;
                    this.RaisePropertyChanged("AddressType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AddressTypeName {
            get {
                return this.AddressTypeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressTypeNameField, value) != true)) {
                    this.AddressTypeNameField = value;
                    this.RaisePropertyChanged("AddressTypeName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode {
            get {
                return this.PostalCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostalCodeField, value) != true)) {
                    this.PostalCodeField = value;
                    this.RaisePropertyChanged("PostalCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street {
            get {
                return this.StreetField;
            }
            set {
                if ((object.ReferenceEquals(this.StreetField, value) != true)) {
                    this.StreetField = value;
                    this.RaisePropertyChanged("Street");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Suburb {
            get {
                return this.SuburbField;
            }
            set {
                if ((object.ReferenceEquals(this.SuburbField, value) != true)) {
                    this.SuburbField = value;
                    this.RaisePropertyChanged("Suburb");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CustomerService.ICustomerService")]
    public interface ICustomerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/createCustomer", ReplyAction="http://tempuri.org/ICustomerService/createCustomerResponse")]
        int createCustomer(string Username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/createCustomer", ReplyAction="http://tempuri.org/ICustomerService/createCustomerResponse")]
        System.Threading.Tasks.Task<int> createCustomerAsync(string Username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/getCustIDByUsername", ReplyAction="http://tempuri.org/ICustomerService/getCustIDByUsernameResponse")]
        int getCustIDByUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/getCustIDByUsername", ReplyAction="http://tempuri.org/ICustomerService/getCustIDByUsernameResponse")]
        System.Threading.Tasks.Task<int> getCustIDByUsernameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/getAddressesByCustID", ReplyAction="http://tempuri.org/ICustomerService/getAddressesByCustIDResponse")]
        UnitTest___WCF.CustomerService.AddressDTO[] getAddressesByCustID(int customerID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/getAddressesByCustID", ReplyAction="http://tempuri.org/ICustomerService/getAddressesByCustIDResponse")]
        System.Threading.Tasks.Task<UnitTest___WCF.CustomerService.AddressDTO[]> getAddressesByCustIDAsync(int customerID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/createAddress", ReplyAction="http://tempuri.org/ICustomerService/createAddressResponse")]
        int createAddress(UnitTest___WCF.CustomerService.AddressDTO newAddress, int customerID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/createAddress", ReplyAction="http://tempuri.org/ICustomerService/createAddressResponse")]
        System.Threading.Tasks.Task<int> createAddressAsync(UnitTest___WCF.CustomerService.AddressDTO newAddress, int customerID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : UnitTest___WCF.CustomerService.ICustomerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<UnitTest___WCF.CustomerService.ICustomerService>, UnitTest___WCF.CustomerService.ICustomerService {
        
        public CustomerServiceClient() {
        }
        
        public CustomerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int createCustomer(string Username) {
            return base.Channel.createCustomer(Username);
        }
        
        public System.Threading.Tasks.Task<int> createCustomerAsync(string Username) {
            return base.Channel.createCustomerAsync(Username);
        }
        
        public int getCustIDByUsername(string username) {
            return base.Channel.getCustIDByUsername(username);
        }
        
        public System.Threading.Tasks.Task<int> getCustIDByUsernameAsync(string username) {
            return base.Channel.getCustIDByUsernameAsync(username);
        }
        
        public UnitTest___WCF.CustomerService.AddressDTO[] getAddressesByCustID(int customerID) {
            return base.Channel.getAddressesByCustID(customerID);
        }
        
        public System.Threading.Tasks.Task<UnitTest___WCF.CustomerService.AddressDTO[]> getAddressesByCustIDAsync(int customerID) {
            return base.Channel.getAddressesByCustIDAsync(customerID);
        }
        
        public int createAddress(UnitTest___WCF.CustomerService.AddressDTO newAddress, int customerID) {
            return base.Channel.createAddress(newAddress, customerID);
        }
        
        public System.Threading.Tasks.Task<int> createAddressAsync(UnitTest___WCF.CustomerService.AddressDTO newAddress, int customerID) {
            return base.Channel.createAddressAsync(newAddress, customerID);
        }
    }
}