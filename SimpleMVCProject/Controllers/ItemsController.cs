using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleMVCProject.Models;
using Utils.Web;
using SimpleMVCProject.ViewModels;
using SimpleMVCProject.Services;

namespace SimpleMVCProject.Controllers
{
    public class ItemsController : ApiController, ICrudService<ItemViewModel>
    {
        private readonly IItemsService _itemsService;



        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        public ItemsController(IItemsService itemsService)
        {
            // Injected service
            _itemsService = itemsService;
        }

        // Create 
        [ValidateModel]
        public HttpResponseMessage Post(ItemViewModel itemVm)
        {
            Item item = itemVm.MapToEntity();
            var addedItem = _itemsService.PostItem(item);
            return Request.CreateResponse(HttpStatusCode.Created, addedItem);
        }

        // Read all 
        public HttpResponseMessage Get()
        {
            var allItems = _itemsService.GetItems().Select(ItemViewModel.MapFromEntity);
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }

        // Read by id
        public HttpResponseMessage Get(int id)
        {
            var item = _itemsService.GetItem(id);
            if (item == null)
            {
                Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ItemViewModel.MapFromEntity(item));
        }

        // Update
        [ValidateModel]
        public HttpResponseMessage Put([FromBody]ItemViewModel updatedItemVm)
        {
            // Item must exists
            if (updatedItemVm.Id == 0 || !_itemsService.Any(updatedItemVm.Id))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ServerConstants.UpdateError);
            }

            Item item = updatedItemVm.MapToEntity();

            if (!_itemsService.PutItem(item))
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, ServerConstants.UpdateError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // Delete
        public HttpResponseMessage Delete(int id)
        {
            var item = _itemsService.GetItem(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ServerConstants.DeleteError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _itemsService.Get(id));
        }






    }
}