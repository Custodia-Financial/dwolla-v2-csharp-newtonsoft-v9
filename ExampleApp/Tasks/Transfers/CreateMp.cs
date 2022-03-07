using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LocalDwollaLink
{
    public Uri href { get; set; }
}

public class LocalDwollaMoney
{
    public string value { get; set; }

    public string currency { get; set; }
}

public class MassPaymentDestinationItem
{
    public Dictionary<string, LocalDwollaLink> _links { get; set; }
    public LocalDwollaMoney amount { get; set; }
    public string correlationId { get; set; }
}

public class MassPayment
{
    public Dictionary<string, LocalDwollaLink> _links { get; set; }
    public MassPaymentDestinationItem[] items { get; set; }
    public string correlationId { get; set; }
}

namespace ExampleApp.Tasks.Transfers
{
    [Task("cmp", "Create Masspayment")]
    internal class Masspayment : BaseTask
    {
        public override async Task Run()
        {
            var massPaymentTransaction = new MassPayment
            {
                _links = new Dictionary<string, LocalDwollaLink> { { "source", new LocalDwollaLink { href = new Uri("https://api-sandbox.dwolla.com/funding-sources/c3f40ff2-fefe-4f8d-b938-37dd1f4b7181") } } },
                correlationId = Guid.NewGuid().ToString(),
                items = new MassPaymentDestinationItem[2]
            };

            // var customerFundingSource = await exchange.GetAsync<FundingSource>(new Uri($"{exchange.ApiBaseAddress}/customers/120e4a9d-050d-4d94-aa8a-ea6ba0f68a90/funding-sources"), headers);

            var correlationId1 = Guid.NewGuid().ToString();
            var correlationId2 = Guid.NewGuid().ToString();

            massPaymentTransaction.items[0] = new MassPaymentDestinationItem
            {
                _links = new Dictionary<string, LocalDwollaLink> { { "destination", new LocalDwollaLink { href = new Uri("https://api-sandbox.dwolla.com/funding-sources/3fa399ae-5dd6-4b74-bca7-a4a04c3d9734") } } },
                amount = new LocalDwollaMoney { currency = "USD", value = 50.00.ToString("F2") },
                correlationId = correlationId1
            };

            massPaymentTransaction.items[1] = new MassPaymentDestinationItem
            {
                _links = new Dictionary<string, LocalDwollaLink> { { "destination", new LocalDwollaLink { href = new Uri("https://api-sandbox.dwolla.com/funding-sources/3fa399ae-5dd6-4b74-bca7-a4a04c3d9734") } } },
                amount = new LocalDwollaMoney { currency = "USD", value = 50.00.ToString("F2") },
                correlationId = correlationId2
            };

            Uri uri;

            uri = await Broker.CreateMasspaymentAsync(new Uri("https://api-sandbox.dwolla.com/mass-payments"), massPaymentTransaction);


            if (uri == null) return;
        }
    }
}