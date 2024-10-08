﻿using Infrastructure.Common.Models;
using Inventory.Product.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inventory;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Inventory.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryServices _inventoryServices;
        public InventoryController(IInventoryServices inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }

        [Route("items/{itemNo}", Name = "GetAllByItemNo")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNo([Required] string itemNo)
        {
            var result = await _inventoryServices.GetAllByItemNoAsync(itemNo);
            return Ok(result);
        }

        [Route("items/{itemNo}/paging", Name = "GetAllByItemNoPaging")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<InventoryEntryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedList<InventoryEntryDto>>> GetAllByItemNoPaging([Required] string itemNo, [FromQuery] InventoryPagingQuery query)
        {
            query.SetItemNo(itemNo);
            var result = await _inventoryServices.GetAllByItemNoPagingAsync(query);
            return Ok(result);
        }

        [Route("{id}", Name = "GetInventoryById")]
        [HttpGet]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> GetInventoryById([Required] string id)
        {
            var result = await _inventoryServices.GetByIdAsync(id);
            return Ok(result);
        }

        [Route("purchase/{itemNo}", Name = "PurchaseOrder")]
        [HttpPost]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder([Required] string itemNo, [FromBody] PurchaseProductDto model)
        {
            var result = await _inventoryServices.PurchaseItemAsync(itemNo, model);
            return Ok(result);
        }

        [Route("{id}", Name = "DeleteById")]
        [HttpDelete]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> DeleteById([Required] string id)
        {
            var entity = await _inventoryServices.GetByIdAsync(id);
            if (entity == null) return NotFound();
            await _inventoryServices.DeleteAsync(id);
            return NoContent();
        }

        [Route("sales/{itemNo}", Name = "SalesItem")]
        [HttpPost]
        [ProducesResponseType(typeof(InventoryEntryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<InventoryEntryDto>> SalesItem([Required] string itemNo, [FromBody] SalesProductDto model)
        {
            model.SetItemNo(itemNo);
            var result = await _inventoryServices.SalesItemAsync(itemNo, model);
            return Ok(result);
        }

        [Route("sales/order-no/{orderNo}", Name = "SalesOrder")]
        [HttpPost]
        [ProducesResponseType(typeof(CreatedSalesOrderSuccessDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatedSalesOrderSuccessDto>> SalesOrder([Required] string orderNo, [FromBody] SalesOrderDto model)
        {
            model.OrderNo = orderNo;
            var documentNo = await _inventoryServices.SalesOrderAsync(model);
            var result = new CreatedSalesOrderSuccessDto(documentNo);
            return Ok(result);
        }

    }
}
