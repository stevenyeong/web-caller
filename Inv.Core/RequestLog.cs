//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inv.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestLog()
        {
            this.RequestType = 0;
        }
    
        public long ID { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string ResponseData { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ProcessedOn { get; set; }
        public Nullable<int> RequestType { get; set; }
    }
}
