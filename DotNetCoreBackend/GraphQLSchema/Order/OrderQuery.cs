using GraphQL.Types;
using System.Collections.Generic;

using DotNetCoreBackend.Services;
using DotNetCoreBackend.DAL;

namespace DotNetCoreBackend.GraphQLSchema
{
    public class OrderQuery : ObjectGraphType
    {
        public OrderQuery(IOrderService orderService)
        {
            Name = "OrderQuery";
            Description = "订单模块，主要包含订单的查询";

            FieldAsync<ListGraphType<OrderType>>(
                "orders",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "limit" },
                    new QueryArgument<IntGraphType> { Name = "offset" }
                    ),
                resolve: async context =>
                {
                    var page = new Pageable(context);
                    return await orderService.GetOrderList(page.Offset, page.Limit);
                }
            );
        }
    }
}
