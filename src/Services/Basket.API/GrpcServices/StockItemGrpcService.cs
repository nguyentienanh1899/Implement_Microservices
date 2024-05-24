﻿using Inventory.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class StockItemGrpcService 
    {
        private readonly StockProtoService.StockProtoServiceClient _stockProtoService;
        public StockItemGrpcService(StockProtoService.StockProtoServiceClient stockProtoService)
        {
                _stockProtoService = stockProtoService ?? throw new ArgumentNullException(nameof(stockProtoService));
        }

        public async Task<StockModel> GetStock(string itemNo)
        {
            try
            {
                var stockItemRequest = new GetstockRequest { ItemNo = itemNo };
                return await _stockProtoService.GetstockAsync(stockItemRequest);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
