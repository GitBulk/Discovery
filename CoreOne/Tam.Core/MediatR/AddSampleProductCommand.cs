using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public class AddSampleProductCommand : ICommandResultAsyncRequest
    {
        public SampleProduct Product { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public AddSampleProductCommand(SampleProduct product, ModelStateDictionary modelState)
        {
            this.Product = product;
            this.ModelState = modelState;
        }
    }
}
