﻿namespace ReferenceClass.Models;

public class OrderItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
}