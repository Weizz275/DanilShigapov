//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shigapov_glazki
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
            this.Shop = new HashSet<Shop>();
        }

        public static object Sales { get; internal set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public int AgentTypeID { get; set; }
        public string Address { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string DirectorName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public int Priority { get; set; }

        public string AgentTypeS { get; set; }

        public decimal Prod
        {
            get
            {
                decimal p = 0;
                foreach (ProductSale sales in ProductSale)
                {
                    p = p + sales.Stoimost;
                }
                return p;
            }
        }

        public int Discount
        {
            get
            {
                int s = 0;
                if (Prod > 10000 && Prod < 50000)
                    s = 5;
                if (Prod >= 50000 && Prod < 150000)
                    s = 10;
                if (Prod >= 150000 && Prod < 500000)
                    s = 20;
                if (Prod >= 500000)
                    s = 25;

                return s;
            }
        }

        public SolidColorBrush FonStyle        {
            get
            {
                if (Discount >= 25)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("LightGreen");
                }
                else
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("White");
                }
            }
        }

        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shop { get; set; }
    }
}