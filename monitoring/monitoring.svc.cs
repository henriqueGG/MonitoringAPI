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
        public Result NewsItems(string fullText, string since, int page, int itemsPerPage, string source)
        {
            int splitedItemsPerPage;

            if (itemsPerPage == 0)
            {
                itemsPerPage = 20;
            }

            if (source == "all" || source == null)
            {
                source = string.Empty;
                splitedItemsPerPage = itemsPerPage / 2;
            }
            else
            {
                splitedItemsPerPage = itemsPerPage;
            }

            DateTime sinceDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(since))
            {
                sinceDate = Convert.ToDateTime(since);
            }                      

            Result result = new Result();
            result.Articles = new List<Article>();

            List<Article> articles = new List<Article>();

            SolrNet.SolrQueryResults<ArchiveArticle> archiveArticles = new SolrQueryResults<ArchiveArticle>();            

            SolrNet.SolrQueryResults<SolrWISEdoc> wiseArticles = new SolrQueryResults<SolrWISEdoc>();

            switch (source.ToLower())
            {
                case "wise": 
                    wiseArticles = GetWiseSolr(fullText, sinceDate, page, itemsPerPage);
                    break;
                case "s3": 
                    archiveArticles = GetArchiveSearchSolr(fullText, sinceDate, page, itemsPerPage);
                    break;
                default:
                    archiveArticles = GetArchiveSearchSolr(fullText, sinceDate, page, splitedItemsPerPage);
                    wiseArticles = GetWiseSolr(fullText, sinceDate, page, splitedItemsPerPage);
                    break;
            }            

            foreach (ArchiveArticle aa in archiveArticles)
            {
                Article a = new Article();

                a.AuthorName = aa.author;
                a.BodyText = aa.fulltext;
                a.HeadLine = aa.title;
                a.Id = aa.Id;
                a.Language = aa.language;
                a.PublicationDate = aa.pub_date;
                a.SourceName = aa.outlet_name;
                a.Topics = aa.topics != null ? aa.topics.ToList() : null;
                a.UrlAddress = aa.url;
                a.CreateDate = aa.created_date;
                a.ServiceName = "S3";

                articles.Add(a);
            }

            foreach (SolrWISEdoc aa in wiseArticles)
            {
                Article a = new Article();

                a.AuthorName = aa.AuthorName;
                a.BodyText = aa.BodyText;
                a.HeadLine = aa.HeadLine;
                a.Id = aa.Id;
                a.Language = aa.Language;
                a.PublicationDate = aa.PublicationDate;
                a.SourceName = aa.SourceName;
                a.Topics = aa.Topics != null ? aa.Topics.ToList() : null;
                a.UrlAddress = aa.UrlAddress;
                a.CreateDate = aa.PublicationDate;
                a.ServiceName = "WISE";

                articles.Add(a);
            }

            result.Articles = articles.OrderByDescending(d => d.PublicationDate).Take(itemsPerPage).ToList();
            result.ResultCount = archiveArticles.NumFound + wiseArticles.NumFound;

            return result;
        }

        private SolrNet.SolrQueryResults<ArchiveArticle> GetArchiveSearchSolr(string fullText, DateTime sinceDate, int page, int itemsPerPage)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<ArchiveArticle>>();

            List<ISolrQuery> query = new List<ISolrQuery>();

            if(!string.IsNullOrEmpty(fullText))
                query.Add(new SolrQueryByField("fulltext", fullText));

            if (sinceDate > DateTime.MinValue)
                query.Add(new SolrQueryByRange<DateTime>("pub_date", sinceDate, DateTime.MaxValue));
            

            var articles = solr.Query(SolrQuery.All, new QueryOptions
            {
                FilterQueries = query,
                Start = page * itemsPerPage,
                Rows = itemsPerPage
            }
            );

            return articles;
        }

        private SolrNet.SolrQueryResults<SolrWISEdoc> GetWiseSolr(string fullText, DateTime sinceDate, int page, int itemsPerPage)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrWISEdoc>>();
            var articles = solr.Query(SolrQuery.All, new QueryOptions
            {
                FilterQueries = new ISolrQuery[] {
                            new SolrQueryByField("BodyText", fullText),
                            new SolrQueryByRange<DateTime>("PublicationDate", sinceDate, DateTime.Now ),
                    },
                Start = page * itemsPerPage,
                Rows = itemsPerPage,
                OrderBy = new[] { new SortOrder("PublicationDate", Order.DESC) }
            }
            );

            return articles;
        }
    }
}
