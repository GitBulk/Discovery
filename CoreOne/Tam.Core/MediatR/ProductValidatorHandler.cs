using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.MediatR
{
    public class ProductValidatorHandler : CommandValidator<AddSampleProductCommand>
    {
        private readonly DbContext context;
        public ProductValidatorHandler(DbContext context)
        {
            this.context = context;
            this.Validators = new Action<AddSampleProductCommand>[]
            {
                EnsurePriceIsValid
            };
        }

        public void EnsurePriceIsValid(AddSampleProductCommand message)
        {
            if (message.Product.Price <= 0)
            {
                message.ModelState.AddModelError("Name", $"The price of product {message.Product.Id} must be > 0");
            }
        }
    }
}
