using AppShared.Models;


namespace AppFront.Services
{
    public class GeoLocationService : BasicClientService<GeoLocation>
    {
        public GeoLocationService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class GeoLocationTypeService : BasicClientService<GeoLocationType>
    {
        public GeoLocationTypeService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class GeoMunicipalityService : BasicClientService<GeoMunicipality>
    {
        public GeoMunicipalityService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class GeoMunicTypeService : BasicClientService<GeoMunicType>
    {
        public GeoMunicTypeService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class GeoRegionService : BasicClientService<GeoRegion>
    {
        public GeoRegionService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class GeoRegionCenterService : BasicClientService<GeoRegionCenter>
    {
        public GeoRegionCenterService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }

}