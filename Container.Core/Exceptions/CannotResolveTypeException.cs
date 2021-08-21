﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core.Exceptions
{
	public class CannotResolveTypeException : Exception
	{
		public CannotResolveTypeException(string message, Type type)
			: base(message)
		{
			Type = type;
		}

		public Type Type { get; }
	}
}