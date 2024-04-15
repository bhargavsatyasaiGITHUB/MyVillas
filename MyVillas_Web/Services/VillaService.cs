using MyVillas_Utility;
using MyVillas_Web.Models;
using MyVillas_Web.Models.Dto;
using MyVillas_Web.Services.IServices;

namespace MyVillas_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;
        public VillaService(IHttpClientFactory clientFactory,IConfiguration configuration):base(clientFactory)
        {
                _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                url = villaUrl + "/api/VillaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,

                url = villaUrl + "/api/VillaAPI/" + id + ","
            }); ;
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
               
                url = villaUrl + "/api/VillaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,

                url = villaUrl + "/api/VillaAPI/" + id+","
            }); ;
        }

        //public Task<T> sendAsync<T>(APIRequest aPIRequest)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                url = villaUrl + "/api/VillaAPI/" + dto.Id+","
            }); ;
        }
    }
}
