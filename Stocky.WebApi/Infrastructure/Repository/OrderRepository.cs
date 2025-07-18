﻿using Microsoft.EntityFrameworkCore;
using Stocky.Shared.Models;
using Stocky.WebApi.Application.Interfaces;
using Stocky.WebApi.Infrastructure.Databases;

namespace Stocky.WebApi.Infrastructure.Repository;

public class OrderRepository(ApplicationContext context) : BaseRepository<Order>(context), IOrderRepository
{
    public async Task<Order> Add(Order order)
    {
        var entity = context.Orders.Add(order).Entity;
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<Order> Update(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> Delete(int id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order is null)
        {
            return false;
        }

        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Order?> GetById(int id)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(int id)
    {
        var orders = context.Orders.Where(x => x.UserId == id).AsEnumerable();
        if (orders is null)
        {
            return null;
        }

        return orders;
    }

    public async Task<int> Count()
    {
        return await context.Orders.CountAsync();
    }

    public async Task<IEnumerable<Order>> GetOrderByPagination(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return await context.Orders
            .Skip((page) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}