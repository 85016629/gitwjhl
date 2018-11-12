﻿using AutoMapper;
using fx.Domain.core;
using fx.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace fx.Infra.Data.SqlServer
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly CustomerDbContext dbContext = new CustomerDbContext();

        public Task Add(Customer entity)
        {
            dbContext.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            dbContext.Remove(entityDto);
            return dbContext.SaveChangesAsync();
        }

        public Customer FindById(string id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Customer>(entityDto);
        }


        public Task<Customer> FindByIdAsync(int id)
        {
            var entityDto = dbContext.Find<CustomerDto>(id);
            return Mapper.Map<Task<Customer>>(entityDto);
        }

        public Task<Customer> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public  Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd)
        {
            var entityDto = dbContext.Customers.Where(u => u.LoginId == userLoginId && u.Passwd == passwd).FirstOrDefault();
            if (entityDto == null)
                return null;
            return entityDto;
        }

        public int Update(Customer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public Task<int> UpdateAsync(Customer entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        Task<string> IRepository<Customer, string>.UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
