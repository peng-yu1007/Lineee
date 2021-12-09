//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lineee.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExaOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExaOrders()
        {
            this.ExaOrderDetails = new HashSet<ExaOrderDetails>();
            this.Report = new HashSet<Report>();
        }
    
        public int exam_number { get; set; }
        public int exam_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        public System.DateTime order_date { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Exam Exam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaOrderDetails> ExaOrderDetails { get; set; }
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Report { get; set; }
    }
}
