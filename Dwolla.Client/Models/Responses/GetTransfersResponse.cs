using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dwolla.Client.Models.Responses
{
    public class GetTransfersResponse : BaseGetResponse<TransferResponse>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new TransfersEmbed Embedded { get; set; }
    }

    public class TransfersEmbed : Embed<TransferResponse>
    {
        public List<TransferResponse> Transfers { get; set; }

        public override List<TransferResponse> Results() => Transfers;
    }
}
