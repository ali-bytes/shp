//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.EStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartDetail
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public double Quntity { get; set; }
        public decimal Total { get; set; }
    
        public virtual Cart Cart { get; set; }
    }
}