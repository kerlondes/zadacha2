//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zadacha2
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZadachaZacasa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZadachaZacasa()
        {
            this.Zadacha_zapchast = new HashSet<Zadacha_zapchast>();
        }
    
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<int> SotrudnicId { get; set; }
        public Nullable<int> ZacazId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zadacha_zapchast> Zadacha_zapchast { get; set; }
    }
}
