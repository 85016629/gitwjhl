namespace fx.Domain.Customer
{
    using fx.Domain.core;
    using System.Threading.Tasks;

    public interface ICustomerRepository : IRepository<Customer, string>
    {
        Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd);
    }
}
