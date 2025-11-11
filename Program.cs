//This is my attempt to use the C# SDK to find the vendor with the 6th most amount of accounts

//Get the EnergyCAP Software Developer Kit tools
using EnergyCap.Sdk; 
using EnergyCap.Sdk.Extensions;
using EnergyCap.Sdk.Models;
using System.Net.Http.Headers;
using System.Text.Json;


// Takes my API key and the environment variable to be able to connect to the Implementation environment API
var settings = new EnergyCapClientSettingsApiKeyAuth
{
    ApiKey = "ZWNhcHxJbXBsZW1lbnRhdGlvbg==.aWVfZXhhbV9wbXwxMDI0fDMxMDQ=.ZDE2NTJkNDMtZThkOS00NWZmLWIxMTYtN2MxZjJlOWU1OWRh", //API Key
    TimeoutSeconds = 300, //Following the documentation

    EnvironmentUrl = EnergyCapEnvironments.Implementation //Connect to the proper environment

};

// Connects to the EnergyCap EnergyCapApiClientFactory which is a factory class to help establish the client connection correctly
//Passes the settings variable in to connect to the API
var client = await EnergyCapApiClientFactory.CreateClientAsync(settings);

// Initializing a httpClient to be able to use raw Http calls 
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri(EnergyCapEnvironments.Implementation);
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", settings.ApiKey.Split('.').Last());



// Asks EnergyCAP for data which comes in the form of pages because the data size can be to large
//Looking at the client.GetVendorsAsync to get all vendors 
var allVendors = new List<dynamic>(); //using dynamic to get all fields
int page = 1;

while (true)
{
    var pageVendors = await client.GetVendorsAsync(
        filter: "",
        pageSize: 50,
        pageNumber: page);

    if (pageVendors == null || !pageVendors.Any())
        break;  // No more pages

    allVendors.AddRange(pageVendors);
    Console.WriteLine($"Fetched page {page} ({pageVendors.Count} vendors)");
    page++;
}

Console.WriteLine($"Total vendors fetched: {allVendors.Count}");
//---------------------------------------------------------------------------------------

// // //Prints the list of vendor fields if at least one vendor is returned from the API
// // I want this list to be able to have a reference for the table fields

// // if (vendors?.Any() == true)
// // {
// //     var firstVendor = vendors.First();               // pick the first one
// //     var propertyNames = firstVendor
// //         .GetType()
// //         .GetProperties()
// //         .Select(p => p.Name)
// //         .OrderBy(n => n);                            // alphabetical (optional)

// //     foreach (var name in propertyNames)
// //     {
// //         Console.WriteLine(name);
// //     }
// // }


// // Address
// // ConsentToRepresent
// // CreatedBy
// // CreatedDate
// // EdiCode
// // Email
// // ModifiedBy
// // ModifiedDate
// // PayDays
// // PhoneNumber
// // Rates
// // SplitAccountChargesConfigured
// // VendorCode
// // VendorDescription
// // VendorId
// // VendorInfo
// // Website


// Attempt to gather the unique accounts
// I could not find the correct endpoint to be able to see which accounts are tied to which vendor
//This is an attempt to find the number of unique accounts per vendor by connnecting the vendor, bill, and account tables
// === COUNT UNIQUE ACCOUNTS PER VENDOR ===
Console.WriteLine("Counting unique accounts per vendor...");
var vendorAccountCount = new List<(int VendorId, string Name, int AccountCount)>();

foreach (dynamic v in allVendors)
{
    int vendorId = v.VendorId;
    string name = v.VendorInfo?.ToString() ?? v.VendorCode?.ToString() ?? $"<ID {vendorId}>";

    // Step 1: Get ALL bills for this vendor
    var url = $"/api/v3/vendor/{vendorId}/bill?pageSize=1000"; // large page to get all
    var accountIds = new HashSet<int>();

    try
    {
        var resp = await httpClient.GetAsync(url);
        if (resp.IsSuccessStatusCode)
        {
            var json = await resp.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);

            if (data.TryGetProperty("items", out var items))
            {
                foreach (var item in items.EnumerateArray())
                {
                    if (item.TryGetProperty("accountId", out var aid))
                    {
                        accountIds.Add(aid.GetInt32());
                    }
                }
            }
        }
    }
    catch { }

    int count = accountIds.Count;
    vendorAccountCount.Add((vendorId, name, count));
    Console.WriteLine($"  {name} → {count} unique accounts");
}



// End of program
Console.WriteLine("=== DONE === Press any key...");
Console.ReadKey();