using SolutionName.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionName.Infrastructure.Data
{
    public static class DbInitializer
    {
        private static void SeedExamples(ExampleContext context)
        {
            if (!context.Examples.Any())
            {
                context.Add(new Example()
                {
                    CreatedDate = DateTime.Now,
                    Name = "First example"
                });

                context.Add(new Example()
                {
                    CreatedDate = DateTime.Now,
                    Name = "Second example"
                });

                context.SaveChanges();
            }
        }

        //Run this seed for development. You can add one for test/staging/prod aswell.
        public static void InitializeForDevelopment(ExampleContext context)
        {
            context.Database.EnsureCreated();

            SeedExamples(context);
        }
    }
}
