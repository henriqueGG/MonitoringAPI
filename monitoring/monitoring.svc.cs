using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SolrNet;
using Microsoft.Practices.ServiceLocation;
using SolrNet.Commands.Parameters;

namespace monitoring
{    
    public class monitoring : Imonitoring
    {      
        public Result NewsItems(string fullText, string since, int page, int itemsPerPage)
        {          

            if (itemsPerPage == 0)
            {
                itemsPerPage = 25;
            }

            DateTime sinceDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(since))
            {
                sinceDate = Convert.ToDateTime(since);
            }

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Article>>();
            var articles = solr.Query(SolrQuery.All, new QueryOptions
                {
                    FilterQueries = new ISolrQuery[] {
                            new SolrQueryByField("fulltext", fullText),
                            new SolrQueryByRange<DateTime>("pub_date", sinceDate, DateTime.Now )
                    },
                    Start = page * itemsPerPage,
                    Rows = itemsPerPage
                }
            );

            Result result = new Result();

            result.Articles = articles;
            result.ResultCount = articles.NumFound;

            return result;
        }
    }
}
