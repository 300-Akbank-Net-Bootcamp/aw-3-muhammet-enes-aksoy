using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Cqrs;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;
public record GetCustomerByParameterQuery(string FirstName) : IRequest<ApiResponse<List<CustomerResponse>>>;

public record CreateCustomerCommand(CustomerRequest Model) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(int Id,CustomerRequest Model) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;