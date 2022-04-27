using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Inheritance
{
	public abstract class Animal
	{
		private string _name;

		public Animal(string name) {
			_name = name;
		}

		public abstract string Cry();

		public virtual string WhatsMyName()
		{
			return _name;
		}
	}
}
