namespace fx.Domain.Customer
{
    using fx.Domain.core;
    using System.Threading.Tasks;

    public interface ICustomerRepository : IRepository
    {
        Customer QueryCustomerByIdAndPwd(string userLoginId, string passwd);

        Customer FindById(string id);
    }
}
