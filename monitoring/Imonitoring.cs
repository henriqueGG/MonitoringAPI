using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SolrNet;

namespace monitoring
{
    [ServiceContract]
    public interface Imonitoring
    {        
        [OperationContract(Name = "NewsItems")]
        [WebGet(UriTemplate = "NewsItems?fulltext={fullText}&since={since}&page={page}&itemsPerPage={itemsPerPage}")]
        Result NewsItems(string fullText, string since, int page, int itemsPerPage);
    }   
}
