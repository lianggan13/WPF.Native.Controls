using System.Collections.Generic;
using System.Linq;

namespace WPF.Virtualization.DataVirtualization.Model
{
    /// <summary>
    /// Demo customer object.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Some dummy data to give the instance a bigger memory footprint.
        /// </summary>
        private byte[] data = new byte[100];


        internal List<Customer>? GenerateCustomers()
        {
            List<Customer> fakeSource = Enumerable.Range(0, 1000).Select(i =>
            {
                return new Customer()
                {
                    Id = i,
                    Name = $"Item{i}"
                };
            }).ToList();

            return fakeSource;

        }
    }
}
