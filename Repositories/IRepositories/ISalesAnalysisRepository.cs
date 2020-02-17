using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Resources.SalesAnalysis;

namespace Repositories.IRepositories
{

    public interface ISalesAnalysisRepository
    {
        Task<IEnumerable<SalesTrendResource>> ReadSalesTrendAsync(string start_date, string end_date);
        Task<IEnumerable<SalesProductTypeStatisticsResource>> ReadSalesProductTypeStatisticsAsync(string start_date, string end_date, string top);
        Task<IEnumerable<SalesProductsStatisticsResource>> ReadSalesProductStatisticsAsync(string start_date, string end_date, string top, string product_type);
        Task<IEnumerable<SalesProductsStatisticsResource>> ReadSalesAllProductStatisticsAsync(string sql, string start_date, string end_date);
        Task<IEnumerable<SalesOrdersResource>> ReadSalesOrderAsync(string sql, string start_date, string end_date);
        Task<IEnumerable<ForeignSaleOrderDetailsResource>> ReadSalesOrderDetailsAsync(string sql, string start_date, string end_date, string order_id);
        Task<IEnumerable<SalesMembersResource>> ReadSalesMembersAsync(string sql);
        Task<IEnumerable<SalesMemberTypesResource>> ReadSalesMemberTypesAsync(string sql);
        Task<IEnumerable<SalesMemberProductCountResource>> ReadSalesMemberProductCountAsync(string sql, string member_number);
        Task<IEnumerable<SalesProductMemberRankingResource>> ReadSalesProductMemberRankingAsync(string sql, string product_name);
    }
}