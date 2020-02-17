/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Context;
using Repositories.IRepositories;
using Repositories.Persistence;
using Resources.SalesAnalysis;
using Utils;

namespace Repositories
{
    public class SalesAnalysisRepository : BaseRepository, ISalesAnalysisRepository
    {
        DatabaseReadUtils db;

        public SalesAnalysisRepository(CustomContext context) : base(context)
        {
            db = new DatabaseReadUtils(_context);
        }

        public async Task<IEnumerable<SalesTrendResource>> ReadSalesTrendAsync(string start_date, string end_date)
        {
            string sql = @"select a.payment_date,
                                  sum(a.order_money) as sum_money
                          from (select order_money,
                                       FORMAT(payment_date, 'yyyy-MM-dd') as payment_date
                                from order_fun(@start, @end)
                               ) as a 
                          group by a.payment_date";
            var result = await db.DatabaseRead(sql, start_date, end_date, null, null, null, null);
            List<SalesTrendResource> SalesTrend = new List<SalesTrendResource>();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesTrend.Add(new SalesTrendResource()
                    {
                        payment_date = DateTime.Parse(result[0].ToString()),
                        order_money = decimal.Parse(result[1].ToString())
                    });
                }
            }

            db.DatabaseClose();

            return SalesTrend;
        }

        public async Task<IEnumerable<SalesProductTypeStatisticsResource>> ReadSalesProductTypeStatisticsAsync(string start_date, string end_date, string top)
        {
            string sql = "";
            if (top != null)
            {
                sql = string.Format(@"select top {0} product_type,
                                      			     sum(product_count) as sum_count 
                                      from order_fun(@start, @end)
                                      group by product_type,
                                      order by sum_count desc", top);
            }
            else
            {
                sql = @"select product_type,
                        	   sum(product_count) as sum_count 
                        from order_fun(@start, @end)
                        group by product_type
                        order by sum_count desc";
            }

            var result = await db.DatabaseRead(sql, start_date, end_date, null, null, null, null);
            List<SalesProductTypeStatisticsResource> SalesProductTypeStatistics = new List<SalesProductTypeStatisticsResource>();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesProductTypeStatistics.Add(new SalesProductTypeStatisticsResource()
                    {

                        products_type = result[0].ToString(),
                        product_count = int.Parse(result[1].ToString()),
                    });
                }
            }

            db.DatabaseClose();

            return SalesProductTypeStatistics;
        }


        public async Task<IEnumerable<SalesProductsStatisticsResource>> ReadSalesProductStatisticsAsync(string start_date, string end_date, string top, string product_type)
        {
            string sql = string.Format(@"select top {0} product_type,
                                         			    product_name,
                                         			    sum(product_count) as sum_count 
                                         from order_fun(@start, @end)", top);
            if (!string.IsNullOrEmpty(product_type))
                sql += "where product_type = @type ";

            sql += @" group by product_type,product_name order by sum_count desc";

            var result = await db.DatabaseRead(sql, start_date, end_date, product_type, null, null, null);
            List<SalesProductsStatisticsResource> SalesProductStatistics = new List<SalesProductsStatisticsResource>();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesProductStatistics.Add(new SalesProductsStatisticsResource()
                    {
                        products_type = result[0].ToString(),
                        products_name = result[1].ToString(),
                        product_count = int.Parse(result[2].ToString()),
                    });
                }
            }

            db.DatabaseClose();

            return SalesProductStatistics;
        }

        public async Task<IEnumerable<SalesProductsStatisticsResource>> ReadSalesAllProductStatisticsAsync(string sql, string start_date, string end_date)
        {
            var result = await db.DatabaseRead(sql, start_date, end_date, null, null, null, null);
            List<SalesProductsStatisticsResource> SalesProductStatistics = new List<SalesProductsStatisticsResource>();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesProductStatistics.Add(new SalesProductsStatisticsResource()
                    {
                        products_type = result[0].ToString(),
                        products_name = result[1].ToString(),
                        product_count = int.Parse(result[2].ToString()),
                    });
                }
            }

            db.DatabaseClose();

            return SalesProductStatistics;
        }


        public async Task<IEnumerable<SalesOrdersResource>> ReadSalesOrderAsync(string sql, string start_date, string end_date)
        {
            DatabaseReadUtils db = new DatabaseReadUtils(_context);
            List<SalesOrdersResource> SalesOrders = new List<SalesOrdersResource>();
            var result = await db.DatabaseRead(sql, start_date, end_date, null, null, null, null);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesOrders.Add(new SalesOrdersResource()
                    {
                        order_id = result[0].ToString(),
                        order_mode = result[1].ToString(),
                        order_type = result[2].ToString(),
                        order_date = DateTime.Parse(result[3].ToString()),
                        order_money = decimal.Parse(result[4].ToString()),
                        discount = double.Parse(result[5].ToString()),
                        discount_price = decimal.Parse(result[6].ToString()),
                        order_details = null
                    });
                }
            }

            db.DatabaseClose();

            return SalesOrders;
        }

        public async Task<IEnumerable<ForeignSaleOrderDetailsResource>> ReadSalesOrderDetailsAsync(string sql, string start_date, string end_date, string oredr_id)
        {
            DatabaseReadUtils db = new DatabaseReadUtils(_context);
            List<ForeignSaleOrderDetailsResource> SalesOrderDetails = new List<ForeignSaleOrderDetailsResource>();
            var result = await db.DatabaseRead(sql, start_date, end_date, null, oredr_id, null, null);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesOrderDetails.Add(new ForeignSaleOrderDetailsResource()
                    {
                        product_id = result[0].ToString(),
                        product_name = result[1].ToString(),
                        product_type = result[2].ToString(),
                        product_count = int.Parse(result[3].ToString()),
                        order_detail_price = decimal.Parse(result[4].ToString()),
                    });
                }
            }

            db.DatabaseClose();

            return SalesOrderDetails;
        }

        public async Task<IEnumerable<SalesMembersResource>> ReadSalesMembersAsync(string sql)
        {
            List<SalesMembersResource> SalesMembers = new List<SalesMembersResource>();

            var result = await db.DatabaseRead(sql, null, null, null, null, null, null);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesMembers.Add(new SalesMembersResource
                    {
                        member_number = result[0].ToString(),
                        member_name = result[1].ToString(),
                        age = result[2].ToString(),
                        total_money = decimal.Parse(result[3].ToString()),
                        average_money = decimal.Parse(result[4].ToString()),
                        buy_count = int.Parse(result[5].ToString()),
                        buy_cycle = int.Parse(result[6].ToString()),
                        last_buy_date = DateTime.Parse(result[7].ToString())
                    });
                }
            }

            db.DatabaseClose();

            return SalesMembers;
        }

        public async Task<IEnumerable<SalesMemberTypesResource>> ReadSalesMemberTypesAsync(string sql)
        {
            List<SalesMemberTypesResource> SalesMemberTypes = new List<SalesMemberTypesResource>();

            var result = await db.DatabaseRead(sql, null, null, null, null, null, null);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesMemberTypes.Add(new SalesMemberTypesResource
                    {
                        member_number = result[0].ToString(),
                        order_type = result[1].ToString(),
                        order_type_count = int.Parse(result[2].ToString())
                    });
                }
            }

            db.DatabaseClose();

            return SalesMemberTypes;
        }

        public async Task<IEnumerable<SalesMemberProductCountResource>> ReadSalesMemberProductCountAsync(string sql, string member_number)
        {
            List<SalesMemberProductCountResource> SalesMemberProductCount = new List<SalesMemberProductCountResource>();

            var result = await db.DatabaseRead(sql, "1001-01-01", DateTime.Now.ToString("yyyy-MM-dd"), null, null, member_number, null);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesMemberProductCount.Add(new SalesMemberProductCountResource
                    {
                        member_number = result[0].ToString(),
                        member_name = result[1].ToString(),
                        product_name = result[2].ToString(),
                        buy_count = int.Parse(result[3].ToString())
                    });
                }
            }

            return SalesMemberProductCount;
        }

        public async Task<IEnumerable<SalesProductMemberRankingResource>> ReadSalesProductMemberRankingAsync(string sql, string product_name)
        {
            List<SalesProductMemberRankingResource> SalesProductMemberRanking = new List<SalesProductMemberRankingResource>();

            var result = await db.DatabaseRead(sql, "1001-01-01", DateTime.Now.ToString("yyyy-MM-dd"), null, null, null, product_name);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    SalesProductMemberRanking.Add(new SalesProductMemberRankingResource
                    {
                        product_name = result[0].ToString(),
                        member_number = result[1].ToString(),
                        member_name = result[2].ToString(),
                        buy_count = int.Parse(result[3].ToString())
                    });
                }
            }

            return SalesProductMemberRanking;
        }
    }
}*/