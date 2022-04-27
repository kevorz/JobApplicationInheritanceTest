using Case;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using Case.Inheritance;

namespace CaseTester
{
	[TestClass]
	public class AnimalTest
	{
		private CaseInheritance _case;

		public AnimalTest()
		{
			_case = new CaseInheritance();
			_case.Setup();
		}

		[TestMethod]
		public void Has2Dogs()
		{
			Assert.IsTrue(_case.animals.Where(x => x.GetType().Equals(typeof(Dog))).Count() == 2);
		}

		[TestMethod]
		public void DogBaseTypeAnimal()
		{
			var dog = _case.animals.FirstOrDefault(x => x.GetType().Equals(typeof(Dog)));

			Assert.IsTrue(dog is Animal);
		}

		[TestMethod]
		public void Has2Cats()
		{
			Assert.IsTrue(_case.animals.Where(x => x.GetType().Equals(typeof(Cat))).Count() == 2);
		}

		[TestMethod]
		public void CatBaseTypeAnimal()
		{
			var cat = _case.animals.FirstOrDefault(x => x.GetType().Equals(typeof(Cat)));

			Assert.IsTrue(cat is Animal);
		}

		[TestMethod]
		public void WhatsMyNameOverridden()
		{
			var result = false;

			foreach(var animal in _case.animals)
			{
				var field = animal.GetType().BaseType.GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic);
				var privateName = (string)field.GetValue(animal);
				var name = animal.WhatsMyName();

				result = !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(privateName) && name != privateName;
			}

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void AnimalHasProperyCries()
		{
			var result = true;

			foreach (var animal in _case.animals)
			{
				var cry = animal.Cry();

				if (animal is Dog)
				{
					if (!cry.ToLower().Equals("woof") && !cry.ToLower().Equals("bark"))
					{
						result = false;
					}
				}
				else if (animal is Cat)
				{
					if (!cry.ToLower().Equals("meow") && !cry.ToLower().Equals("hiss"))
					{
						result = false;
					}
				}
			}

			Assert.IsTrue(result);
		}
	}
}
