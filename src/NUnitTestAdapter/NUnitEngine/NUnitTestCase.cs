﻿// ***********************************************************************
// Copyright (c) 2020-2020 Charlie Poole, Terje Sandstrom
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Xml;
using NUnit.VisualStudio.TestAdapter.Dump;

namespace NUnit.VisualStudio.TestAdapter.NUnitEngine
{
    public abstract class NUnitTestNode
    {
        public XmlNode Node { get; protected set; }
        public string Id => Node.GetAttribute("id");
        public string FullName => Node.GetAttribute("fullname");
        public string Name => Node.GetAttribute("name");
        public bool IsNull => Node == null;

        protected NUnitTestNode(XmlNode node)
        {
            Node = node;
        }
    }



    public class NUnitTestCase : NUnitTestNode
    {
        public bool IsTestCase => !IsNull && Node.Name == "test-case";
        public bool IsParameterizedMethod => Type == "ParameterizedMethod";
        public string Type => Node.GetAttribute("type");
        public string ClassName => Node.GetAttribute("classname");
        public string MethodName => Node.GetAttribute("methodname");

        public NUnitTestCase(XmlNode testCase) : base(testCase)
        {
        }

        public NUnitTestCase Parent() => new NUnitTestCase(Node.ParentNode);
    }
}