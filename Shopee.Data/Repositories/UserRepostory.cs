﻿using Microsoft.EntityFrameworkCore;
using Shopee.Data.DbContexts;
using Shopee.Data.IRepositories;
using Shopee.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Shopee.Data.Repositories;
public class UserRepostory : IUserRepostory
{
    private ShopeDbContext context = new ShopeDbContext();
    public UserRepostory()
    {

    }

    public async Task<User> CreateAsync(User user)
    {
        var userForInsert = await this.context.Users.AddAsync(user);
        await this.context.SaveChangesAsync();
        return userForInsert.Entity;
    }

    public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        var ProductForDelete = await this.context.Users.FirstOrDefaultAsync(expression);

        this.context.Users.Remove(ProductForDelete);
        await this.context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAllASync(Expression<Func<User, bool>> expression = null)
        => await this.context.Users.ToListAsync();

    public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        => await this.context.Users.FirstOrDefaultAsync(expression);

    public async Task<User> UpdateAsync(User user)
    {
        var userForUpdate = await this.context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        userForUpdate.FirstName = user.FirstName;
        userForUpdate.LastName = user.LastName;
        userForUpdate.Email = user.Email;
        userForUpdate.UserName = user.UserName;
        userForUpdate.Password = user.Password;
        userForUpdate.Phone = user.Phone;

        await this.context.SaveChangesAsync();

        return userForUpdate;
    }
}