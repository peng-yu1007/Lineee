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
    
    public partial class Report
    {
        public int report_id { get; set; }
        public int exam_number { get; set; }
        public string exam_name { get; set; }
        public string exam_value { get; set; }
        public System.DateTime report_date { get; set; }
    
        public virtual ExaOrders ExaOrders { get; set; }

        public int exam_id { get; set; }
    }
}
