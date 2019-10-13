﻿/*MIT License

Copyright(c) 2017 Jeiel Aranal

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using UnityEngine;

public enum ReorderableNamingType
{
    None,
    ObjectName,
    VariableValue
}

namespace SubjectNerd.Utilities
{
	/// <summary>
	/// Display a List/Array as a sortable list in the inspector
	/// </summary>
	public class ReorderableAttribute : PropertyAttribute
	{
	    public ReorderableNamingType namingType;
        public string nestedPath;

        /// <summary>
        /// Display a List/Array as a sortable list in the inspector
        /// </summary>
        /// <param name="namingType">Name array element in inspector based on selected option</param>
        /// <param name="nestedPath">x.y.z path to target</param>
        public ReorderableAttribute(ReorderableNamingType namingType = ReorderableNamingType.None, string nestedPath = "")
		{
			this.namingType = namingType;
		    this.nestedPath = nestedPath;
        }
	}
}