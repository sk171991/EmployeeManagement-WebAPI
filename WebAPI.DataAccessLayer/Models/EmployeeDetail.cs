//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.DataAccessLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public string UserLocation { get; set; }
        public string EmailId { get; set; }
        public System.DateTime CreatedTimeStamp { get; set; }
        public System.DateTime LastModifiedTimeStamp { get; set; }
        public Nullable<int> MetaActive { get; set; }
    }
}
