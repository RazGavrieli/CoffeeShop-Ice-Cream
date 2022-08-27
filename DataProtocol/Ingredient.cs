using System;

namespace DataProtocol {
	/**Ingridient is the base class for basic ingridient such as
	 * Ice cream ball, sirop and such..
	 */
	public class Ingredient
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public float Price { get; set; }

	}
}
