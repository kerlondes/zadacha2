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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Comentariy_tabel = new HashSet<Comentariy_tabel>();
            this.ZadachaZacasas = new HashSet<ZadachaZacasa>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public int StatusID { get; set; }
        public Nullable<int> SotrudnicId { get; set; }
        public int OrgId { get; set; }
        public int ClientId { get; set; }
        public string Model { get; set; }
        public Nullable<int> ProblemId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentariy_tabel> Comentariy_tabel { get; set; }
        public virtual Orgtehnica Orgtehnica { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZadachaZacasa> ZadachaZacasas { get; set; }
    }
}